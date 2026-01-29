using System.Collections.Generic;
using System.Linq;
using OutwardArchipelago.Dialogue.Builders.Actions;
using OutwardArchipelago.Dialogue.Builders.Nodes;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class InsertLocationCheckPatch : IDialoguePatch
    {
        public int ReplaceNodeID { get; set; }

        public IReadOnlyList<long> LocationIds { get; set; }

        public long LocationId { set => LocationIds = new[] { value }; }

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
                    Actions = LocationIds.Select(l => new LocationCheckActionBuilder { LocationId = l }).Concat(OtherActions).ToList(),
                    NextNode = NextNode ?? new ChildOriginalNodeBuilder { NodeID = ReplaceNodeID },
                }
            }.ApplyPatch(context);
        }
    }
}
