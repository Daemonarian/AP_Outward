using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Builders.Nodes
{
    internal interface INodeBuilder
    {
        abstract Node BuildNode(IDialoguePatchContext context);
    }
}
