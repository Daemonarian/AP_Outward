using System.ComponentModel;
using Conflux.NodeCanvas.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("hasQuestEvent")]
    internal class HasQuestEventConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "event")]
        public ConfluxQuestEventReference? QuestEvent { get; set; } = new();

        [YamlMember(Alias = "count")]
        [DefaultValue(1)]
        public int Count { get; set; } = 1;

        public override QuestEventOccuredNodeCanvasCondition BuildCondition(NodeCanvasGraphContext context) => new()
        {
            QuestEvent = QuestEvent?.BuildNodeCanvasObject(context),
            MinStack = Count,
        };
    }
}
