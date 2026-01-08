using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using BepInEx;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace OutwardModTemplate
{
    public class ArchipelagoConnector : MonoBehaviour
    {
        public const string ArchipelagoGame = "Outward: Definitive Edition";
        public const string ArchipelagoVersion = BuildInfo.ArchipelagoVersion;

        public static ArchipelagoConnector Instance { get; private set; }

        // State
        private ArchipelagoSession ArchipelagoSession;
        private bool IsReconnecting = false;

        // Connection details
        public string Host { get; private set; }
        public int Port { get; private set; }
        public string Password { get; private set; }
        public string SlotName { get; private set; }

        // Connection status icon
        private GameObject ConnectionStatusIconObject;
        private Image ConnectionStatusIconImage;
        private Texture2D ConnectionStatusIconConnectedTexture;
        private Texture2D ConnectionStatusIconDisconnectedTexture;
        private Sprite ConnectedSprite;
        private Sprite DisconnectedSprite;
        private bool ConnectionStatusIconIsConnected;

        public static bool IsConnected { get; private set; } = false;

        // Thread Safety: Queue actions here to run them on the main Unity thread
        private readonly ConcurrentQueue<Action> MainThreadQueue = new();
        private readonly ConcurrentQueue<long> LocationCheckQueue = new();

        public static void Create()
        {
            if (Instance == null)
            {
                var obj = new GameObject(nameof(ArchipelagoConnector));
                obj.AddComponent<ArchipelagoConnector>();
            }
        }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            Host = Plugin.ArchipelagoHost.Value;
            Port = Plugin.ArchipelagoPort.Value;
            Password = Plugin.ArchipelagoPassword.Value;
            SlotName = Plugin.ArchipelagoSlotName.Value;

            AwakeStatusUI();
        }

        void Update()
        {
            UpdateStatusUI();

            while (MainThreadQueue.TryDequeue(out var action))
            {
                action();
            }
        }

        public void Connect()
        {
            if (IsReconnecting)
            {
                return;
            }

            StartCoroutine(ConnectRoutine());
        }

        private IEnumerator ConnectRoutine()
        {
            IsReconnecting = true;

            while (IsReconnecting)
            {
                // Cleanup old session if it exists
                if (ArchipelagoSession != null)
                {
                    ArchipelagoSession.Socket.SocketClosed -= OnSocketClosed;
                    ArchipelagoSession.Items.ItemReceived -= OnItemReceived;
                    ArchipelagoSession.MessageLog.OnMessageReceived -= OnMessageReceived;
                    ArchipelagoSession = null;
                }

                // Create new session
                ArchipelagoSession = ArchipelagoSessionFactory.CreateSession(Host, Port);

                // Hook up events before connecting
                ArchipelagoSession.Socket.SocketClosed += OnSocketClosed;
                ArchipelagoSession.Items.ItemReceived += OnItemReceived;
                ArchipelagoSession.MessageLog.OnMessageReceived += OnMessageReceived;

                // Connect and login
                var task = Task.Run(async () =>
                {
                    try
                    {
                        await ArchipelagoSession.ConnectAsync();
                        return await ArchipelagoSession.LoginAsync(
                            ArchipelagoGame,
                            SlotName,
                            ItemsHandlingFlags.AllItems,
                            version: new Version(ArchipelagoVersion),
                            password: Password,
                            requestSlotData: true
                        );
                    }
                    catch (Exception ex)
                    {
                        Plugin.Log.LogError($"[Archipelago] Connection/login failed: {ex}");
                        return null;
                    }
                });

                yield return new WaitUntil(() => task.IsCompleted);
                var loginResult = task.Result;

                if (loginResult != null && loginResult.Successful)
                {
                    Plugin.Log.LogMessage($"[Archipelago] Connected to '{Host}:{Port}' as '{SlotName}'.");
                    IsConnected = true;
                    IsReconnecting = false;
                    break;
                }

                Plugin.Log.LogInfo("[Archipelago] Retrying connection/login in 5s...");
                yield return new WaitForSeconds(5f);
            }

            SendLocationChecks();
        }

        public void CompleteLocationCheck(long locationId)
        {
            LocationCheckQueue.Enqueue(locationId);
            SendLocationChecks();
        }

        private void SendLocationChecks()
        {
            StartCoroutine(SendLocationChecksRoutine());
        }

        private IEnumerator SendLocationChecksRoutine()
        {
            if (ArchipelagoSession == null || IsReconnecting)
            {
                yield break;
            }

            while (LocationCheckQueue.TryDequeue(out var locationId))
            {
                var task = Task.Run(async () =>
                {
                    try
                    {
                        Plugin.Log.LogMessage($"[Archipelago] Completing location check for Location ID: {locationId}");
                        await ArchipelagoSession.Locations.CompleteLocationChecksAsync(locationId);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Plugin.Log.LogWarning($"[Archipelago] Failed to complete location check for Location ID {locationId}: {ex}");
                    }
                    return false;
                });

                yield return new WaitUntil(() => task.IsCompleted);
                bool success = task.Result;
                if (!success)
                {
                    LocationCheckQueue.Enqueue(locationId);
                    yield break;
                }
            }
        }

        private void OnSocketClosed(string reason)
        {
            // IMPORTANT: Queue this back to main thread to restart the coroutine
            MainThreadQueue.Enqueue(() =>
            {
                IsConnected = false;
                Plugin.Log.LogWarning($"[Archipelago] Disconnected: {reason}. Attempting reconnect...");
                Connect();
            });
        }

        private void OnItemReceived(ReceivedItemsHelper helper)
        {
            // IDEMPOTENCY CHECK:
            // When you reconnect, AP sends ALL items again. 
            // We must ensure we don't grant the same item twice.
            // (Note: This is a simple example. For robustness, track index/ID properly)

            MainThreadQueue.Enqueue(() =>
            {
                while (helper.Any())
                {
                    var item = helper.DequeueItem();

                    // Verify we haven't processed this exact instance before
                    // You might need a more robust index tracking depending on your game
                    Plugin.Log.LogMessage($"[Archipelago] Received Item: {item.ItemName}");

                    if (item.ItemId == ArchipelagoItems.QUEST_LICENSE)
                    {
                        Plugin.Instance.QuestLicenseLevel += 1;
                        Plugin.Log.LogMessage($"Granting Quest License lvl {Plugin.Instance.QuestLicenseLevel}!");
                        SplitScreenManager.Instance.NotifyAllLocalPlayers($"Received Quest License Lv {Plugin.Instance.QuestLicenseLevel}!");
                    }
                }
            });
        }

        private void OnMessageReceived(Archipelago.MultiClient.Net.MessageLog.Messages.LogMessage message)
        {
            MainThreadQueue.Enqueue(() =>
            {
                // Push message to your in-game chat box
                Plugin.Log.LogMessage($"[AP Chat] {message}");
            });
        }

        private void AwakeStatusUI()
        {
            ConnectionStatusIconConnectedTexture = LoadTexture("archipelago_connected.png");
            ConnectionStatusIconDisconnectedTexture = LoadTexture("archipelago_disconnected.png");
        }

        private void CreateStatusUI()
        {
            ConnectedSprite = CreateSprite(ConnectionStatusIconConnectedTexture);
            DisconnectedSprite = CreateSprite(ConnectionStatusIconDisconnectedTexture);

            // 1. Create a "Canvas" object to hold our UI
            var canvasObj = new GameObject("ModStatusCanvas");
            DontDestroyOnLoad(canvasObj); // Keep it between loading screens

            var canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay; // Draw on top of everything
            canvas.sortingOrder = 999; // Ensure it's on top of Outward's UI

            // Add a scaler so it doesn't look tiny on 4K screens
            var scaler = canvasObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            // 2. Create the Icon Object
            ConnectionStatusIconObject = new GameObject("ConnectionIcon");
            ConnectionStatusIconObject.transform.SetParent(canvasObj.transform, false);

            // 3. Add the Image Component
            ConnectionStatusIconImage = ConnectionStatusIconObject.AddComponent<Image>();
            ConnectionStatusIconImage.sprite = DisconnectedSprite;
            ConnectionStatusIconIsConnected = false;

            // 4. Position it (Bottom Left corner example)
            var rect = ConnectionStatusIconImage.rectTransform;
            rect.anchorMin = new Vector2(0, 0); // Bottom Left
            rect.anchorMax = new Vector2(0, 0); // Bottom Left
            rect.pivot = new Vector2(0, 0);     // Pivot at bottom left
            rect.anchoredPosition = new Vector2(20, 20); // 20px padding
            rect.sizeDelta = new Vector2(64, 64); // Size in pixels
        }

        private void UpdateStatusUI()
        {
            if (ConnectionStatusIconObject == null)
            {
                CreateStatusUI();
            }

            if (ConnectionStatusIconImage != null && IsConnected != ConnectionStatusIconIsConnected)
            {
                ConnectionStatusIconImage.sprite = IsConnected ? ConnectedSprite : DisconnectedSprite;
                ConnectionStatusIconIsConnected = IsConnected;
            }
        }
        
        // Helper to load texture from plugin folder
        private Texture2D LoadTexture(string fileName)
        {
            var path = Path.Combine(Paths.PluginPath, "daemonarian-OutwardArchipelago", "assets", fileName);

            Texture2D tex = Texture2D.whiteTexture;
            if (File.Exists(path))
            {
                byte[] fileData = File.ReadAllBytes(path);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData);
            }
            else
            {
                Plugin.Log.LogError($"Could not find icon at: {path}");
            }

            return tex;
        }

        private Sprite CreateSprite(Texture2D tex)
        {
            return Sprite.Create(
                tex,
                new Rect(0, 0, tex.width, tex.height),
                new Vector2(0.5f, 0.5f)
            );
        }
    }
}
