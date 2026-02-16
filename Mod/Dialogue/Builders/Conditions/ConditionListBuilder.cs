using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Builders.Conditions
{
    /// <summary>
    /// Build a complex condition with logical ands or ors.
    /// </summary>
    internal class ConditionListBuilder : IConditionBuilder
    {
        /// <summary>
        /// Mode to use to check the overall success.
        /// </summary>
        public ConditionList.ConditionsCheckMode CheckMode { get; set; } = ConditionList.ConditionsCheckMode.AllTrueRequired;

        /// <summary>
        /// The list of conditions to check.
        /// </summary>
        public IReadOnlyList<IConditionBuilder> Conditions { get; set; } = new List<IConditionBuilder>();

        /// <summary>
        /// The singular condition to check.
        /// Can be used in place of <see cref="Conditions"/>.
        /// </summary>
        public IConditionBuilder Condition { set => Conditions = new List<IConditionBuilder> { value }; }

        /// <summary>
        /// Whether trivial conditions lists should be simplified to singular conditions.
        /// </summary>
        public bool DoSimplifyCondition { get; set; } = true;

        public ConditionTask BuildCondition(IDialoguePatchContext context)
        {
            var conditions = (from cb in Conditions
                              select cb.BuildCondition(context)).ToList();

            ConditionTask condition;
            if (DoSimplifyCondition && conditions.Count == 1)
            {
                condition = conditions[0];
            }
            else
            {
                condition = new ConditionList
                {
                    checkMode = CheckMode,
                    conditions = conditions,
                };
            }

            return condition;
        }
    }
}
