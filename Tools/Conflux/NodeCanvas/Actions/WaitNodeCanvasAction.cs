using System.ComponentModel;
using System.Text;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Nodes;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.Wait")]
    internal class WaitNodeCanvasAction : ActionTask
    {
        [JsonProperty("waitTime")]
        [DefaultValue(1f)]
        public BBParameter<float>? WaitTime { get; set; } = 1f;

        [JsonProperty("finishStatus")]
        [DefaultValue(FinishNode.CompactStatus.Success)]
        public FinishNode.CompactStatus FinishState { get; set; } = FinishNode.CompactStatus.Success;

        public override string GetGraphVizShortName() => "Wait";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            if (WaitTime is not null)
            {
                content.AppendLine($"WaitTime: {GraphVizConverter.IndentLines(WaitTime.ToGraphVizLabel())}");
            }

            if (FinishState != FinishNode.CompactStatus.Success)
            {
                content.AppendLine($"FinishState: {FinishState}");
            }

            return content.ToString().TrimEnd();
        }

        public override WaitConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            WaitTime = WaitTime?.BuildConfluxBlackboardVariableReference(graph),
            FinishStatus = FinishState,
        };
    }
}
