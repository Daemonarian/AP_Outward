using System.Collections;
using System.Collections.Frozen;
using System.ComponentModel;
using System.Reflection;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Serialization
{
    internal class ConfluxMainPropertyTypeConverter : IYamlTypeConverter
    {
        private static readonly FrozenSet<Type> YamlScalarTypes = [
            typeof(string),
            typeof(decimal),
            typeof(Guid),
            typeof(DateTime),
            typeof(TimeSpan),
        ];

        private readonly ThreadLocal<HashSet<Type>> _bypassedTypes = new(() => []);

        public bool Accepts(Type type)
        {
            if (_bypassedTypes.Value!.Contains(type))
            {
                return false;
            }

            return ConfluxMainPropertyInfo.TryGet(type, out _);
        }

        public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
        {
            throw new NotImplementedException($"Deserialization is handled by {nameof(ConfluxMainPropertyDeserializer)}.");
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
        {
            if (value is null)
            {
                serializer(null);
                return;
            }

            string? serializationKey = null;
            if (ConfluxDerivedInfo.TryGet(type, out var derivedInfo))
            {
                serializationKey = derivedInfo.Key;
            }

            if (serializationKey is not null)
            {
                emitter.Emit(new MappingStart(null, null, true, MappingStyle.Any));
                emitter.Emit(new Scalar(serializationKey));
            }

            if (ConfluxMainPropertyInfo.TryGet(value.GetType(), out var mainProp) &&
                CanInline(value))
            {
                var innerValue = mainProp.Property.GetValue(value);
                serializer(innerValue);
            }
            else
            {
                // Attempt to perform default serialization, ignoring this TypeConverter.
                _bypassedTypes.Value!.Add(type);
                try
                {
                    serializer(value);
                }
                finally
                {
                    _bypassedTypes.Value!.Remove(type);
                }
            }

            if (serializationKey is not null)
            {
                emitter.Emit(new MappingEnd());
            }
        }

        /// <summary>
        /// Determine if the value can be in-lined.
        /// 
        /// This means that either the main property is forced, or will be
        /// serialized as a non-mapping type.
        /// </summary>
        /// <param name="value">The value that we are trying to in-line via its main property.</param>
        /// <param name=requireNonMapping">Additionally require that it can be in-lined to a non-mapping type in YAML.</param>
        /// <returns>Whether the value can be in-lined.</returns>
        private static bool CanInline(object value, bool requireNonMapping = false)
        {
            var actualType = value.GetType();
            if (!ConfluxMainPropertyInfo.TryGet(actualType, out var mainProp))
            {
                return false;
            }

            if (mainProp.IsForced)
            {
                return !requireNonMapping || !DoesSerializeToMapping(mainProp.Property.PropertyType, mainProp.Property.GetValue(value));
            }

            foreach (var prop in actualType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop == mainProp.Property)
                {
                    continue;
                }

                if (prop.GetCustomAttribute<YamlIgnoreAttribute>() is not null)
                {
                    continue;
                }

                var propValue = prop.GetValue(value);
                if (!IsDefaultValue(prop, propValue))
                {
                    return false;
                }
            }

            return !DoesSerializeToMapping(mainProp.Property.PropertyType, mainProp.Property.GetValue(value));
        }

        /// <summary>
        /// Determines if a value of the expected type will be serialized to a mapping.
        /// </summary>
        /// <param name="type">The expected type.</param>
        /// <param name="value">The value of the type.</param>
        /// <returns>Whether the value will be serialized to a mapping.</returns>
        private static bool DoesSerializeToMapping(Type type, object? value)
        {
            if (value is null)
            {
                return false;
            }

            // ConfluxPolymorphic types get wrapped in a single-key mapping.
            if (ConfluxPolymorphicInfo.TryGet(type, out _))
            {
                return true;
            }

            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type.IsPrimitive ||
                type.IsEnum ||
                YamlScalarTypes.Contains(type))
            {
                return false;
            }

            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                return true;
            }

            if (type.GetInterfaces().Any(i => i.IsGenericType &&
                (i.GetGenericTypeDefinition() == typeof(IDictionary<,>) ||
                 i.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>))))
            {
                return true;
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return false;
            }

            // Check if the main-property can be inlined as a non-mapping.
            if (ConfluxMainPropertyInfo.TryGet(type, out var mainProp) &&
                CanInline(value, requireNonMapping: true))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if the given property value can be omitted.
        /// </summary>
        /// <param name="prop">The property.</param>
        /// <param name="value">The value of the property.</param>
        /// <returns>Whether the property value can be omitted.</returns>
        private static bool IsDefaultValue(PropertyInfo prop, object? value)
        {
            if (value is null)
            {
                return true;
            }

            var defaultValueAttr = prop.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttr != null) return Equals(defaultValueAttr.Value, value);

            var type = prop.PropertyType;
            if (type.IsValueType) return Equals(Activator.CreateInstance(type), value);

            return false;
        }
    }
}