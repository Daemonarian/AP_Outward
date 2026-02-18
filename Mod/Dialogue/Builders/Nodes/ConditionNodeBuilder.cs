using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Builders.Conditions;

namespace OutwardArchipelago.Dialogue.Builders.Nodes
{
    internal class ConditionNodeBuilder : INodeBuilder
    {
        public IConditionBuilder Condition { get; set; }

        public INodeBuilder OnSuccess { get; set; }

        public INodeBuilder OnFailure { get; set; }

        public Node BuildNode(IDialoguePatchContext context)
        {
            var node = context.Tree.AddNode<ConditionNode>();
            node.condition = Condition.BuildCondition(context);
            Connection.Create(node, OnSuccess.BuildNode(context));
            Connection.Create(node, OnFailure.BuildNode(context));
            return node;
        }
    }
}
