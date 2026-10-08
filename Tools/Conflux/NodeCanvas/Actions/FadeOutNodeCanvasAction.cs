using System.ComponentModel;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.FadeOut")]
    internal class FadeOutNodeCanvasAction : ActionTask
    {
        [JsonProperty("fadeTime")]
        [DefaultValue(1f)]
        public float FadeTime { get; set; } = 1f;

        public override string GetGraphVizShortName() => "FadeOut";

        public override string GetGraphVizContent() => FadeTime == 1f ? "" : $"{FadeTime} seconds";

        public override FadeOutConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Duration = FadeTime,
        };
    }
}
