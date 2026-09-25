using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("parallel")]
    internal class ParallelConfluxAction : ConfluxAction
    {
        [YamlMember(Alias = "actions")]
        [ConfluxMainProperty]
        public List<ConfluxAction> Actions { get; set; } = [];

        public override ActionTask BuildAction(INodeCanvasGraphContext context)
        {
            var actions = Actions.Select(a => a.BuildAction(context)).ToList();
            return new ActionList
            {
                ExecutionMode = ActionList.ActionsExecutionMode.ActionsRunInParallel,
                Actions = actions,
            };
        }
    }
}
