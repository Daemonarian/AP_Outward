using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Conditions
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
