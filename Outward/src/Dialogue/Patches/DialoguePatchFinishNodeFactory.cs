using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatchFinishNodeFactory : IDialoguePatchNodeFactory
    {
        public Node CreateNode(IDialoguePatchContext context)
        {
            return context.Tree.AddNode<FinishNode>();
        }
    }
}
