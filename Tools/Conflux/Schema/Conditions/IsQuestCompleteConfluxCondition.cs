using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.References;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("isQuestComplete")]
    internal class IsQuestCompleteConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "quest")]
        public ConfluxBlackboardVariableReference<QuestReference, ConfluxQuestReference> Quest { get; set; } = new();

        public override Condition_IsQuestCompleted BuildCondition(NodeCanvasGraphContext context) => new()
        {
            QuestRef = Quest.BuildBBParameter(context),
        };
    }
}
