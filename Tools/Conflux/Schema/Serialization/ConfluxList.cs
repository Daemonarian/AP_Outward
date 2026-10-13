using Conflux.GraphViz;
using Conflux.NodeCanvas;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Context;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Serialization
{
    internal class ConfluxList<T> : INodeCanvasObjectBuilder<NodeCanvasList<T>> where T : IGraphVizLabelable
    {
        [ConfluxMainProperty(Force = true)]
        [YamlMember(Alias = "items")]
        public List<T>? Items { get; set; }

        public NodeCanvasList<T> BuildNodeCanvasObject(NodeCanvasGraphContext context) => new()
        {
            Items = Items is null ? null : [.. Items],
        };
    }

    internal class ConfluxList<TNodeCanvas, TConflux> : INodeCanvasObjectBuilder<NodeCanvasList<TNodeCanvas, TConflux>> where TNodeCanvas : IGraphVizLabelable, IConfluxObjectBuilder<TConflux> where TConflux : INodeCanvasObjectBuilder<TNodeCanvas>
    {
        [ConfluxMainProperty(Force = true)]
        [YamlMember(Alias = "items")]
        public List<TConflux>? Items { get; set; }

        public NodeCanvasList<TNodeCanvas, TConflux> BuildNodeCanvasObject(NodeCanvasGraphContext context) => new()
        {
            Items = Items is null ? null : [.. Items.Select(i => i.BuildNodeCanvasObject(context))],
        };
    }
}
