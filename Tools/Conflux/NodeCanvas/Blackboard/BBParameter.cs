using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Exceptions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Blackboard
{
    internal class BBParameter : IConfluxObjectBuilder<ConfluxBlackboardVariableReference>, IGraphVizLabelable, IDefaultCheckable
    {
        [JsonProperty("_name")]
        public string? Name { get; set; }

        [JsonProperty("_targetVariableID")]
        public string? TargetVariableID { get; set; }

        public virtual bool IsReference() => !string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(TargetVariableID);

        public virtual ConfluxBlackboardVariableReference BuildConfluxBlackboardVariableReference(Graph graph) => BuildReferenceBlackboardVariableReference<ConfluxBlackboardVariableReference>(graph);

        protected T BuildReferenceBlackboardVariableReference<T>(Graph graph) where T : ConfluxBlackboardVariableReference, new() => new()
        {
            Name = Name,
            ID = string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(TargetVariableID) ? Guid.Parse(TargetVariableID) : null,
        };

        [return: NotNull]
        public ConfluxBlackboardVariableReference BuildConfluxObject(Graph graph) => BuildConfluxBlackboardVariableReference(graph);

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

        public virtual bool IsDefaultValue(object? value)
        {
            if (value is not null)
            {
                throw new ConfluxException($"The default value of a {nameof(BBParameter)} must be null.");
            }

            return !IsReference();
        }
    }

    internal class BBParameter<T> : BBParameter, IConfluxObjectBuilder
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

            return new()
            {
                Value = ConfluxObjectBuilder.Build<T, T2>(graph, Value),
            };
        }

        public bool TryBuildConfluxObject<T2>(Graph graph, [NotNullWhen(true)] out T2? target)
        {
            if (typeof(T2).IsGenericType && typeof(T2).GetGenericTypeDefinition() == typeof(ConfluxBlackboardVariableReference<>))
            {
                var buildMethod = GetType()
                    .GetMethod(nameof(BuildConfluxBlackboardVariableReference), BindingFlags.Instance)
                    ?.MakeGenericMethod(typeof(T2).GetGenericArguments());
                if (buildMethod is not null)
                {
                    target = (T2)buildMethod.Invoke(this, [graph])!;
                    return true;
                }
            }

            target = default;
            return false;
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
