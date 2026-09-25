using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.Connections;
using Conflux.NodeCanvas.Nodes;
using Conflux.Schema;

namespace Conflux.Schema.Context
{
    internal class NodeCanvasDialogueTreeContext(ConfluxScript script) : NodeCanvasGraphContext(script)
    {
        public override string GraphType => "NodeCanvas.DialogueTrees.DialogueTreeExt";

        public override Type NodeBaseType => typeof(DTNode);

        public override Type ConnectionBaseType => typeof(DTConnection);

        public override Connection BuildConnection(Node source, Node? target) => new DTConnection
        {
            SourceNode = source,
            TargetNode = target,
        };

        public override Node BuildTerminalNode() => new FinishNode
        {
            FinishState = FinishNode.CompactStatus.Success,
        };

        public override Node BuildIfNode(ConditionTask condition) => new ConditionNode
        {
            Condition = condition,
        };

        public override Node BuildDoNode(ActionTask action) => new ActionNode
        {
            Action = action,
        };
    }
}
