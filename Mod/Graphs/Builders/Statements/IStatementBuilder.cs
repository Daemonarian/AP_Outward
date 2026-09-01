using NodeCanvas.DialogueTrees;

namespace OutwardArchipelago.Graphs.Builders.Statements
{
    internal interface IStatementBuilder
    {
        abstract Statement BuildStatement(IGraphPatchContext context);
    }
}
