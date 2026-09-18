using System.Text.Json.Serialization;
using NodeCanvas.Tool;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    internal class Statement : IGraphVizLabelable
    {
        [JsonPropertyName("_text")]
        public string Text { get; set; } = "";

        [JsonPropertyName("_meta")]
        public string? Meta { get; set; } = null;

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
    }
}
