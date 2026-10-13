using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.FadeIn")]
    [GraphVizObject(Alias = "FadeIn")]
    internal class FadeInNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("fadeTime")]
        [DefaultValue(1f)]
        public float FadeTime { get; set; } = 1f;

        public override FadeInConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Duration = FadeTime,
        };
    }
}
