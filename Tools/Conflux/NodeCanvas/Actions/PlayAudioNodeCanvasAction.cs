using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.PlayAudioAtPosition")]
    [GraphVizObject(Alias = "PlayAudio")]
    internal class PlayAudioNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("audioClip")]
        [GraphVizProperty(IsMainProperty = true)]
        public BBParameter<UnityObject>? AudioClip { get; set; }

        [JsonProperty("volume")]
        [DefaultValue(1f)]
        public float Volume { get; set; } = 1f;

        [JsonProperty("waitActionFinish")]
        [DefaultValue(false)]
        public bool WaitActionFinish { get; set; } = false;

        public override PlayAudioConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            AudioClip = AudioClip?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            Volume = Volume,
            WaitActionFinish = WaitActionFinish,
        };
    }
}
