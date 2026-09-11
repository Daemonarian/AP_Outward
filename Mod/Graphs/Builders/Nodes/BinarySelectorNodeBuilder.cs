using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Graphs.Builders.Conditions;

namespace OutwardArchipelago.Graphs.Builders.Nodes
{
    internal class BinarySelectorNodeBuilder : INodeBuilder
    {
        public IConditionBuilder Condition { get; set; }

        public INodeBuilder OnSuccess { get; set; }

        public INodeBuilder OnFailure { get; set; }

        public Node BuildNode(IGraphPatchContext context)
        {
            var node = context.Graph.AddNode<BinarySelector>();
            node.condition = Condition.BuildCondition(context);
            Connection.Create(node, OnSuccess.BuildNode(context));
            Connection.Create(node, OnFailure.BuildNode(context));
            return node;
        }
    }
}
