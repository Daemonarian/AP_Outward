using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("finish")]
    internal class FinishConfluxStatement : ConfluxStatement
    {
        public override ConfluxGraph BuildGraph(INodeCanvasGraphContext context)
        {
            return ConfluxGraph.CreateTerminal(context);
        }
    }
}
