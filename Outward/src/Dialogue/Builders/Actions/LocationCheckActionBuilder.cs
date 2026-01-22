using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago.Data;
using OutwardArchipelago.Dialogue.Actions;

namespace OutwardArchipelago.Dialogue.Builders.Actions
{
    internal class LocationCheckActionBuilder : IActionBuilder
    {
        public ArchipelagoLocationData Location { get; set; }

        public ActionTask BuildAction(IDialoguePatchContext context) => new Action_CompleteLocationCheck(Location);
    }
}
