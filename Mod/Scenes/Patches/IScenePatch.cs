namespace OutwardArchipelago.Scenes.Patches
{
    /// <summary>
    /// An object that knows how to apply a small patch to a Unity scene.
    /// </summary>
    internal interface IScenePatch
    {
        /// <summary>
        /// Check that all the parameters of this patch are valid.
        /// </summary>
        public void Validate();

        /// <summary>
        /// Apply this patch to the current scene.
        /// </summary>
        public void ApplyPatch();
    }
}
