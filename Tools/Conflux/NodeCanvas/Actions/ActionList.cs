using System.ComponentModel;
using System.Runtime.Serialization;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Framework.ActionList")]
    internal class ActionList : NodeCanvasAction
    {
        [JsonProperty("executionMode")]
        [GraphVizIgnore]
        [DefaultValue(ActionsExecutionMode.ActionsRunInSequence)]
        public ActionsExecutionMode ExecutionMode { get; set; } = ActionsExecutionMode.ActionsRunInSequence;

        [JsonProperty("actions")]
        [GraphVizProperty(IsMainProperty = true)]
        public List<NodeCanvasAction> Actions { get; set; } = [];

        public override string GetGraphVizShortName() => ExecutionMode switch
        {
            ActionsExecutionMode.ActionsRunInSequence => "Sequence",
            ActionsExecutionMode.ActionsRunInParallel => "Parallel",
            _ => ExecutionMode.ToString(),
        };

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
