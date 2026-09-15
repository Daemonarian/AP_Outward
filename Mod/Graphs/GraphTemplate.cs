using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OutwardArchipelago.Graphs
{
    internal class GraphTemplate
    {
        [JsonProperty("replace")]
        public List<string> PathsToReplace { get; set; } = new();

        [JsonProperty("graph")]
        public JRaw Graph { get; set; }
    }
}
