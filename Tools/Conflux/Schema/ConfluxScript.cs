using Conflux.NodeCanvas;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.DerivedData;
using Conflux.Schema.Context;
using YamlDotNet.Serialization;

namespace Conflux.Schema
{
    /// <summary>
    /// Defines the main file format for a Conflux template.
    /// </summary>
    internal class ConfluxScript
    {
        [YamlMember(Alias = "replace")]
        public List<string> Replace { get; set; } = [];

        [YamlMember(Alias = "threads")]
        public Dictionary<string, ConfluxBlock> Threads { get; set; } = [];

        [YamlMember(Alias = "derivedData")]
        public DTDerivedData DerivedData { get; set; } = new();

        [YamlMember(Alias = "localBlackboard")]
        public BlackboardSource LocalBlackboard { get; set; } = new();

        [YamlMember(Alias = "blackboard")]
        public Dictionary<string, string> Blackboard { get; set; } = [];

        [YamlIgnore]
        public INodeCanvasGraphContext Context { get; init; }

        public ConfluxScript()
        {
            Context = new NodeCanvasDialogueTreeContext(this);
        }

        public GraphTemplate BuildGraphReplacementTemplate()
        {
            return new GraphTemplate
            {
                Routes = Replace,
                Graph = BuildNodeCanvasGraph(),
            };
        }

        private Graph BuildNodeCanvasGraph()
        {
            var graph = ConfluxGraph.CreateTerminal(Context);
            foreach (var (label, block) in Threads)
            {
                var blockGraph = block.BuildGraph(Context);
                graph = graph.Merge(blockGraph, label);
            }

            return graph.BuildNodeCanvasGraph();
        }
    }
}
