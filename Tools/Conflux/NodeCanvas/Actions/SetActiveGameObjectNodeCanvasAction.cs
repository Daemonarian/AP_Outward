using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.SetActiveGameObject")]
    [GraphVizObject(Alias = "SetActiveGameObject")]
    internal class SetActiveGameObjectNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("Target")]
        [GraphVizProperty(IsMainProperty = true)]
        public BBParameter<UnityObject>? Target { get; set; }

        [JsonProperty("IsActive")]
        public BBParameter<bool>? IsActive { get; set; }

        public override SetActiveGameObjectConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Target = Target?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            IsActive = IsActive?.BuildConfluxBlackboardVariableReference(graph),
        };
    }
}
