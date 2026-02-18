using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OutwardArchipelago.Scenes.Patches.GameObjects
{
    /// <summary>
    /// Builder which simply finds the specified pre-existing game object.
    /// </summary>
    internal class ExistingGameObjectBuilder : IGameObjectBuilder
    {
        /// <summary>
        /// The path of the Unity game object.
        /// </summary>
        public string Path { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Path))
            {
                throw new ArgumentNullException(nameof(Path));
            }
        }

        public GameObject Build()
        {
            var parts = Path.Split('/');

            var roots = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var root in roots)
            {
                if (root.name == parts[0])
                {
                    if (parts.Length == 1)
                    {
                        return root;
                    }

                    var remainingPath = string.Join("/", parts, 1, parts.Length - 1);
                    return root.transform?.Find(remainingPath)?.gameObject ?? throw new Exception($"could not find a game object with the specified path: {Path}");
                }
            }

            throw new Exception($"could not find the root object of the specified game object path: {Path}");
        }
    }
}
