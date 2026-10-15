using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Conditions;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_HasStatusLevel")]
    [GraphVizObject(Alias = "HasStatusLevel")]
    internal class HasStatusLevelNodeCanvasCondition : NodeCanvasCondition
    {
        [JsonProperty("statuses")]
        public BBParameter<NodeCanvasList<UnityObject>>? Statuses { get; set; }

        [JsonProperty("minLevel")]
        public BBParameter<int>? MinLevel { get; set; }

        protected override HasStatusLevelConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            Statuses = Statuses?.BuildConfluxBlackboardVariableReference<ConfluxList<ConfluxUnityObjectReference>>(graph),
            MinLevel = MinLevel?.BuildConfluxBlackboardVariableReference(graph),
        };
    }
}
