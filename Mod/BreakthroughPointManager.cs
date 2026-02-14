using HarmonyLib;
using OutwardArchipelago.Archipelago;
using UnityEngine;

namespace OutwardArchipelago
{
    internal static class BreakthroughPointManager
    {
        public static int AcquiredBreakthoughPoints => ArchipelagoConnector.Instance.Items.GetCount(APWorld.Item.BreakthroughPoint);

        [HarmonyPatch(typeof(PlayerCharacterStats), nameof(PlayerCharacterStats.RemainingBreakthrough), MethodType.Getter)]
        private static class Patch_PlayerCharacterStats_RemainingBreakthough_Getter
        {
            private static bool Prefix(ref int __result, PlayerCharacterStats __instance)
            {
                __result = Mathf.Clamp(AcquiredBreakthoughPoints - __instance.m_usedBreakthroughCount, 0, 20);
                return false;
            }
        }
    }
}
