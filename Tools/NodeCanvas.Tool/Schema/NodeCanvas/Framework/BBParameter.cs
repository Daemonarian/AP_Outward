using System.Text.Json.Serialization;
using NodeCanvas.Tool;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework
{
    internal class BBParameter : IGraphVizLabelable
    {
        [JsonPropertyName("_name")]
        public string? Name { get; set; }

        [JsonPropertyName("_targetVariableID")]
        public string? TargetVariableID { get; set; }

        public virtual bool HasValue() => !string.IsNullOrWhiteSpace(TargetVariableID);

        public virtual string ToGraphVizLabel()
        {
            if (!string.IsNullOrWhiteSpace(TargetVariableID))
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    return Name.Trim();
                }

                return TargetVariableID.Trim();
            }

            return string.Empty;
        }
    }

    internal class BBParameter<T> : BBParameter
    {
        [JsonPropertyName("_value")]
        public T? Value { get; set; }

        public override bool HasValue() => base.HasValue() || !EqualityComparer<T>.Default.Equals(Value, default);

        public override string ToGraphVizLabel()
        {
            if (base.HasValue())
            {
                return base.ToGraphVizLabel();
            }

            if (Value is IGraphVizLabelable labelableValue)
            {
                return labelableValue.ToGraphVizLabel();
            }

            return Value?.ToString() ?? string.Empty;
        }
    }
}
