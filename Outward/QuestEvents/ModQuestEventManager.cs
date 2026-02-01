namespace OutwardArchipelago.QuestEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OutwardArchipelago.Archipelago;
    using OutwardArchipelago.Utils;

    /// <summary>
    /// Encapsulates all the logic for our mod's custom quest events.
    /// </summary>
    internal sealed class ModQuestEventManager
    {
        /// <summary>
        /// Prefix to use for the name of all custom quest events managed by this class.
        /// </summary>
        private const string QUEST_EVENT_NAME_PREFIX = "OutwardArchipelago";

        /// <summary>
        /// Prefix to use for the UID of all custom quest events managed by this class.
        /// 
        /// This is simply a pre-generated 128-bit GUID encoded in Base64 that has been confirmed to be unique amongst
        /// event UIDs that already exist in Outward.
        /// </summary>
        private const string QUEST_EVENT_UID_PREFIX = "MgB4Mh0qAkCR_zlanuNsKg";

        /// <summary>
        /// Singleton pattern with lazy initialization.
        /// </summary>
        private static readonly Lazy<ModQuestEventManager> _instance = new(() => new ModQuestEventManager());

        /// <summary>
        /// Sub-manager for quest events related to Archipelago items.
        /// </summary>
        private readonly ItemManager _items;

        /// <summary>
        /// Sub-manager for quest events related to Archipelago locations.
        /// </summary>
        private readonly LocationManager _locations;

        /// <summary>
        /// Kept private for Singleton pattern.
        /// </summary>
        private ModQuestEventManager()
        {
            _items = new ItemManager(this);
            _locations = new LocationManager(this);
        }

        /// <summary>
        /// Singleton pattern.
        /// </summary>
        public static ModQuestEventManager Instance => _instance.Value;

        /// <summary>
        /// Sub-manager for quest events related to Archipelago items.
        /// </summary>
        public IItemManager Items => _items;

        /// <summary>
        /// Sub-manager for quest events related to Archipelago locations.
        /// </summary>
        public ILocationManager Locations => _locations;

        /// <summary>
        /// Called after QuestEventDictionary.Load.
        /// 
        /// Used to insert our own quest events into the dictionary.
        /// </summary>
        public void QuestEventDictionary_OnLoad()
        {
            QuestEventDictionary.CurrentStatus = QuestEventDictionary.Status.Loading;

            try
            {
                var newQuestEventFamilies = CreateQuestEventFamilies();
                foreach (var questEventFamily in newQuestEventFamilies)
                {
                    QuestEventDictionary.m_sections.Add(questEventFamily);
                    questEventFamily.SortEvents();
                    questEventFamily.RelinkEvents();
                }

                QuestEventDictionary.RefreshExistingQuestEvents();
                QuestEventDictionary.SortSections();
            }
            catch (Exception ex)
            {
                OutwardArchipelagoMod.Log.LogError($"an error occured while registering our custom quest events:\n{ex}");
            }
            finally
            {
                QuestEventDictionary.CurrentStatus = QuestEventDictionary.Status.Ready;
            }
        }

        /// <summary>
        /// Create all of the custom quest event families for our mod.
        /// </summary>
        /// <returns>Newly constructed custom quest event families.</returns>
        private List<QuestEventFamily> CreateQuestEventFamilies()
        {
            return new List<QuestEventFamily>
            {
                _items.CreateQuestEventFamily(),
                _locations.CreateQuestEventFamily(),
            };
        }

        /// <summary>
        /// A sub-manager for quest events related to Archipelago items.
        /// </summary>
        public interface IItemManager
        {
            /// <summary>
            /// The main quest event manager associated with this sub-manager.
            /// </summary>
            public abstract ModQuestEventManager Parent { get; }

            /// <summary>
            /// Get the number of an Archipelago item that exist in the world.
            /// </summary>
            /// <param name="itemId">The Outward APWorld item ID.</param>
            /// <returns>The count of that item.</returns>
            public abstract int GetCount(long itemId);

            /// <summary>
            /// Add a single Archipelago item to the world.
            /// 
            /// This does not actually grant the item, it merely updates the count in the save file.
            /// </summary>
            /// <param name="itemId">The Outward APWorld item ID.</param>
            public abstract void Add(long itemId);
        }

        /// <summary>
        /// A sub-manager for quest events related to Archipelago locations.
        /// </summary>
        public interface ILocationManager
        {
            /// <summary>
            /// The main quest event manager associated with this sub-manager.
            /// </summary>
            public abstract ModQuestEventManager Parent { get; }

            /// <summary>
            /// Check whether an Archipelago location has been triggered.
            /// </summary>
            /// <param name="locationId">The Outward APWorld location ID.</param>
            /// <returns>Whether the Archipelago location has been triggered.</returns>
            public abstract bool Contains(long locationId);

            /// <summary>
            /// Trigger an Archipelago location.
            /// 
            /// This does not contact the Archipelago server, it merely updates the save file.
            /// </summary>
            /// <param name="locationId">The Outward APWorld location ID.</param>
            public abstract void Add(long locationId);
        }

        /// <summary>
        /// Concrete implementation of the sub-manager for quest events related to Archipelago items.
        /// </summary>
        private sealed class ItemManager : IItemManager
        {
            /// <summary>
            /// The main quest event manager associated with this sub-manager.
            /// </summary>
            private readonly ModQuestEventManager _parent;

            /// <summary>
            /// A mapping from Outward APWorld item IDs to their corresponding event UIDs.
            /// </summary>
            private readonly IReadOnlyDictionary<long, string> _itemToEvent;

            public ItemManager(ModQuestEventManager parent)
            {
                _parent = parent;
                _itemToEvent = APWorldItem.All.ToDictionary(id => id, id => $"{QUEST_EVENT_UID_PREFIX}A{Base64SafeEncoder<long>.Default.Encode(id)}");
            }

            public ModQuestEventManager Parent => _parent;

            public int GetCount(long itemId)
            {
                if (!_itemToEvent.TryGetValue(itemId, out var eventUid))
                {
                    OutwardArchipelagoMod.Log.LogError($"tried to get the quest event stack count for item {itemId}; but the corresponding event UID could not be found");
                    return 0;
                }

                if (QuestEventManager.Instance is null)
                {
                    OutwardArchipelagoMod.Log.LogError($"tried to get the quest event stack count for item {itemId}; but the QuestEventManager was not ready");
                    return 0;
                }

                var stackCount = QuestEventManager.Instance.GetEventCurrentStack(eventUid);
                if (stackCount < 0)
                {
                    stackCount = 0;
                }

                return stackCount;
            }

            public void Add(long itemId)
            {
                if (!_itemToEvent.TryGetValue(itemId, out var eventUid))
                {
                    OutwardArchipelagoMod.Log.LogError($"tried to add a quest event stack for item {itemId}; but the corresponding event UID could not be found");
                    return;
                }

                if (QuestEventManager.Instance is null)
                {
                    OutwardArchipelagoMod.Log.LogError($"tried to add a quest event stack for item {itemId}; but the QuestEventManager was not ready");
                    return;
                }

                if (!QuestEventManager.Instance.AddEvent(eventUid))
                {
                    OutwardArchipelagoMod.Log.LogError($"tried to add a quest event stack for item {itemId}; but the event '{eventUid}' could not be found");
                }
            }

            /// <summary>
            /// Create a custom quest event family for Archipelago items.
            /// </summary>
            /// <returns>A newly constructed quest event family.</returns>
            internal QuestEventFamily CreateQuestEventFamily()
            {
                return new QuestEventFamily
                {
                    Name = $"{QUEST_EVENT_NAME_PREFIX}_APItem",
                    Events = APWorldItem.All.Select(CreateQuestEventSignature).ToList(),
                };
            }

            /// <summary>
            /// Create a custom quest event signature for an Archipelago item.
            /// </summary>
            /// <param name="itemId">The Outward APWorld item ID.</param>
            /// <returns>A newly constructed quest event signature.</returns>
            private QuestEventSignature CreateQuestEventSignature(long itemId)
            {
                return new QuestEventSignature
                {
                    EventUID = _itemToEvent[itemId],
                    EventName = $"{QUEST_EVENT_NAME_PREFIX}_APItem_{itemId}",
                    Description = $"Triggers when the Archipelago item with id {itemId} is received.",
                    Savable = true,
                    IsStackable = true,
                    IsTimedEvent = false,
                    IsEphemeral = false,
                    DLCId = 0,
                };
            }
        }

        /// <summary>
        /// Concrete implementation of the sub-manager for quest events related to Archipelago locations.
        /// </summary>
        private sealed class LocationManager : ILocationManager
        {
            /// <summary>
            /// The main quest event manager associated with this sub-manager.
            /// </summary>
            private readonly ModQuestEventManager _parent;

            /// <summary>
            /// A mapping from Outward APWorld location IDs to their corresponding event UIDs.
            /// </summary>
            private readonly IReadOnlyDictionary<long, string> _locationToEvent;

            public LocationManager(ModQuestEventManager parent)
            {
                _parent = parent;
                _locationToEvent = APWorldLocation.All.ToDictionary(id => id, id => $"{QUEST_EVENT_UID_PREFIX}B{Base64SafeEncoder<long>.Default.Encode(id)}");
            }

            public ModQuestEventManager Parent => _parent;

            public bool Contains(long locationId)
            {
                if (!_locationToEvent.TryGetValue(locationId, out var eventUid))
                {
                    OutwardArchipelagoMod.Log.LogError($"tried to check the quest event for location {locationId}; but the corresponding event UID could not be found");
                    return false;
                }

                if (QuestEventManager.Instance is null)
                {
                    OutwardArchipelagoMod.Log.LogError($"tried to check the quest event stack count for location {locationId}; but the QuestEventManager was not ready");
                    return false;
                }

                return QuestEventManager.Instance.HasQuestEvent(eventUid);
            }

            public void Add(long locationId)
            {
                if (!_locationToEvent.TryGetValue(locationId, out var eventUid))
                {
                    OutwardArchipelagoMod.Log.LogError($"tried to add quest event for location {locationId}; but the corresponding event UID could not be found");
                    return;
                }

                if (QuestEventManager.Instance is null)
                {
                    OutwardArchipelagoMod.Log.LogError($"tried to add quest event for location {locationId}; but the QuestEventManager was not ready");
                    return;
                }

                if (!QuestEventManager.Instance.AddEvent(eventUid))
                {
                    OutwardArchipelagoMod.Log.LogError($"tried to add quest event for location {locationId}; but the event '{eventUid}' could not be found");
                }
            }

            /// <summary>
            /// Create a custom quest event family for Archipelago locations.
            /// </summary>
            /// <returns>A newly constructed quest event family.</returns>
            internal QuestEventFamily CreateQuestEventFamily()
            {
                return new QuestEventFamily
                {
                    Name = $"{QUEST_EVENT_NAME_PREFIX}_APLocation",
                    Events = APWorldLocation.All.Select(CreateQuestEventSignature).ToList(),
                };
            }

            /// <summary>
            /// Create a custom quest event signature for an Archipelago item.
            /// </summary>
            /// <param name="itemId">The Outward APWorld item ID.</param>
            /// <returns>A newly constructed quest event signature.</returns>
            private QuestEventSignature CreateQuestEventSignature(long locationId)
            {
                return new QuestEventSignature
                {
                    EventUID = _locationToEvent[locationId],
                    EventName = $"{QUEST_EVENT_NAME_PREFIX}_APLocation_{locationId}",
                    Description = $"Triggers when the Archipelago location with id {locationId} has been checked.",
                    Savable = true,
                    IsStackable = false,
                    IsTimedEvent = false,
                    IsEphemeral = false,
                    DLCId = 0,
                };
            }

        }
    }
}
