using HarmonyLib;

namespace OutwardArchipelago.Patch
{
    [HarmonyPatch(typeof(QuestEventManager), nameof(QuestEventManager.NotifyOnQEAddedListeners), new[] { typeof(QuestEventData) })]
    internal class QuestEventManager_NotifyOnQEAddedListeners
    {
        private static bool Prefix(QuestEventManager __instance, QuestEventData _eventData)
        {
            OutwardArchipelagoMod.Log.LogDebug($"[QuestEventManager.NotifyOnQEAddedListeners] {_eventData.Name} ({_eventData.EventUID}) IsStackable={_eventData.IsStackable} StackCount={_eventData.StackCount}");
            return true;
        }
    }
}
