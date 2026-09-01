namespace OutwardArchipelago.Graphs.Patches
{
    /// <summary>
    /// Represents a single patch that can be applied to a graph.
    /// </summary>
    /// <remarks>
    /// Implementers should assume the <see cref="IGraphPatchContext.Graph"/> and its nodes are mutable
    /// and that multiple patches may be applied to the same graph in sequence. Patches do not need to be
    /// immutable, but they should assume that other patches may run before or after them.
    ///
    /// Thread-safety: patch application is expected to happen on the main thread; implementations that capture
    /// global state or invoke Unity-specific APIs must ensure they are called from the appropriate thread.
    /// </remarks>
    internal interface IGraphPatch
    {
        /// <summary>
        /// Applies the patch to the provided graph context.
        /// </summary>
        /// <param name="context">
        /// The graph patch context that provides access to the graph, its nodes, and related metadata.
        /// Cannot be <c>null</c>.
        /// </param>
        /// <remarks>
        /// Implementations should:
        /// - Use <see cref="IGraphPatchContext.NodesByID"/> to find nodes by their original IDs when needed.
        /// - Modify <see cref="IGraphPatchContext.Graph"/> and its contained nodes to apply the desired changes.
        /// - Avoid breaking references or creating inconsistent state; if structural changes require mapping old IDs
        ///   to new ones, document the behavior.
        ///
        /// Exceptions:
        /// - Implementations may throw other exceptions to indicate unrecoverable errors; the caller is responsible
        ///   for handling them according to the patching workflow.
        /// </remarks>
        abstract void ApplyPatch(IGraphPatchContext context);
    }
}
