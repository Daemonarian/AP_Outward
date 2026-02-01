using System;
using HarmonyLib;
using OutwardArchipelago.QuestEvents;

namespace OutwardArchipelago.Patch
{
    [HarmonyPatch(typeof(QuestEventDictionary), nameof(QuestEventDictionary.Load), new Type[] { })]
    internal static class QuestEventDictionary_Load
    {
        private static void Postfix()
        {
            ModQuestEventManager.Instance.QuestEventDictionary_OnLoad();
        }
    }
}
