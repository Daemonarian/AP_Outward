using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.References;
using Conflux.Outward;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("hasQuestEvent")]
    internal class HasQuestEventConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "event")]
        public string Event { get; set; } = string.Empty;

        [YamlMember(Alias = "count")]
        public int Count { get; set; } = 1;

        public override ConditionTask BuildCondition(INodeCanvasGraphContext context)
        {
            return new Condition_QuestEventOccured
            {
                QuestEventRef = new QuestEventReference
                {
                    EventUID = QuestEvent.ByKey[Event].UID,
                },
                MinStack = Count,
            };
        }
    }
}
