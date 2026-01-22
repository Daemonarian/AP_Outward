using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Dialogue;

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

            Log.LogMessage($"{NAME} {VERSION} started successfully");
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
    }
}
