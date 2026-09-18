using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using NodeCanvas.Tool;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework.Actions;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
    [JsonDerivedType(typeof(BranchDialogue), "NodeCanvas.Tasks.Actions.BranchDialogue")]
    [JsonDerivedType(typeof(Action_CompleteLocationCheck), "OutwardArchipelago.Graphs.Actions.Action_CompleteLocationCheck")]
    [JsonDerivedType(typeof(SendQuestEvent), "NodeCanvas.Tasks.Actions.SendQuestEvent")]
    [JsonDerivedType(typeof(ActionList), "NodeCanvas.Framework.ActionList")]
    [JsonDerivedType(typeof(GiveReward), "NodeCanvas.Tasks.Actions.GiveReward")]
    internal class ActionTask : IGraphVizLabelable
    {
        public virtual string GetGraphVizShortName() => GetType().Name;

        public virtual string GetGraphVizContent() => "\n" + JsonSerializer.Serialize(this, GraphVizConverter.DefaultSerializerOptions);

        public string ToGraphVizLabel()
        {
            var label = new StringBuilder();

            label.Append(GetGraphVizShortName().Trim());

            var content = GetGraphVizContent();
            if (!string.IsNullOrWhiteSpace(content))
            {
                content = GraphVizConverter.IndentLines(content, indentFirstLine: false);
                label.Append(": ").Append(content);
            }

            return label.ToString().TrimEnd();
        }
    }
}
