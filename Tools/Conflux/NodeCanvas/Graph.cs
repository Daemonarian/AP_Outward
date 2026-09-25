using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Connections;
using Conflux.NodeCanvas.DerivedData;
using Conflux.NodeCanvas.Nodes;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas
{
    internal class Graph
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
        public DerivedData.DerivedData DerivedData { get; set; } = new();
    }
}
