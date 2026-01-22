using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Builders.BBParameters
{
    internal interface IBBParameterBuilder<T>
    {
        abstract BBParameter<T> BuildBBParameter(IDialoguePatchContext context);
    }
}
