using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.FadeOut")]
    [GraphVizObject(Alias = "FadeOut")]
    internal class FadeOutNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("fadeTime")]
        [DefaultValue(1f)]
        public float FadeTime { get; set; } = 1f;

        public override FadeOutConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Duration = FadeTime,
        };
    }
}
