using System.Diagnostics.CodeAnalysis;
using Conflux.Schema.Exceptions;

namespace Conflux.NodeCanvas
{
    internal static class ConfluxObjectBuilder
    {
        [return: NotNullIfNotNull(nameof(source))]
        public static TConflux? Build<TNodeCanvas, TConflux>(Graph graph, TNodeCanvas? source)
        {
            if (source is null)
            {
                return default;
            }

            if (source is IConfluxObjectBuilder<TConflux> builder)
            {
                return builder.BuildConfluxObject(graph);
            }

            if (source is IConfluxObjectBuilder genericBuilder &&
                genericBuilder.TryBuildConfluxObject<TConflux>(graph, out var genericTarget))
            {
                return genericTarget;
            }

            if (source is TConflux target)
            {
                return target;
            }

            throw new ConfluxException($"Cannot build Conflux type {nameof(TConflux)} from NodeCanvas type {nameof(TNodeCanvas)}.");
        }
    }

    internal interface IConfluxObjectBuilder<T>
    {
        [return: NotNull]
        public T BuildConfluxObject(Graph graph);
    }

    internal interface IConfluxObjectBuilder
    {
        public bool TryBuildConfluxObject<T>(Graph graph, [NotNullWhen(true)] out T? target);
    }
}
