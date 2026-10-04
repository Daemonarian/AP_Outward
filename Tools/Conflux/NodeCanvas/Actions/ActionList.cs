using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Framework.ActionList")]
    internal class ActionList : ActionTask
    {
        [JsonProperty("executionMode")]
        [DefaultValue(ActionsExecutionMode.ActionsRunInSequence)]
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

        public override ConfluxAction BuildConfluxAction(Graph graph) => ExecutionMode switch
        {
            ActionsExecutionMode.ActionsRunInSequence => new SequenceConfluxAction
            {
                Actions = [.. Actions.Select(action => action.BuildConfluxAction(graph))],
            },
            ActionsExecutionMode.ActionsRunInParallel => new ParallelConfluxAction
            {
                Actions = [.. Actions.Select(action => action.BuildConfluxAction(graph))],
            },
            _ => throw new NotImplementedException($"ActionList with ExecutionMode {ExecutionMode} is not implemented."),
        };


        [JsonConverter(typeof(StringEnumConverter))]
        public enum ActionsExecutionMode
        {
            [EnumMember(Value = "ActionsRunInSequence")]
            ActionsRunInSequence,

            [EnumMember(Value = "ActionsRunInParallel")]
            ActionsRunInParallel,
        }
    }
}
