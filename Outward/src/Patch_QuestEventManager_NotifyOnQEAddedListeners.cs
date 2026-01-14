using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutwardArchipelago
{
    [HarmonyPatch(typeof(QuestEventManager), nameof(QuestEventManager.NotifyOnQEAddedListeners))]
    internal class Patch_QuestEventManager_NotifyOnQEAddedListeners
    {
        private static void Prefix(QuestEventManager __instance, QuestEventData _eventData)
        {
            OutwardArchipelagoMod.Log.LogDebug($"[QuestEventManager.NotifyOnQEAddedListeners] {_eventData.Name} ({_eventData.EventUID}) IsStackable={_eventData.IsStackable} StackCount={_eventData.StackCount}");
        }
    }
}
