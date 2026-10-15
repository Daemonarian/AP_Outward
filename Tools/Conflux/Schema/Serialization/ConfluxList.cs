using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Context;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Serialization
{
    internal class ConfluxList<T> : INodeCanvasObjectBuilder
    {
        [ConfluxMainProperty(Force = true)]
        [YamlMember(Alias = "items")]
        public List<T>? Items { get; set; }

        public NodeCanvasList<T2> BuildNodeCanvasList<T2>(NodeCanvasGraphContext context) => new()
        {
            Items = Items is null ? null : [.. Items.Select(item => NodeCanvasObjectBuilder.Build<T, T2>(context, item)!)],
        };

        public bool TryBuildNodeCanvasObject<T2>(NodeCanvasGraphContext context, [NotNullWhen(true)] out T2? target)
        {
            if (typeof(T2).IsGenericType && typeof(T2).GetGenericTypeDefinition() == typeof(NodeCanvasList<>))
            {
                var buildMethod = GetType()
                    .GetMethod(nameof(BuildNodeCanvasList), BindingFlags.Instance)
                    ?.MakeGenericMethod(typeof(T2).GetGenericArguments());
                if (buildMethod is not null)
                {
                    target = (T2)buildMethod.Invoke(this, [context])!;
                    return true;
                }
            }

            target = default;
            return false;
        }
    }
}
