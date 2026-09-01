using NodeCanvas.Framework;

namespace OutwardArchipelago.Graphs.Builders.BBParameters
{
    internal interface IBBParameterBuilder<T>
    {
        abstract BBParameter<T> BuildBBParameter(IGraphPatchContext context);
    }
}
