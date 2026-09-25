using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Schema
{
    internal class GraphReplacementTemplate
    {
        [JsonProperty("replace")]
        public List<string> Routes { get; set; } = [];

        [JsonProperty("graph")]
        public GraphSerializationData Graph { get; set; } = new();
    }
}
