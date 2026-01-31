using HarmonyLib;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Patch
{
    [HarmonyPatch(typeof(ItemDropper), nameof(ItemDropper.GenerateItem), new[] { typeof(ItemContainer), typeof(BasicItemDrop), typeof(int) })]
    internal class ItemDropper_GenerateItem
    {
        private static bool Prefix(ItemDropper __instance, ItemContainer _container, BasicItemDrop _itemDrop, int _spawnAmount)
        {
            if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled)
            {
                if (_container != null && _itemDrop?.DroppedItem != null && _spawnAmount > 0)
                {
                    var itemId = _itemDrop.DroppedItem.ItemID;
                    if (APWorldData.ItemToLocation.TryGetValue(itemId, out var location))
                    {
                        OutwardArchipelagoMod.Log.LogInfo($"NPC dropped item ({itemId}) associated with location ({location}); swallowing drop and sending location check");
                        ArchipelagoConnector.Instance.CompleteLocationCheck(location);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
