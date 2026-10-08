using System.Runtime.Serialization;
using System.Text;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Outward;
using Conflux.Schema.Actions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.PlaySound")]
    internal class PlaySoundNodeCanvasAction : ActionTask
    {
        [JsonProperty("Sound")]
        public OutwardAudio.Sounds Sound { get; set; }

        [JsonProperty("PlayAt")]
        public PositionType PlayAt { get; set; }

        public BBParameter<UnityObject>? TransPos { get; set; }

        public NodeCanvasVector3? Pos { get; set; }

        public override string GetGraphVizShortName() => "PlaySound";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            content.AppendLine($"Sound: {Sound}");

            if (PlayAt == PositionType.Transform)
            {
                content.AppendLine($"Transform: {GraphVizConverter.IndentLines(TransPos?.ToGraphVizLabel() ?? "unknown")}");
            }
            else if (PlayAt == PositionType.Pos)
            {
                content.AppendLine($"Position: {GraphVizConverter.IndentLines(Pos?.ToGraphVizLabel() ?? "unknown")}");
            }

            return content.ToString().TrimEnd();
        }

        public override PlaySoundConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Sound = Sound,
            Transform = PlayAt == PositionType.Transform ? TransPos?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph) : null,
            Position = PlayAt == PositionType.Pos ? Pos?.BuildConfluxObject(graph) : null,
        };

        [JsonConverter(typeof(StringRuneEnumerator))]
        public enum PositionType
        {
            [EnumMember(Value = "Camera")]
            Camera,

            [EnumMember(Value = "Transform")]
            Transform,

            [EnumMember(Value = "Pos")]
            Pos,
        }
    }
}
