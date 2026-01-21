using NodeCanvas.DialogueTrees;
using OutwardArchipelago.Dialogue.Patches;
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
        /// Enumerates the patches that have been registered for the given dialogue tree.
        /// </summary>
        /// <param name="tree">The dialogue tree to patch.</param>
        /// <returns>An enumerable over the patches.</returns>
        public abstract IEnumerable<IDialoguePatch> PatchesByDialogueTree(DialogueTreeExt tree);
    }
}
