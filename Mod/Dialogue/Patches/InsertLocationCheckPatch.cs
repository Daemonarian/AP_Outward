using System;
using System.Collections.Generic;
using System.Linq;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Dialogue.Builders.Actions;
using OutwardArchipelago.Dialogue.Builders.Nodes;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class InsertLocationCheckPatch : IDialoguePatch
    {
        public INodeBuilder ReplaceNode { get; set; }

        public int ReplaceNodeID { set => ReplaceNode = new OriginalNodeBuilder { NodeID = value }; }

        public IReadOnlyList<APWorld.Location> Locations { get; set; }

        public APWorld.Location Location { set => Locations = new[] { value }; }

        public IReadOnlyList<IActionBuilder> OtherActions { get; set; } = new IActionBuilder[0];

        public IActionBuilder OtherAction { set => OtherActions = new IActionBuilder[] { value }; }

        public INodeBuilder NextNode { get; set; }

        public void ApplyPatch(IDialoguePatchContext context)
        {
            if (ReplaceNode is null)
            {
                throw new ArgumentNullException($"{nameof(InsertLocationCheckPatch)}.{nameof(ApplyPatch)}: '{nameof(ReplaceNode)}' should not be null");
            }

            if (Locations is null)
            {
                throw new ArgumentNullException($"{nameof(InsertLocationCheckPatch)}.{nameof(ApplyPatch)}: '{nameof(Locations)}' should not be null");
            }

            if (OtherActions is null)
            {
                throw new ArgumentNullException($"{nameof(InsertLocationCheckPatch)}.{nameof(ApplyPatch)}: '{nameof(OtherActions)}' should not be null");
            }

            new InsertNodePatch
            {
                ReplaceNode = ReplaceNode,
                NewNode = new ActionNodeBuilder
                {
                    Actions = Locations.Select(loc => new LocationCheckActionBuilder { Location = loc }).Concat(OtherActions).ToList(),
                    NextNode = NextNode ?? new DescendantNodeBuilder { Node = ReplaceNode },
                }
            }.ApplyPatch(context);
        }
    }
}
