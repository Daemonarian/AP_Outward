using System.Text;
using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework
{
    [NodeCanvasType("NodeCanvas.Framework.ActionList")]
    internal class ActionList : ActionTask
    {
        [JsonProperty("executionMode")]
        public ActionsExecutionMode ExecutionMode { get; set; } = ActionsExecutionMode.ActionsRunInSequence;

        [JsonProperty("actions")]
        public List<ActionTask> Actions { get; set; } = [];

        public override string GetGraphVizShortName() => "Run";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            var executionModeLabel = ExecutionMode switch
            {
                ActionsExecutionMode.ActionsRunInSequence => "sequence",
                ActionsExecutionMode.ActionsRunInParallel => "parallel",
                _ => ExecutionMode.ToString(),
            };

            content.AppendLine(executionModeLabel);

            foreach (var action in Actions)
            {
                var actionLabel = action.ToGraphVizLabel();
                actionLabel = GraphVizConverter.IndentLines(actionLabel, indentFirstLine: false);
                content.AppendLine($"- {actionLabel}");
            }

            return content.ToString().TrimEnd();
        }

        public enum ActionsExecutionMode
        {
            ActionsRunInSequence,
            ActionsRunInParallel
        }
    }
}
