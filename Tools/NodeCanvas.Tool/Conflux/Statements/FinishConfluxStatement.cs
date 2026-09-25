using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;

namespace NodeCanvas.Tool.Conflux.Statements
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
