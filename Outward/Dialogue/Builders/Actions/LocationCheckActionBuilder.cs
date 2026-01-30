using System.Collections.Generic;
using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Actions;

namespace OutwardArchipelago.Dialogue.Builders.Actions
{
    internal class LocationCheckActionBuilder : IActionBuilder
    {
        public IReadOnlyList<long> LocationIds { get; set; } = new long[0];

        public long LocationId { set { LocationIds = new[] { value }; } }

        public ActionTask BuildAction(IDialoguePatchContext context) => new Action_CompleteLocationCheck(LocationIds);
    }
}
