using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal interface IDialoguePatchActionFactory
    {
        public abstract ActionTask CreateAction(IDialoguePatchContext context);
    }
}