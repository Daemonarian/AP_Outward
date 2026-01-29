using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Actions;

namespace OutwardArchipelago.Dialogue.Builders.Actions
{
    internal class LocationCheckActionBuilder : IActionBuilder
    {
        public long LocationId { get; set; }

        public ActionTask BuildAction(IDialoguePatchContext context) => new Action_CompleteLocationCheck(LocationId);
    }
}
