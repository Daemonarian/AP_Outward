using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using NodeCanvas.Tool;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework.Conditions;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
    [JsonDerivedType(typeof(Condition_IsQuestCompleted), "NodeCanvas.Tasks.Conditions.Condition_IsQuestCompleted")]
    [JsonDerivedType(typeof(ConditionList), "NodeCanvas.Framework.ConditionList")]
    [JsonDerivedType(typeof(Condition_QuestEventOccured), "NodeCanvas.Tasks.Conditions.Condition_QuestEventOccured")]
    [JsonDerivedType(typeof(Condition_KnowQuest), "NodeCanvas.Tasks.Conditions.Condition_KnowQuest")]
    internal class ConditionTask : IGraphVizLabelable
    {
        [JsonPropertyName("_invert")]
        public bool Invert { get; set; }

        public virtual string GetGraphVizShortName() => GetType().Name;

        public virtual string GetGraphVizContent() => "\n" + JsonSerializer.Serialize(this, GraphVizConverter.DefaultSerializerOptions);

        public string ToGraphVizLabel()
        {
            var label = new StringBuilder();

            if (Invert)
            {
                label.Append("not ");
            }

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
