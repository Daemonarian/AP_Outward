using System.Diagnostics.CodeAnalysis;
using Conflux.Schema.Exceptions;

namespace Conflux.Schema.Context
{
    internal static class NodeCanvasObjectBuilder
    {
        [return: NotNullIfNotNull(nameof(source))]
        public static TNodeCanvas? Build<TConflux, TNodeCanvas>(NodeCanvasGraphContext context, TConflux? source)
        {
            if (source is null)
            {
                return default;
            }

            if (source is INodeCanvasObjectBuilder<TNodeCanvas> builder)
            {
                return builder.BuildNodeCanvasObject(context);
            }

            if (source is INodeCanvasObjectBuilder genericBuilder &&
                genericBuilder.TryBuildNodeCanvasObject<TNodeCanvas>(context, out var genericTarget))
            {
                return genericTarget;
            }

            if (source is TNodeCanvas target)
            {
                return target;
            }

            throw new ConfluxException($"Cannot build NodeCanvas type {nameof(TNodeCanvas)} from Conflux type {nameof(TConflux)}.");
        }
    }

    internal interface INodeCanvasObjectBuilder<T>
    {
        [return: NotNull]
        public T BuildNodeCanvasObject(NodeCanvasGraphContext context);
    }

    internal interface INodeCanvasObjectBuilder
    {
        public bool TryBuildNodeCanvasObject<T>(NodeCanvasGraphContext context, [NotNullWhen(true)] out T? target);
    }
}
