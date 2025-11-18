using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace OutwardArchipelago
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        // Choose a GUID for your project. Change "myname" and "mymod".
        public const string GUID = "apoutward.daemonarium.com";
        // Choose a NAME for your project, generally the same as your Assembly Name.
        public const string NAME = "Outward Archipelago";
        // Increment the VERSION when you release a new version of your mod.
        public const string VERSION = "1.0.0";

        // For accessing your BepInEx Logger from outside of this class (eg Plugin.Log.LogMessage("");)
        internal static ManualLogSource Log;

        // Awake is called when your plugin is created. Use this to set up your mod.
        internal void Awake()
        {
            Log = this.Logger;
            Log.LogMessage($"Hello world from {NAME} {VERSION}!");

            // Harmony is for patching methods. If you're not patching anything, you can comment-out or delete this line.
            new Harmony(GUID).PatchAll();

            SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(OnSceneLoaded);
        }

        internal void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.ToLower().Contains("lowmemory") || scene.name.ToLower().Contains("mainmenu"))
            {
                return;
            }

            Log.LogMessage($"Scene loaded: {scene.name}");

            this.StartCoroutine(WaitForSceneReady());
        }

        private IEnumerator WaitForSceneReady()
        {
            while (!NetworkLevelLoader.Instance.IsOverallLoadingDone || !NetworkLevelLoader.Instance.AllPlayerDoneLoading)
            {
                yield return null;
            }

            Log.LogMessage("Scene ready");

            this.StartCoroutine(WaitForGameplayResumed());
        }

        private IEnumerator WaitForGameplayResumed()
        {
            while (NetworkLevelLoader.Instance.IsGameplayPaused)
            {
                yield return null;
            }

            Log.LogMessage("Gameplay has resumed");

            OnSceneFullyLoaded();
        }

        private void OnSceneFullyLoaded()
        {
            if (PhotonNetwork.isNonMasterClientInRoom)
            {
                Log.LogMessage("Not master client, skipping item spawn.");
                return;
            }

            if (SceneManagerHelper.ActiveSceneName == "CierzoTutorial")
            {
                Log.LogMessage("CierzoTutorial detected, checking item manager");

                List<Item> itemsToRemove = new List<Item>();
                foreach (var key in ItemManager.Instance.WorldItems.Keys)
                {
                    var worldItem = ItemManager.Instance.WorldItems[key];
                    Log.LogMessage($"World item found: '{key}' {worldItem.Name}-{worldItem.ItemID} at position {worldItem.transform.position}");
                    if (worldItem.ItemID == 5100060)
                    {
                        Log.LogMessage("Removing Makeshift Torch!");
                        itemsToRemove.Add(worldItem);
                    }
                }

                foreach (var item in itemsToRemove)
                {
                    var newItem = ItemManager.Instance.GenerateItemNetwork(3000250);
                    newItem.ChangeParent(null, item.transform.position, item.transform.rotation);
                    ItemManager.Instance.DestroyItem(item);
                }
            }
            else if (SceneManagerHelper.ActiveSceneName == "CierzoNewTerrain")
            {
                Log.LogMessage("CierzoNewTerrain detected, spawning bird mask.");
                Item birdMask = ItemManager.Instance.GenerateItemNetwork(3000250);
                birdMask.ChangeParent(null, new UnityEngine.Vector3(-172.2164f, -1500.047f, 759.5696f), UnityEngine.Quaternion.identity);
            }
        }

        [HarmonyPatch(typeof(CharacterInventory), nameof(CharacterInventory.TakeItem), new Type[] {typeof(Item), typeof(bool)})]
        public class CharacterInventory_TakeItem
        {
            static void Prefix(ref Item takenItem)
            {
                // https://harmony.pardeike.net/
                Plugin.Log.LogMessage($"CharacterInventory.TakeItem was called for item: {takenItem.Name} ({takenItem.ItemID})");

                if (takenItem.ItemID == 3000250)
                {
                    Plugin.Log.LogMessage("Replacing bird mask with makeshift torch.");

                    var newItem = ItemManager.Instance.GenerateItemNetwork(5100060);
                    newItem.ChangeParent(null, takenItem.transform.position, takenItem.transform.rotation);

                    ItemManager.Instance.DestroyItem(takenItem);

                    takenItem = newItem;
                }
            }
        }
    }
}
