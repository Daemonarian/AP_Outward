using Conflux.NodeCanvas.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.References;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("knowQuest")]
    internal class KnowQuestConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "quest")]
        public ConfluxQuestReference Quest { get; init; } = new();

        public override Condition_CustomKnowQuest BuildCondition(NodeCanvasGraphContext context) => new()
        {
            Quest = Quest.BuildNodeCanvasObject(context),
        };
    }
}
