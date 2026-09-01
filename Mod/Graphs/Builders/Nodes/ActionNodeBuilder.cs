using System;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Graphs.Builders.Actions;

namespace OutwardArchipelago.Graphs.Builders.Nodes
{
    internal class ActionNodeBuilder : INodeBuilder
    {
        public ActionList.ActionsExecutionMode ExecutionMode { get; set; } = ActionList.ActionsExecutionMode.ActionsRunInSequence;

        public IReadOnlyList<IActionBuilder> Actions { get; set; }

        public IActionBuilder Action { set => Actions = new IActionBuilder[] { value }; }

        public INodeBuilder NextNode { get; set; }

        public Node BuildNode(IGraphPatchContext context)
        {
            var node = context.Graph.AddNode<ActionNode>();

            if (Actions.Count == 1)
            {
                node.action = Actions[0].BuildAction(context);
            }
            else if (Actions.Count > 1)
            {
                node.action = new ActionList
                {
                    executionMode = ExecutionMode,
                    actions = Actions.Select(a => a.BuildAction(context)).ToList(),
                };
            }
            else
            {
                throw new InvalidOperationException("cannot create an action node with no action");
            }

            var nextNode = NextNode?.BuildNode(context);
            if (nextNode != null)
            {
                Connection.Create(node, nextNode);
            }

            return node;
        }
    }
}
