using Conflux.NodeCanvas;
using Conflux.NodeCanvas.Connections;
using Conflux.NodeCanvas.DerivedDatas;
using Conflux.NodeCanvas.Nodes;

namespace Conflux.Schema.Context
{
    internal abstract class NodeCanvasGraphContext(ConfluxScript script)
    {
        public ConfluxScript Script { get; init; } = script;

        public abstract Graph.GraphType GraphType { get; }

        public abstract Type NodeBaseType { get; }

        public abstract Type ConnectionBaseType { get; }

        public abstract Type DerivedDataBaseType { get; }

        public abstract Connection BuildConnection(Node source, Node target);

        public abstract Node BuildTerminalNode();

        public bool IsValidNode(Node? node) => node is not null && node.GetType().IsAssignableTo(NodeBaseType);

        public bool IsValidConnection(Connection? connection) => connection is not null && connection.GetType().IsAssignableTo(ConnectionBaseType);

        public bool IsValidDerivedData(DerivedData? derivedData) => derivedData is not null && derivedData.GetType().IsAssignableTo(DerivedDataBaseType);
    }
}
