using System;
using HarmonyLib;

namespace OutwardArchipelago.Patch
{
    [HarmonyPatch(typeof(Item), nameof(Item.StartInit), new Type[] { })]
    internal static class Item_StartInit
    {
        private static void Postfix(Item __instance)
        {
            if (__instance.ItemID == OutwardItem.APItem)
            {
                __instance.gameObject.AddComponent<RainbowGlow>();
            }
        }
    }
}
