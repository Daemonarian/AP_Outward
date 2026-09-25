using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;

namespace NodeCanvas.Tool.Conflux.Statements
{
    [ConfluxPolymorphic]
    internal abstract class ConfluxStatement
    {
        public abstract ConfluxGraph BuildGraph(INodeCanvasGraphContext context);
    }
}
