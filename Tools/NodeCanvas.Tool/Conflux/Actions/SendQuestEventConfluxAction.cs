using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework.Actions;
using NodeCanvas.Tool.Schema.Records;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Actions
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
                QuestEventRef = new Schema.QuestEventReference
                {
                    EventUID = QuestEvent.ByKey[Key].UID,
                },
                StackAmount = Count,
                IgnoreNetworkSync = IgnoreNetworkSync,
            };
        }
    }
}
