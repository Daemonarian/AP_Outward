using NodeCanvas.Framework;

namespace OutwardArchipelago.Graphs.Builders.Nodes
{
    internal class OriginalNodeBuilder : INodeBuilder
    {
        public int NodeID { get; set; } = -1;

        public Node BuildNode(IGraphPatchContext context) => context.NodesByID[NodeID];
    }
}
