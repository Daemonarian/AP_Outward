using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Conflux.NodeCanvas.Serialization
{
    internal class NodeCanvasContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (typeof(IDefaultCheckable).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = instance =>
                {
                    var propValue = property.ValueProvider?.GetValue(instance);
                    var defaultValue = member.GetCustomAttribute<DefaultValueAttribute>();
                    if (defaultValue is null)
                    {
                        return propValue is not null;
                    }

                    if (propValue is IDefaultCheckable checkablePropValue)
                    {
                        return !checkablePropValue.IsDefaultValue(defaultValue.Value);
                    }

                    return true;
                };
            }

            return property;
        }
    }
}
