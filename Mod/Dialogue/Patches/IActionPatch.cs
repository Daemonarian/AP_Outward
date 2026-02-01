using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal interface IActionPatch
    {
        abstract ActionTask BuildAction(IDialoguePatchContext context, ActionTask action);
    }
}
