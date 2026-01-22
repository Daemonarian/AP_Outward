using System.Collections.Generic;
using System.Linq;
using OutwardArchipelago.Archipelago.Data;
using OutwardArchipelago.Dialogue.Builders.Actions;
using OutwardArchipelago.Dialogue.Builders.Nodes;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class InsertLocationCheckPatch : IDialoguePatch
    {
        public int ReplaceNodeID { get; set; }

        public IReadOnlyList<ArchipelagoLocationData> Locations { get; set; }

        public ArchipelagoLocationData Location { set => Locations = new ArchipelagoLocationData[] { value }; }

        public IReadOnlyList<IActionBuilder> OtherActions { get; set; } = new IActionBuilder[0];

        public IActionBuilder OtherAction { set => OtherActions = new IActionBuilder[] { value }; }

        public INodeBuilder NextNode { get; set; }

        public void ApplyPatch(IDialoguePatchContext context)
        {
            new InsertNodePatch
            {
                ReplaceNodeID = ReplaceNodeID,
                Node = new ActionNodeBuilder
                {
                    Actions = Locations.Select(l => new LocationCheckActionBuilder { Location = l }).Concat(OtherActions).ToList(),
                    NextNode = NextNode ?? new ChildOriginalNodeBuilder { NodeID = ReplaceNodeID },
                }
            }.ApplyPatch(context);
        }
    }
}
