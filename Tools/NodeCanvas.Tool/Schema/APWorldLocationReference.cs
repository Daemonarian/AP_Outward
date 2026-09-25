using Newtonsoft.Json;

namespace NodeCanvas.Tool.Schema
{
    internal class APWorldLocationReference
    {
        [JsonProperty("_key")]
        public string? Key { get; set; }

        public virtual string ToGraphVizLabel() => Key ?? string.Empty;
    }
}
