using System.Collections.Generic;
using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Dialogue.Builders.Actions;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class ReplaceItemRewardWithLocationCheckActionPatch : IActionPatch
    {
        public IReadOnlyDictionary<int, APWorld.Location> ItemToLocation { get; set; } = new Dictionary<int, APWorld.Location>();

        public ActionTask BuildAction(IDialoguePatchContext context, ActionTask action)
        {
            if (action is GiveReward giveRewardAction)
            {
                if (giveRewardAction.ItemReward != null)
                {
                    var locations = new List<APWorld.Location>();
                    foreach (var itemReward in giveRewardAction.ItemReward)
                    {
                        var itemId = itemReward?.Item?.value?.ItemID;
                        if (itemId.HasValue && ItemToLocation.TryGetValue(itemId.Value, out var location))
                        {
                            locations.Add(location);
                        }
                    }

                    if (locations.Count > 0)
                    {
                        return new LocationCheckActionBuilder { Locations = locations }.BuildAction(context);
                    }
                }
            }

            return null;
        }
    }
}
