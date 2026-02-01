using System.Collections.Generic;
using HarmonyLib;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Patch
{
    [HarmonyPatch(typeof(LootableOnDeath), nameof(LootableOnDeath.OnDeath), new[] { typeof(bool) })]
    internal class LootableOnDeath_OnDeath
    {
        private static bool Prefix(LootableOnDeath __instance, bool _loadedDead)
        {
            if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled)
            {
                if (__instance.Character?.Alive == false && __instance.EnabledPouch && __instance.Character.Inventory.Pouch)
                {
                    var items = __instance.Character.Inventory.Pouch.GetContainedItems();
                    if (items != null)
                    {
                        var itemsToRemove = new List<Item>();
                        foreach (var item in items)
                        {
                            if (APWorld.ItemToLocation.TryGetValue(item.ItemID, out var location))
                            {
                                OutwardArchipelagoMod.Log.LogInfo($"NPC died with item ({item.ItemID}) associated with location ({location}) in their lootable inventory; removing item and sending location check");
                                itemsToRemove.Add(item);
                                ArchipelagoConnector.Instance.Locations.Complete(location);
                            }
                        }

                        foreach (var item in itemsToRemove)
                        {
                            __instance.Character.Inventory.Pouch.RemoveItem(item);
                        }
                    }
                }
            }

            return true;
        }
    }
}
