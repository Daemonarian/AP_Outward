using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Blackboards;
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

        public virtual ConfluxBlackboardVariableReference BuildConfluxBlackboardVariableReference(Graph graph) => BuildReferenceBlackboardVariableReference<ConfluxBlackboardVariableReference>(graph);

        protected T BuildReferenceBlackboardVariableReference<T>(Graph graph) where T : ConfluxBlackboardVariableReference, new() => new()
        {
            Name = Name,
            ID = string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(TargetVariableID) ? Guid.Parse(TargetVariableID) : null,
        };

        public virtual bool IsDefaultValue(object? value)
        {
            if (value is not null)
            {
                throw new ConfluxException($"The default value of a {nameof(BBParameter)} must be null.");
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

        public override ConfluxBlackboardVariableReference<T> BuildConfluxBlackboardVariableReference(Graph graph) => BuildValueConfluxBlackboardVaraibleReference<T>(graph);

        public ConfluxBlackboardVariableReference<T2> BuildConfluxBlackboardVariableReference<T2>(Graph graph) => BuildValueConfluxBlackboardVaraibleReference<T2>(graph);

        protected ConfluxBlackboardVariableReference<T2> BuildValueConfluxBlackboardVaraibleReference<T2>(Graph graph)
        {
            if (IsReference())
            {
                return BuildReferenceBlackboardVariableReference<ConfluxBlackboardVariableReference<T2>>(graph);
            }

            T2? value;
            if (Value is null)
            {
                value = default;
            }
            else if (Value is IConfluxObjectBuilder<T2> valueBuilder)
            {
                value = valueBuilder.BuildConfluxObject(graph);
            }
            else if (Value is T2 newValue)
            {
                value = newValue;
            }
            else
            {
                throw new ConfluxException($"Cannot build Conflux type {typeof(T2).Name} from NodeCanvas type {typeof(T).Name}.");
            }

            return new()
            {
                Value = value,
            };
        }

        public override bool IsDefaultValue(object? value)
        {
            if (IsReference())
            {
                return false;
            }

            if (Value is IDefaultCheckable checkableValue)
            {
                return checkableValue.IsDefaultValue(value);
            }

            return EqualityComparer<T?>.Default.Equals(Value, (T?)value);
        }

        public static implicit operator BBParameter<T>(T value) => new() { Value = value };
    }
}
