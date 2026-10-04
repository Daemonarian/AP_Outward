using Conflux.GraphViz;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.References
{
    internal class Statement : IGraphVizLabelable
    {
        [JsonProperty("_text")]
        public string? Text { get; set; }

        [JsonProperty("_meta")]
        public string? Meta { get; set; }

        public string ToGraphVizLabel()
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                return Text.Trim();
            }

            if (!string.IsNullOrWhiteSpace(Meta))
            {
                return $"Meta: {Meta.Trim()}";
            }

            return string.Empty;
        }

        public ConfluxStatementReference BuildConfluxStatementReference(Graph graph) => new()
        {
            Meta = Meta,
            Text = Text,
        };
    }
}
