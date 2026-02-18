using OutwardArchipelago.Scenes.Patches.GameObjects;
using UnityEngine;

namespace OutwardArchipelago.Scenes.Patches
{
    /// <summary>
    /// Patches a scene by replacing one game object with another.
    /// </summary>
    internal class ReplaceGameObjectPatch : IScenePatch
    {
        /// <summary>
        /// The Unity game object to replace.
        /// </summary>
        public IGameObjectBuilder Target { get; set; }

        /// <summary>
        /// The Unity game object with which to replace the target.
        /// </summary>
        public IGameObjectBuilder Replacement { get; set; }

        public void Validate()
        {
            Target.Validate();
            Replacement.Validate();
        }

        public void ApplyPatch()
        {
            var target = Target.Build();
            var replacement = Replacement.Build();

            replacement.transform.SetParent(target.transform.parent ?? EnvironmentConditions.Instance.transform);
            replacement.transform.position = target.transform.position;
            replacement.transform.rotation = target.transform.rotation;

            target.SetActive(false);
            GameObject.Destroy(target);

            OutwardArchipelagoMod.Log.LogDebug($"replaced game object \"{target}\" with \"{replacement}\"");
        }
    }
}
