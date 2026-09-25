using Conflux.NodeCanvas.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("not")]
    internal class NotConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "condition")]
        public ConfluxCondition? Condition { get; set; }

        public override ConditionTask BuildCondition(INodeCanvasGraphContext context)
        {
            if (Condition is null)
            {
                throw new Exception($"Condition must not be null.");
            }

            var condition = Condition.BuildCondition(context);
            condition.Invert = !condition.Invert;
            return condition;
        }
    }
}
