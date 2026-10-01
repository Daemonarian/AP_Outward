using Conflux.NodeCanvas.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("and")]
    internal class AndConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "conditions")]
        public List<ConfluxCondition> Conditions { get; set; } = [];

        public override ConditionList BuildCondition(NodeCanvasGraphContext context) => new()
        {
            CheckMode = ConditionList.ConditionsCheckMode.AllTrueRequired,
            Conditions = [.. Conditions.Select(c => c.BuildCondition(context))],
        };
    }
}
