using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using UnityEngine;

namespace OutwardArchipelago.Archipelago
{
    internal class ArchipelagoConnector : MonoBehaviour
    {
        public const string ArchipelagoGame = "Outward: Definitive Edition";
        public const string ArchipelagoVersion = APWorldInfo.ArchipelagoVersion;

        public static ArchipelagoConnector Instance { get; private set; }

        // State
        private readonly SemaphoreSlim _archipelagoSessionSemaphore = new(1, 1);
        private ArchipelagoSession _archipelagoSession;

        // Connection details
        public string Host { get; private set; }
        public int Port { get; private set; }
        public string Password { get; private set; }
        public string SlotName { get; private set; }
        public int ReconnectInterval { get; private set; } = 10000;

        public bool IsConnected { get; private set; }

        public ArchipelagoConnectionStatus ConnectionStatus { get; private set; }

        // Thread Safety: Queue actions here to run them on the main Unity thread
        private readonly ConcurrentQueue<Action> MainThreadQueue = new();
        private readonly ConcurrentQueue<string> IncomingMessageQueue = new();
        private readonly ConcurrentQueue<long> IncomingItemQueue = new();

        private readonly Dictionary<long, int> ItemCounts = new();

        public static void Create()
        {
            if (Instance == null)
            {
                var obj = new GameObject(nameof(ArchipelagoConnector));
                obj.AddComponent<ArchipelagoConnector>();
            }
        }

        private void Awake()
        {
            // singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            // load Archipelago connection details
            Host = OutwardArchipelagoMod.ArchipelagoHost.Value;
            Port = OutwardArchipelagoMod.ArchipelagoPort.Value;
            Password = OutwardArchipelagoMod.ArchipelagoPassword.Value;
            SlotName = OutwardArchipelagoMod.ArchipelagoSlotName.Value;
            IsConnected = false;

            // create connection status icon
            var connectionStatusObj = new GameObject(nameof(ArchipelagoConnectionStatus));
            DontDestroyOnLoad(connectionStatusObj);
            ConnectionStatus = connectionStatusObj.AddComponent<ArchipelagoConnectionStatus>();

            Connect();
        }

        private void Update()
        {
            while (MainThreadQueue.TryDequeue(out var action))
            {
                action();
            }

            if (PhotonNetwork.isMasterClient && Global.Lobby.PlayersInLobbyCount > 0 && NetworkLevelLoader.Instance.IsOverallLoadingDone)
            {
                if (!IncomingMessageQueue.IsEmpty)
                {
                    var character = CharacterManager.Instance.GetFirstLocalCharacter();
                    if (character != null && character.CharacterUI != null && character.CharacterUI.ChatPanel != null)
                    {
                        while (IncomingMessageQueue.TryDequeue(out var message))
                        {
                            OutwardArchipelagoMod.Log.LogInfo($"[Archipelago] Sending message as {character.Name} ({character.UID}): {message}");
                            SendSystemMessage(character, message);
                        }
                    }
                }

                if (!IncomingItemQueue.IsEmpty)
                {
                    while (IncomingItemQueue.TryDequeue(out var itemId))
                    {
                        if (!ItemCounts.ContainsKey(itemId))
                        {
                            ItemCounts[itemId] = 0;
                        }

                        ItemCounts[itemId] += 1;

                        var count = ItemCounts[itemId];

                        GiveItemToPlayer(itemId, count);
                    }
                }
            }
        }

        private void Connect()
        {
            // run in a background thread so we do not block the main thread
            Task.Run(async () =>
            {
                // ensure write access to _archipelagoSession
                await _archipelagoSessionSemaphore.WaitAsync();
                try
                {
                    // short-circuit if a connected session already exists
                    if (_archipelagoSession != null && _archipelagoSession.Socket.Connected)
                    {
                        return;
                    }

                    // set the value of IsConnected on the main thread, if necessary
                    if (IsConnected)
                    {
                        await RunOnMainThread(() => IsConnected = false);
                    }

                    var first = true;
                    while (true)
                    {
                        // cleanup old session if it exists
                        var doWaitInterval = false;
                        if (_archipelagoSession != null)
                        {
                            _archipelagoSession.Socket.SocketClosed -= OnSocketClosed;
                            _archipelagoSession.Items.ItemReceived -= OnItemReceived;
                            _archipelagoSession.MessageLog.OnMessageReceived -= OnMessageReceived;
                            _archipelagoSession = null;
                            doWaitInterval = true;
                        }

                        // clear the incoming item queue
                        while (IncomingItemQueue.TryDequeue(out _)) { }
                        await RunOnMainThread(() => ItemCounts.Clear());

                        if (doWaitInterval)
                        {
                            var verb = first ? "Reconnecting" : "Retrying";
                            OutwardArchipelagoMod.Log.LogInfo($"[Archipelago] {verb} in {ReconnectInterval} ms...");
                            await Task.Delay(ReconnectInterval);
                        }
                        first = false;

                        OutwardArchipelagoMod.Log.LogInfo($"[Archipelago] Logging into Archipelago server at '{Host}:{Port}' with slot '{SlotName}' and game '{ArchipelagoGame}'.");

                        // create new session
                        _archipelagoSession = ArchipelagoSessionFactory.CreateSession(Host, Port);

                        // hook up events before connecting
                        _archipelagoSession.Socket.SocketClosed += OnSocketClosed;
                        _archipelagoSession.Items.ItemReceived += OnItemReceived;
                        _archipelagoSession.MessageLog.OnMessageReceived += OnMessageReceived;

                        // connect
                        try
                        {
                            await _archipelagoSession.ConnectAsync();
                        }
                        catch (Exception ex)
                        {
                            OutwardArchipelagoMod.Log.LogError($"[Archipelago] Connect failed: {ex}");
                            continue;
                        }

                        // login
                        LoginResult loginResult;
                        try
                        {
                            loginResult = await _archipelagoSession.LoginAsync(
                                ArchipelagoGame,
                                SlotName,
                                ItemsHandlingFlags.AllItems,
                                version: new Version(ArchipelagoVersion),
                                password: Password,
                                requestSlotData: true
                            );
                        }
                        catch (Exception ex)
                        {
                            OutwardArchipelagoMod.Log.LogError($"[Archipelago] Login failed: {ex}");
                            continue;
                        }

                        if (loginResult == null)
                        {
                            // this should not happend
                            OutwardArchipelagoMod.Log.LogError("[Archipelago] Login failed for unknown reason.");
                            continue;
                        }

                        if (loginResult is LoginFailure loginFailure)
                        {
                            OutwardArchipelagoMod.Log.LogError($"[Archipelago] Login failed: {string.Join("\n", loginFailure.Errors)}");
                            continue;
                        }

                        if (!loginResult.Successful)
                        {
                            // this should not happen
                            OutwardArchipelagoMod.Log.LogError("[Archipelago] Login failed for unknown reason.");
                            continue;
                        }

                        break;
                    }

                    OutwardArchipelagoMod.Log.LogInfo("[Archipelago] Login success.");
                    await RunOnMainThread(() => IsConnected = true);
                }
                finally
                {
                    _archipelagoSessionSemaphore.Release();
                }
            });
        }

        public void CompleteLocationCheck(long locationId)
        {
            if (PhotonNetwork.isMasterClient)
            {
                // run in background so you do not hold up the main thread
                Task.Run(async () =>
                {
                    while (true)
                    {
                        await _archipelagoSessionSemaphore.WaitAsync();
                        try
                        {
                            if (_archipelagoSession != null)
                            {
                                try
                                {
                                    OutwardArchipelagoMod.Log.LogInfo($"[Archipelago] Completing location check: {locationId}");
                                    _archipelagoSession.Locations.CompleteLocationChecks(locationId);
                                    OutwardArchipelagoMod.Log.LogInfo($"[Archipelago] Completed location check: {locationId}");
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    OutwardArchipelagoMod.Log.LogError($"[Archipelago] Complete location check failed: {locationId}\n{ex}");
                                }
                            }
                        }
                        finally
                        {
                            _archipelagoSessionSemaphore.Release();
                        }

                        OutwardArchipelagoMod.Log.LogInfo($"[Archipelago] Retrying complete location check in {ReconnectInterval} ms...");
                        await Task.Delay(ReconnectInterval);
                    }
                });
            }
        }

        private void OnSocketClosed(string reason)
        {
            OutwardArchipelagoMod.Log.LogWarning($"[Archipelago] Lost connection: {reason}");
            Connect();
        }

        private void OnItemReceived(ReceivedItemsHelper helper)
        {
            OutwardArchipelagoMod.Log.LogInfo($"[Archipelago] Received {helper.Index} items...");
            while (helper.Any())
            {
                var item = helper.DequeueItem();
                OutwardArchipelagoMod.Log.LogInfo($"[Archipelago] Received item {item.ItemName} ({item.ItemId}).");
                IncomingItemQueue.Enqueue(item.ItemId);
            }
        }

        private void OnMessageReceived(global::Archipelago.MultiClient.Net.MessageLog.Messages.LogMessage message)
        {
            OutwardArchipelagoMod.Log.LogMessage($"[Archipelago::Chat] {message}");
            IncomingMessageQueue.Enqueue(FormatArchipelagoMessage(message));
        }

        private void GiveItemToPlayer(long itemId, int count)
        {
            OutwardArchipelagoMod.Log.LogInfo($"[Archipelago] Giving item to player: {itemId} x{count}");
            var character = CharacterManager.Instance.GetFirstLocalCharacter();
            if (character == null)
            {
                OutwardArchipelagoMod.Log.LogError($"[Archipelago] Could not find local player to give item.");
                return;
            }

            try
            {
                ArchipelagoItemManager.Instance.GiveItemToPlayer(itemId, character);
            }
            catch (Exception ex)
            {
                OutwardArchipelagoMod.Log.LogError($"[Archipelago] Failed to give item {itemId} to player: {ex}");
            }
        }

        private async Task<T> RunOnMainThread<T>(Func<T> action)
        {
            var tcs = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
            MainThreadQueue.Enqueue(() =>
            {
                try
                {
                    var result = action();
                    tcs.TrySetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            });
            return await tcs.Task;
        }

        private async Task RunOnMainThread(Action action)
        {
            await RunOnMainThread(() =>
            {
                action();
                return true;
            });
        }

        private static string FormatArchipelagoMessage(LogMessage message)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var part in message.Parts)
            {
                sb.Append($"<color=#{part.Color.R:X2}{part.Color.G:X2}{part.Color.B:X2}>{part.Text}</color>");
            }

            return sb.ToString();
        }

        private static void SendSystemMessage(Character character, string message)
        {
            // This code mostly adapts the implementation of ChatPanel.ChatMessageReceived.
            var chatPanel = character.CharacterUI.ChatPanel;
            if (chatPanel.m_messageArchive.Count < chatPanel.MaxMessageCount)
            {
                var chatEntry = UnityEngine.Object.Instantiate<ChatEntry>(UIUtilities.ChatEntryPrefab);
                chatEntry.transform.SetParent(chatPanel.m_chatDisplay.content);
                chatEntry.transform.ResetLocal(true);
                chatEntry.SetCharacterUI(chatPanel.m_characterUI);
                chatPanel.m_messageArchive.Insert(0, chatEntry);
            }
            else
            {
                var item = chatPanel.m_messageArchive[chatPanel.m_messageArchive.Count - 1];
                chatPanel.m_messageArchive.RemoveAt(chatPanel.m_messageArchive.Count - 1);
                chatPanel.m_messageArchive.Insert(0, item);
            }
            chatPanel.m_messageArchive[0].transform.SetAsLastSibling();
            chatPanel.m_messageArchive[0].SetEntry(">", message, true);
            chatPanel.m_lastHideTime = Time.time;
            if (!chatPanel.IsDisplayed)
            {
                chatPanel.Show();
            }
            chatPanel.Invoke("DelayedScroll", 0.1f);
        }

        public bool IsLocationCheckCompleted(long locationId)
        {
            try
            {
                return _archipelagoSession.Locations.AllLocationsChecked.Contains(locationId);
            }
            catch (Exception ex)
            {
                OutwardArchipelagoMod.Log.LogError($"[Archipelago] Failed to test if location check {locationId} is completed: {ex}");
            }

            return false;
        }
    }
}
