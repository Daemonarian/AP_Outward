using System.Text.Json.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework
{
    internal class GraphSerializationData
    {
        [JsonPropertyName("version")]
        public float Version { get; set; } = 2.92f;

        [JsonPropertyName("type")]
        public string Type { get; set; } = "NodeCanvas.DialogueTrees.DialogueTreeExt";

        [JsonPropertyName("nodes")]
        public List<Node> Nodes { get; set; } = [];

        [JsonPropertyName("connections")]
        public List<Connection> Connections { get; set; } = [];
    }
}
