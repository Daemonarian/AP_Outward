using System.ComponentModel;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.FadeIn")]
    internal class FadeInNodeCanvasAction : ActionTask
    {
        [JsonProperty("fadeTime")]
        [DefaultValue(1f)]
        public float FadeTime { get; set; } = 1f;

        public override string GetGraphVizShortName() => "FadeIn";

        public override string GetGraphVizContent() => FadeTime == 1f ? "" : $"{FadeTime} seconds";

        public override FadeInConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Duration = FadeTime,
        };
    }
}
