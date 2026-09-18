using System.Text;
using System.Text.Json.Serialization;
using NodeCanvas.Tool;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    internal class StatementNodeExt : Node
    {
        [JsonPropertyName("statement")]
        public Statement Statement { get; set; } = new Statement { Text = "This is a dialogue text" };

        [JsonPropertyName("_actorName")]
        public string ActorName { get; set; } = "INSTIGATOR";

        [JsonPropertyName("_actorParameterID")]
        public string? ActorParameterID { get; set; } = null;

        public override string GetGraphVizShortName() => "Say";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(ActorName))
            {
                content.Append(ActorName.Trim()).Append(": ");
            }
            else if (!string.IsNullOrWhiteSpace(ActorParameterID))
            {
                content.Append(ActorParameterID.Trim()).Append(": ");
            }

            content.Append(Statement.ToGraphVizLabel());

            return GraphVizConverter.WordWrap(content.ToString().TrimEnd());
        }
    }
}
