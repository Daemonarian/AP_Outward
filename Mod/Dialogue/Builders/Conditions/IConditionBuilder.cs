using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Builders.Conditions
{
    internal interface IConditionBuilder
    {
        abstract ConditionTask BuildCondition(IDialoguePatchContext context);
    }
}
