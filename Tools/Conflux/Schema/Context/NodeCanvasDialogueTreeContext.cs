using Conflux.NodeCanvas;
using Conflux.NodeCanvas.Connections;
using Conflux.NodeCanvas.DerivedDatas;
using Conflux.NodeCanvas.Nodes;

namespace Conflux.Schema.Context
{
    internal class NodeCanvasDialogueTreeContext(ConfluxScript script) : NodeCanvasGraphContext(script)
    {
        public override Graph.GraphType GraphType => Graph.GraphType.DialogueTree;

        public override Type NodeBaseType => typeof(DTNode);

        public override Type ConnectionBaseType => typeof(DTConnection);

        public override Type DerivedDataBaseType => typeof(DTDerivedData);

        public override DTConnection BuildConnection(Node source, Node target) => new()
        {
            SourceNode = source,
            TargetNode = target,
        };

        public override Node BuildTerminalNode() => new FinishNode();
    }
}
