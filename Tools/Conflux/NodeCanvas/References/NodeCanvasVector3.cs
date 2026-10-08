using Conflux.GraphViz;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.References
{
    internal class NodeCanvasVector3 : IGraphVizLabelable, IConfluxObjectBuilder<ConfluxVector3>
    {
        [JsonProperty("x", DefaultValueHandling = DefaultValueHandling.Include)]
        public float X { get; set; }

        [JsonProperty("y", DefaultValueHandling = DefaultValueHandling.Include)]
        public float Y { get; set; }

        [JsonProperty("z", DefaultValueHandling = DefaultValueHandling.Include)]
        public float Z { get; set; }

        public string ToGraphVizLabel() => $"({X}, {Y}, {Z})";

        public ConfluxVector3 BuildConfluxObject(Graph graph) => new()
        {
            X = X,
            Y = Y,
            Z = Z,
        };
    }
}
