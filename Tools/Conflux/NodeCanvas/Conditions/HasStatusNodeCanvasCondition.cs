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
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_HasStatus")]
    [GraphVizObject(Alias = "HasStatus")]
    internal class HasStatusNodeCanvasCondition : NodeCanvasCondition
    {
        [JsonProperty("character")]
        public BBParameter<UnityObject>? Character { get; set; }

        [JsonProperty("statuses")]
        public BBParameter<NodeCanvasList<UnityObject, ConfluxUnityObjectReference>>? Statuses { get; set; }

        protected override HasStatusConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            Character = Character?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            Statuses = Statuses?.BuildConfluxBlackboardVariableReference<ConfluxList<UnityObject, ConfluxUnityObjectReference>>(graph),
        };
    }
}
