using Conflux.NodeCanvas.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("or")]
    internal class OrConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "conditions")]
        public List<ConfluxCondition> Conditions { get; set; } = [];

        public override ConditionList BuildCondition(NodeCanvasGraphContext context) => new()
        {
            CheckMode = ConditionList.ConditionsCheckMode.AnyTrueSuffice,
            Conditions = [.. Conditions.Select(c => c.BuildCondition(context))],
        };
    }
}
