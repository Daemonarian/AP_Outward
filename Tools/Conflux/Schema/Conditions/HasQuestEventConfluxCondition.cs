using Conflux.NodeCanvas.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.References;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("hasQuestEvent")]
    internal class HasQuestEventConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "event")]
        public ConfluxQuestEventReference QuestEvent { get; set; } = new();

        [YamlMember(Alias = "count")]
        public int Count { get; set; } = 1;

        public override Condition_QuestEventOccured BuildCondition(NodeCanvasGraphContext context) => new()
        {
            QuestEventRef = QuestEvent.BuildNodeCanvasObject(context),
            MinStack = Count,
        };
    }
}
