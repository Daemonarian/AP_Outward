using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal interface IDialoguePatchNodeFactory
    {
        public abstract Node CreateNode(IDialoguePatchContext context);
    }
}
