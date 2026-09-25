using Newtonsoft.Json;

namespace Conflux.NodeCanvas
{
    internal class GraphTemplate
    {
        [JsonProperty("replace")]
        public List<string> Routes { get; set; } = [];

        [JsonProperty("graph")]
        public Graph Graph { get; set; } = new();
    }
}
