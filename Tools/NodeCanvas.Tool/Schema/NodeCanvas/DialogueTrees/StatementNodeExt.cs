using System.Text;
using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.StatementNodeExt")]
    internal class StatementNodeExt : DTNode
    {
        [JsonProperty("statement")]
        public Statement Statement { get; set; } = new Statement { Text = "This is a dialogue text" };

        [JsonProperty("_actorName")]
        public string? ActorName { get; set; } = "INSTIGATOR";

        [JsonProperty("_actorParameterID")]
        public string? ActorParameterID { get; set; } = null;

        public override int OutConnectionCount => 1;

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
