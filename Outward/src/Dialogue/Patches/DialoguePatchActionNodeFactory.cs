using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatchActionNodeFactory : IDialoguePatchNodeFactory
    {
        public IDialoguePatchActionFactory ActionFactory { get; private set; }

        public IDialoguePatchNodeFactory NextNodeFactory { get; private set; }

        public DialoguePatchActionNodeFactory(IDialoguePatchActionFactory actionFactory, IDialoguePatchNodeFactory nextNodeFactory)
        {
            ActionFactory = actionFactory;
            NextNodeFactory = nextNodeFactory;
        }

        public Node CreateNode(IDialoguePatchContext context)
        {
            var nextNode = NextNodeFactory.CreateNode(context);
            var action = ActionFactory.CreateAction(context);

            var node = context.Tree.AddNode<ActionNode>();
            node.action = action;

            Connection.Create(node, nextNode);

            return node;
        }
    }
}
