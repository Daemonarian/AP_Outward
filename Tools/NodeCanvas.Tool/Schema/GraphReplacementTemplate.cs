using System.Text.Json.Serialization;
using NodeCanvasRenderer.Schema.NodeCanvas.Framework;

namespace NodeCanvasRenderer.Schema
{
    internal class GraphReplacementTemplate
    {
        [JsonPropertyName("routes")]
        public List<string> Routes { get; set; } = [];

        [JsonPropertyName("graph")]
        public GraphSerializationData Graph { get; set; } = new();
    }
}
