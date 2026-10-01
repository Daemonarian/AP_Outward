using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Conditions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_KnowQuest")]
    internal class Condition_KnowQuest : ConditionTask
    {
        [JsonProperty("quest")]
        public BBParameter<UnityObject>? Quest { get; set; }

        public override string GetGraphVizShortName() => "KnowQuest";

        public override string GetGraphVizContent() => Quest?.ToGraphVizLabel() ?? string.Empty;

        public override KnowQuestConfluxCondition BuildConfluxCondition(Graph graph) => new()
        {
            Quest = new()
            {
                Key = "UNKNOWN",
            },
        };
    }
}
