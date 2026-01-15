using NodeCanvas.DialogueTrees;
using System.Collections.Generic;

namespace OutwardArchipelago.Dialogue
{
    /// <summary>
    /// Represents a read-only collection of dialogue patches.
    /// </summary>
    /// <remarks>
    /// Implementations expose query-only access to patches that apply to dialogue trees.
    /// Callers should treat returned sequences as immutable; implementations should prefer
    /// returning an empty sequence when no patches match rather than <c>null</c>.
    /// </remarks>
    internal interface IReadOnlyDialoguePatchCollection
    {
        /// <summary>
        /// Retrieves all dialogue patches associated with the specified dialogue tree.
        /// </summary>
        /// <param name="tree">The dialogue tree for which to obtain patches.</param>
        /// <returns>
        /// A sequence of <see cref="IDialoguePatch"/> instances that apply to <paramref name="tree"/>.
        /// If no patches apply, an empty sequence should be returned.
        /// </returns>
        public abstract IEnumerable<IDialoguePatch> PatchesByDialogueTree(DialogueTreeExt tree);
    }
}
