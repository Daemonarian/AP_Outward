using HarmonyLib;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Patch
{
    [HarmonyPatch(typeof(CharacterInventory), nameof(CharacterInventory.TakeItem), new[] { typeof(Item), typeof(bool) })]
    internal static class CharacterInventory_TakeItem
    {
        /// <summary>
        /// This patch aims to detect when items associated with Archipelago location checks
        /// are picked up.
        /// </summary>
        private static bool Prefix(CharacterInventory __instance, Item takenItem, bool _tryToEquip)
        {
            if (takenItem.TryGetSideData("AP_Location", out APWorld.Location location))
            {
                if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled)
                {
                    OutwardArchipelagoMod.Log.LogInfo($"player picked up an item tagged with location ({location}); sending location check");
                    ArchipelagoConnector.Instance.Locations.Complete(location);
                }
                else
                {
                    OutwardArchipelagoMod.Log.LogWarning($"player picked up an item tagged with location ({location}) while not playing in an Archipelago session");
                }
            }

            if (takenItem.ItemID == OutwardItem.APItem)
            {
                OutwardArchipelagoMod.Log.LogInfo("player picked up an AP item; removing item");
                ItemManager.Instance.DestroyItem(takenItem);
                return false;
            }

            return true;
        }
    }
}
