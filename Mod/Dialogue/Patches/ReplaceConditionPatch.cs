using System;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Builders.Conditions;
using OutwardArchipelago.Dialogue.Builders.Nodes;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class ReplaceConditionPatch : IDialoguePatch
    {
        public INodeBuilder Node { get; set; } = null;

        public IReadOnlyList<IConditionBuilder> Conditions { get; set; } = new IConditionBuilder[0];

        public IConditionBuilder Condition { set => Conditions = new IConditionBuilder[] { value }; }

        public ConditionList.ConditionsCheckMode CheckMode { get; set; } = ConditionList.ConditionsCheckMode.AllTrueRequired;

        public void ApplyPatch(IDialoguePatchContext context)
        {
            var node = Node.BuildNode(context);
            if (node is not ConditionNode conditionNode)
            {
                throw new ArgumentException($"cannot replace condition on node because it is not a condition node: {node}");
            }

            ConditionTask condition;
            if (Conditions.Count <= 0)
            {
                condition = null;
            }
            else if (Conditions.Count == 1)
            {
                condition = Conditions[0].BuildCondition(context);
            }
            else
            {
                condition = new ConditionList
                {
                    conditions = Conditions.Select(b => b.BuildCondition(context)).ToList(),
                    checkMode = CheckMode,
                };
            }

            conditionNode.condition = condition;
        }
    }
}
