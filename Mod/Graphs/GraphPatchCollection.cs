using System.Collections.Generic;
using OutwardArchipelago.Graphs.Patches;

namespace OutwardArchipelago.Graphs
{
    /// <summary>
    /// Internal collection that stores and provides lookup for <see cref="IGraphPatch"/> instances.
    /// </summary>
    /// <remarks>
    /// The collection indexes patches by combinations of a dialogue tree's name and hash so callers can
    /// efficiently obtain patches that apply to a particular <see cref="IGraphPatchContext"/>.
    /// Lookup order used by <see cref="GetPatchesForGraphContext"/>:
    /// 1. Patches registered for path and name combination.
    /// 2. Patches registered for path-only.
    /// 3. patches registered for name-only.
    /// 4. Patches registered global use.
    /// </remarks>
    internal class GraphPatchCollection : IReadOnlyGraphPatchCollection
    {
        /// <summary>
        /// Index of patches keyed first by the graph owner's path in the scene hierarchy, then by the graph name.
        /// Used for patches that specify both a Path and a Name.
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, List<IGraphPatch>>> ByPathAndName = new();

        /// <summary>
        /// Index of patches keyed by the graph owner's path in the scene hierarchy.
        /// Used for patches that specify a Path but no Name.
        /// </summary>
        private readonly Dictionary<string, List<IGraphPatch>> ByPath = new();

        /// <summary>
        /// Index of patches keyed by graph name only.
        /// Used for patches that specify a Name but no Path.
        /// </summary>
        private readonly Dictionary<string, List<IGraphPatch>> ByName = new();

        /// <summary>
        /// Patches that do not specify a Path or Name and therefore apply generically.
        /// </summary>
        private readonly List<IGraphPatch> OtherPatches = new();

        /// <summary>
        /// Enumerates patches applicable to the provided graph <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The graph context to find patches for.</param>
        /// <returns>
        /// An enumerable sequence of <see cref="IGraphPatch"/> instances that were registered
        /// for the given context, in the resolution order described in the class remarks.
        /// </returns>
        public IEnumerable<IGraphPatch> GetPatchesForGraphContext(IGraphPatchContext context)
        {
            var id = GraphID.FromContext(context);

            if (id.Path != null && id.Name != null)
            {
                if (ByPathAndName.TryGetValue(id.Path, out var byName) && byName.TryGetValue(id.Name, out var patches))
                {
                    foreach (var patch in patches)
                    {
                        yield return patch;
                    }
                }
            }

            if (id.Path != null)
            {
                if (ByPath.TryGetValue(id.Path, out var patches))
                {
                    foreach (var patch in patches)
                    {
                        yield return patch;
                    }
                }
            }

            if (id.Name != null)
            {
                if (ByName.TryGetValue(id.Name, out var patches))
                {
                    foreach (var patch in patches)
                    {
                        yield return patch;
                    }
                }
            }

            foreach (var patch in OtherPatches)
            {
                yield return patch;
            }
        }

        /// <summary>
        /// Register a patch in the collection.
        /// The patch is stored in the most specific index that its <see cref="IGraphPatch"/> supports:
        /// - path + name -> <see cref="ByPathAndName"/>
        /// - path only -> <see cref="ByPath"/>
        /// - name only -> <see cref="ByName"/>
        /// - neither -> <see cref="OtherPatches"/>
        /// </summary>
        /// <param name="patch">The patch to register.</param>
        public void Register(GraphID id, IGraphPatch patch)
        {
            if (id.Path != null && id.Name != null)
            {
                if (!ByPathAndName.TryGetValue(id.Path, out var byName))
                {
                    byName = new();
                    ByPathAndName.Add(id.Path, byName);
                }

                if (!byName.TryGetValue(id.Name, out var patches))
                {
                    patches = new();
                    byName.Add(id.Name, patches);
                }

                patches.Add(patch);
            }
            else if (id.Path != null)
            {
                if (!ByPath.TryGetValue(id.Path, out var patches))
                {
                    patches = new();
                    ByPath.Add(id.Path, patches);
                }

                patches.Add(patch);
            }
            else if (id.Name != null)
            {
                if (!ByName.TryGetValue(id.Name, out var patches))
                {
                    patches = new();
                    ByName.Add(id.Name, patches);
                }

                patches.Add(patch);
            }
            else
            {
                OtherPatches.Add(patch);
            }
        }
    }
}
