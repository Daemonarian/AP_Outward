using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Exceptions;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using HarmonyLib;
using OutwardArchipelago.QuestEvents;
using OutwardArchipelago.Utils;
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
        /// The sub-manager for all things related to Archipelago death links.
        /// </summary>
        private readonly DeathLinkManager _deathLink;

        /// <summary>
        /// The sub-manager for all things related to Archipelago hints.
        /// </summary>
        private readonly HintManager _hints;

        /// <summary>
        /// All of the sub-managers in an iterable form to more easily manage collectively.
        /// </summary>
        private readonly IReadOnlyList<ManagerBase> _allManagers;

        /// <summary>
        /// The background Archipelago session worker.
        /// </summary>
        private Task _sessionWorker = null;

        /// <summary>
        /// The last recieved slot data.
        /// </summary>
        private APSlotData _slotData = new();

        public ArchipelagoConnector()
        {
            _items = new ItemManager(this);
            _locations = new LocationManager(this);
            _messages = new MessageManager(this);
            _deathLink = new DeathLinkManager(this);
            _hints = new HintManager(this);
            _allManagers = new ManagerBase[] { _items, _locations, _messages, _deathLink, _hints };
        }

        /// <summary>
        /// Singleton pattern.
        /// </summary>
        public static ArchipelagoConnector Instance { get; private set; } = null;

        /// <summary>
        /// The last recieved slot data.
        /// </summary>
        public APSlotData SlotData => _slotData;

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

        /// <summary>
        /// The sub-manager for all things related to Archipelago death links.
        /// </summary>
        public IDeathLinkManager DeathLink => _deathLink;

        /// <summary>
        /// The sub-manager for all things related to Archipelago hints.
        /// </summary>
        public IHintManager Hints => _hints;

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

            foreach (var manager in _allManagers)
            {
                manager.Update();
            }
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
                    await UpdateSession();
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
        private async Task UpdateSession()
        {
            if (_session == null || !_session.Socket.Connected)
            {
                if (IsConnected)
                {
                    await UnityMainThreadDispatcher.Run(() => IsConnected = false);
                }

                var success = await Connect();
                if (!success)
                {
                    OutwardArchipelagoMod.Log.LogInfo($"waiting {ReconnectInterval} ms before retrying connection to Archipelago server...");
                    await Task.Delay(ReconnectInterval);
                    return;
                }

                await UnityMainThreadDispatcher.Run(() => IsConnected = true);
            }

            foreach (var manager in _allManagers)
            {
                await manager.UpdateSession(_session);
            }
        }

        /// <summary>
        /// Connect (or re-connect) to the Archipelago server.
        /// 
        /// This creates a brand new Archipelago session.
        /// </summary>
        private async Task<bool> Connect()
        {
            await Disconnect();

            OutwardArchipelagoMod.Log.LogInfo($"logging into Archipelago server at \"{Host}:{Port}\" with slot \"{SlotName}\" and game \"{APWorld.Game}\"...");
            _session = ArchipelagoSessionFactory.CreateSession(Host, Port);

            // register all event handlers before connecting
            foreach (var manager in _allManagers)
            {
                await manager.OnSessionInit(_session);
            }

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

            if (loginResult is not LoginSuccessful loginSuccessful)
            {
                // this should not happen
                OutwardArchipelagoMod.Log.LogError("failed to login to Archipelago server for unknown reason");
                return false;
            }

            _slotData = new APSlotData(loginSuccessful.SlotData);

            foreach (var manager in _allManagers)
            {
                await manager.OnSessionLoginSuccess(_session, loginSuccessful);
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
                await UnityMainThreadDispatcher.Run(() => IsConnected = false);
            }

            if (_session != null)
            {
                foreach (var manager in _allManagers)
                {
                    await manager.OnSessionDispose(_session);
                }

                _session = null;
            }
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
        /// A sub-manager for Archipelago deathlink.
        /// </summary>
        public interface IDeathLinkManager
        {
            public ArchipelagoConnector Parent { get; }
        }

        /// <summary>
        /// An Archipelago hint, with all the data needed by the UI.
        /// </summary>
        public interface IHint
        {
            /// <summary>
            /// Whether hinted item belongs to the active player.
            /// </summary>
            public bool IsOwnItem { get; }

            /// <summary>
            /// The name of the Player to which the item belongs.
            /// </summary>
            public string PlayerName { get; }

            /// <summary>
            /// The flags representing the type of the item.
            /// </summary>
            public ItemFlags ItemFlags { get; }

            /// <summary>
            /// Get a human-readable representation of the item flags.
            /// </summary>
            public string ItemFlagsString { get; }

            /// <summary>
            /// The hinted item, if it belongs to the active player. Otherwise null.
            /// </summary>
            public APWorld.Item OwnItem { get; }

            /// <summary>
            /// The name of the hinted item.
            /// </summary>
            public string ItemName { get; }
        }

        /// <summary>
        /// A sub-manager for Archipelago hints.
        /// </summary>
        public interface IHintManager
        {
            /// <summary>
            /// The main manager associated with this instance.
            /// </summary>
            public ArchipelagoConnector Parent { get; }

            /// <summary>
            /// Get a hint for a specified location.
            /// 
            /// The callback will be called exactly once and from the main thread.
            /// </summary>
            /// <param name="location">The AP location.</param>
            /// <param name="callback">A callback handler for the hint.</param>
            public void GetHint(APWorld.Location location, Action<IHint> callback);

            /// <summary>
            /// Attempts to retrieve a hint associated with the specified location.
            /// </summary>
            /// <param name="location">The location for which to obtain the hint. Cannot be null.</param>
            /// <param name="hint">When this method returns, contains the hint for the specified location if one exists; otherwise,
            /// contains null. This parameter is passed uninitialized.</param>
            /// <returns>true if a hint was found for the specified location; otherwise, false.</returns>
            bool TryGetHint(APWorld.Location location, out IHint hint);
        }

        /// <summary>
        /// A common base class for all the sub-managers to make managing them easier.
        /// </summary>
        private abstract class ManagerBase
        {
            /// <summary>
            /// Called every frame from the main thread.
            /// </summary>
            public virtual void Update() { }

            /// <summary>
            /// Call every cycle from the Archipelago session thread.
            /// </summary>
            public virtual async Task UpdateSession(ArchipelagoSession session) { }

            /// <summary>
            /// Called when an Archipelago session is initialized.
            /// </summary>
            /// <param name="session">The Archipelago session.</param>
            public virtual async Task OnSessionInit(ArchipelagoSession session) { }

            /// <summary>
            /// Called when an Archipelago session successfully logs into the server.
            /// </summary>
            /// <param name="session">The Archipelago session.</param>
            public virtual async Task OnSessionLoginSuccess(ArchipelagoSession session, LoginSuccessful loginSuccesful) { }

            /// <summary>
            /// Called right before an Archipelago session is disposed.
            /// </summary>
            /// <param name="session">The Archipelago session.</param>
            public virtual async Task OnSessionDispose(ArchipelagoSession session) { }
        }

        /// <summary>
        /// Concrete implementation of the sub-manager for Archipelago items.
        /// </summary>
        private class ItemManager : ManagerBase, IItemManager
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

            public override void Update()
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

            public override async Task OnSessionInit(ArchipelagoSession session)
            {
                session.Items.ItemReceived += OnItemReceived;
            }

            public override async Task OnSessionDispose(ArchipelagoSession session)
            {
                session.Items.ItemReceived -= OnItemReceived;

                while (_incomingItems.TryDequeue(out var _)) { }
                await UnityMainThreadDispatcher.Run(() => _sessionItemCounts.Clear());
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
        private class LocationManager : ManagerBase, ILocationManager
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

                ModSceneManager.Instance.OnEnterArchipelagoGame += SendAllLocalLocationChecks;
            }

            public ArchipelagoConnector Parent => _parent;

            public override async Task OnSessionLoginSuccess(ArchipelagoSession session, LoginSuccessful loginSuccessful)
            {
                if (OutwardArchipelagoMod.Instance.IsInArchipelagoGame)
                {
                    SendAllLocalLocationChecks();
                }
            }

            public override async Task UpdateSession(ArchipelagoSession session)
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
                        await session.Locations.CompleteLocationChecksAsync(locations.Select(loc => loc.Id).ToArray());
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

            private void SendAllLocalLocationChecks()
            {
                if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled)
                {
                    foreach (var location in APWorld.Location.ById.Values)
                    {
                        if (ModQuestEventManager.Instance.Locations.Contains(location))
                        {
                            OutwardArchipelagoMod.Log.LogInfo($"resending {location} found in the local save");
                            _outgoingLocations.Enqueue(location);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Concrete implementation of the sub-manager for Archipelago messages.
        /// </summary>
        private class MessageManager : ManagerBase, IMessageManager
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

            public override async Task OnSessionInit(ArchipelagoSession session)
            {
                session.MessageLog.OnMessageReceived += OnMessageReceived;
            }

            public override async Task OnSessionDispose(ArchipelagoSession session)
            {
                session.MessageLog.OnMessageReceived -= OnMessageReceived;
            }

            public override void Update()
            {
                // try to process an item from the queue
                if (OutwardArchipelagoMod.Instance.IsInGame && _incomingMessages.TryDequeue(out var message))
                {
                    var formattedMessage = ArchipelagoToOutwardMessage(message);
                    ChatPanelManager.Instance.SendSystemMessage(formattedMessage);
                }
            }

            public override async Task UpdateSession(ArchipelagoSession session)
            {
                while (_outgoingMessages.TryDequeue(out var message))
                {
                    session.Say(message);
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

        /// <summary>
        /// Concrete implementation of the sub-manager for Archipelago messages.
        /// </summary>
        private class DeathLinkManager : ManagerBase, IDeathLinkManager
        {
            /// <summary>
            /// The main manager associated with this instance.
            /// </summary>
            private readonly ArchipelagoConnector _parent;

            /// <summary>
            /// Queue for messages created on the Archieplago thread to be sent to system chat.
            /// </summary>
            private readonly ConcurrentQueue<string> _incomingMessage = new();

            /// <summary>
            /// Queue for incoming death links to be handled by the main thread.
            /// </summary>
            private readonly ConcurrentQueue<DeathLink> _incomingDeathLinks = new();

            /// <summary>
            /// Queue for outgoing death links to be handled by the Archipelago thread.
            /// </summary>
            private readonly ConcurrentQueue<DeathLink> _outgoingDeathLinks = new();

            /// <summary>
            /// The death-link service for the current session.
            /// </summary>
            private DeathLinkService _deathLinkService = null;

            /// <summary>
            /// A flag to help ensure deaths caused by death link do not initiate another death link.
            /// </summary>
            private bool _ignoreDeaths = false;

            public DeathLinkManager(ArchipelagoConnector parent)
            {
                _parent = parent;
            }

            public ArchipelagoConnector Parent => _parent;

            public override async Task OnSessionLoginSuccess(ArchipelagoSession session, LoginSuccessful loginSuccessful)
            {
                if (_parent.SlotData.IsDeathLinkEnabled)
                {
                    OutwardArchipelagoMod.Log.LogInfo($"enabling death link...");
                    _deathLinkService = session.CreateDeathLinkService();
                    _deathLinkService.OnDeathLinkReceived += OnDeathLinkRecieved;
                    _deathLinkService.EnableDeathLink();
                    OutwardArchipelagoMod.Log.LogInfo($"death link enabled!");
                    _incomingMessage.Enqueue("<color=#FF0000>Death-Link:</color> <color=#FFFFFF>Enabled</color>");
                }
            }

            public override async Task OnSessionDispose(ArchipelagoSession session)
            {
                if (_deathLinkService is not null)
                {
                    OutwardArchipelagoMod.Log.LogInfo($"disabling death link...");
                    _deathLinkService.DisableDeathLink();
                    _deathLinkService.OnDeathLinkReceived -= OnDeathLinkRecieved;
                    _deathLinkService = null;
                    OutwardArchipelagoMod.Log.LogInfo($"death link disabled");
                    _incomingMessage.Enqueue("<color=#FF0000>Death-Link:</color> <color=#777777>Disabled</color>");
                }
            }

            public override void Update()
            {
                if (OutwardArchipelagoMod.Instance.IsInGame)
                {
                    while (_incomingMessage.TryDequeue(out var message))
                    {
                        ChatPanelManager.Instance.SendSystemMessage(message);
                    }

                    if (_incomingDeathLinks.TryDequeue(out var deathLink))
                    {
                        var message = deathLink.Cause ?? $"<color=#EE00EE>{deathLink.Source}</color> has died.";
                        message = $"<color=#FF0000>Death Link:</color> {message}";
                        ChatPanelManager.Instance.SendSystemMessage(message);

                        OutwardArchipelagoMod.Log.LogMessage($"death link recieved: {message}");

                        if (OutwardArchipelagoMod.Instance.IsInArchipelagoGame)
                        {
                            var character = CharacterManager.Instance.GetFirstLocalCharacter();
                            if (character.m_characterActive && character.Alive)
                            {
                                _ignoreDeaths = true;
                                try
                                {
                                    character.Stats.SetHealth(0f);
                                    character.Die(Vector3.up, false);
                                }
                                finally
                                {
                                    _ignoreDeaths = false;
                                }
                            }
                        }
                    }
                }
            }

            public override async Task UpdateSession(ArchipelagoSession session)
            {
                while (_outgoingDeathLinks.TryDequeue(out var deathLink))
                {
                    _deathLinkService?.SendDeathLink(deathLink);
                }
            }

            /// <summary>
            /// Event handler for when death links are recieved.
            /// </summary>
            /// <param name="deathLink"></param>
            private void OnDeathLinkRecieved(DeathLink deathLink)
            {
                _incomingDeathLinks.Enqueue(deathLink);
            }

            /// <summary>
            /// Called when the Archipelago player has died.
            /// </summary>
            private void OnDeath()
            {
                if (!_ignoreDeaths)
                {
                    OutwardArchipelagoMod.Log.LogMessage($"sending death link");
                    var deathLink = new DeathLink(Instance.SlotName);
                    _outgoingDeathLinks.Enqueue(deathLink);
                }
            }

            /// <summary>
            /// Called when any character has died.
            /// </summary>
            /// <param name="character"></param>
            private void OnCharacterDie(Character character)
            {
                if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled && character.IsLocalPlayer && character.IsWorldHost)
                {
                    OnDeath();
                }
            }

            /// <summary>
            /// Patch to tell when the player has died.
            /// </summary>
            [HarmonyPatch(typeof(Character), nameof(Character.Die), new[] { typeof(Vector3), typeof(bool) })]
            private static class Patch_Character_Die
            {
                private static void Postfix(Character __instance)
                {
                    Instance?._deathLink?.OnCharacterDie(__instance);
                }
            }
        }

        private class Hint : IHint
        {
            private readonly string _playerName;
            private readonly string _itemName;
            private readonly ItemFlags _itemFlags;
            private readonly APWorld.Item _ownItem;

            public Hint(string playerName, string itemName, ItemFlags itemFlags, APWorld.Item ownItem)
            {
                _playerName = playerName;
                _itemName = itemName;
                _itemFlags = itemFlags;
                _ownItem = ownItem;
            }

            public string PlayerName => _playerName;

            public string ItemName => _itemName;

            public ItemFlags ItemFlags => _itemFlags;

            public string ItemFlagsString
            {
                get
                {
                    if (ItemFlags.HasFlag(ItemFlags.Advancement))
                    {
                        return "progression";
                    }
                    else if (ItemFlags.HasFlag(ItemFlags.NeverExclude))
                    {
                        return "useful";
                    }
                    else if (ItemFlags.HasFlag(ItemFlags.Trap))
                    {
                        return "trap";
                    }

                    return "filler";
                }
            }

            public bool IsOwnItem => _ownItem is not null;

            public APWorld.Item OwnItem => _ownItem;

            public override string ToString() => $"{PlayerName}'s {ItemName} ({ItemFlagsString})";
        }

        /// <summary>
        /// Concrete implementation of the sub-manager for Archipelago hints.
        /// </summary>
        private class HintManager : ManagerBase, IHintManager
        {
            /// <summary>
            /// The main manager associated with this instance.
            /// </summary>
            private readonly ArchipelagoConnector _parent;

            /// <summary>
            /// A thread-safe queue of hints that have been requested.
            /// </summary>
            private readonly ConcurrentQueue<APWorld.Location> _requestedHintQueue = new();

            /// <summary>
            /// A lock-object for accessing <see cref="_locationToHint"/> and <see cref="_locationToCallbacks"/>.
            /// </summary>
            private readonly object _locationLock = new();

            /// <summary>
            /// A mapping from locations to hints already known.
            /// </summary>
            private readonly Dictionary<APWorld.Location, Hint> _locationToHint = new();

            /// <summary>
            /// A mapping from locations to callbacks requested for each location.
            /// </summary>
            private readonly Dictionary<APWorld.Location, HashSet<Action<IHint>>> _locationToCallbacks = new();

            /// <summary>
            /// Initializes a new instance of the HintManager class using the specified ArchipelagoConnector.
            /// </summary>
            /// <param name="parent">The ArchipelagoConnector instance to associate with this HintManager. Cannot be null.</param>
            public HintManager(ArchipelagoConnector parent)
            {
                _parent = parent;
            }

            public ArchipelagoConnector Parent => _parent;

            public virtual async Task OnSessionLoginSuccessful(ArchipelagoSession session)
            {
                session.Hints.TrackHints(OnHintsUpdated);
                var hints = await session.Hints.GetHintsAsync();
                OnHintsUpdated(hints);
            }

            public override async Task UpdateSession(ArchipelagoSession session)
            {
                var locations = new List<APWorld.Location>();
                while (_requestedHintQueue.TryDequeue(out var location))
                {
                    locations.Add(location);
                }

                if (locations.Count > 0)
                {
                    try
                    {
                        session.Hints.CreateHints(HintStatus.Unspecified, locations.Select(x => x.Id).ToArray());
                    }
                    catch (Exception)
                    {
                        foreach (var location in locations)
                        {
                            _requestedHintQueue.Enqueue(location);
                        }

                        throw;
                    }

                    var hints = await session.Hints.GetHintsAsync();
                    OnHintsUpdated(hints);
                }
            }

            /// <summary>
            /// Attempts to retrieve the hint associated with the specified location.
            /// </summary>
            /// <remarks>This method is thread-safe.</remarks>
            /// <param name="location">The location for which to retrieve the associated hint.</param>
            /// <param name="hint">When this method returns, contains the hint associated with the specified location, if found; otherwise,
            /// the default value for <see cref="Hint"/>.</param>
            /// <returns>true if a hint is found for the specified location; otherwise, false.</returns>
            public bool TryGetHint(APWorld.Location location, out IHint hint)
            {
                bool hasHint;
                Hint realHint;
                lock (_locationLock)
                {
                    hasHint = _locationToHint.TryGetValue(location, out realHint);
                }

                if (hasHint)
                {
                    hint = realHint;
                    return true;
                }
                else
                {
                    hint = null;
                    return false;
                }
            }

            /// <summary>
            /// Retrieves the hint associated with the specified location and invokes the provided callback when the
            /// hint is available.
            /// </summary>
            /// <param name="location">The location for which to retrieve the hint.</param>
            /// <param name="callback">The callback to invoke with the retrieved hint..</param>
            public void GetHint(APWorld.Location location, Action<IHint> callback)
            {
                bool hasHint;
                Hint hint;
                lock (_locationLock)
                {
                    hasHint = _locationToHint.TryGetValue(location, out hint);
                    if (!hasHint)
                    {
                        if (!_locationToCallbacks.TryGetValue(location, out var callbacks))
                        {
                            callbacks = new();
                            _locationToCallbacks[location] = callbacks;
                        }

                        callbacks.Add(callback);
                    }
                }

                if (hasHint)
                {
                    callback(hint);
                }
                else
                {
                    _requestedHintQueue.Enqueue(location);
                }
            }

            /// <summary>
            /// Processes updated hint information and notifies registered callbacks for relevant locations.
            /// </summary>
            /// <remarks>Only hints associated with the active player are processed. Registered
            /// callbacks for affected locations are invoked on the main thread when their corresponding hints are
            /// updated.</remarks>
            /// <param name="hintInfos">An array of updated hints to process. Each hint represents information associated with a specific
            /// location and player.</param>
            private void OnHintsUpdated(global::Archipelago.MultiClient.Net.Models.Hint[] hintInfos)
            {
                foreach (var hintInfo in hintInfos)
                {
                    if (hintInfo.FindingPlayer == _parent._session.Players.ActivePlayer.Slot && APWorld.Location.ById.TryGetValue(hintInfo.LocationId, out var location))
                    {
                        var hint = CreateHint(_parent._session, hintInfo);
                        OutwardArchipelagoMod.Log.LogInfo($"received hint for {hint.PlayerName}'s {hint.ItemName} ({hint.ItemFlagsString}) in {location.Name}");

                        HashSet<Action<IHint>> callbacks;
                        lock (_locationLock)
                        {
                            _locationToHint[location] = hint;
                            if (_locationToCallbacks.TryGetValue(location, out callbacks))
                            {
                                _locationToCallbacks.Remove(location);
                            }
                            else
                            {
                                callbacks = null;
                            }
                        }

                        if (callbacks is not null)
                        {
                            foreach (var callback in callbacks)
                            {
                                OutwardArchipelagoMod.Log.LogInfo($"queueing hint callback for {location.Name}");
                                UnityMainThreadDispatcher.Run(() => callback(hint));
                            }
                        }
                    }
                }
            }

            /// <summary>
            /// Creates a new Hint instance based on the specified session and hint information.
            /// </summary>
            /// <param name="session">The current Archipelago session used to resolve player and item details.</param>
            /// <param name="hintInfo">The hint information containing the receiving player and item identifiers.</param>
            /// <returns>A Hint object representing the resolved hint for the specified player and item.</returns>
            private Hint CreateHint(ArchipelagoSession session, global::Archipelago.MultiClient.Net.Models.Hint hintInfo)
            {
                var receivingPlayerInfo = session.Players.GetPlayerInfo(hintInfo.ReceivingPlayer);
                var itemName = session.Items.GetItemName(hintInfo.ItemId, receivingPlayerInfo.Game);

                APWorld.Item ownItem = null;
                if (receivingPlayerInfo.Slot == session.Players.ActivePlayer.Slot)
                {
                    if (!APWorld.Item.ById.TryGetValue(hintInfo.ItemId, out ownItem))
                    {
                        ownItem = null;
                    }
                }

                return new Hint(receivingPlayerInfo.Name, itemName, hintInfo.ItemFlags, ownItem);
            }
        }
    }
}
