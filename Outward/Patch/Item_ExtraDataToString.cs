using System.Collections.Generic;
using HarmonyLib;

namespace OutwardArchipelago.Patch
{
    [HarmonyPatch(typeof(Item), nameof(Item.ExtraDataToString), new[] { typeof(Item.SyncType) })]
    internal static class Item_ExtraDataToString
    {
        /// <summary>
        /// This patch attempts to preserve the "side-data" we have inserted into these Items using
        /// <see cref="ItemSideData"/> when saving or syncing the data. This also ensures that this
        /// side data gets saved and synced.
        /// </summary>
        private static bool Prefix(ref string __result, Item __instance, Item.SyncType _syncType)
        {
            var sideData = new Dictionary<string, string>();
            foreach (var pair in __instance.m_tempExtraData)
            {
                if (pair.Key.StartsWith(ItemSideData.KEY_PREFIX))
                {
                    sideData.Add(pair.Key, pair.Value);
                }
            }

            string text = "";
            __instance.m_tempExtraData.Clear();
            __instance.BuildExtraInfoData(_syncType);

            foreach (var pair in sideData)
            {
                __instance.m_tempExtraData[pair.Key] = pair.Value;
            }

            foreach (var pair in __instance.m_tempExtraData)
            {
                text = string.Concat(new string[] { text, pair.Key, "/", pair.Value, ";" });
            }

            __result = text;
            return false;
        }
    }
}
