using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.References;
using Conflux.Outward;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("sendQuestEvent")]
    internal class SendQuestEventConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "event")]
        public string Key { get; set; } = string.Empty;

        [YamlMember(Alias = "count")]
        public int Count { get; set; } = 1;

        [YamlMember(Alias = "ignoreNetworkSync")]
        public bool IgnoreNetworkSync { get; set; } = false;

        public override ActionTask BuildAction(INodeCanvasGraphContext context)
        {
            return new SendQuestEvent
            {
                QuestEventRef = new QuestEventReference
                {
                    EventUID = QuestEvent.ByKey[Key].UID,
                },
                StackAmount = Count,
                IgnoreNetworkSync = IgnoreNetworkSync,
            };
        }
    }
}
