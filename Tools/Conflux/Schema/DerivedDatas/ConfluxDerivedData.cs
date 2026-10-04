using Conflux.NodeCanvas.DerivedDatas;
using Conflux.Schema.Context;

namespace Conflux.Schema.DerivedDatas
{
    internal abstract class ConfluxDerivedData
    {
        public abstract DerivedData BuildDerivedData(NodeCanvasGraphContext context);

        public abstract bool IsEmpty();
    }
}
