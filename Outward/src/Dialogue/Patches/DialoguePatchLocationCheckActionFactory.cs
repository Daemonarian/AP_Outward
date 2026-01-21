using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago.Data;
using OutwardArchipelago.Dialogue.Actions;
using System.Collections.Generic;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatchLocationCheckActionFactory : IDialoguePatchActionFactory
    {
        public IReadOnlyList<ArchipelagoLocationData> Locations { get; private set; }

        public DialoguePatchLocationCheckActionFactory(IReadOnlyList<ArchipelagoLocationData> locations)
        {
            Locations = locations;
        }

        public DialoguePatchLocationCheckActionFactory(ArchipelagoLocationData location) : this(new[] { location }) { }

        public ActionTask CreateAction(IDialoguePatchContext context)
        {
            return new Action_CompleteLocationCheck(Locations);
        }
    }
}
