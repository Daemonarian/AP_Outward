using Conflux.NodeCanvas.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("not")]
    internal class NotConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty(Force = true)]
        [YamlMember(Alias = "condition")]
        public ConfluxCondition? Condition { get; set; }

        public override ConditionTask BuildCondition(NodeCanvasGraphContext context)
        {
            if (Condition is null)
            {
                throw new ConfluxValueException($"The value not.condition must be specified.");
            }

            var condition = Condition.BuildCondition(context);
            condition.Invert = !condition.Invert;
            return condition;
        }
    }
}
