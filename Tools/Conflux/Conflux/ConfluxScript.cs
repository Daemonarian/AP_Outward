using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Schema;
using NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework.Internal;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux
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
        public DTDerivedSerializationData DerivedData { get; set; } = new();

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

        public GraphReplacementTemplate BuildGraphReplacementTemplate()
        {
            return new GraphReplacementTemplate
            {
                Routes = Replace,
                Graph = BuildNodeCanvasGraph(),
            };
        }

        private GraphSerializationData BuildNodeCanvasGraph()
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
