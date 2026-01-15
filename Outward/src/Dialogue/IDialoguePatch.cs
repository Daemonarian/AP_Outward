namespace OutwardArchipelago.Dialogue
{
    /// <summary>
    /// Represents a single patch that can be applied to a dialogue tree.
    /// </summary>
    /// <remarks>
    /// Implementations identify which dialogue tree they target via the <see cref="TreeID"/> property
    /// and perform in-place modifications when <see cref="ApplyPatch(IDialoguePatchContext)"/> is invoked.
    ///
    /// Implementers should assume the <see cref="IDialoguePatchContext.Tree"/> and its nodes are mutable
    /// and that multiple patches may be applied to the same tree in sequence. Patches do not need to be
    /// immutable, but they should assume that other patches may run before or after them.
    ///
    /// The matching rules used by the framework are defined by <see cref="DialogueTreeID"/>. A patch should
    /// set <see cref="TreeID"/> to a value that uniquely (or sufficiently) identifies the tree(s) it is
    /// intended to modify (by name, hash, or both).
    ///
    /// Thread-safety: patch application is expected to happen on the main thread; implementations that capture
    /// global state or invoke Unity-specific APIs must ensure they are called from the appropriate thread.
    /// </remarks>
    internal interface IDialoguePatch
    {
        /// <summary>
        /// The identity of the dialogue tree this patch targets.
        /// </summary>
        /// <value>
        /// A <see cref="DialogueTreeID"/> describing the tree by name and/or hash. The patch runner
        /// uses this value to decide which patches to apply to which dialogue trees.
        /// </value>
        /// <remarks>
        /// Provide as much identifying information as available. For example:
        /// - Use <see cref="DialogueTreeID.FromTree(NodeCanvas.DialogueTrees.DialogueTreeExt)"/> when creating
        ///   a patch that is auto-generated from an existing tree.
        /// - Use <see cref="DialogueTreeID.FromName(string)"/> or <see cref="DialogueTreeID.FromHash(ulong)"/>
        ///   when targeting trees by a stable name or precomputed hash.
        ///
        /// The framework treats name and hash with "soft equality": if both patches and tree provide a given
        /// field (name or hash) they must match; otherwise a missing field is ignored (see <see cref="DialogueTreeID.Matches"/>).
        /// </remarks>
        public abstract DialogueTreeID TreeID { get; }

        /// <summary>
        /// Applies the patch to the provided dialogue patch context.
        /// </summary>
        /// <param name="context">
        /// The dialogue patch context that provides access to the dialogue tree, its nodes, and related metadata.
        /// Cannot be <c>null</c>.
        /// </param>
        /// <remarks>
        /// Implementations should:
        /// - Use <see cref="IDialoguePatchContext.NodesByID"/> to find nodes by their original IDs when needed.
        /// - Modify <see cref="IDialoguePatchContext.Tree"/> and its contained nodes to apply the desired changes.
        /// - Avoid breaking references or creating inconsistent state; if structural changes require mapping old IDs
        ///   to new ones, document the behavior.
        ///
        /// Exceptions:
        /// - Implementations may throw other exceptions to indicate unrecoverable errors; the caller is responsible
        ///   for handling them according to the patching workflow.
        /// </remarks>
        public abstract void ApplyPatch(IDialoguePatchContext context);
    }
}
