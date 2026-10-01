using System.Runtime.Serialization;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Connections;
using Conflux.NodeCanvas.DerivedDatas;
using Conflux.NodeCanvas.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.NodeCanvas
{
    internal class Graph
    {
        [JsonProperty("version")]
        public float Version { get; set; } = 2.92f;

        [JsonProperty("type")]
        public GraphType Type { get; set; } = GraphType.DialogueTree;

        [JsonProperty("nodes")]
        public List<Node> Nodes { get; set; } = [];

        [JsonProperty("connections")]
        public List<Connection> Connections { get; set; } = [];

        [JsonProperty("localBlackboard")]
        public BlackboardSource LocalBlackboard { get; set; } = new();

        [JsonProperty("DerivedData")]
        public DerivedData DerivedData { get; set; } = new();

        [JsonConverter(typeof(StringEnumConverter))]
        public enum GraphType
        {
            [EnumMember(Value = "NodeCanvas.DialogueTrees.DialogueTreeExt")]
            DialogueTree,

            [EnumMember(Value = "NodeCanvas.BehaviourTrees.BehaviourTree")]
            BehaviourTree
        }
    }
}
