using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("sequence")]
    internal class SequenceConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "actions")]
        public List<ConfluxAction> Actions { get; set; } = [];

        public override ActionList BuildAction(NodeCanvasGraphContext context) => new()
        {
            ExecutionMode = ActionList.ActionsExecutionMode.ActionsRunInSequence,
            Actions = [.. Actions.Select(a => a.BuildAction(context))],
        };
    }
}
