using System.Collections.Generic;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue
{
    /// <summary>
    /// Provides contextual information for dialogue patch operations, including access to dialogue nodes by their
    /// unique identifiers.
    /// </summary>
    internal interface IDialoguePatchContext
    {
        /// <summary>
        /// The unique ID of the dialogue tree being patched.
        /// </summary>
        abstract DialogueTreeID TreeID { get; }

        /// <summary>
        /// The dialogue tree being patched.
        /// </summary>
        abstract DialogueTreeExt Tree { get; }

        /// <summary>
        /// All the original nodes in the dialogue tree by their original ID.
        /// </summary>
        abstract IReadOnlyDictionary<int, Node> NodesByID { get; }
    }
}