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
    [GraphVizObject(Alias = "PlaySound")]
    internal class PlaySoundNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("Sound")]
        [GraphVizProperty(IsMainProperty = true)]
        public OutwardAudio.Sounds Sound { get; set; }

        [JsonProperty("PlayAt")]
        public PositionType PlayAt { get; set; }

        [JsonProperty("TransPos")]
        public BBParameter<UnityObject>? Transform { get; set; }

        [JsonProperty("Pos")]
        public NodeCanvasVector3? Position { get; set; }

        public override PlaySoundConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Sound = Sound,
            Transform = PlayAt == PositionType.Transform ? Transform?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph) : null,
            Position = PlayAt == PositionType.Pos ? Position?.BuildConfluxObject(graph) : null,
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
