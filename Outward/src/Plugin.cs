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
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace OutwardArchipelago
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

        [HarmonyPatch(typeof(DialogueTree), nameof(DialogueTree.OnGraphStarted))]
        public class DialogueTree_OnGraphStarted
        {
            static void Prefix(DialogueTree __instance)
            {
                if (__instance.name == "Dialogue_RissaAberdeen_Neut_Prequest")
                {
                    if (__instance.primeNode as ConditionNode == null || (__instance.primeNode as ConditionNode).condition as Condition_QuestLicense == null)
                    {
                        var originalStartNode = __instance.primeNode;

                        var gateNode = __instance.AddNode<ConditionNode>();
                        gateNode.condition = new Condition_QuestLicense(1);

                        var rejectNode = __instance.AddNode<StatementNodeExt>();
                        rejectNode.actorName = __instance.actorParameters.FirstOrDefault()?.name ?? "Speaker";
                        rejectNode.statement = new Statement("Come back to me when you have Quest License 1.");

                        Connection.Create(gateNode, originalStartNode);
                        Connection.Create(gateNode, rejectNode);

                        __instance.primeNode = gateNode;
                    }
                }
            }
        }
    }
}
