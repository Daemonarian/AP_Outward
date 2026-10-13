using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.SendQuestEvent")]
    [GraphVizObject(Alias = "SendQuestEvent")]
    internal class SendQuestEventNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("QuestEventRef")]
        [GraphVizProperty(IsMainProperty = true)]
        public QuestEventReference? QuestEvent { get; set; }

        [JsonProperty("StackAmount")]
        [DefaultValue(1)]
        public int StackAmount { get; set; } = 1;

        [JsonProperty("IgnoreNetworkSync")]
        [DefaultValue(false)]
        public bool IgnoreNetworkSync { get; set; } = false;

        public override SendQuestEventConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            QuestEvent = QuestEvent?.BuildConfluxObject(graph),
            Count = StackAmount,
            IgnoreNetworkSync = IgnoreNetworkSync,
        };
    }
}
