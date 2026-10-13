using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Serialization;
using Conflux.Outward;
using Conflux.Schema.Conditions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_OwnsDLC")]
    [GraphVizObject(Alias = "OwnsDLC")]
    internal class OwnsDLCNodeCanvasCondition : NodeCanvasCondition
    {
        [JsonProperty("DLC")]
        [GraphVizProperty(IsMainProperty = true)]
        public BBParameter<OutwardStore.DLCs>? DLC { get; set; }

        protected override OwnsDLCConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            DLC = DLC?.BuildConfluxBlackboardVariableReference(graph),
        };
    }
}
