using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatchConditionNodeFactory : IDialoguePatchNodeFactory
    {
        public IDialoguePatchConditionFactory ConditionFactory { get; private set; }

        public IDialoguePatchNodeFactory NextNodeFactory { get; private set; }

        public IDialoguePatchNodeFactory NoNodeFactory { get; private set; }

        public DialoguePatchConditionNodeFactory(IDialoguePatchConditionFactory conditionFactory, IDialoguePatchNodeFactory yesNodeFactory, IDialoguePatchNodeFactory noNodeFactory)
        {
            ConditionFactory = conditionFactory;
            NextNodeFactory = yesNodeFactory;
            NoNodeFactory = noNodeFactory;
        }

        public Node CreateNode(IDialoguePatchContext context)
        {
            var yesNode = NextNodeFactory.CreateNode(context);
            var noNode = NoNodeFactory.CreateNode(context);
            var condition = ConditionFactory.CreateCondition(context);

            var node = context.Tree.AddNode<ConditionNode>();
            node.condition = condition;

            Connection.Create(node, yesNode);
            Connection.Create(node, noNode);

            return node;
        }
    }
}
