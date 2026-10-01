using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("parallel")]
    internal class ParallelConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "actions")]
        public List<ConfluxAction> Actions { get; set; } = [];

        public override ActionList BuildAction(NodeCanvasGraphContext context) => new()
        {
            ExecutionMode = ActionList.ActionsExecutionMode.ActionsRunInParallel,
            Actions = [.. Actions.Select(a => a.BuildAction(context))],
        };
    }
}
