using System.Diagnostics.CodeAnalysis;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Blackboards
{
    internal class ConfluxBlackboardVariableReference : IDefaultCheckable
    {
        [YamlMember(Alias = "name")]
        public string? Name { get; set; }

        [YamlMember(Alias = "id")]
        public Guid? ID { get; set; }

        public bool IsReference() => !string.IsNullOrWhiteSpace(Name) || ID.HasValue;

        public bool TryGetVariable(NodeCanvasGraphContext context, [NotNullWhen(true)] out ConfluxLocalBlackboardVariable? variable)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ConfluxValueException("The value variableReference.name must be specified.");
            }

            if (context.Script.LocalBlackboard.Variables.TryGetValue(Name, out var localBlackboardVariable))
            {
                variable = localBlackboardVariable;
                return true;
            }

            variable = null;
            return false;
        }

        public virtual BBParameter BuildBBParameter(NodeCanvasGraphContext context) => BuildReferenceBBParameter<BBParameter>(context);

        protected virtual T BuildReferenceBBParameter<T>(NodeCanvasGraphContext context) where T : BBParameter, new()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ConfluxValueException("The value variableReference.name must be specified.");
            }

            var id = ID;
            if (TryGetVariable(context, out var variable))
            {
                if (id.HasValue && id != variable.ID)
                {
                    throw new ConfluxValueException($"The value variableReference.id must match the variable definition in the local blackboard, {variable.ID}, not {ID}.");
                }

                id = variable.ID;
            }

            return new T
            {
                Name = Name,
                TargetVariableID = id?.ToString()
            };
        }

        public virtual bool IsDefaultValue(object? value)
        {
            if (value is not null)
            {
                throw new ConfluxException($"Default value of a {nameof(ConfluxBlackboardVariableReference)} may only be null.");
            }

            return !IsReference();
        }
    }

    internal class ConfluxBlackboardVariableReference<T> : ConfluxBlackboardVariableReference
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "value")]
        public T? Value { get; set; }

        public bool HasValue() => Value is not null;

        public override BBParameter<T> BuildBBParameter(NodeCanvasGraphContext context) => BuildValueBBParameter<T>(context);

        public virtual BBParameter<T2> BuildBBParameter<T2>(NodeCanvasGraphContext context) => BuildValueBBParameter<T2>(context);

        protected BBParameter<T2> BuildValueBBParameter<T2>(NodeCanvasGraphContext context)
        {
            if (IsReference())
            {
                if (HasValue())
                {
                    throw new ConfluxException($"A variable should specify either a reference of a value, not both {Name} ({ID}) and {Value}.");
                }

                return BuildReferenceBBParameter<BBParameter<T2>>(context);
            }

            T2? value;
            if (Value is null)
            {
                value = default;
            }
            else if (Value is INodeCanvasObjectBuilder<T2> valueBuilder)
            {
                value = valueBuilder.BuildNodeCanvasObject(context);
            }
            else if (Value is T2 newValue)
            {
                value = newValue;
            }
            else
            {
                throw new ConfluxException($"Cannot build NodeCanvas type {typeof(T2).Name} from Conflux type {Value.GetType().Name}.");
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

        public static implicit operator ConfluxBlackboardVariableReference<T>(T value) => new() { Value = value };
    }
}
