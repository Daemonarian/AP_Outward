using System.Collections.Frozen;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Conflux.Schema.Exceptions;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Serialization
{
    internal class ConfluxMainPropertyTypeConverter : IYamlTypeConverter
    {
        public static FrozenDictionary<Type, MainPropertyInfo> MainPropertyMapping => _mainPropertyMapping.Value;
        private static readonly Lazy<FrozenDictionary<Type, MainPropertyInfo>> _mainPropertyMapping = new(GetMainPropertyMapping);

        private readonly ThreadLocal<HashSet<Type>> _bypassedTypes = new(() => []);

        private static FrozenDictionary<Type, MainPropertyInfo> GetMainPropertyMapping()
        {
            var mainPropertyMapping = new Dictionary<Type, MainPropertyInfo>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (var property in type.GetProperties())
                {
                    var mainPropertyAttribute = property.GetCustomAttribute<ConfluxMainPropertyAttribute>();
                    if (mainPropertyAttribute is null) continue;

                    if (mainPropertyMapping.TryGetValue(type, out var otherProperty))
                    {
                        throw new Exception($"Found multiple {typeof(ConfluxMainPropertyAttribute)} for type {type}.");
                    }

                    mainPropertyMapping[type] = new MainPropertyInfo(mainPropertyAttribute, property);
                }
            }
            return mainPropertyMapping.ToFrozenDictionary();
        }

        private bool TryGetMainProperty(Type expectedType, [NotNullWhen(true)] out MainPropertyInfo? mainProperty)
        {
            if (MainPropertyMapping.TryGetValue(expectedType, out mainProperty)) return true;

            if (!expectedType.IsGenericType)
            {
                mainProperty = null;
                return false;
            }

            var expectedGenericType = expectedType.GetGenericTypeDefinition();
            if (!MainPropertyMapping.TryGetValue(expectedGenericType, out var genericMainProperty))
            {
                mainProperty = null;
                return false;
            }

            var boundGenericMainProperty = expectedType.GetProperty(genericMainProperty.Property.Name) ??
                throw new ConfluxException($"Could not bind generic main property \"{genericMainProperty.Property}\" to specialized type \"{expectedType}\".");

            mainProperty = new MainPropertyInfo(genericMainProperty.Attribute, boundGenericMainProperty);
            return true;
        }

        private bool TryGetPolymorphicKey(Type type, [NotNullWhen(true)] out string? key)
        {
            foreach (var polyInfos in ConfluxPolymorphicLookup.ByBaseType.Values)
            {
                if (polyInfos.ByDerivedType.TryGetValue(type, out var polyInfo))
                {
                    key = polyInfo.Key;
                    return true;
                }
            }

            key = null;
            return false;
        }

        public bool Accepts(Type type)
        {
            if (_bypassedTypes.Value!.Contains(type))
            {
                return false;
            }

            return TryGetMainProperty(type, out _);
        }

        public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
        {
            throw new NotImplementedException("Deserialization is handled by ConfluxMainPropertyDeserializer.");
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
        {
            if (value == null)
            {
                serializer(null);
                return;
            }

            if (TryGetPolymorphicKey(type, out var polyKey))
            {
                emitter.Emit(new MappingStart(null, null, true, MappingStyle.Any));
                emitter.Emit(new Scalar(polyKey));
            }

            if (TryGetMainProperty(type, out var mainProperty) &&
                (mainProperty.Attribute.Force || CanInline(value, type, mainProperty.Property)))
            {
                var innerValue = mainProperty.Property.GetValue(value);
                serializer(innerValue);
            }
            else
            {
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

            if (TryGetPolymorphicKey(type, out var _))
            {
                emitter.Emit(new MappingEnd());
            }
        }

        private bool CanInline(object obj, Type type, PropertyInfo mainProp)
        {
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.Name == mainProp.Name) continue;

                var val = prop.GetValue(obj);
                if (!IsDefaultValue(prop, val))
                {
                    // We found a non-default extra property, so we cannot inline safely
                    return false;
                }
            }

            return true;
        }

        private bool IsDefaultValue(PropertyInfo prop, object? value)
        {
            if (value == null) return true;

            var defaultValueAttr = prop.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttr != null) return Equals(defaultValueAttr.Value, value);

            var type = prop.PropertyType;
            if (type.IsValueType) return Equals(Activator.CreateInstance(type), value);

            return false;
        }

        public record MainPropertyInfo(ConfluxMainPropertyAttribute Attribute, PropertyInfo Property);
    }
}