using System.Collections.Generic;
using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using OutwardArchipelago.Dialogue.Builders.Actions;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class ReplaceItemRewardWithLocationCheckActionPatch : IActionPatch
    {
        public IReadOnlyDictionary<int, long> ItemToLocation { get; set; } = new Dictionary<int, long>();

        public ActionTask BuildAction(IDialoguePatchContext context, ActionTask action)
        {
            if (action is GiveReward giveRewardAction)
            {
                if (giveRewardAction.ItemReward != null)
                {
                    var locationIds = new List<long>();
                    foreach (var itemReward in giveRewardAction.ItemReward)
                    {
                        var itemId = itemReward?.Item?.value?.ItemID;
                        if (itemId.HasValue && ItemToLocation.TryGetValue(itemId.Value, out var locationId))
                        {
                            locationIds.Add(locationId);
                        }
                    }

                    if (locationIds.Count > 0)
                    {
                        return new LocationCheckActionBuilder { LocationIds = locationIds }.BuildAction(context);
                    }
                }
            }

            return null;
        }
    }
}
