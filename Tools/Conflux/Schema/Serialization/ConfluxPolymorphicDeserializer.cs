using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Serialization
{
    public class ConfluxPolymorphicDeserializer : INodeDeserializer
    {
        public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value, ObjectDeserializer rootDeserializer)
        {
            if (!ConfluxPolymorphicInfo.TryGet(expectedType, out var polymorphicInfo))
            {
                value = null;
                return false;
            }

            var isMapping = reader.TryConsume<MappingStart>(out _);
            if (!reader.TryConsume<Scalar>(out var keyScalar))
            {
                throw new YamlException(reader.Current?.Start ?? Mark.Empty, reader.Current?.End ?? Mark.Empty, $"Expected a scalar serialization key for the Conflux polymorphic type {expectedType.Name}.");
            }

            var key = keyScalar.Value;
            if (!polymorphicInfo.TryGetDerivedInfo(key, out var derivedInfo))
            {
                throw new YamlException(keyScalar.Start, keyScalar.End, $"Unknown serialization key for Conflux polymorphic type {expectedType.Name}: '{key}'.");
            }

            value = isMapping ? nestedObjectDeserializer(reader, derivedInfo.Type) : Activator.CreateInstance(derivedInfo.Type);

            if (isMapping && !reader.TryConsume<MappingEnd>(out _))
            {
                throw new YamlException(reader.Current?.Start ?? Mark.Empty, reader.Current?.End ?? Mark.Empty, $"Expected a single-key mapping for Conflux polymorphic type {expectedType.Name} with serialization key '{key}', but found multiple keys.");
            }

            return true;
        }
    }
}