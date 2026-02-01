using System;
using HarmonyLib;

namespace OutwardArchipelago.Patch
{
    [HarmonyPatch(typeof(Item), nameof(Item.UpdateExtraDataSync), new Type[] { })]
    internal static class Item_UpdateExtraDataSync
    {
        /// <summary>
        /// This patch attempts to restore the "side data" inserted by <see cref="OutwardItemSideData"/>,
        /// after a save file is loaded, or when the data is synced over the network.
        /// </summary>
        private static bool Prefix(Item __instance)
        {
            if (__instance.m_tempExtraData != null && __instance.m_lastReceivedExtraData != null)
            {
                foreach (var pair in __instance.m_lastReceivedExtraData)
                {
                    if (pair.Key.StartsWith(OutwardItemSideData.KEY_PREFIX))
                    {
                        __instance.m_tempExtraData[pair.Key] = pair.Value;
                    }
                }
            }

            return true;
        }
    }
}
