using System.Text;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Serialization;
using Conflux.Outward;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.StartMusicEvent")]
    internal class MusicNodeCanvasAction : ActionTask
    {
        [JsonProperty("Stop")]
        public BBParameter<bool>? Stop { get; set; }

        [JsonProperty("Music")]
        public BBParameter<OutwardAudio.Sounds>? Music { get; set; }

        public override string GetGraphVizShortName() => "Music";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();
            content.AppendLine();

            if (Music is not null)
            {
                content.AppendLine($"Music: {Music.ToGraphVizLabel()}.");
            }

            if (Stop is not null)
            {
                content.AppendLine($"Stop: {Stop.ToGraphVizLabel()}.");
            }

            return content.ToString().TrimEnd();
        }

        public override MusicConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Music = Music?.BuildConfluxBlackboardVariableReference(graph),
            Stop = Stop?.BuildConfluxBlackboardVariableReference(graph),
        };
    }
}
