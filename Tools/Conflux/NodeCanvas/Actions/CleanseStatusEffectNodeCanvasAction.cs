using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.CleanseStatusEffect")]
    [GraphVizObject(Alias = "Cleanse")]
    internal class CleanseStatusEffectNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("Target")]
        public BBParameter<UnityObject>? Target { get; set; }

        [JsonProperty("StatusEffect")]
        public BBParameter<UnityObject>? StatusEffect { get; set; }

        public override CleanseStatusEffectConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Target = Target?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            StatusEffect = StatusEffect?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
        };
    }
}
