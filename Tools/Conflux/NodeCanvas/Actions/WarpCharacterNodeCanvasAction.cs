using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.WarpCharacter")]
    [GraphVizObject(Alias = "WarpCharacter")]
    internal class WarpCharacterNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("AllPlayers")]
        public BBParameter<bool>? AllPlayers { get; set; }

        [JsonProperty("TurnCamera")]
        public BBParameter<bool>? TurnCamera { get; set; }

        [JsonProperty("Target")]
        public BBParameter<UnityObject>? Target { get; set; }

        [JsonProperty("TargetTrans")]
        public BBParameter<UnityObject>? TargetTransform { get; set; }

        [JsonProperty("m_targetUID")]
        public string? TargetUID { get; set; }

        public override WarpConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            AllPlayers = AllPlayers?.BuildConfluxBlackboardVariableReference(graph),
            TurnCamera = TurnCamera?.BuildConfluxBlackboardVariableReference(graph),
            Target = Target?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            TargetTransform = TargetTransform?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            TargetUID = TargetUID,
        };
    }
}
