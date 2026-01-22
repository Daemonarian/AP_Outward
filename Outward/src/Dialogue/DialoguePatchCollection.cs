using System.Collections.Generic;
using NodeCanvas.DialogueTrees;
using OutwardArchipelago.Dialogue.Patches;

namespace OutwardArchipelago.Dialogue
{
    /// <summary>
    /// Internal collection that stores and provides lookup for <see cref="IDialoguePatch"/> instances.
    /// </summary>
    /// <remarks>
    /// The collection indexes patches by combinations of a dialogue tree's name and hash so callers can
    /// efficiently obtain patches that apply to a particular <see cref="DialogueTreeExt"/>.
    /// Lookup order used by <see cref="PatchesByDialogueTree"/>:
    /// 1. Patches registered for both name and hash.
    /// 2. Patches registered for name-only.
    /// 3. Patches registered for hash-only.
    /// 4. Patches registered without name or hash (other/global patches).
    /// </remarks>
    internal class DialoguePatchCollection : IReadOnlyDialoguePatchCollection
    {
        /// <summary>
        /// Index of patches keyed first by tree name, then by tree hash.
        /// Used for patches that specify both a Name and a Hash.
        /// </summary>
        private readonly Dictionary<string, Dictionary<ulong, List<IDialoguePatch>>> ByNameAndHash = new();

        /// <summary>
        /// Index of patches keyed by tree name only.
        /// Used for patches that specify a Name but no Hash.
        /// </summary>
        private readonly Dictionary<string, List<IDialoguePatch>> ByName = new();

        /// <summary>
        /// Index of patches keyed by tree hash only.
        /// Used for patches that specify a Hash but no Name.
        /// </summary>
        private readonly Dictionary<ulong, List<IDialoguePatch>> ByHash = new();

        /// <summary>
        /// Patches that do not specify a Name or Hash and therefore apply generically.
        /// </summary>
        private readonly List<IDialoguePatch> OtherPatches = new();

        /// <summary>
        /// Enumerates patches applicable to the provided dialogue <paramref name="tree"/>.
        /// </summary>
        /// <param name="tree">The dialogue tree to find patches for.</param>
        /// <returns>
        /// An enumerable sequence of <see cref="IDialoguePatch"/> instances that were registered
        /// for the given tree, in the resolution order described in the class remarks.
        /// </returns>
        public IEnumerable<IDialoguePatch> PatchesByDialogueTree(DialogueTreeExt tree)
        {
            var id = DialogueTreeID.FromTree(tree);

            if (id.Name != null && id.Hash.HasValue)
            {
                if (ByNameAndHash.TryGetValue(id.Name, out var byHash) && byHash.TryGetValue(id.Hash.Value, out var patches))
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

            if (id.Hash.HasValue)
            {
                if (ByHash.TryGetValue(id.Hash.Value, out var patches))
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
        /// The patch is stored in the most specific index that its <see cref="IDialoguePatch"/> supports:
        /// - name + hash -> <see cref="ByNameAndHash"/>
        /// - name only -> <see cref="ByName"/>
        /// - hash only -> <see cref="ByHash"/>
        /// - neither -> <see cref="OtherPatches"/>
        /// </summary>
        /// <param name="patch">The patch to register.</param>
        public void Register(DialogueTreeID id, IDialoguePatch patch)
        {
            if (id.Name != null && id.Hash.HasValue)
            {
                if (!ByNameAndHash.TryGetValue(id.Name, out var byHash))
                {
                    byHash = new();
                    ByNameAndHash.Add(id.Name, byHash);
                }

                if (!byHash.TryGetValue(id.Hash.Value, out var patches))
                {
                    patches = new();
                    byHash.Add(id.Hash.Value, patches);
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
            else if (id.Hash.HasValue)
            {
                if (!ByHash.TryGetValue(id.Hash.Value, out var patches))
                {
                    patches = new();
                    ByHash.Add(id.Hash.Value, patches);
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
