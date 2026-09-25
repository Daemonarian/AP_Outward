using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;

namespace Conflux.Schema.Actions
{
    [ConfluxPolymorphic]
    internal abstract class ConfluxAction
    {
        public abstract ActionTask BuildAction(INodeCanvasGraphContext context);
    }
}
