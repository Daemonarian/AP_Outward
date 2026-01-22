using NodeCanvas.DialogueTrees;

namespace OutwardArchipelago.Dialogue.Builders.Statements
{
    internal interface IStatementBuilder
    {
        abstract Statement BuildStatement(IDialoguePatchContext context);
    }
}
