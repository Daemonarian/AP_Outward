using Conflux.Schema.Context;
using Conflux.Schema.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxPolymorphic]
    internal abstract class ConfluxStatement
    {
        public abstract ConfluxGraph BuildGraph(NodeCanvasGraphContext context);
    }
}
