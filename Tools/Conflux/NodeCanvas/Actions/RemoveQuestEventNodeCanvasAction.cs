using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.RemoveQuestEvent")]
    [GraphVizObject(Alias = "RemoveQuestEvent")]
    internal class RemoveQuestEventNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("QuestEventRef")]
        [GraphVizProperty(IsMainProperty = true)]
        public QuestEventReference? QuestEvent { get; set; }

        [JsonProperty("RemoveAllStack")]
        [DefaultValue(false)]
        public bool RemoveAllStack { get; set; } = false;

        [JsonProperty("StackDecreaseAmount")]
        [DefaultValue(1)]
        public int StackDecreaseAmount { get; set; } = 1;

        public override RemoveQuestEventConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            QuestEvent = QuestEvent?.BuildConfluxObject(graph),
            RemoveAllStack = RemoveAllStack,
            StackDecreaseAmount = StackDecreaseAmount,
        };
    }
}
