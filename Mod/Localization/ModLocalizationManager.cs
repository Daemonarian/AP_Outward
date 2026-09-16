using System;
using System.IO;
using System.Linq;
using System.Text;
using HarmonyLib;
using Localizer;
using Newtonsoft.Json;
using UnityEngine;

namespace OutwardArchipelago.Localization
{
    internal class ModLocalizationManager : MonoBehaviour
    {
        public const string LogName = "Localization";

        public static ModLocalizationManager Instance { get; private set; } = null;

        protected void Awake()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = this;
        }

        /// <summary>
        /// Load the mod-specific dialogue localizations.
        /// </summary>
        /// <param name="localizationManager">The core Outward localization manager.</param>
        private void LoadDialogueLocalization(LocalizationManager localizationManager)
        {
            try
            {
                localizationManager.m_dialogueLocalizationLoaded = false;

                OutwardArchipelagoMod.Log.LogInfo($"[{LogName}] Loading localization packs...");

                var localizationPath = Path.Combine(OutwardArchipelagoMod.Instance.ModPath, "lang");
                var filePaths = Directory.GetFiles(localizationPath, "*.dialogue.json", SearchOption.TopDirectoryOnly);
                foreach (var filePath in filePaths)
                {
                    var rawText = File.ReadAllText(filePath, Encoding.UTF8);
                    var packTemplate = JsonConvert.DeserializeObject<DialogueLocalizationPackTemplate>(rawText);

                    if (!string.Equals(packTemplate.Language, localizationManager.CurrentLanguage, StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }

                    OutwardArchipelagoMod.Log.LogInfo($"[{LogName}]   Loading dialogue localization pack: {Path.GetFileName(filePath)}");

                    foreach (var entry in packTemplate.Entries)
                    {
                        var key = entry.Key.Trim();
                        if (string.IsNullOrEmpty(key))
                        {
                            continue;
                        }

                        key = key.StartsWith("/") ? key.Substring(1) : $"{OutwardArchipelagoMod.GUID}.{key}";

                        var emoteData = string.Join(";", entry.EmoteTags);
                        var animData = string.Join(";", entry.AnimTags);

                        var dialogueLocalization = new DialogueLocalization(key, entry.General, entry.Female, entry.UniqueAudioName, emoteData, animData, entry.DLC);
                        localizationManager.m_dialogueLocalization[key] = dialogueLocalization;
                    }
                }

                localizationManager.m_debugDialogueLocalization = localizationManager.m_dialogueLocalization.Values.ToArray();
            }
            catch (Exception e)
            {
                OutwardArchipelagoMod.Log.LogError($"[{LogName}] An error occurred while loading mod dialogue localization: {e}");
            }
            finally
            {
                localizationManager.m_dialogueLocalizationLoaded = true;
            }
        }

        [HarmonyPatch(typeof(LocalizationManager), nameof(LocalizationManager.LoadDialogueLocalization), new Type[] { })]
        private static class LocalizationManager_LoadDialogueLocalization
        {
            private static void Postfix(LocalizationManager __instance)
            {
                Instance?.LoadDialogueLocalization(__instance);
            }
        }
    }
}
