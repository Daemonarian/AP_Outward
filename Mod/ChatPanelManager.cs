using OutwardArchipelago.Archipelago;
using UnityEngine;

namespace OutwardArchipelago
{
    /// <summary>
    /// Encapsulates the logic of dealing with the chat panel in Outward.
    /// </summary>
    internal class ChatPanelManager
    {
        /// <summary>
        /// Singleton pattern.
        /// </summary>
        private static readonly ChatPanelManager _instance = new();

        /// <summary>
        /// Made private to support Singleton pattern.
        /// </summary>
        private ChatPanelManager() { }

        /// <summary>
        /// Singleton pattern.
        /// </summary>
        public static ChatPanelManager Instance => _instance;

        /// <summary>
        /// Send a system message (with no visible sender) to the chat panel of a character.
        /// 
        /// The message may contain xml formatting.
        /// If no character is specified, it will be sent to the first local player.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="character">The recipient.</param>
        public void SendSystemMessage(string message, Character character = null)
        {
            character ??= CharacterManager.Instance.GetFirstLocalCharacter();

            // This code mostly adapts the implementation of ChatPanel.ChatMessageReceived.
            var chatPanel = character.CharacterUI.ChatPanel;
            ChatEntry chatEntry;
            if (chatPanel.m_messageArchive.Count < chatPanel.MaxMessageCount)
            {
                chatEntry = UnityEngine.Object.Instantiate<ChatEntry>(UIUtilities.ChatEntryPrefab);
                chatEntry.transform.SetParent(chatPanel.m_chatDisplay.content);
                chatEntry.transform.ResetLocal(true);
                chatEntry.SetCharacterUI(chatPanel.m_characterUI);
                chatPanel.m_messageArchive.Insert(0, chatEntry);
            }
            else
            {
                chatEntry = chatPanel.m_messageArchive[chatPanel.m_messageArchive.Count - 1];
                chatPanel.m_messageArchive.RemoveAt(chatPanel.m_messageArchive.Count - 1);
                chatPanel.m_messageArchive.Insert(0, chatEntry);
            }

            if (chatEntry is not null)
            {
                chatPanel.m_messageArchive[0].transform.SetAsLastSibling();

                if (chatEntry.m_lblPlayerName is not null)
                {
                    chatEntry.m_lblPlayerName.text = string.Empty;
                }

                if (chatEntry.m_lblMessage is not null)
                {
                    chatEntry.m_lblMessage.text = message;
                }
            }

            chatPanel.m_lastHideTime = Time.time;
            if (!chatPanel.IsDisplayed)
            {
                chatPanel.Show();
            }

            chatPanel.Invoke("DelayedScroll", 0.1f);
        }

        /// <summary>
        /// Called when ChatPanel.SendChatMessage would be invoked.
        /// 
        /// We will use this to implement an AP chat command.
        /// </summary>
        /// <param name="chatPanel">The chat panel.</param>
        /// <returns>Whether to continue running the normal method.</returns>
        public bool ChatPanel_SendChatMessage(ChatPanel chatPanel)
        {
            var message = chatPanel.m_chatEntry.text;
            if (message.StartsWith("/ap ", System.StringComparison.InvariantCultureIgnoreCase))
            {
                var apMessage = message.Substring("/ap ".Length);
                ArchipelagoConnector.Instance.Messages.SendMessage(apMessage);

                chatPanel.m_chatEntry.text = string.Empty;
                chatPanel.HideInput();

                return false;
            }

            return true;
        }
    }
}
