using System.ComponentModel;
using Conflux.NodeCanvas.Serialization;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.ObjectGraphVisitors;

namespace Conflux.Schema.Serialization
{
    internal class ConfluxDefaultValuesVisitor(IObjectGraphVisitor<IEmitter> nextVisitor)
        : ChainedObjectGraphVisitor(nextVisitor)
    {
        public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context, ObjectSerializer serializer)
        {
            if (value.Value is IDefaultCheckable checkable)
            {
                var defaultValueAttr = key.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultValueAttr is not null &&
                    checkable.IsDefaultValue(defaultValueAttr.Value))
                {
                    return false;
                }
            }

            return base.EnterMapping(key, value, context, serializer);
        }
    }
}
