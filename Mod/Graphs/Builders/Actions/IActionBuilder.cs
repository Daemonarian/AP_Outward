using NodeCanvas.Framework;

namespace OutwardArchipelago.Graphs.Builders.Actions
{
    internal interface IActionBuilder
    {
        abstract ActionTask BuildAction(IGraphPatchContext context);
    }
}
