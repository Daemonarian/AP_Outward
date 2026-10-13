using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Serialization;
using Conflux.Outward;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.StartMusicEvent")]
    [GraphVizObject(Alias = "Music")]
    internal class MusicNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("Stop")]
        public BBParameter<bool>? Stop { get; set; }

        [JsonProperty("Music")]
        public BBParameter<OutwardAudio.Sounds>? Music { get; set; }

        public override MusicConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Music = Music?.BuildConfluxBlackboardVariableReference(graph),
            Stop = Stop?.BuildConfluxBlackboardVariableReference(graph),
        };
    }
}
