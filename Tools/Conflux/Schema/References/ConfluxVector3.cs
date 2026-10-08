using Conflux.NodeCanvas.References;
using Conflux.Schema.Context;
using YamlDotNet.Serialization;

namespace Conflux.Schema.References
{
    internal class ConfluxVector3 : INodeCanvasObjectBuilder<NodeCanvasVector3>
    {
        [YamlMember(Alias = "x", DefaultValuesHandling = DefaultValuesHandling.Preserve)]
        public float X { get; set; }

        [YamlMember(Alias = "y", DefaultValuesHandling = DefaultValuesHandling.Preserve)]
        public float Y { get; set; }

        [YamlMember(Alias = "z", DefaultValuesHandling = DefaultValuesHandling.Preserve)]
        public float Z { get; set; }

        public NodeCanvasVector3 BuildNodeCanvasObject(NodeCanvasGraphContext context) => new()
        {
            X = X,
            Y = Y,
            Z = Z,
        };
    }
}
