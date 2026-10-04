using System.Collections.Immutable;
using System.ComponentModel;
using System.Text;
using Conflux.GraphViz;
using Conflux.NodeCanvas.DerivedDatas;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.StatementNodeExt")]
    internal class StatementNodeExt : DTNode
    {
        public const string DefaultActorName = "INSTIGATOR";

        [JsonProperty("statement")]
        public Statement? Statement { get; set; }

        [JsonProperty("_actorName")]
        [DefaultValue(DefaultActorName)]
        public string ActorName { get; set; } = DefaultActorName;

        [JsonProperty("_actorParameterID")]
        public string? ActorParameterID { get; set; }

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

            content.Append(Statement?.ToGraphVizLabel());

            return GraphVizConverter.WordWrap(content.ToString().TrimEnd());
        }

        protected override SayConfluxStatement BuildConfluxStatement(Graph graph)
        {
            var actorName = ActorName;
            if (graph.DerivedData is DTDerivedData dtDerivedData &&
                !string.IsNullOrWhiteSpace(ActorParameterID))
            {
                var matchingActorParameters = dtDerivedData.ActorParameters
                    .Where(ap => string.Equals(ap.ID, ActorParameterID, StringComparison.Ordinal))
                    .ToImmutableList();
                if (matchingActorParameters.Count == 1)
                {
                    actorName = matchingActorParameters[0].Key;
                }
                else if (matchingActorParameters.Count > 1)
                {
                    throw new ConfluxException($"Found multiple actor parameters matching ID \"{ActorParameterID}\": {matchingActorParameters[0].Key} and {matchingActorParameters[1].Key}.");
                }
            }

            actorName = actorName?.Trim();

            return new()
            {
                Statement = Statement?.BuildConfluxStatementReference(graph),
                Actor = new()
                {
                    Key = string.Equals(actorName, DefaultActorName, StringComparison.Ordinal) ? null : actorName,
                },
            };
        }
    }
}
