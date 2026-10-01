using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxPolymorphic]
    internal abstract class ConfluxAction
    {
        public abstract ActionTask BuildAction(NodeCanvasGraphContext context);
    }
}
