using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using System.Collections.Generic;
using System.Linq;

namespace OutwardArchipelago.Dialogue
{
    /// <summary>
    /// Provides contextual information for dialogue patching operations, including access to the dialogue tree and its
    /// nodes.
    /// </summary>
    internal class DialoguePatchContext : IDialoguePatchContext
    {
        public DialogueTreeID TreeID { get; private set; }

        public DialogueTreeExt Tree { get; private set; }

        public IReadOnlyDictionary<int, Node> NodesByID { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DialoguePatchContext class using the specified dialogue tree.
        /// </summary>
        /// <param name="tree">The dialogue tree to use as the context for patching operations. Cannot be null.</param>
        public DialoguePatchContext(DialogueTreeExt tree)
        {
            TreeID = DialogueTreeID.FromTree(tree);
            Tree = tree;
            NodesByID = tree.allNodes.ToDictionary((node) => node.ID);
        }
    }
}
