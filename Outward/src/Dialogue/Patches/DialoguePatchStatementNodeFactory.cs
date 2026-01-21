using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatchStatementNodeFactory : IDialoguePatchNodeFactory
    {
        public string ActorName { get; private set; }

        public string MessageKey { get; private set; }

        public IDialoguePatchNodeFactory NextNodeFactory { get; private set; }

        public DialoguePatchStatementNodeFactory(string messageKey, IDialoguePatchNodeFactory nextNodeFactory = null, string actorName = null)
        {
            MessageKey = messageKey;
            NextNodeFactory = nextNodeFactory;
            ActorName = actorName;
        }

        public Node CreateNode(IDialoguePatchContext context)
        {
            var actorName = ActorName;
            if (string.IsNullOrEmpty(actorName))
            {
                actorName = context.Tree.actorParameters.FirstOrDefault()?.name ?? "Speaker";
            }

            var message = OutwardArchipelagoMod.Instance.GetLocalizedModString(MessageKey);

            var node = context.Tree.AddNode<StatementNodeExt>();
            node.actorName = actorName;
            node.statement = new Statement(message);

            var nextNode = NextNodeFactory?.CreateNode(context);
            if (nextNode != null)
            {
                Connection.Create(node, nextNode);
            }

            return node;
        }
    }
}
