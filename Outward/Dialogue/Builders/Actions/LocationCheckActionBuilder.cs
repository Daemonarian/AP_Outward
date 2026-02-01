using System.Collections.Generic;
using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Dialogue.Actions;

namespace OutwardArchipelago.Dialogue.Builders.Actions
{
    internal class LocationCheckActionBuilder : IActionBuilder
    {
        public IReadOnlyList<APWorld.Location> Locations { get; set; } = new APWorld.Location[0];

        public APWorld.Location Location { set { Locations = new[] { value }; } }

        public ActionTask BuildAction(IDialoguePatchContext context) => new Action_CompleteLocationCheck(Locations);
    }
}
