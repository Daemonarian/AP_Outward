using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Dialogue;
using UnityEngine.SceneManagement;

namespace OutwardArchipelago
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class OutwardArchipelagoMod : BaseUnityPlugin
    {
        public const string GUID = "com.daemonarium.apoutward";
        public const string NAME = "Outward Archipelago";
        public const string VERSION = BuildInfo.ModVersion;

        // The singleton instance of this plugin.
        public static OutwardArchipelagoMod Instance;

        // For accessing your BepInEx Logger from outside of this class (eg Plugin.Log.LogMessage("");)
        internal static ManualLogSource Log;

        // Config settings
        public static ConfigEntry<string> ArchipelagoHost;
        public static ConfigEntry<int> ArchipelagoPort;
        public static ConfigEntry<string> ArchipelagoPassword;
        public static ConfigEntry<string> ArchipelagoSlotName;

        /// <summary>
        /// Whether or not the game is in the main menu.
        /// </summary>
        public bool IsInMainMenu => Global.Lobby.PlayersInLobbyCount == 0;

        /// <summary>
        /// Whether or not Archipelago-related modifications should be applied.
        /// </summary>
        public bool IsArchipelagoEnabled => PhotonNetwork.isMasterClient;

        /// <summary>
        /// Whether or not the player is actually playing (not in a loading screen or paused).
        /// </summary>
        public bool IsInGame => !IsInMainMenu && NetworkLevelLoader.Instance.IsOverallLoadingDone && !NetworkLevelLoader.Instance.IsGameplayPaused;

        /// <summary>
        /// Whether or not the player is actually playing in an Archipelago-ready game.
        /// </summary>
        public bool IsInArchipelagoGame => IsArchipelagoEnabled && IsInGame;

        /// <summary>
        /// Whether or not the current scene has been initialized with our own mod-specific initialization.
        /// </summary>
        private bool hasInitializedScene = true;

        /// <summary>
        /// Initializes or updates the configuration settings for the current instance.
        /// </summary>
        private void BindConfig()
        {
            const string ArchipelagoSectionName = "Archipelago";

            ArchipelagoHost = Config.Bind(
                ArchipelagoSectionName,
                "Host",
                "archipelago.gg",
                "Archipelago server host name."
            );

            ArchipelagoPort = Config.Bind(
                ArchipelagoSectionName,
                "Port",
                38281,
                new ConfigDescription(
                    "Archipelago server port.",
                    new AcceptableValueRange<int>(0, 65535)
                )
            );

            ArchipelagoPassword = Config.Bind(
                ArchipelagoSectionName,
                "Password",
                "",
                "The password to use when logging into the Archipelago server. Leave blank for no password."
            );

            ArchipelagoSlotName = Config.Bind(
                ArchipelagoSectionName,
                "Slot",
                "Player1",
                "The name of the slot to connect to on the Archipelago server."
            );
        }

        internal void Awake()
        {
            Instance = this;
            Log = this.Logger;

            Log.LogMessage($"Starting {NAME} {VERSION}...");

            BindConfig();
            new Harmony(GUID).PatchAll();
            ArchipelagoConnector.Create();
            DialoguePatcher.Instance.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;

            Log.LogMessage($"{NAME} {VERSION} started successfully");
        }

        internal void Update()
        {
            if (!hasInitializedScene && IsInGame)
            {
                if (IsArchipelagoEnabled && NetworkLevelLoader.Instance.LevelLoadedForFirstTime)
                {
                    InitScene();
                }

                hasInitializedScene = true;
            }
        }

        public byte[] LoadAsset(string fileName)
        {
            var path = Path.Combine(Path.GetDirectoryName(Info.Location), "assets", fileName);
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }

            Log.LogError($"Could not find asset at: {path}");
            return null;
        }

        /// <summary>
        /// Retrieves the localized string associated with the specified key for the current mod.
        /// </summary>
        /// <remarks>If the specified key does not exist in the localization resources, an error is logged
        /// and a placeholder string is returned instead of throwing an exception. This allows the application to
        /// continue running even if some localization entries are missing.</remarks>
        /// <param name="key">The key identifying the localized string to retrieve. This key is combined with the mod's GUID to form the
        /// full localization key.</param>
        /// <returns>The localized string corresponding to the specified key. If the key is not found, a placeholder string
        /// containing the full key is returned.</returns>
        public string GetLocalizedModString(string key)
        {
            var fullMessageKey = $"{GUID}.{key}";
            if (!LocalizationManager.Instance.TryGetLoc(fullMessageKey, out var text))
            {
                Log.LogError($"Failed to find localized string: {fullMessageKey}");
                text = $"[LOC] {fullMessageKey}";
            }

            return text;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            hasInitializedScene = false;
        }

        private void InitScene()
        {
            var items = ItemManager.Instance.WorldItems.Values.ToArray();
            foreach (var item in items)
            {
                if (item != null && item.transform?.parent?.parent?.name != "EquipmentSlots")
                {
                    if (APWorld.ItemToLocation.TryGetValue(item.ItemID, out var location))
                    {
                        var transform = item.transform;
                        var transforms = new List<string>();
                        while (transform != null)
                        {
                            transforms.Add(transform.name);
                            transform = transform.parent;
                        }

                        transforms.Reverse();

                        Log.LogInfo($"world spawned item ({item.ItemID}) associated with location ({location}) with transform: {string.Join(" > ", transforms)}");
                        var newItem = ItemManager.Instance.GenerateItemNetwork(OutwardItem.APItem);
                        newItem.SetSideData("AP_Location", location);
                        newItem.ChangeParent(item.transform.parent, item.transform.position, item.transform.rotation);
                        ItemManager.Instance.DestroyItem(item);
                    }
                }
            }
        }
    }
}
