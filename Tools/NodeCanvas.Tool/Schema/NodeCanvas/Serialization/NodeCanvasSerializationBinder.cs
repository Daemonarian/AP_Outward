using System.Collections.Frozen;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Serialization
{
    internal class NodeCanvasSerializationBinder : DefaultSerializationBinder
    {
        public static NodeCanvasTypeLookup TypeLookup => _typeLookup.Value;

        private static readonly Lazy<NodeCanvasTypeLookup> _typeLookup = new(GetTypeLookup);

        private static NodeCanvasTypeLookup GetTypeLookup()
        {
            var infos = new List<NodeCanvasTypeInfo>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var typeAttribute = type.GetCustomAttribute<NodeCanvasTypeAttribute>();
                if (typeAttribute is null)
                {
                    continue;
                }

                var info = new NodeCanvasTypeInfo(typeAttribute.TypeName, type);
                infos.Add(info);
            }

            var byName = infos.ToFrozenDictionary(info => info.Name);
            var byType = infos.ToFrozenDictionary(info => info.Type);

            return new NodeCanvasTypeLookup(byName, byType);
        }

        public override void BindToName(Type serializedType, out string? assemblyName, out string? typeName)
        {
            if (TypeLookup.ByType.TryGetValue(serializedType, out var info))
            {
                assemblyName = null;
                typeName = info.Name;
            }
            else
            {
                base.BindToName(serializedType, out assemblyName, out typeName);
            }
        }

        public override Type BindToType(string? assemblyName, string typeName)
        {
            if (TypeLookup.ByName.TryGetValue(typeName, out var info))
            {
                return info.Type;
            }

            return base.BindToType(assemblyName, typeName);
        }

        public record NodeCanvasTypeInfo(string Name, Type Type);

        public record NodeCanvasTypeLookup(FrozenDictionary<string, NodeCanvasTypeInfo> ByName, FrozenDictionary<Type, NodeCanvasTypeInfo> ByType);
    }
}
