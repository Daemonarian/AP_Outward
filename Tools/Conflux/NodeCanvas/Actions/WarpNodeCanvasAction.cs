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
    [NodeCanvasType("NodeCanvas.Tasks.Actions.WarpCharacter")]
    internal class WarpNodeCanvasAction : ActionTask
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

        public override string GetGraphVizShortName() => "Warp";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            if (AllPlayers is not null)
            {
                content.AppendLine($"AllPlayers: {GraphVizConverter.IndentLines(AllPlayers.ToGraphVizLabel())}");
            }

            if (TurnCamera is not null)
            {
                content.AppendLine($"TurnCamera: {GraphVizConverter.IndentLines(TurnCamera.ToGraphVizLabel())}");
            }

            if (Target is not null)
            {
                content.AppendLine($"Target: {GraphVizConverter.IndentLines(Target.ToGraphVizLabel())}");
            }

            if (TargetTransform is not null)
            {
                content.AppendLine($"TargetTransform: {GraphVizConverter.IndentLines(TargetTransform.ToGraphVizLabel())}");
            }

            if (TargetUID is not null)
            {
                content.AppendLine($"TargetUID: {GraphVizConverter.IndentLines(TargetUID)}");
            }

            return content.ToString().TrimEnd();
        }

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
