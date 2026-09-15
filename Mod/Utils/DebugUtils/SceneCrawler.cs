#if DEBUG

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OutwardArchipelago.Utils.DebugUtils
{
    public class SceneCrawler : MonoBehaviour
    {
        public static SceneCrawler Instance { get; private set; } = null;

        private bool isCrawling = false;

        private int currentSceneIndex = 0;

        public static void Create()
        {
            if (Instance is null)
            {
                var obj = new GameObject(nameof(SceneCrawler));
                obj.AddComponent<SceneCrawler>();
            }
        }

        protected void Awake()
        {
            // singleton pattern
            if (Instance is not null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            OutwardArchipelagoMod.Log.LogDebug("[SceneCrawler] Awake and listening for key press...");
        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.F12) && !isCrawling)
            {
                StartCrawling();
            }
        }

        private void StartCrawling()
        {
            OutwardArchipelagoMod.Log.LogDebug("[SceneCrawler] Starting automated scene crawl...");

            isCrawling = true;
            currentSceneIndex = 0;

            SceneManager.sceneLoaded += OnSceneLoaded;

            LoadNextScene();
        }

        private void StopCrawling()
        {
            isCrawling = false;

            SceneManager.sceneLoaded -= OnSceneLoaded;

            OutwardArchipelagoMod.Log.LogDebug("[SceneCrawler] Stopped automated scene crawl.");
        }

        private void LoadNextScene()
        {
            if (currentSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                var nextScene = SceneManager.GetSceneByBuildIndex(currentSceneIndex);
                OutwardArchipelagoMod.Log.LogDebug($"[SceneCrawler] Loading scene: {nextScene.name}");

                SceneManager.LoadScene(currentSceneIndex);
            }
            else
            {
                OutwardArchipelagoMod.Log.LogDebug("[SceneCrawler] Scene crawl complete!");
                StopCrawling();
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (!isCrawling)
            {
                return;
            }

            OutwardArchipelagoMod.Log.LogDebug($"[SceneCrawler] Loaded scene: {scene.name}");

            StartCoroutine(WaitAndLoadNext());
        }

        private IEnumerator WaitAndLoadNext()
        {
            yield return new WaitForSeconds(3f);

            currentSceneIndex++;
            LoadNextScene();
        }
    }
}

#endif