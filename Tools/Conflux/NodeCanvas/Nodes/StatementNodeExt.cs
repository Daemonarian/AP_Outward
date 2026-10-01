using System.Text;
using Conflux.GraphViz;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.StatementNodeExt")]
    internal class StatementNodeExt : DTNode
    {
        public const string DefaultActorName = "INSTIGATOR";

        [JsonProperty("statement")]
        public Statement Statement { get; set; } = new Statement { Text = "This is a dialogue text" };

        [JsonProperty("_actorName")]
        public string? ActorName { get; set; } = DefaultActorName;

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

        protected override SayConfluxStatement BuildConfluxStatement(Graph graph) => new()
        {
            Statement = Statement.BuildConfluxStatementReference(graph),
            Actor = new()
            {
                Key = ActorName is null || string.Equals(ActorName, DefaultActorName, StringComparison.Ordinal) ? null : ActorName,
            },
        };
    }
}
