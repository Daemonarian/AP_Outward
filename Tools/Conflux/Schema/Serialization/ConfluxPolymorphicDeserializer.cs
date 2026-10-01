using Conflux.Schema.Serialization;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

public class ConfluxPolymorphicDeserializer : INodeDeserializer
{
    public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value, ObjectDeserializer rootDeserializer)
    {
        // Only intercept when YamlDotNot expects one of the polymorphic base classes.

        if (!ConfluxPolymorphicLookup.ByBaseType.TryGetValue(expectedType, out var basePolymorphicLookup))
        {
            value = null;
            return false;
        }

        // Try to consume a scalar key (e.g. "finish").

        if (reader.TryConsume<Scalar>(out var keyScalar))
        {
            var key = keyScalar.Value;
            if (!basePolymorphicLookup.ByKey.TryGetValue(key, out var polymorphicRecord))
            {
                throw new YamlException(keyScalar.Start, keyScalar.End, $"Unkown type for {expectedType}: '{key}'.");
            }

            value = Activator.CreateInstance(polymorphicRecord.DerivedType);
            return true;
        }

        // Try to consume the start of a mapping (e.g. "say:").

        if (reader.TryConsume<MappingStart>(out _))
        {

            // Read the single key (e.g., "say" or "give").

            if (!reader.TryConsume<Scalar>(out var keyScalar2))
            {
                throw new YamlException(reader.Current?.Start ?? Mark.Empty, reader.Current?.End ?? Mark.Empty, "Expected a scalar key for the node type.");
            }

            var key = keyScalar2.Value;
            if (!basePolymorphicLookup.ByKey.TryGetValue(key, out var polymorphicRecord))
            {
                throw new YamlException(keyScalar2.Start, keyScalar2.End, $"Unknown type for {expectedType}: '{key}'.");
            }

            // Delegate the nested properties directly to the specific subclass.

            value = nestedObjectDeserializer(reader, polymorphicRecord.DerivedType);

            // Consume the end of the outer dictionary.

            if (!reader.TryConsume<MappingEnd>(out _))
            {
                throw new YamlException(reader.Current?.Start ?? Mark.Empty, reader.Current?.End ?? Mark.Empty, $"Expected a single-key mapping for node '{key}', but found multiple keys.");
            }

            return true;
        }

        value = null;
        return false;
    }
}