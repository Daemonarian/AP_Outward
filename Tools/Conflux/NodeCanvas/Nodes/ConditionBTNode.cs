using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.ConditionNode")]
    internal class ConditionBTNode : BTNode
    {
        [JsonProperty("_condition")]
        public ConditionTask? Condition { get; set; }

        public override int OutConnectionCount => 1;

        public override string GetGraphVizShortName() => "Condition";

        public override string GetGraphVizContent() => Condition?.ToGraphVizLabel() ?? string.Empty;

        protected override ConditionConfluxStatement BuildConfluxStatement(Graph graph) => new()
        {
            Condition = Condition?.BuildConfluxCondition(graph),
        };
    }
}
