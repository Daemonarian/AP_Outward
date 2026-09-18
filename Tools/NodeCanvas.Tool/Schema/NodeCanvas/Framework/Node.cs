using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using NodeCanvasRenderer.GraphViz;
using NodeCanvasRenderer.Schema.NodeCanvas.DialogueTrees;

namespace NodeCanvasRenderer.Schema.NodeCanvas.Framework
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
    [JsonDerivedType(typeof(ActionNode), "NodeCanvas.DialogueTrees.ActionNode")]
    [JsonDerivedType(typeof(ConditionNode), "NodeCanvas.DialogueTrees.ConditionNode")]
    [JsonDerivedType(typeof(FinishNode), "NodeCanvas.DialogueTrees.FinishNode")]
    [JsonDerivedType(typeof(GoToNode), "NodeCanvas.DialogueTrees.GoToNode")]
    [JsonDerivedType(typeof(MultipleChoiceNode), "NodeCanvas.DialogueTrees.MultipleChoiceNodeExt")]
    [JsonDerivedType(typeof(StatementNodeExt), "NodeCanvas.DialogueTrees.StatementNodeExt")]
    internal abstract class Node : IGraphVizLabelable
    {
        public virtual string GetGraphVizShortName() => GetType().Name;

        public virtual string GetGraphVizContent() => JsonSerializer.Serialize(this, GraphVizConverter.DefaultSerializerOptions);

        public string ToGraphVizLabel()
        {
            var label = new StringBuilder();
            label.AppendLine(GetGraphVizShortName().Trim());
            label.AppendLine(GetGraphVizContent());
            return label.ToString().TrimEnd();
        }
    }
}
