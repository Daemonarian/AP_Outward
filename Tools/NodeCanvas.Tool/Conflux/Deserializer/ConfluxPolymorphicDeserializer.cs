using System.Collections.Frozen;
using System.Reflection;
using NodeCanvas.Tool.Conflux.Deserializer;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

public class ConfluxPolymorphicDeserializer : INodeDeserializer
{
    public static FrozenDictionary<Type, FrozenDictionary<string, Type>> KeyTypeMappings => _keyTypeMappings.Value;

    private static readonly Lazy<FrozenDictionary<Type, FrozenDictionary<string, Type>>> _keyTypeMappings = new(GetKeyTypeMappings);

    private static FrozenDictionary<Type, FrozenDictionary<string, Type>> GetKeyTypeMappings()
    {
        var keyMappings = new Dictionary<Type, FrozenDictionary<string, Type>>();
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            var confluxPolymorphicAttribute = type.GetCustomAttribute<ConfluxPolymorphicAttribute>();
            if (confluxPolymorphicAttribute is null)
            {
                continue;
            }

            var keyMapping = new Dictionary<string, Type>();
            foreach (var subType in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (!subType.IsSubclassOf(type))
                {
                    continue;
                }

                var confluxDerivedAttribute = subType.GetCustomAttribute<ConfluxDerivedAttribute>();
                if (confluxDerivedAttribute is null)
                {
                    continue;
                }

                var key = confluxDerivedAttribute.Key;
                if (keyMapping.TryGetValue(key, out var otherSubType))
                {
                    throw new Exception($"Found multiple derived types of {type} with key \"{key}\": {subType} and {otherSubType}.");
                }

                keyMapping[key] = subType;
            }

            keyMappings[type] = keyMapping.ToFrozenDictionary();
        }

        return keyMappings.ToFrozenDictionary();
    }

    public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value, ObjectDeserializer rootDeserializer)
    {
        // Only intercept when YamlDotNot expects one of the polymorphic base classes.

        if (!KeyTypeMappings.TryGetValue(expectedType, out var keyTypeMapping))
        {
            value = null;
            return false;
        }

        // Try to consume a scalar key (e.g. "finish").

        if (reader.TryConsume<Scalar>(out var keyScalar))
        {
            var key = keyScalar.Value;
            if (!keyTypeMapping.TryGetValue(key, out var subType))
            {
                throw new YamlException(keyScalar.Start, keyScalar.End, $"Unkown type for {expectedType}: '{key}'.");
            }

            value = Activator.CreateInstance(subType);
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
            if (!keyTypeMapping.TryGetValue(key, out var subType))
            {
                throw new YamlException(keyScalar2.Start, keyScalar2.End, $"Unknown type for {expectedType}: '{key}'.");
            }

            // Delegate the nested properties directly to the specific subclass.

            value = nestedObjectDeserializer(reader, subType);

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