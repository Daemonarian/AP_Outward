using UnityEngine;

namespace OutwardArchipelago.Scenes.Patches.GameObjects
{
    /// <summary>
    /// Represents an object that can build Unity game objects.
    /// </summary>
    internal interface IGameObjectBuilder
    {
        /// <summary>
        /// Validate that all the inputs to this builder are properly set.
        /// </summary>
        public void Validate();

        /// <summary>
        /// Build the Unity game object.
        /// </summary>
        /// <returns>The game object.</returns>
        public GameObject Build();
    }
}
