using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal interface IDialoguePatchConditionFactory
    {
        public abstract ConditionTask CreateCondition(IDialoguePatchContext context);
    }
}