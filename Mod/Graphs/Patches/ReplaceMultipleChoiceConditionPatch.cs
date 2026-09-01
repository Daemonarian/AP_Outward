using System;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Graphs.Builders.Conditions;
using OutwardArchipelago.Graphs.Builders.Nodes;

namespace OutwardArchipelago.Graphs.Patches
{
    internal class ReplaceMultipleChoiceConditionPatch : IGraphPatch
    {
        public INodeBuilder Node { get; set; } = null;

        public int ChoiceIndex { get; set; } = -1;

        public IReadOnlyList<IConditionBuilder> Conditions { get; set; } = new IConditionBuilder[0];

        public IConditionBuilder Condition { set => Conditions = new IConditionBuilder[] { value }; }

        public ConditionList.ConditionsCheckMode CheckMode { get; set; } = ConditionList.ConditionsCheckMode.AllTrueRequired;

        public void ApplyPatch(IGraphPatchContext context)
        {
            var node = Node.BuildNode(context);
            if (node is not MultipleChoiceNodeExt mcNode)
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

            mcNode.availableChoices[ChoiceIndex].condition = condition;
        }
    }
}
