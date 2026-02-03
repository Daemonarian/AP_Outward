using UnityEngine;
using UnityEngine.UI;

namespace OutwardArchipelago.Archipelago
{
    public class ArchipelagoConnectionStatus : MonoBehaviour
    {
        // Connection status icon
        private GameObject ConnectionStatusIconObject;
        private Image ConnectionStatusIconImage;
        private Texture2D ConnectionStatusIconConnectedTexture;
        private Texture2D ConnectionStatusIconDisconnectedTexture;
        private Sprite ConnectedSprite;
        private Sprite DisconnectedSprite;
        private bool ConnectionStatusIconIsConnected;

        internal void Awake()
        {
            ConnectionStatusIconConnectedTexture = LoadTexture("archipelago_connected.png");
            ConnectionStatusIconDisconnectedTexture = LoadTexture("archipelago_disconnected.png");
        }

        private void Update()
        {
            if (ConnectionStatusIconObject == null)
            {
                CreateStatusUI();
            }

            if (ConnectionStatusIconImage != null && ArchipelagoConnector.Instance.IsConnected != ConnectionStatusIconIsConnected)
            {
                ConnectionStatusIconImage.sprite = ArchipelagoConnector.Instance.IsConnected ? ConnectedSprite : DisconnectedSprite;
                ConnectionStatusIconIsConnected = ArchipelagoConnector.Instance.IsConnected;
            }
        }

        private void CreateStatusUI()
        {
            ConnectedSprite = CreateSprite(ConnectionStatusIconConnectedTexture);
            DisconnectedSprite = CreateSprite(ConnectionStatusIconDisconnectedTexture);

            // 1. Create a "Canvas" object to hold our UI
            var canvasObj = new GameObject("ArchipelagoConnectionStatusCanvas");
            DontDestroyOnLoad(canvasObj); // Keep it between loading screens

            var canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay; // Draw on top of everything
            canvas.sortingOrder = 999; // Ensure it's on top of Outward's UI

            // Add a scaler so it doesn't look tiny on 4K screens
            var scaler = canvasObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            // 2. Create the Icon Object
            ConnectionStatusIconObject = new GameObject("ArchipelagoConnectionIcon");
            ConnectionStatusIconObject.transform.SetParent(canvasObj.transform, false);

            // 3. Add the Image Component
            ConnectionStatusIconImage = ConnectionStatusIconObject.AddComponent<Image>();
            ConnectionStatusIconImage.sprite = DisconnectedSprite;
            ConnectionStatusIconIsConnected = false;

            // 4. Position it (Bottom Left corner example)
            var rect = ConnectionStatusIconImage.rectTransform;
            rect.anchorMin = new Vector2(1, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(1, 1);
            rect.anchoredPosition = new Vector2(-16, -16);
            rect.sizeDelta = new Vector2(64, 64);
        }

        // Helper to load texture from plugin folder
        private Texture2D LoadTexture(string fileName)
        {
            var tex = Texture2D.whiteTexture;

            var assetData = OutwardArchipelagoMod.Instance.LoadAsset(fileName);
            if (assetData != null)
            {
                tex = new Texture2D(2, 2);
                tex.LoadImage(assetData);
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
