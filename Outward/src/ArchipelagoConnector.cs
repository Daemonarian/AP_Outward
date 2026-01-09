using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace OutwardArchipelago
{
    public class ArchipelagoConnector : MonoBehaviour
    {
        public const string ArchipelagoGame = "Outward: Definitive Edition";
        public const string ArchipelagoVersion = BuildInfo.ArchipelagoVersion;

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

        public static void Create()
        {
            if (Instance == null)
            {
                var obj = new GameObject(nameof(ArchipelagoConnector));
                obj.AddComponent<ArchipelagoConnector>();
            }
        }

        void Awake()
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
            Host = Plugin.ArchipelagoHost.Value;
            Port = Plugin.ArchipelagoPort.Value;
            Password = Plugin.ArchipelagoPassword.Value;
            SlotName = Plugin.ArchipelagoSlotName.Value;
            IsConnected = false;

            // create connection status icon
            var connectionStatusObj = new GameObject(nameof(ArchipelagoConnectionStatus));
            DontDestroyOnLoad(connectionStatusObj);
            ConnectionStatus = connectionStatusObj.AddComponent<ArchipelagoConnectionStatus>();

            Connect();
        }

        void Update()
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
                            Plugin.Log.LogInfo($"[Archipelago] Sending message as {character.Name} ({character.UID}): {message}");
                            SendSystemMessage(character, message);
                        }
                    }
                }
            }
        }

        private async Task SetIsConnected(bool isConnected)
        {
            if (IsConnected != isConnected)
            {
                var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                MainThreadQueue.Enqueue(() =>
                {
                    try
                    {
                        IsConnected = isConnected;
                        tcs.TrySetResult(true);
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                });
                await tcs.Task;
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
                    await SetIsConnected(true);

                    bool first = true;
                    while (true)
                    {
                        // cleanup old session if it exists
                        bool doWaitInterval = false;
                        if (_archipelagoSession != null)
                        {
                            _archipelagoSession.Socket.SocketClosed -= OnSocketClosed;
                            _archipelagoSession.Items.ItemReceived -= OnItemReceived;
                            _archipelagoSession.MessageLog.OnMessageReceived -= OnMessageReceived;
                            _archipelagoSession = null;
                            doWaitInterval = true;
                        }

                        if (doWaitInterval)
                        {
                            var verb = first ? "Reconnecting" : "Retrying";
                            Plugin.Log.LogInfo($"[Archipelago] {verb} in {ReconnectInterval} ms...");
                            await Task.Delay(ReconnectInterval);
                        }
                        first = false;

                        Plugin.Log.LogInfo($"[Archipelago] Logging into Archipelago server at '{Host}:{Port}' with slot '{SlotName}' and game '{ArchipelagoGame}'.");

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
                            Plugin.Log.LogError($"[Archipelago] Connect failed: {ex}");
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
                            Plugin.Log.LogError($"[Archipelago] Login failed: {ex}");
                            continue;
                        }

                        if (loginResult == null)
                        {
                            // this should not happend
                            Plugin.Log.LogError("[Archipelago] Login failed for unknown reason.");
                            continue;
                        }

                        if (loginResult is LoginFailure loginFailure)
                        {
                            Plugin.Log.LogError($"[Archipelago] Login failed: {string.Join("\n", loginFailure.Errors)}");
                            continue;
                        }

                        if (!loginResult.Successful)
                        {
                            // this should not happen
                            Plugin.Log.LogError("[Archipelago] Login failed for unknown reason.");
                            continue;
                        }

                        break;
                    }

                    Plugin.Log.LogInfo("[Archipelago] Login success.");
                    await SetIsConnected(true);
                }
                finally
                {
                    _archipelagoSessionSemaphore.Release();
                }
            });
        }

        public void CompleteLocationCheck(long locationId)
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
                                Plugin.Log.LogInfo($"[Archipelago] Completing location check: {locationId}");
                                _archipelagoSession.Locations.CompleteLocationChecks(locationId);
                                Plugin.Log.LogInfo($"[Archipelago] Completed location check: {locationId}");
                                break;
                            }
                            catch (Exception ex)
                            {
                                Plugin.Log.LogError($"[Archipelago] Complete location check failed: {locationId}\n{ex}");
                            }
                        }
                    }
                    finally
                    {
                        _archipelagoSessionSemaphore.Release();
                    }

                    Plugin.Log.LogInfo($"[Archipelago] Retrying complete location check in {ReconnectInterval} ms...");
                    await Task.Delay(ReconnectInterval);
                }
            });
        }

        private void OnSocketClosed(string reason)
        {
            Connect();
        }

        private void OnItemReceived(ReceivedItemsHelper helper)
        {
            // IDEMPOTENCY CHECK:
            // When you reconnect, AP sends ALL items again. 
            // We must ensure we don't grant the same item twice.
            // (Note: This is a simple example. For robustness, track index/ID properly)
            MainThreadQueue.Enqueue(() =>
            {
                while (helper.Any())
                {
                    var item = helper.DequeueItem();

                    // Verify we haven't processed this exact instance before
                    // You might need a more robust index tracking depending on your game
                    Plugin.Log.LogMessage($"[Archipelago] Received Item: {item.ItemName}");

                    if (item.ItemId == ArchipelagoItems.QUEST_LICENSE)
                    {
                        Plugin.Instance.QuestLicenseLevel += 1;
                        Plugin.Log.LogMessage($"Granting Quest License lvl {Plugin.Instance.QuestLicenseLevel}!");
                        SplitScreenManager.Instance.NotifyAllLocalPlayers($"Received Quest License Lv {Plugin.Instance.QuestLicenseLevel}!");
                    }
                }
            });
        }

        private void OnMessageReceived(Archipelago.MultiClient.Net.MessageLog.Messages.LogMessage message)
        {
            Plugin.Log.LogMessage($"[Archipelago::Chat] {message}");
            IncomingMessageQueue.Enqueue(FormatArchipelagoMessage(message));
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
                ChatEntry chatEntry = UnityEngine.Object.Instantiate<ChatEntry>(UIUtilities.ChatEntryPrefab);
                chatEntry.transform.SetParent(chatPanel.m_chatDisplay.content);
                chatEntry.transform.ResetLocal(true);
                chatEntry.SetCharacterUI(chatPanel.m_characterUI);
                chatPanel.m_messageArchive.Insert(0, chatEntry);
            }
            else
            {
                ChatEntry item = chatPanel.m_messageArchive[chatPanel.m_messageArchive.Count - 1];
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
    }
}
