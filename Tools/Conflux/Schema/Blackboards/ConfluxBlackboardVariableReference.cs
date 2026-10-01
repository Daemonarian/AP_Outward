using Conflux.NodeCanvas.Blackboard;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.Exceptions;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Blackboards
{
    internal class ConfluxBlackboardVariableReference
    {
        [YamlMember(Alias = "name")]
        public string? Name { get; set; } = null;

        [YamlMember(Alias = "id")]
        public Guid? ID { get; set; } = null;

        public bool TryGetVariable(NodeCanvasGraphContext context, out ConfluxLocalBlackboardVariable variable)
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

            variable = new();
            return false;
        }

        public virtual BBParameter BuildBBParameter(NodeCanvasGraphContext context)
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

            return CreateBBParameter(Name, id?.ToString());
        }

        protected virtual BBParameter CreateBBParameter(string? name, string? id) => new()
        {
            Name = name,
            TargetVariableID = id,
        };
    }

    internal class ConfluxBlackboardVariableReference<T> : ConfluxBlackboardVariableReference
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "value")]
        public T? Value { get; set; } = default;

        public override BBParameter<T> BuildBBParameter(NodeCanvasGraphContext context)
        {
            if (Value is null)
            {
                return (BBParameter<T>)base.BuildBBParameter(context);
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                throw new ConfluxValueException($"The values variableReference.name and variable.value may not both be specified, not {Name} and {Value}.");
            }

            if (ID.HasValue)
            {
                throw new ConfluxValueException($"The values variableReference.name and variable.id may not both be specified, not {Name} and {Value}");
            }

            return new()
            {
                Value = Value,
            };
        }

        protected override BBParameter<T> CreateBBParameter(string? name, string? id) => new()
        {
            Name = name,
            TargetVariableID = id,
        };
    }

    internal class ConfluxBlackboardVariableReference<Tout, Tin> : ConfluxBlackboardVariableReference where Tin : INodeCanvasObjectBuilder<Tout>
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "value")]
        public Tin? Value { get; set; } = default;

        public override BBParameter<Tout> BuildBBParameter(NodeCanvasGraphContext context)
        {
            if (Value is null)
            {
                return (BBParameter<Tout>)base.BuildBBParameter(context);
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                throw new ConfluxValueException($"The values variableReference.name and variable.value may not both be specified, not {Name} and {Value}.");
            }

            if (ID.HasValue)
            {
                throw new ConfluxValueException($"The values variableReference.name and variable.id may not both be specified, not {Name} and {Value}");
            }

            return new()
            {
                Value = Value.BuildNodeCanvasObject(context),
            };
        }

        protected override BBParameter<Tout> CreateBBParameter(string? name, string? id) => new()
        {
            Name = name,
            TargetVariableID = id,
        };
    }
}
