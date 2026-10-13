using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Conditions;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_KnowQuest")]
    [GraphVizObject(Alias = "KnowQuest")]
    internal class Condition_KnowQuest : NodeCanvasCondition
    {
        [JsonProperty("quest")]
        [GraphVizProperty(IsMainProperty = true)]
        public BBParameter<UnityObject>? Quest { get; set; }

        protected override KnowQuestConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            QuestObject = Quest?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph) ?? throw new ConfluxValueException("Quest should not be null."),
        };
    }
}
