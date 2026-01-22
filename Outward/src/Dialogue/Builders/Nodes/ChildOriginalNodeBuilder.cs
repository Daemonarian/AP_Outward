using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Builders.Nodes
{
    internal class ChildOriginalNodeBuilder : INodeBuilder
    {
        public int NodeID { get; set; }

        public IReadOnlyList<int> ChildIndices { get; set; } = new int[] { 0 };

        public INodeBuilder DefaultNode { get; set; } = new FinishNodeBuilder();

        public Node BuildNode(IDialoguePatchContext context)
        {
            var node = context.NodesByID[NodeID];
            foreach (var i in ChildIndices)
            {
                if (node == null)
                {
                    break;
                }

                node = node.outConnections?.ElementAtOrDefault(i)?.targetNode;
            }

            node ??= DefaultNode.BuildNode(context);

            return node;
        }
    }
}
