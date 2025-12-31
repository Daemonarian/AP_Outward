using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
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


namespace OutwardArchipelago
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        // Choose a GUID for your project. Change "myname" and "mymod".
        public const string GUID = "apoutward.daemonarium.com";
        // Choose a NAME for your project, generally the same as your Assembly Name.
        public const string NAME = "Outward Archipelago";
        // Increment the VERSION when you release a new version of your mod.
        public const string VERSION = "1.0.0";

        // For accessing your BepInEx Logger from outside of this class (eg Plugin.Log.LogMessage("");)
        internal static ManualLogSource Log;

        // Awake is called when your plugin is created. Use this to set up your mod.
        internal void Awake()
        {
            Log = this.Logger;
            Log.LogMessage($"Hello world from {NAME} {VERSION}!");

            // Harmony is for patching methods. If you're not patching anything, you can comment-out or delete this line.
            new Harmony(GUID).PatchAll();
        }

        [HarmonyPatch(typeof(QuestEventManager), nameof(QuestEventManager.NotifyOnQEAddedListeners), new Type[] { typeof(QuestEventData) })]
        public class QuestEventManager_NotifyOnQEAddedListeners
        {
            static void Prefix(QuestEventData _eventData)
            {
                Plugin.Log.LogMessage($"QuestEventAdded: EventUID = {_eventData.EventUID}, Name = {_eventData.Name}");
            }
        }

        [HarmonyPatch(typeof(DialogueTree), nameof(DialogueTree.OnGraphStarted))]
        public class DialogueTree_OnGraphStarted
        {
            static void Prefix(DialogueTree __instance)
            {
                Plugin.Log.LogInfo($"Started Dialogue Tree: {__instance.name}");

                foreach (var node in __instance.allNodes.OfType<MultipleChoiceNodeExt>())
                {
                    Plugin.Log.LogInfo($"  - Node: {node.ID} - {node.tag}");
                    foreach (var choice in node.availableChoices)
                    {
                        Plugin.Log.LogInfo($"    - Option: '{choice.statement.text}' ({choice.statement.meta})");
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
                return false;
            }
        }
    }
}
