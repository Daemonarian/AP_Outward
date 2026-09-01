using System.Collections.Generic;
using System.Linq;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Graphs.Builders.Actions;
using OutwardArchipelago.Graphs.Builders.Conditions;
using OutwardArchipelago.Graphs.Builders.Nodes;

namespace OutwardArchipelago.Graphs.Patches
{
    internal class InsertOneTimeLocationCheckPatch : IGraphPatch
    {
        public INodeBuilder ReplaceNode { get; set; }

        public int ReplaceNodeID { set => ReplaceNode = new OriginalNodeBuilder { NodeID = value }; }

        public APWorld.Location Location { get; set; }

        public IReadOnlyList<IActionBuilder> OtherActions { get; set; } = new IActionBuilder[0];

        public IActionBuilder OtherAction { set => OtherActions = new IActionBuilder[] { value }; }

        public INodeBuilder NextNode { get; set; }

        public void ApplyPatch(IGraphPatchContext context)
        {
            new InsertNodePatch
            {
                ReplaceNode = ReplaceNode,
                NewNode = new ConditionNodeBuilder
                {
                    Condition = new LocationCheckConditionBuilder { Location = Location },
                    OnSuccess = ReplaceNode,
                    OnFailure = new ActionNodeBuilder
                    {
                        Actions = new IActionBuilder[] { new LocationCheckActionBuilder { Location = Location } }.Concat(OtherActions).ToList(),
                        NextNode = NextNode ?? new DescendantNodeBuilder { Node = ReplaceNode },
                    },
                },
            }.ApplyPatch(context);
        }
    }
}
