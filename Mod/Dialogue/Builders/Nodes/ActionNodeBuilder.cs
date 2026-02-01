using System;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Builders.Actions;

namespace OutwardArchipelago.Dialogue.Builders.Nodes
{
    internal class ActionNodeBuilder : INodeBuilder
    {
        public ActionList.ActionsExecutionMode ExecutionMode { get; set; } = ActionList.ActionsExecutionMode.ActionsRunInSequence;

        public IReadOnlyList<IActionBuilder> Actions { get; set; }

        public IActionBuilder Action { set => Actions = new IActionBuilder[] { value }; }

        public INodeBuilder NextNode { get; set; }

        public Node BuildNode(IDialoguePatchContext context)
        {
            var node = context.Tree.AddNode<ActionNode>();

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
