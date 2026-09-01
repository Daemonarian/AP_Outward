using System.Collections.Generic;
using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Graphs.Actions;

namespace OutwardArchipelago.Graphs.Builders.Actions
{
    internal class LocationCheckActionBuilder : IActionBuilder
    {
        public IReadOnlyList<APWorld.Location> Locations { get; set; } = new APWorld.Location[0];

        public APWorld.Location Location { set { Locations = new[] { value }; } }

        public ActionTask BuildAction(IGraphPatchContext context) => new Action_CompleteLocationCheck(Locations);
    }
}
