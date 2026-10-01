using System.Collections.Frozen;
using System.Reflection;
using Conflux.Schema.Exceptions;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Serialization
{
    internal class ConfluxMainPropertyDeserializer : INodeDeserializer
    {
        public static FrozenDictionary<Type, MainPropertyInfo> MainPropertyMapping => _mainPropertyMapping.Value;

        private static readonly Lazy<FrozenDictionary<Type, MainPropertyInfo>> _mainPropertyMapping = new(GetMainPropertyMapping);

        private static FrozenDictionary<Type, MainPropertyInfo> GetMainPropertyMapping()
        {
            var mainPropertyMapping = new Dictionary<Type, MainPropertyInfo>();
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

                    mainPropertyMapping[type] = new(mainPropertyAttribute, property);
                }
            }

            return mainPropertyMapping.ToFrozenDictionary();
        }

        public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value, ObjectDeserializer rootDeserializer)
        {
            // Look for a main property for the expected type.

            if (!MainPropertyMapping.TryGetValue(expectedType, out var mainProperty))
            {
                if (!expectedType.IsGenericType)
                {
                    value = null;
                    return false;
                }

                var expectedGenericType = expectedType.GetGenericTypeDefinition();
                if (!MainPropertyMapping.TryGetValue(expectedGenericType, out var genericMainProperty))
                {
                    value = null;
                    return false;
                }

                var boundGenericMainProperty = expectedType.GetProperty(genericMainProperty.Property.Name) ??
                    throw new ConfluxException($"Could not bind generic main property \"{genericMainProperty.Property}\" to specialized type \"{expectedType}\".");
                mainProperty = new(genericMainProperty.Attribute, boundGenericMainProperty);
            }

            // Only intercept if the main property is forced or the reader is at a non-mapping type.

            if (!mainProperty.Attribute.Force && reader.Accept<MappingStart>(out _))
            {
                value = null;
                return false;
            }

            // Deserialize the main property.

            var propertyValue = nestedObjectDeserializer(reader, mainProperty.Property.PropertyType);

            // Instantiate the type and set the main property.

            var obj = Activator.CreateInstance(expectedType);
            mainProperty.Property.SetValue(obj, propertyValue);

            value = obj;
            return true;
        }

        public record MainPropertyInfo(ConfluxMainPropertyAttribute Attribute, PropertyInfo Property);
    }
}
