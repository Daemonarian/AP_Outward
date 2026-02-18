using System.Collections.Generic;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Scenes.Patches;
using OutwardArchipelago.Scenes.Patches.GameObjects;

namespace OutwardArchipelago.Scenes
{
    internal class ScenePatcher
    {
        private static readonly ScenePatcher _instance = new();

        public static ScenePatcher Instance => _instance;

        private readonly Dictionary<string, List<IScenePatch>> SceneNameToPatches = new();

        private ScenePatcher()
        {
            RegisterAllPatches();
            ModSceneManager.Instance.OnArchipelagoSceneReadyFirstTime += PatchScene;
            OutwardArchipelagoMod.Log.LogDebug($"scene patching subsystem loaded and registered");
        }

        /// <summary>
        /// Apply patches to the current scene.
        /// </summary>
        private void PatchScene()
        {
            if (SceneNameToPatches.TryGetValue(SceneManagerHelper.ActiveSceneName, out var patches))
            {
                OutwardArchipelagoMod.Log.LogDebug($"scene \"{SceneManagerHelper.ActiveSceneName}\" loaded for the first time, applying patches...");

                foreach (var patch in patches)
                {
                    patch.ApplyPatch();
                }

                OutwardArchipelagoMod.Log.LogDebug($"all scene patches applied");
            }
        }

        private void RegisterPatch(string sceneName, IScenePatch patch)
        {
            patch.Validate();

            if (!SceneNameToPatches.TryGetValue(sceneName, out var patches))
            {
                patches = new List<IScenePatch>();
                SceneNameToPatches[sceneName] = patches;
            }

            patches.Add(patch);
        }

        private void RegisterAllPatches()
        {
            RegisterPatch(
                OutwardSceneName.AncestorsRestingPlace,
                new ReplaceGameObjectPatch
                {
                    Target = new ExistingGameObjectBuilder { Path = "Interactions/NoteBalthazar/mdl_env_propMapAuraiB" },
                    Replacement = new APItemGameObjectBuilder { Location = APWorld.Location.BalthazarNote },
                });
        }
    }
}
