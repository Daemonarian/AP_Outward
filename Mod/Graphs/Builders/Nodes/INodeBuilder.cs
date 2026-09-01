using NodeCanvas.Framework;

namespace OutwardArchipelago.Graphs.Builders.Nodes
{
    internal interface INodeBuilder
    {
        abstract Node BuildNode(IGraphPatchContext context);
    }
}
