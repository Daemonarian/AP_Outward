using Newtonsoft.Json;
using NodeCanvas.Tool.Conflux.Deserializer;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    internal class Statement : IGraphVizLabelable
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "text")]
        [JsonProperty("_text")]
        public string Text { get; set; } = "";

        [YamlMember(Alias = "meta")]
        [JsonProperty("_meta")]
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
