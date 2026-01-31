using HarmonyLib;

namespace OutwardArchipelago.Patch
{
    [HarmonyPatch(typeof(QuestEventManager), nameof(QuestEventManager.NotifyOnQERemovedListeners), new[] { typeof(string), typeof(int) })]
    internal class QuestEventManager_NotifyOnQERemovedListeners
    {
        private static bool Prefix(QuestEventManager __instance, string _eventuid, int _stackID)
        {
            OutwardArchipelagoMod.Log.LogDebug($"[QuestEventManager.NotifyOnQERemovedListeners] _eventuid={_eventuid} _stackID={_stackID}");

            return true;
        }
    }
}
