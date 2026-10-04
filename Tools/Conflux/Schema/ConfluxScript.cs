using Conflux.NodeCanvas;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.DerivedDatas;
using YamlDotNet.Serialization;

namespace Conflux.Schema
{
    /// <summary>
    /// Defines the main file format for a Conflux template.
    /// </summary>
    internal abstract class ConfluxScript
    {
        [YamlMember(Alias = "type")]
        public abstract string TypeName { get; set; }

        [YamlMember(Alias = "replace")]
        public List<string> Replace { get; set; } = [];

        [YamlMember(Alias = "threads")]
        public Dictionary<string, ConfluxBlock> Threads { get; set; } = [];

        [YamlIgnore]
        public ConfluxLocalBlackboard LocalBlackboard { get; set; } = new();

        [YamlMember(Alias = "blackboard")]
        public ConfluxLocalBlackboard? LocalBlackboardProxy
        {
            get => LocalBlackboard.IsEmpty() ? null : LocalBlackboard;
            set => LocalBlackboard = value ?? new();
        }

        [YamlIgnore]
        public abstract ConfluxDerivedData DerivedData { get; set; }

        public GraphTemplate BuildGraphReplacementTemplate()
        {
            return new GraphTemplate
            {
                Routes = Replace,
                Graph = BuildNodeCanvasGraph(),
            };
        }

        protected abstract NodeCanvasGraphContext CreateContext();

        private Graph BuildNodeCanvasGraph()
        {
            var context = CreateContext();
            var graph = ConfluxGraph.CreateTerminal(context);
            foreach (var (label, block) in Threads)
            {
                var blockGraph = block.BuildGraph(context);
                graph = graph.Merge(blockGraph, label);
            }

            return graph.BuildNodeCanvasGraph();
        }
    }
}
