using System.ComponentModel;
using Conflux.NodeCanvas.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    internal class IsQuestEventExpiredConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "event")]
        public ConfluxQuestEventReference QuestEvent { get; set; } = new();

        [YamlMember(Alias = "expiryTime")]
        [DefaultValue(1)]
        public int ExpiryTime { get; set; } = 1;

        public override QuestEventExpiredCondition BuildCondition(NodeCanvasGraphContext context) => new()
        {
            QuestEvent = QuestEvent.BuildNodeCanvasObject(context),
            ExpiryTime = ExpiryTime,
        };
    }
}
