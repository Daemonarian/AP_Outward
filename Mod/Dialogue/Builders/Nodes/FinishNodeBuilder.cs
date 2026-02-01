using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using ParadoxNotion;

namespace OutwardArchipelago.Dialogue.Builders.Nodes
{
    internal class FinishNodeBuilder : INodeBuilder
    {
        public CompactStatus FinishState { get; set; } = CompactStatus.Success;

        public Node BuildNode(IDialoguePatchContext context)
        {
            var node = context.Tree.AddNode<FinishNode>();
            node.finishState = FinishState;
            return node;
        }
    }
}
