using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Serialization
{
    internal class ConfluxMainPropertyDeserializer : INodeDeserializer
    {
        public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value, ObjectDeserializer rootDeserializer)
        {
            if (!ConfluxMainPropertyInfo.TryGet(expectedType, out var mainProperty))
            {
                value = null;
                return false;
            }

            if (!mainProperty.IsForced && reader.Accept<MappingStart>(out _))
            {
                value = null;
                return false;
            }

            var propertyValue = nestedObjectDeserializer(reader, mainProperty.Property.PropertyType);
            value = Activator.CreateInstance(expectedType);
            mainProperty.Property.SetValue(value, propertyValue);

            return true;
        }
    }
}
