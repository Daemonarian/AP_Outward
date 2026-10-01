using Conflux.Schema.DerivedDatas;

namespace Conflux.NodeCanvas.DerivedDatas
{
    internal abstract class DerivedData
    {
        public abstract ConfluxDerivedData BuildConfluxDerivedData(Graph graph);
    }
}
