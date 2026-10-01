using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.References
{
    internal class APWorldLocationReference : IConfluxObjectBuilder<ConfluxLocationReference>
    {
        [JsonProperty("_key")]
        public string? Key { get; set; }

        public virtual string ToGraphVizLabel() => Key ?? string.Empty;

        public ConfluxLocationReference BuildConfluxObject(Graph graph) => new()
        {
            Key = Key,
        };
    }
}
