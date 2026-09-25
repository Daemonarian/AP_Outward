using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Conflux.Actions
{
    [ConfluxPolymorphic]
    internal abstract class ConfluxAction
    {
        public abstract ActionTask BuildAction(INodeCanvasGraphContext context);
    }
}
