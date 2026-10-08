using System.ComponentModel;
using System.Text;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.RemoveQuestEvent")]
    internal class RemoveQuestEventNodeCanvasAction : ActionTask
    {
        [JsonProperty("QuestEventRef")]
        public QuestEventReference? QuestEventRef { get; set; }

        [JsonProperty("RemoveAllStack")]
        [DefaultValue(false)]
        public bool RemoveAllStack { get; set; } = false;

        [JsonProperty("StackDecreaseAmount")]
        [DefaultValue(1)]
        public int StackDecreaseAmount { get; set; } = 1;

        public override string GetGraphVizShortName() => "RemoveQuestEvent";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            content.AppendLine(QuestEventRef?.ToGraphVizLabel());

            if (RemoveAllStack)
            {
                content.AppendLine($"RemoveAll: yes");
            }

            if (StackDecreaseAmount != 1)
            {
                content.AppendLine($"Count: {StackDecreaseAmount}");
            }

            return content.ToString().TrimEnd();
        }

        public override RemoveQuestEventConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            QuestEvent = QuestEventRef?.BuildConfluxObject(graph),
            RemoveAllStack = RemoveAllStack,
            StackDecreaseAmount = StackDecreaseAmount,
        };
    }
}
