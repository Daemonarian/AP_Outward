using Conflux.NodeCanvas;
using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.Connections;
using Conflux.NodeCanvas.DerivedDatas;
using Conflux.NodeCanvas.Nodes;

namespace Conflux.Schema.Context
{
    internal class NodeCanvasBehaviourTreeContext(ConfluxScript script) : NodeCanvasGraphContext(script)
    {
        public override Graph.GraphType GraphType => Graph.GraphType.BehaviourTree;

        public override Type NodeBaseType => typeof(BTNode);

        public override Type ConnectionBaseType => typeof(BTConnection);

        public override Type DerivedDataBaseType => typeof(BTDerivedData);

        public override BTConnection BuildConnection(Node source, Node target) => new()
        {
            SourceNode = source,
            TargetNode = target,
        };

        public override Node BuildTerminalNode() => new BTActionNode
        {
            Action = new ActionList
            {
                ExecutionMode = ActionList.ActionsExecutionMode.ActionsRunInSequence,
                Actions = [],
            },
        };
    }
}
