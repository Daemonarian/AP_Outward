using System.Text.Json.Serialization;
using NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
    [JsonDerivedType(typeof(DTConnection), "NodeCanvas.DialogueTrees.DTConnection")]
    internal class Connection
    {
        [JsonPropertyName("_sourceNode")]
        public Node? SourceNode { get; set; }

        [JsonPropertyName("_targetNode")]
        public Node? TargetNode { get; set; }

        [JsonPropertyName("_isDisabled")]
        public bool IsDisabled { get; set; }
    }
}
