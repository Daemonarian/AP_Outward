using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Nodes;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.Wait")]
    [GraphVizObject(Alias = "Wait")]
    internal class WaitNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("waitTime")]
        [GraphVizProperty(IsMainProperty = true)]
        [DefaultValue(1f)]
        public BBParameter<float>? WaitTime { get; set; } = 1f;

        [JsonProperty("finishStatus")]
        [DefaultValue(FinishNode.CompactStatus.Success)]
        public FinishNode.CompactStatus FinishState { get; set; } = FinishNode.CompactStatus.Success;

        public override WaitConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            WaitTime = WaitTime?.BuildConfluxBlackboardVariableReference(graph),
            FinishStatus = FinishState,
        };
    }
}
