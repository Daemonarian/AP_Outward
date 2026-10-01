using Conflux.GraphViz;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Blackboard
{
    internal class BBParameter : IGraphVizLabelable
    {
        [JsonProperty("_name")]
        public string? Name { get; set; }

        [JsonProperty("_targetVariableID")]
        public string? TargetVariableID { get; set; }

        public virtual bool HasValue() => !string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(TargetVariableID);

        public virtual string ToGraphVizLabel()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                return Name.Trim();
            }

            if (!string.IsNullOrWhiteSpace(TargetVariableID))
            {
                return TargetVariableID.Trim();
            }

            return string.Empty;
        }

        public ConfluxBlackboardVariableReference BuildConfluxBlackboardVariableReference(Graph graph) => new()
        {
            Name = Name,
            ID = string.IsNullOrEmpty(TargetVariableID) ? null : Guid.Parse(TargetVariableID),
        };
    }

    internal class BBParameter<T> : BBParameter
    {
        [JsonProperty("_value")]
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

        public new ConfluxBlackboardVariableReference<T> BuildConfluxBlackboardVariableReference(Graph graph)
        {
            if (HasValue())
            {
                return new()
                {
                    Name = Name,
                    ID = string.IsNullOrEmpty(TargetVariableID) ? null : Guid.Parse(TargetVariableID),
                };
            }

            return new()
            {
                Value = Value,
            };
        }

        public ConfluxBlackboardVariableReference<T, T2> BuildConfluxBlackboardVariableReference<T2>(Graph graph) where T2 : INodeCanvasObjectBuilder<T>
        {
            if (HasValue())
            {
                return new()
                {
                    Name = Name,
                    ID = string.IsNullOrEmpty(TargetVariableID) ? null : Guid.Parse(TargetVariableID),
                };
            }

            var builder = Value as IConfluxObjectBuilder<T2> ?? throw new NotImplementedException($"The value of type {typeof(T).Name} does not implement IConfluxObjectBuilder<{typeof(T2).Name}>.");

            return new()
            {
                Value = builder.BuildConfluxObject(graph),
            };
        }
    }
}
