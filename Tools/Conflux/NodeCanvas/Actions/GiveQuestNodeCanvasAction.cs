using System.Text;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.GiveQuest")]
    internal class GiveQuestNodeCanvasAction : ActionTask
    {
        [JsonProperty("quest")]
        public BBParameter<UnityObject>? Quest { get; set; }

        [JsonProperty("questRef")]
        public BBParameter<QuestReference>? QuestRef { get; set; }

        public override string GetGraphVizShortName() => "GiveQuest";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();
            content.AppendLine();

            if (Quest is not null)
            {
                content.AppendLine($"Quest: {GraphVizConverter.IndentLines(Quest.ToGraphVizLabel())}");
            }

            if (QuestRef is not null)
            {
                content.AppendLine($"QuestRef: {GraphVizConverter.IndentLines(QuestRef.ToGraphVizLabel())}");
            }

            return content.ToString().TrimEnd();
        }

        public override GiveQuestConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            QuestObject = Quest?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            Quest = QuestRef?.BuildConfluxBlackboardVariableReference<ConfluxQuestReference>(graph),
        };
    }
}
