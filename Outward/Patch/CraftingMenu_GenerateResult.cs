using HarmonyLib;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Patch
{
    [HarmonyPatch(typeof(CraftingMenu), nameof(CraftingMenu.GenerateResult), new[] { typeof(ItemReferenceQuantity), typeof(int) })]
    internal class CraftingMenu_GenerateResult
    {
        private static bool Prefix(CraftingMenu __instance, ItemReferenceQuantity _result, int resultMultiplier)
        {
            if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled)
            {
                if (_result != null && _result.Quantity * resultMultiplier > 0)
                {
                    if (APWorldData.ItemToLocation.TryGetValue(_result.ItemID, out var locationId))
                    {
                        OutwardArchipelagoMod.Log.LogInfo($"player crafted item ({_result.ItemID}) which is associated with location ({locationId}); swallowing crafting result and sending location check");
                        ArchipelagoConnector.Instance.Locations.Complete(locationId);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
