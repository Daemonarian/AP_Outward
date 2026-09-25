using Newtonsoft.Json;

namespace Conflux.NodeCanvas.References
{
    internal class APWorldLocationReference
    {
        [JsonProperty("_key")]
        public string? Key { get; set; }

        public virtual string ToGraphVizLabel() => Key ?? string.Empty;
    }
}
