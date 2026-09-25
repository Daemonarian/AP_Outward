using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Conflux.Conditions
{
    [ConfluxPolymorphic]
    internal abstract class ConfluxCondition
    {
        public abstract ConditionTask BuildCondition(INodeCanvasGraphContext context);
    }
}
