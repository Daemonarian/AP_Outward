using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Exceptions;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using OutwardArchipelago.QuestEvents;
using UnityEngine;

namespace OutwardArchipelago.Archipelago
{
    /// <summary>
    /// Encapsulates all the logic of communicating with the Archipelago server.
    /// </summary>
    internal class ArchipelagoConnector : MonoBehaviour
    {
        /// <summary>
        /// The session with the Archipelago server.
        /// </summary>
        private ArchipelagoSession _session = null;

        /// <summary>
        /// The sub-manager for all things related to Archipelago items.
        /// </summary>
        private readonly ItemManager _items;

        /// <summary>
        /// The sub-manager for all things related to Archipelago locations.
        /// </summary>
        private readonly LocationManager _locations;

        /// <summary>
        /// The sub-manager for all things related to Archipelago messages.
        /// </summary>
        private readonly MessageManager _messages;

        /// <summary>
        /// The background Archipelago session worker.
        /// </summary>
        private Task _sessionWorker = null;

        /// <summary>
        /// Delegates to be run on the main thread.
        /// </summary>
        private readonly ConcurrentQueue<Action> _mainThreadQueue = new();

        public ArchipelagoConnector()
        {
            _items = new ItemManager(this);
            _locations = new LocationManager(this);
            _messages = new MessageManager(this);
        }

        /// <summary>
        /// Singleton pattern.
        /// </summary>
        public static ArchipelagoConnector Instance { get; private set; } = null;

        /// <summary>
        /// The sub-manager for all things related to Archipelago items.
        /// </summary>
        public IItemManager Items => _items;

        /// <summary>
        /// The sub-manager for all things related to Archipelago locations.
        /// </summary>
        public ILocationManager Locations => _locations;

        /// <summary>
        /// The sub-manager for all things related to Archipelago messages.
        /// </summary>
        public IMessageManager Messages => _messages;

        // Connection details
        public string Host { get; private set; } = null;

        public int Port { get; private set; } = 0;

        public string Password { get; private set; } = null;

        public string SlotName { get; private set; } = null;

        public int ReconnectInterval { get; private set; } = 10000;

        /// <summary>
        /// Whether or not we have an active session with the Archipelago server.
        /// </summary>
        public bool IsConnected { get; private set; } = false;

        /// <summary>
        /// An Archipelago connection status indicator game component.
        /// </summary>
        public ArchipelagoConnectionStatus ConnectionStatus { get; private set; } = null;

        /// <summary>
        /// Create the Singleton instance if it does not already exist.
        /// </summary>
        public static void Create()
        {
            if (Instance == null)
            {
                var obj = new GameObject(nameof(ArchipelagoConnector));
                obj.AddComponent<ArchipelagoConnector>();
            }
        }

        protected void Awake()
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
        }

        protected void Update()
        {
            if (_sessionWorker is null || _sessionWorker.IsCompleted)
            {
                _sessionWorker = SessionWorker();
            }

            while (_mainThreadQueue.TryDequeue(out var action))
            {
                action();
            }

            _items.Update();
            _messages.Update();
        }

        /// <summary>
        /// The main background event loop for the Archipelago session.
        /// </summary>
        private async Task SessionWorker()
        {
            while (true)
            {
                try
                {
                    await UpdateAsync();
                }
                catch (ArchipelagoSocketClosedException ex)
                {
                    OutwardArchipelagoMod.Log.LogWarning($"lost connection to Archipelago server:\n{ex}");
                }
                catch (Exception ex)
                {
                    OutwardArchipelagoMod.Log.LogError($"error on Archipelago session background thread:\n{ex}");
                }

                // avoid creating a busy loop
                await Task.Delay(100);
            }
        }

        /// <summary>
        /// Do work on the Archipelago session background thread.
        /// </summary>
        private async Task UpdateAsync()
        {
            if (_session == null || !_session.Socket.Connected)
            {
                if (IsConnected)
                {
                    await RunOnMainThread(() => IsConnected = false);
                }

                var success = await Connect();
                if (!success)
                {
                    OutwardArchipelagoMod.Log.LogInfo($"waiting {ReconnectInterval} ms before retrying connection to Archipelago server...");
                    await Task.Delay(ReconnectInterval);
                    return;
                }

                await RunOnMainThread(() => IsConnected = true);
            }

            await _locations.UpdateAsync();
            await _messages.UpdateAsync();
        }

        /// <summary>
        /// Connect (or re-connect) to the Archipelago server.
        /// 
        /// This creates a brand new Archipelago session.
        /// </summary>
        private async Task<bool> Connect()
        {
            await Disconnect();

            await _items.ResetSession();

            OutwardArchipelagoMod.Log.LogInfo($"logging into Archipelago server at \"{Host}:{Port}\" with slot \"{SlotName}\" and game \"{APWorld.Game}\"...");
            _session = ArchipelagoSessionFactory.CreateSession(Host, Port);

            // register all event handlers before connecting
            _items.RegisterEventHandlers(_session);
            _messages.RegisterEventHandlers(_session);

            try
            {
                await _session.ConnectAsync();
            }
            catch (Exception ex)
            {
                OutwardArchipelagoMod.Log.LogError($"failed to connect to Archipelago server:\n{ex}");
                return false;
            }

            LoginResult loginResult;
            try
            {
                loginResult = await _session.LoginAsync(
                    APWorld.Game,
                    SlotName,
                    ItemsHandlingFlags.AllItems,
                    version: new Version(APWorld.ArchipelagoVersion),
                    password: Password,
                    requestSlotData: true
                );
            }
            catch (Exception ex)
            {
                OutwardArchipelagoMod.Log.LogError($"failed to login to Archipelago server:\n{ex}");
                return false;
            }

            if (loginResult == null)
            {
                // this should not happen
                OutwardArchipelagoMod.Log.LogError("failed to login to Archipelago server for unknown reason");
                return false;
            }

            if (loginResult is LoginFailure loginFailure)
            {
                OutwardArchipelagoMod.Log.LogError($"failed to login to Archipelago server:\n{string.Join("\n", loginFailure.Errors)}");
                return false;
            }

            if (!loginResult.Successful)
            {
                // this should not happen
                OutwardArchipelagoMod.Log.LogError("failed to login to Archipelago server for unknown reason");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Disconnect from the Archipelago server if connected.
        /// </summary>
        private async Task Disconnect()
        {
            if (IsConnected)
            {
                await RunOnMainThread(() => IsConnected = false);
            }

            if (_session != null)
            {
                _items.UnregisterEventHandlers(_session);
                _messages.UnregisterEventHandlers(_session);

                _session = null;
            }
        }

        /// <summary>
        /// Queues a delegate to run on the main-thread and wait for it to complete.
        /// 
        /// Do not run this from the Unity main thread.
        /// </summary>
        /// <typeparam name="T">The return type of the delegate.</typeparam>
        /// <param name="action">The delegate.</param>
        /// <returns>The return of the delegate.</returns>
        private async Task<T> RunOnMainThread<T>(Func<T> action)
        {
            var tcs = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
            _mainThreadQueue.Enqueue(() =>
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

        /// <summary>
        /// Queues a delegate to run on the main-thread and wait for it to complete.
        /// 
        /// Do not run this from the Unity main thread.
        /// </summary>
        /// <param name="action">The delegate.</param>
        private async Task RunOnMainThread(Action action)
        {
            await RunOnMainThread(() =>
            {
                action();
                return true;
            });
        }

        /// <summary>
        /// A sub-manager for Archipelago items.
        /// </summary>
        public interface IItemManager
        {
            /// <summary>
            /// The main manager associated with this instance.
            /// </summary>
            public ArchipelagoConnector Parent { get; }

            /// <summary>
            /// Get the number of an Archipelago item that already exist in the world.
            /// 
            /// This method should be called from the Unity main thread.
            /// </summary>
            /// <param name="item">The Outward APWorld item.</param>
            /// <returns>The count of the specified Archipelago item.</returns>
            public int GetCount(APWorld.Item item);
        }

        /// <summary>
        /// A sub-manager for Archipelago locations.
        /// </summary>
        public interface ILocationManager
        {
            /// <summary>
            /// The main manager associated with this instance.
            /// </summary>
            public ArchipelagoConnector Parent { get; }

            /// <summary>
            /// Check whether an Archipelago location check has been completed.
            /// 
            /// This method should be called from the Unity main thread.
            /// </summary>
            /// <param name="location">The Outward APWorld location.</param>
            /// <returns>Whether the Archipelago location check has been completed.</returns>
            public bool IsComplete(APWorld.Location location);

            /// <summary>
            /// Complete an Archipelago location check.
            /// 
            /// This method should be called from the Unity main thread.
            /// </summary>
            /// <param name="location">The Outward APWorld location.</param>
            public void Complete(APWorld.Location location);
        }

        /// <summary>
        /// A sub-manager for Archipelago messages.
        /// </summary>
        public interface IMessageManager
        {
            /// <summary>
            /// The main manager associated with this instance.
            /// </summary>
            public ArchipelagoConnector Parent { get; }

            /// <summary>
            /// Send a message to the Archipelago server on behalf of the player.
            /// 
            /// This method should be called from the Unity main thread.
            /// </summary>
            /// <param name="message">The message to send.</param>
            public void SendMessage(string message);
        }

        /// <summary>
        /// Concrete implementation of the sub-manager for Archipelago items.
        /// </summary>
        private class ItemManager : IItemManager
        {
            /// <summary>
            /// The main manager associated with this instance.
            /// </summary>
            private readonly ArchipelagoConnector _parent;

            /// <summary>
            /// Received items to process.
            /// </summary>
            private readonly ConcurrentQueue<APWorld.Item> _incomingItems = new();

            /// <summary>
            /// A mapping from Outward APWorld items to counts recieved in the current Archipelago session.
            /// </summary>
            private readonly Dictionary<APWorld.Item, int> _sessionItemCounts = new();

            public ItemManager(ArchipelagoConnector parent)
            {
                _parent = parent;
            }

            public ArchipelagoConnector Parent => _parent;

            public int GetCount(APWorld.Item item) => ModQuestEventManager.Instance.Items.GetCount(item);

            /// <summary>
            /// Register Archipelago session event handlers.
            /// </summary>
            /// <param name="session"></param>
            public void RegisterEventHandlers(ArchipelagoSession session)
            {
                session.Items.ItemReceived += OnItemReceived;
            }

            /// <summary>
            /// Unregister Archipelago session event handlers.
            /// </summary>
            /// <param name="session"></param>
            public void UnregisterEventHandlers(ArchipelagoSession session)
            {
                session.Items.ItemReceived -= OnItemReceived;
            }

            /// <summary>
            /// Called every frame while <see cref="Parent"/> is enabled.
            /// </summary>
            public void Update()
            {
                // try to process an item from the queue
                if (OutwardArchipelagoMod.Instance.IsInArchipelagoGame && _incomingItems.TryDequeue(out var item))
                {
                    // Every time we connect to the Archipelago server, it resends every single item. To avoid giving
                    // the player extra copies of the items every time they reconnect, we will keep track of how many
                    // of each item were recieved in the world and how many were recieved in just this Archipelago
                    // session, and only give the player the item if the amount of that item recieved this session
                    // is greater than the amount already saved to the world.

                    if (!_sessionItemCounts.TryGetValue(item, out var count))
                    {
                        count = 0;
                    }

                    count += 1;
                    _sessionItemCounts[item] = count;

                    if (GetCount(item) < count)
                    {
                        Give(item);
                    }
                }
                else if (OutwardArchipelagoMod.Instance.IsInMainMenu && _sessionItemCounts.Count > 0)
                {
                    // This mostly affects if the player switches save files mid-session.
                    // If we throw all the previously seen items back into the queue, we can re-attempt
                    // the logic of giving them to the player one-by-one when they enter a save again.

                    foreach (var pair in _sessionItemCounts)
                    {
                        for (var i = 0; i < pair.Value; i++)
                        {
                            _incomingItems.Enqueue(pair.Key);
                        }
                    }

                    _sessionItemCounts.Clear();
                }
            }

            /// <summary>
            /// Reset state related to the current Archipelago session.
            /// 
            /// This will be called from the Archipelago thread.
            /// </summary>
            public async Task ResetSession()
            {
                while (_incomingItems.TryDequeue(out var _)) { }
                await _parent.RunOnMainThread(() => _sessionItemCounts.Clear());
            }

            /// <summary>
            /// Event delegate for when items are recieved from the Archipelago server.
            /// 
            /// This will be called from the Archipelago thread.
            /// </summary>
            /// <param name="helper"></param>
            private void OnItemReceived(ReceivedItemsHelper helper)
            {
                while (helper.Any())
                {
                    var itemInfo = helper.DequeueItem();
                    if (APWorld.Item.ById.TryGetValue(itemInfo.ItemId, out var item))
                    {
                        OutwardArchipelagoMod.Log.LogInfo($"recieved {item} from the Archipelago server");
                        _incomingItems.Enqueue(item);
                    }
                    else
                    {
                        OutwardArchipelagoMod.Log.LogError($"recieved unrecognize item with ID {itemInfo.ItemId} from the Archipelago server");
                    }
                }
            }

            /// <summary>
            /// Give the Archipelago item to the player and update world state.
            /// 
            /// This will be called from the main thread.
            /// </summary>
            /// <param name="item">The Outward APWorld item.</param>
            private void Give(APWorld.Item item)
            {
                if (!APWorld.ItemToGiver.TryGetValue(item, out var itemGiver))
                {
                    OutwardArchipelagoMod.Log.LogInfo($"tried to give {item} to player, but could not determine the corresponding Outward reward");
                    return;
                }

                OutwardArchipelagoMod.Log.LogInfo($"giving Archipelago item {item} to player");
                var character = CharacterManager.Instance.GetFirstLocalCharacter();
                itemGiver.GiveItem(character);
                ModQuestEventManager.Instance.Items.Add(item);
            }
        }

        /// <summary>
        /// Concrete implementation of the sub-manager for Archipelago locations.
        /// </summary>
        private class LocationManager : ILocationManager
        {
            /// <summary>
            /// The main manager associated with this instance.
            /// </summary>
            private readonly ArchipelagoConnector _parent;

            /// <summary>
            /// Locations checks to send to the Archipelago server.
            /// </summary>
            private readonly ConcurrentQueue<APWorld.Location> _outgoingLocations = new();

            public LocationManager(ArchipelagoConnector parent)
            {
                _parent = parent;
            }

            public ArchipelagoConnector Parent => _parent;

            public bool IsComplete(APWorld.Location location) => ModQuestEventManager.Instance.Locations.Contains(location);

            public void Complete(APWorld.Location location)
            {
                if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled)
                {
                    OutwardArchipelagoMod.Log.LogDebug($"completing check for {location}");
                    ModQuestEventManager.Instance.Locations.Add(location);
                    _outgoingLocations.Enqueue(location);
                }
            }

            /// <summary>
            /// Run necessary background tasks.
            /// 
            /// Called from the Archipelago session thread.
            /// </summary>
            public async Task UpdateAsync()
            {
                var locations = new List<APWorld.Location>();
                while (_outgoingLocations.TryDequeue(out var location))
                {
                    locations.Add(location);
                }

                if (locations.Count > 0)
                {
                    try
                    {
                        await _parent._session.Locations.CompleteLocationChecksAsync(locations.Select(loc => loc.Id).ToArray());
                        OutwardArchipelagoMod.Log.LogInfo($"completed location checks with Archipelago server: {string.Join(", ", locations)}");
                    }
                    catch (Exception ex)
                    {
                        foreach (var location in locations)
                        {
                            _outgoingLocations.Enqueue(location);
                        }

                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// Concrete implementation of the sub-manager for Archipelago messages.
        /// </summary>
        private class MessageManager : IMessageManager
        {
            /// <summary>
            /// The main manager associated with this instance.
            /// </summary>
            private readonly ArchipelagoConnector _parent;

            /// <summary>
            /// The incoming messages to process.
            /// </summary>
            private readonly ConcurrentQueue<LogMessage> _incomingMessages = new();

            /// <summary>
            /// The outgoing messages to send to the Archipelago server.
            /// </summary>
            private readonly ConcurrentQueue<string> _outgoingMessages = new();

            public MessageManager(ArchipelagoConnector parent)
            {
                _parent = parent;
            }

            public ArchipelagoConnector Parent => _parent;

            public void SendMessage(string message)
            {
                OutwardArchipelagoMod.Log.LogInfo($"sending message to the Archipelago server: {message}");
                _outgoingMessages.Enqueue(message);
            }

            /// <summary>
            /// Register Archipelago session event handlers.
            /// </summary>
            /// <param name="session"></param>
            public void RegisterEventHandlers(ArchipelagoSession session)
            {
                session.MessageLog.OnMessageReceived += OnMessageReceived;
            }

            /// <summary>
            /// Unregister Archipelago session event handlers.
            /// </summary>
            /// <param name="session"></param>
            public void UnregisterEventHandlers(ArchipelagoSession session)
            {
                session.MessageLog.OnMessageReceived -= OnMessageReceived;
            }

            /// <summary>
            /// Called every frame while <see cref="Parent"/> is enabled.
            /// </summary>
            public void Update()
            {
                // try to process an item from the queue
                if (OutwardArchipelagoMod.Instance.IsInGame && _incomingMessages.TryDequeue(out var message))
                {
                    var formattedMessage = ArchipelagoToOutwardMessage(message);
                    ChatPanelManager.Instance.SendSystemMessage(formattedMessage);
                }
            }

            /// <summary>
            /// Run necessary background tasks.
            /// 
            /// Called from the Archipelago session thread.
            /// </summary>
            public async Task UpdateAsync()
            {
                while (_outgoingMessages.TryDequeue(out var message))
                {
                    _parent._session.Say(message);
                }
            }

            /// <summary>
            /// Event delegate for when items are recieved from the Archipelago server.
            /// 
            /// This will be called from the Archipelago thread.
            /// </summary>
            /// <param name="helper"></param>
            private void OnMessageReceived(LogMessage message)
            {
                OutwardArchipelagoMod.Log.LogInfo($"recieved message from Archipelago server: {message}");
                _incomingMessages.Enqueue(message);
            }

            /// <summary>
            /// Translate the formatting from the Archipelago message to an Outward message.
            /// </summary>
            /// <param name="message">The formatted Archipelago message.</param>
            /// <returns>A formatted Outward message.</returns>
            private string ArchipelagoToOutwardMessage(LogMessage message)
            {
                var sb = new System.Text.StringBuilder();
                foreach (var part in message.Parts)
                {
                    sb.Append($"<color=#{part.Color.R:X2}{part.Color.G:X2}{part.Color.B:X2}>{part.Text}</color>");
                }

                return sb.ToString();
            }
        }
    }
}
