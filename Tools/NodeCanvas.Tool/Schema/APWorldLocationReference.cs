using System.Text.Json.Serialization;

namespace NodeCanvasRenderer.Schema
{
    internal class APWorldLocationReference
    {
        [JsonPropertyName("_key")]
        public string? Key { get; set; }

        public virtual string ToGraphVizLabel() => Key ?? string.Empty;
    }
}
