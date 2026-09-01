using System.Collections.Generic;
using OutwardArchipelago.Graphs.Patches;

namespace OutwardArchipelago.Graphs
{
    /// <summary>
    /// Represents a read-only collection of graph patches.
    /// </summary>
    /// <remarks>
    /// Implementations expose query-only access to patches that apply to node graphs.
    /// Callers should treat returned sequences as immutable; implementations should prefer
    /// returning an empty sequence when no patches match rather than <c>null</c>.
    /// </remarks>
    internal interface IReadOnlyGraphPatchCollection
    {
        /// <summary>
        /// Enumerates the patches that have been registered for the given graph context.
        /// </summary>
        /// <param name="context">The graph context to patch.</param>
        /// <returns>An enumerable over the patches.</returns>
        abstract IEnumerable<IGraphPatch> GetPatchesForGraphContext(IGraphPatchContext context);
    }
}
