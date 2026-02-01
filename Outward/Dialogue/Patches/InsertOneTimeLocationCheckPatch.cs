using System.Collections.Generic;
using System.Linq;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Dialogue.Builders.Actions;
using OutwardArchipelago.Dialogue.Builders.Conditions;
using OutwardArchipelago.Dialogue.Builders.Nodes;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class InsertOneTimeLocationCheckPatch : IDialoguePatch
    {
        public int ReplaceNodeID { get; set; }

        public APWorld.Location Location { get; set; }

        public IReadOnlyList<IActionBuilder> OtherActions { get; set; } = new IActionBuilder[0];

        public IActionBuilder OtherAction { set => OtherActions = new IActionBuilder[] { value }; }

        public INodeBuilder NextNode { get; set; }

        public void ApplyPatch(IDialoguePatchContext context)
        {
            new InsertNodePatch
            {
                ReplaceNodeID = ReplaceNodeID,
                Node = new ConditionNodeBuilder
                {
                    Condition = new LocationCheckConditionBuilder { Location = Location },
                    OnSuccess = new OriginalNodeBuilder { NodeID = ReplaceNodeID },
                    OnFailure = new ActionNodeBuilder
                    {
                        Actions = new IActionBuilder[] { new LocationCheckActionBuilder { Location = Location } }.Concat(OtherActions).ToList(),
                        NextNode = NextNode ?? new ChildOriginalNodeBuilder { NodeID = ReplaceNodeID },
                    },
                },
            }.ApplyPatch(context);
        }
    }
}
