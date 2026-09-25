using Conflux.NodeCanvas.Nodes;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Connections
{
    internal class Connection
    {
        [JsonProperty("_sourceNode")]
        public Node? SourceNode { get; set; }

        [JsonProperty("_targetNode")]
        public Node? TargetNode { get; set; }

        [JsonProperty("_isDisabled")]
        public bool IsDisabled { get; set; }
    }
}
