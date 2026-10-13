using Conflux.NodeCanvas.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxPolymorphic]
    internal abstract class ConfluxAction
    {
        public abstract NodeCanvasAction BuildAction(NodeCanvasGraphContext context);
    }
}
