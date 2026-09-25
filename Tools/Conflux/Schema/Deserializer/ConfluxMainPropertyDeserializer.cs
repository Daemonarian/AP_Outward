using System.Collections;
using System.Collections.Frozen;
using System.Reflection;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Deserializer
{
    internal class ConfluxMainPropertyDeserializer : INodeDeserializer
    {
        public static FrozenDictionary<Type, PropertyInfo> MainPropertyMapping => _mainPropertyMapping.Value;

        private static readonly Lazy<FrozenDictionary<Type, PropertyInfo>> _mainPropertyMapping = new(GetMainPropertyMapping);

        private static FrozenDictionary<Type, PropertyInfo> GetMainPropertyMapping()
        {
            var mainPropertyMapping = new Dictionary<Type, PropertyInfo>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (var property in type.GetProperties())
                {
                    var mainPropertyAttribute = property.GetCustomAttribute<ConfluxMainPropertyAttribute>();
                    if (mainPropertyAttribute is null)
                    {
                        continue;
                    }

                    if (mainPropertyMapping.TryGetValue(type, out var otherProperty))
                    {
                        throw new Exception($"Found multiple {typeof(ConfluxMainPropertyAttribute)} for type {type}.");
                    }

                    mainPropertyMapping[type] = property;
                }
            }

            return mainPropertyMapping.ToFrozenDictionary();
        }

        public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value, ObjectDeserializer rootDeserializer)
        {
            // Only intercept when YamlDotNet explicitly asks for a type with a main property.

            if (!MainPropertyMapping.TryGetValue(expectedType, out var mainProperty))
            {
                value = null;
                return false;
            }

            // Peek at the current YAML element without consuming it.

            if (IsMatchingYamlEvent(reader, mainProperty.PropertyType))
            {
                var propertyValue = nestedObjectDeserializer(reader, mainProperty.PropertyType);
                value = Activator.CreateInstance(expectedType);
                mainProperty.SetValue(value, propertyValue);
                return true;
            }

            // Fallback to default deserializer logic.

            value = null;
            return false;
        }

        private static bool IsMatchingYamlEvent(IParser reader, Type targetType)
        {
            if (typeof(IEnumerable).IsAssignableFrom(targetType) &&
                targetType != typeof(string) &&
                !typeof(IDictionary).IsAssignableFrom(targetType))
            {
                return reader.Accept<SequenceStart>(out _);
            }

            if (targetType.IsPrimitive || targetType == typeof(string))
            {
                return reader.Accept<Scalar>(out _);
            }

            return reader.Accept<MappingStart>(out _);
        }
    }
}
