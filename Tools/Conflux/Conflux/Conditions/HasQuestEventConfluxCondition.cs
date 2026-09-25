using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework.Conditions;
using NodeCanvas.Tool.Schema.Records;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Conditions
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
                QuestEventRef = new Schema.QuestEventReference
                {
                    EventUID = QuestEvent.ByKey[Event].UID,
                },
                MinStack = Count,
            };
        }
    }
}
