using Conflux.Schema.Context;
using Conflux.Schema.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("finish")]
    internal class FinishConfluxStatement : ConfluxStatement
    {
        public override ConfluxGraph BuildGraph(NodeCanvasGraphContext context)
        {
            return ConfluxGraph.CreateTerminal(context);
        }
    }
}
