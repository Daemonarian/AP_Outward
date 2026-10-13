using Conflux.GraphViz;
using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.ConditionNode")]
    [GraphVizObject(Alias = "Assert")]
    internal class ConditionBTNode : BTNode
    {
        [JsonProperty("_condition")]
        [GraphVizProperty(IsMainProperty = true)]
        public NodeCanvasCondition? Condition { get; set; }

        public override int OutConnectionCount => 1;

        protected override ConditionConfluxStatement BuildConfluxStatement(Graph graph) => new()
        {
            Condition = Condition?.BuildConfluxCondition(graph),
        };
    }
}
