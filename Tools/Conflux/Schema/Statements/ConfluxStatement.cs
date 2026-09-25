using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;

namespace Conflux.Schema.Statements
{
    [ConfluxPolymorphic]
    internal abstract class ConfluxStatement
    {
        public abstract ConfluxGraph BuildGraph(INodeCanvasGraphContext context);
    }
}
