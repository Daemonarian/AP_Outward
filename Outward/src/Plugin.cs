using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Discord;
using HarmonyLib;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace OutwardModTemplate
{
    

    [BepInPlugin(GUID, NAME, VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "com.daemonarium.apoutward";
        public const string NAME = "Outward Archipelago";
        public const string VERSION = BuildInfo.ModVersion;

        // The singleton instance of this plugin.
        public static Plugin Instance;

        // For accessing your BepInEx Logger from outside of this class (eg Plugin.Log.LogMessage("");)
        internal static ManualLogSource Log;

        public int QuestLicenseLevel = 0;

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
            ConnectToArchipelago();

            Log.LogMessage($"{NAME} {VERSION} started successfully");
        }

        private void ConnectToArchipelago()
        {
            ArchipelagoConnector.Create();
            ArchipelagoConnector.Instance.Connect();
        }

        [HarmonyPatch(typeof(QuestEventManager), nameof(QuestEventManager.NotifyOnQEAddedListeners))]
        public class QuestEventManager_NotifyOnQEAddedListeners
        {
            static void Prefix(QuestEventData _eventData)
            {
                Plugin.Log.LogDebug($"Quest Event Added: {_eventData.EventUID}");
            }
        }

        [HarmonyPatch(typeof(DialogueTree), nameof(DialogueTree.OnGraphStarted))]
        public class DialogueTree_OnGraphStarted
        {
            static void Prefix(DialogueTree __instance)
            {
                Plugin.Log.LogDebug($"Started Dialogue Tree: {__instance.name}");

                foreach (var node in __instance.allNodes.OfType<MultipleChoiceNodeExt>())
                {
                    Plugin.Log.LogDebug($"  - Node: {node.ID} - {node.tag}");
                    foreach (var choice in node.availableChoices)
                    {
                        Plugin.Log.LogDebug($"    - Option: '{choice.statement.text}' ({choice.statement.meta})");
                    }
                }

                if (__instance.name == "Dialogue_RissaAberdeen_Neut_Prequest")
                {
                    var node = __instance.GetNodeWithID(55) as MultipleChoiceNodeExt;
                    if (node != null)
                    {
                        var choice = node.availableChoices[0];
                        if (choice.condition as Condition_CheckLicense != null)
                        {
                            var licenseCondition = new Condition_CheckLicense();
                            choice.condition = licenseCondition;
                        }
                    }
                }
            }
        }

        public class Condition_CheckLicense : ConditionTask
        {
            public override string info => $"Requires Quest License Lv 1";

            public override bool OnCheck()
            {
                return Plugin.Instance.QuestLicenseLevel >= 1;
            }
        }
    }
}
