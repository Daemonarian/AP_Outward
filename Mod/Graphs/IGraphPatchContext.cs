using System.Collections.Generic;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Graphs
{
    /// <summary>
    /// Provides contextual information for Node Graph patch operations, including access to nodes by their
    /// unique identifiers.
    /// </summary>
    internal interface IGraphPatchContext
    {
        /// <summary>
        /// The <see cref="GraphOwner"/> that owns the dialogue tree being patched.
        /// </summary>
        abstract GraphOwner GraphOwner { get; }

        /// <summary>
        /// The <see cref="Graph"/> instance being patched.
        /// </summary>
        abstract Graph Graph { get; }

        /// <summary>
        /// The name of the <see cref="Graph"/>, used for identifying specific graphs that appear in multiple places.
        /// </summary>
        abstract string Name { get; }

        /// <summary>
        /// The path to the <see cref="GraphOwner"/> in the scene hierarchy, used for identifying specific graph instances.
        /// </summary>
        abstract string Path { get; }

        /// <summary>
        /// All the original nodes in the graph by their original ID.
        /// </summary>
        abstract IReadOnlyDictionary<int, Node> NodesByID { get; }
    }
}