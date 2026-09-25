using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework.Internal;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework
{
    internal class GraphSerializationData
    {
        [JsonProperty("version")]
        public float Version { get; set; } = 2.92f;

        [JsonProperty("type")]
        public string Type { get; set; } = "NodeCanvas.DialogueTrees.DialogueTreeExt";

        [JsonProperty("nodes")]
        public List<Node> Nodes { get; set; } = [];

        [JsonProperty("connections")]
        public List<Connection> Connections { get; set; } = [];

        [JsonProperty("localBlackboard")]
        public BlackboardSource LocalBlackboard { get; set; } = new();

        [JsonProperty("DerivedData")]
        public DerivedSerializationData DerivedData { get; set; } = new();
    }
}
