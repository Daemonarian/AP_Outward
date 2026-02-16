using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Builders.Nodes
{
    internal class DescendantNodeBuilder : INodeBuilder
    {
        public INodeBuilder Node { get; set; }

        public int NodeID { set => Node = new OriginalNodeBuilder { NodeID = value }; }

        public IReadOnlyList<int> ChildIndices { get; set; } = new int[] { 0 };

        public INodeBuilder DefaultNode { get; set; } = new FinishNodeBuilder();

        public Node BuildNode(IDialoguePatchContext context)
        {
            var node = Node.BuildNode(context);
            foreach (var i in ChildIndices)
            {
                if (node == null)
                {
                    break;
                }

                node = node.outConnections?.ElementAtOrDefault(i)?.targetNode;
            }

            return node ?? DefaultNode.BuildNode(context);
        }
    }
}
