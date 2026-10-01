using Conflux.NodeCanvas.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxPolymorphic]
    internal abstract class ConfluxCondition
    {
        public abstract ConditionTask BuildCondition(NodeCanvasGraphContext context);
    }
}
