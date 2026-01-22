using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Builders.Actions
{
    internal interface IActionBuilder
    {
        abstract ActionTask BuildAction(IDialoguePatchContext context);
    }
}
