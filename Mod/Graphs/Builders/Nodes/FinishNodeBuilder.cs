using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using ParadoxNotion;

namespace OutwardArchipelago.Graphs.Builders.Nodes
{
    internal class FinishNodeBuilder : INodeBuilder
    {
        public CompactStatus FinishState { get; set; } = CompactStatus.Success;

        public Node BuildNode(IGraphPatchContext context)
        {
            var node = context.Graph.AddNode<FinishNode>();
            node.finishState = FinishState;
            return node;
        }
    }
}
