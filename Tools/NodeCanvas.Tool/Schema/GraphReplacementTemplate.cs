using System.Text.Json.Serialization;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Schema
{
    internal class GraphReplacementTemplate
    {
        [JsonPropertyName("routes")]
        public List<string> Routes { get; set; } = [];

        [JsonPropertyName("graph")]
        public GraphSerializationData Graph { get; set; } = new();
    }
}
