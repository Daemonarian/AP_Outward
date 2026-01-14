using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutwardArchipelago
{
    [HarmonyPatch(typeof(QuestEventManager), nameof(QuestEventManager.NotifyOnQERemovedListeners))]
    internal class Patch_QuestEventManager_NotifyOnQERemovedListeners
    {
        private static void Prefix(QuestEventManager __instance, string _eventuid, int _stackID)
        {
            OutwardArchipelagoMod.Log.LogDebug($"[QuestEventManager.NotifyOnQERemovedListeners] _eventuid={_eventuid} _stackID={_stackID}");
        }
    }
}
