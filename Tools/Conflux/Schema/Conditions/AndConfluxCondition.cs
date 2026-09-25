using Conflux.NodeCanvas.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("and")]
    internal class AndConfluxCondition : ConfluxCondition
    {
        [YamlMember(Alias = "conditions")]
        [ConfluxMainProperty]
        public List<ConfluxCondition> Conditions { get; set; } = [];

        public override ConditionTask BuildCondition(INodeCanvasGraphContext context)
        {
            var conditions = Conditions.Select(c => c.BuildCondition(context)).ToList();
            return new ConditionList
            {
                CheckMode = ConditionList.ConditionsCheckMode.AllTrueRequired,
                Conditions = conditions,
            };
        }
    }
}
