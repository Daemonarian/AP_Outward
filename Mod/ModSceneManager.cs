using System;
using System.Collections;
using HarmonyLib;
using UnityEngine.SceneManagement;

namespace OutwardArchipelago
{
    /// <summary>
    /// Encapsulates some mod-specific logic for managing scenes.
    /// </summary>
    internal class ModSceneManager
    {
        /// <summary>
        /// Singleton pattern.
        /// </summary>
        private static readonly ModSceneManager _instance = new();

        /// <summary>
        /// Singleton pattern.
        /// </summary>
        public static ModSceneManager Instance => _instance;

        /// <summary>
        /// The name of the main menu scene, so we can detect when we enter the main menu.
        /// </summary>
        private const string MAIN_MENU_SCENE_NAME = "MainMenu_Empty";

        /// <summary>
        /// Whether or not we were in a main menu.
        /// </summary>
        private bool _isInMainMenu = true;

        /// <summary>
        /// Keep constructor private to enforce Singleton pattern.
        /// </summary>
        private ModSceneManager()
        {
            SceneManager.sceneLoaded += OnSceneManagerSceneLoaded;
        }

        /// <summary>
        /// Called when a game scene is loaded in our mod's Archipelago game mode.
        /// 
        /// Will be invoked from the main thread.
        /// </summary>
        public event Action OnArchipelagoSceneReady;

        /// <summary>
        /// Called when a game scene is loaded in our mod's Archipelago game mode for the first time in a save file.
        /// 
        /// Will be invoked from the main thread.
        /// </summary>
        public event Action OnArchipelagoSceneReadyFirstTime;

        /// <summary>
        /// Called when the player enters an Archipelago game.
        /// 
        /// Will be invoked from the main thread.
        /// </summary>
        public event Action OnEnterArchipelagoGame;

        /// <summary>
        /// Called when the player enters the main menu.
        /// 
        /// Will be invoked from the main thread.
        /// </summary>
        public event Action OnEnterMainMenu;

        /// <summary>
        /// Callback for SceneManager.sceneLoaded.
        /// </summary>
        /// <param name="scene">The scene that was loaded.</param>
        /// <param name="loadSceneMode">The mode the scene was loaded in.</param>
        private void OnSceneManagerSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == MAIN_MENU_SCENE_NAME)
            {
                if (!_isInMainMenu)
                {
                    _isInMainMenu = true;
                    OnEnterMainMenu?.Invoke();
                }
            }
        }

        /// <summary>
        /// Callback for NetworkLevelLoader.onOverallLoadingDone.
        /// </summary>
        private void OnFinishLoadLevel()
        {
            if (OutwardArchipelagoMod.Instance is not null && OutwardArchipelagoMod.Instance.IsArchipelagoEnabled)
            {
                if (_isInMainMenu)
                {
                    _isInMainMenu = false;
                    OutwardArchipelagoMod.Log.LogDebug($"{nameof(ModSceneManager)}.{nameof(OnEnterArchipelagoGame)} {SceneManagerHelper.ActiveSceneName}");
                    OnEnterArchipelagoGame?.Invoke();
                }

                if (NetworkLevelLoader.Instance?.LevelLoadedForFirstTime == true)
                {
                    OutwardArchipelagoMod.Log.LogDebug($"{nameof(ModSceneManager)}.{nameof(OnArchipelagoSceneReadyFirstTime)} {SceneManagerHelper.ActiveSceneName}");
                    OnArchipelagoSceneReadyFirstTime?.Invoke();
                }

                OutwardArchipelagoMod.Log.LogDebug($"{nameof(ModSceneManager)}.{nameof(OnArchipelagoSceneReady)} {SceneManagerHelper.ActiveSceneName}");
                OnArchipelagoSceneReady?.Invoke();
            }
        }

        /// <summary>
        /// Patch to detect when Outward has finished loading and syncing the level.
        /// </summary>
        [HarmonyPatch(typeof(NetworkLevelLoader), nameof(NetworkLevelLoader.FinishLoadLevel), new Type[] { })]
        private static class NetworkLevelLoader_FinishLoadLevel
        {
            private static IEnumerator Postfix(IEnumerator __result)
            {
                while (__result.MoveNext())
                {
                    yield return __result.Current;
                }

                yield return null;

                Instance?.OnFinishLoadLevel();
            }
        }
    }
}
