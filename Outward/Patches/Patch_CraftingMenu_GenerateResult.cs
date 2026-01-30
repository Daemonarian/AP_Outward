using System.Collections.Generic;
using HarmonyLib;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Patches
{
    [HarmonyPatch(typeof(CraftingMenu), nameof(CraftingMenu.GenerateResult))]
    internal class Patch_CraftingMenu_GenerateResult
    {
        private static readonly IReadOnlyDictionary<int, long> ItemToLocation = new Dictionary<int, long>
        {
            { OutwardItem.Brand, APWorldLocation.SpawnBrand },
            { OutwardItem.GhostParallel, APWorldLocation.SpawnGhostParallel },
            { OutwardItem.Murmure, APWorldLocation.SpawnMurmure },
            { OutwardItem.Tokebakicit, APWorldLocation.SpawnTokebakicit },
            { OutwardItem.GildedShiverOfTramontane, APWorldLocation.SpawnGildedShiverOfTramontane },
            { OutwardItem.RevenantMoon, APWorldLocation.SpawnRevenantMoon },
            { OutwardItem.GepsBlade, APWorldLocation.SpawnGepsBlade },
            { OutwardItem.GepsLongblade, APWorldLocation.SpawnGepsBlade },
        };

        private static bool Prefix(CraftingMenu __instance, ItemReferenceQuantity _result, int resultMultiplier)
        {
            if (_result != null && _result.Quantity * resultMultiplier > 0 && ItemToLocation.TryGetValue(_result.ItemID, out var location))
            {
                if (ArchipelagoConnector.Instance.TryCompleteLocationCheck(location))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
