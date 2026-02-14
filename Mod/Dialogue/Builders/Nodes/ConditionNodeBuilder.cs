using System;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Builders.Conditions;

namespace OutwardArchipelago.Dialogue.Builders.Nodes
{
    internal class ConditionNodeBuilder : INodeBuilder
    {
        public IReadOnlyList<IConditionBuilder> Conditions { get; set; } = new IConditionBuilder[0];

        public IConditionBuilder Condition { set => Conditions = new IConditionBuilder[] { value }; }

        public ConditionList.ConditionsCheckMode CheckMode { get; set; } = ConditionList.ConditionsCheckMode.AllTrueRequired;

        public INodeBuilder OnSuccess { get; set; }

        public INodeBuilder OnFailure { get; set; }

        public Node BuildNode(IDialoguePatchContext context)
        {
            ConditionTask condition;
            if (Conditions.Count <= 0)
            {
                throw new ArgumentException("tried to build a condition node with no conditions");
            }
            else if (Conditions.Count == 1)
            {
                condition = Conditions[0].BuildCondition(context);
            }
            else
            {
                condition = new ConditionList
                {
                    checkMode = CheckMode,
                    conditions = Conditions.Select(b => b.BuildCondition(context)).ToList(),
                };
            }

            var node = context.Tree.AddNode<ConditionNode>();
            node.condition = condition;
            Connection.Create(node, OnSuccess.BuildNode(context));
            Connection.Create(node, OnFailure.BuildNode(context));
            return node;
        }
    }
}
