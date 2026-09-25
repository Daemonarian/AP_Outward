using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Actions
{
    [ConfluxDerived("sequence")]
    internal class SequenceConfluxAction : ConfluxAction
    {
        [YamlMember(Alias = "actions")]
        [ConfluxMainProperty]
        public List<ConfluxAction> Actions { get; set; } = [];

        public override ActionTask BuildAction(INodeCanvasGraphContext context)
        {
            var actions = Actions.Select(a => a.BuildAction(context)).ToList();
            return new ActionList
            {
                ExecutionMode = ActionList.ActionsExecutionMode.ActionsRunInSequence,
                Actions = actions,
            };
        }
    }
}
