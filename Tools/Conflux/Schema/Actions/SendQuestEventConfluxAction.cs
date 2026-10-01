using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.References;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("sendQuestEvent")]
    internal class SendQuestEventConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "event")]
        public ConfluxQuestEventReference QuestEvent { get; set; } = new();

        [YamlMember(Alias = "count")]
        public int Count { get; set; } = 1;

        [YamlMember(Alias = "ignoreNetworkSync")]
        public bool IgnoreNetworkSync { get; set; } = false;

        public override SendQuestEvent BuildAction(NodeCanvasGraphContext context) => new()
        {
            QuestEventRef = QuestEvent.BuildNodeCanvasObject(context),
            StackAmount = Count,
            IgnoreNetworkSync = IgnoreNetworkSync,
        };
    }
}
