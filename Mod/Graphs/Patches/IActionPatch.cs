using NodeCanvas.Framework;

namespace OutwardArchipelago.Graphs.Patches
{
    internal interface IActionPatch
    {
        abstract ActionTask BuildAction(IGraphPatchContext context, ActionTask action);
    }
}
