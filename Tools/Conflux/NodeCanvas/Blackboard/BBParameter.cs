using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Blackboard
{
    internal class BBParameter : IGraphVizLabelable, IDefaultCheckable
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

        public virtual ConfluxBlackboardVariableReference BuildConfluxBlackboardVariableReference(Graph graph) => new()
        {
            Name = Name,
            ID = string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(TargetVariableID) ? Guid.Parse(TargetVariableID) : null,
        };

        public virtual bool IsDefaultValue(object? value)
        {
            if (value is not null)
            {
                throw new ConfluxException("The default value of a BBParameter must be null.");
            }

            return !IsReference();
        }
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

            return GraphVizConverter.BuildGraphVizLabel(Value);
        }

        public override ConfluxBlackboardVariableReference<T> BuildConfluxBlackboardVariableReference(Graph graph)
        {
            if (IsReference())
            {
                return new()
                {
                    Name = Name,
                    ID = string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(TargetVariableID) ? Guid.Parse(TargetVariableID) : null,
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
                    ID = string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(TargetVariableID) ? Guid.Parse(TargetVariableID) : null,
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

        public static implicit operator BBParameter<T>(T value) => new() { Value = value };

        public override bool IsDefaultValue(object? value)
        {
            if (IsReference())
            {
                return false;
            }

            if (typeof(IDefaultCheckable).IsAssignableFrom(typeof(T)))
            {
                if (Value is null)
                {
                    return value is null;
                }

                return ((IDefaultCheckable)Value).IsDefaultValue(value);
            }

            if (value is null)
            {
                return Value is null;
            }

            return EqualityComparer<T>.Default.Equals(Value, (T)value);
        }
    }
}
