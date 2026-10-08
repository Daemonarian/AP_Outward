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

        public virtual bool IsReference() => !string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(TargetVariableID);

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
        };

        public override bool Equals(object? obj) => obj is BBParameter parameter && Name == parameter.Name && TargetVariableID == parameter.TargetVariableID;

        public override int GetHashCode() => HashCode.Combine(Name, TargetVariableID);
    }

    internal class BBParameter<T> : BBParameter
    {
        [JsonProperty("_value")]
        public T? Value { get; set; }

        public override string ToGraphVizLabel()
        {
            if (base.IsReference())
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
            if (IsReference())
            {
                return new()
                {
                    Name = Name,
                };
            }

            return new()
            {
                Value = Value,
            };
        }

        public ConfluxBlackboardVariableReference<T, T2> BuildConfluxBlackboardVariableReference<T2>(Graph graph) where T2 : INodeCanvasObjectBuilder<T>
        {
            if (IsReference())
            {
                return new()
                {
                    Name = Name,
                };
            }

            if (Value is null)
            {
                return new();
            }

            var builder = Value as IConfluxObjectBuilder<T2> ?? throw new NotImplementedException($"The value of type {typeof(T).Name} does not implement IConfluxObjectBuilder<{typeof(T2).Name}>.");

            return new()
            {
                Value = builder.BuildConfluxObject(graph),
            };
        }

        public static implicit operator BBParameter<T>(T value) => new()
        {
            Value = value,
        };

        public override bool Equals(object? obj) => obj is BBParameter<T> parameter && base.Equals(obj) && EqualityComparer<T?>.Default.Equals(Value, parameter.Value);

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Value);
    }
}
