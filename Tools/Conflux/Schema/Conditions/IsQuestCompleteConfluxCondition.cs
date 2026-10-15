using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("isQuestComplete")]
    internal class IsQuestCompleteConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "quest")]
        public ConfluxBlackboardVariableReference<ConfluxQuestReference> Quest { get; set; } = new();

        public override IsQuestCompletedNodeCanvasAction BuildCondition(NodeCanvasGraphContext context) => new()
        {
            Quest = Quest.BuildBBParameter<QuestReference>(context),
        };
    }
}
