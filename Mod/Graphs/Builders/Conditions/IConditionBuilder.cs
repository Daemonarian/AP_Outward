using NodeCanvas.Framework;

namespace OutwardArchipelago.Graphs.Builders.Conditions
{
    internal interface IConditionBuilder
    {
        abstract ConditionTask BuildCondition(IGraphPatchContext context);
    }
}
