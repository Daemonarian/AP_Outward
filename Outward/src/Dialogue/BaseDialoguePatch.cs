namespace OutwardArchipelago.Dialogue
{
    /// <summary>
    /// Provides a base implementation for dialogue patch operations, associating each patch with a specific dialogue
    /// hash.
    /// </summary>
    /// <remarks>This abstract class serves as a foundation for custom dialogue patch types. Derived classes
    /// should implement the <see cref="IDialoguePatch"/> interface and provide logic for applying patches to dialogue
    /// contexts. Instances are identified by their <see cref="DialogueHash"/>, which uniquely represents the target
    /// dialogue.</remarks>
    internal abstract class BaseDialoguePatch : IDialoguePatch
    {
        public DialogueTreeID TreeID { get; private set; }

        public abstract void ApplyPatch(IDialoguePatchContext context);

        public BaseDialoguePatch(DialogueTreeID treeID)
        {
            TreeID = treeID;
        }
    }
}
