using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Builders.Nodes
{
    internal class OriginalNodeBuilder : INodeBuilder
    {
        public int NodeID { get; set; } = -1;

        public Node BuildNode(IDialoguePatchContext context) => context.NodesByID[NodeID];
    }
}
