using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HarmonyLib;
using UnityEngine;

namespace OutwardArchipelago
{
    /// <summary>
    /// Encapsulates the logic of dealing with the chat panel in Outward.
    /// </summary>
    internal class ChatPanelManager : MonoBehaviour
    {
        /// <summary>
        /// Only create the singleton instance when it is needed.
        /// </summary>
        private static readonly Lazy<ChatPanelManager> _instance = new(CreateInstance);

        public static ChatPanelManager Instance => _instance.Value;

        /// <summary>
        /// Create the ChatPanelManager game object and component.
        /// </summary>
        /// <returns>The newly created ChatPanelManager component.</returns>
        private static ChatPanelManager CreateInstance()
        {
            var obj = new GameObject(nameof(ChatPanelManager));
            DontDestroyOnLoad(obj);
            return obj.AddComponent<ChatPanelManager>();
        }

        /// <summary>
        /// Handle a chat command that has been entered into the chat panel.
        /// </summary>
        /// <param name="arg">The rest of the chat command.</param>
        /// <returns>Whether the chat command was handled or not.</returns>
        public delegate bool ChatCommandHandler(string arg);

        /// <summary>
        /// Pattern for valid chat command prefixes.
        /// </summary>
        private readonly Regex _chatCommandPrefixPattern = new(@"^\w+$", RegexOptions.ExplicitCapture | RegexOptions.Singleline);

        /// <summary>
        /// Pattern for matching chat commands typed into the chat panel.
        /// </summary>
        private readonly Regex _chatCommandPattern = new(@"^/(?<prefix>\w+)\s(?<arg>.*)$", RegexOptions.ExplicitCapture | RegexOptions.Singleline);

        /// <summary>
        /// The registered chat command handlers.
        /// </summary>
        private readonly Dictionary<string, ChatCommandHandler> _chatCommandHandlers = new();

        /// <summary>
        /// Queue of system messages to show in the chat panel when it becomes available.
        /// </summary>
        private readonly ConcurrentQueue<Message> _messages = new();

        /// <summary>
        /// Register a chat command with this chat panel manager.
        /// </summary>
        /// <param name="prefix">The chat command prefix.</param>
        /// <param name="handler">The chat command handler.</param>
        /// <returns>Whether the chat command was successfully registered.</returns>
        public bool RegisterChatCommand(string prefix, ChatCommandHandler handler)
        {
            if (!_chatCommandPrefixPattern.IsMatch(prefix))
            {
                OutwardArchipelagoMod.Log.LogError($"invalid chat command prefix: {prefix}");
                return false;
            }

            lock (_chatCommandHandlers)
            {
                _chatCommandHandlers[prefix] = handler;
            }

            return true;
        }

        /// <summary>
        /// Send a message to a local chat panel.
        /// </summary>
        /// <param name="content">A Unity UI string containing the contents of the message to be displayed.</param>
        /// <param name="senderName">The name of the sender to display before the message. May be <code>null</code> to not display a sender.</param>
        /// <param name="receiver">The chat panel to which to send the message.</param>
        public void SendChatMessage(string content, string senderName = null, ReceiverType receiver = ReceiverType.AllPlayers)
        {
            _messages.Enqueue(new(content, senderName, receiver));
        }

        public void Update()
        {
            if (OutwardArchipelagoMod.Instance.IsInGame)
            {
                var playerOneChatPanel = CharacterManager.Instance?.GetFirstLocalCharacter()?.CharacterUI?.ChatPanel;
                if (playerOneChatPanel?.enabled is true)
                {
                    var playerTwoChatPanel = CharacterManager.Instance?.GetSecondLocalCharacter()?.CharacterUI?.ChatPanel;
                    while (_messages.TryDequeue(out var message))
                    {
                        var senderPart = string.IsNullOrEmpty(message.SenderName) ? string.Empty : $"{message.SenderName}: ";

                        OutwardArchipelagoMod.Log.LogInfo($"sending message to {message.Receiver} - {senderPart}{message.Content}");

                        if (message.Receiver.HasFlag(ReceiverType.PlayerOne))
                        {
                            AddMessageToChatPanel(playerOneChatPanel, message.Content, message.SenderName);
                        }

                        if (message.Receiver.HasFlag(ReceiverType.PlayerTwo) && playerTwoChatPanel?.enabled is true)
                        {
                            AddMessageToChatPanel(playerTwoChatPanel, message.Content, message.SenderName);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add a message to the chat panel UI.
        /// </summary>
        /// <param name="content">Unity UI string containing the contents of the message.</param>
        private void AddMessageToChatPanel(ChatPanel chatPanel, string content, string senderName = null)
        {
            // This code mostly adapts the implementation of ChatPanel.ChatMessageReceived.
            ChatEntry chatEntry;
            if (chatPanel.m_messageArchive.Count < chatPanel.MaxMessageCount)
            {
                chatEntry = Instantiate(UIUtilities.ChatEntryPrefab);
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
                    chatEntry.m_lblPlayerName.text = senderName ?? string.Empty;
                }

                if (chatEntry.m_lblMessage is not null)
                {
                    chatEntry.m_lblMessage.text = content;
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
        /// We will use this to implement chat commands.
        /// </summary>
        /// <param name="chatPanel">The chat panel.</param>
        /// <returns>Whether to continue running the normal method.</returns>
        private bool ChatPanel_SendChatMessage(ChatPanel chatPanel)
        {
            var message = chatPanel?.m_chatEntry?.text;
            if (message is not null)
            {
                var match = _chatCommandPattern.Match(message);
                if (match.Success)
                {
                    var prefix = match.Groups["prefix"].Value;
                    if (_chatCommandHandlers.TryGetValue(prefix, out var handler))
                    {
                        var arg = match.Groups["arg"].Value;
                        arg = arg.Trim();

                        OutwardArchipelagoMod.Log.LogInfo($"handling chat command: /{prefix} {arg}");

                        var didHandle = handler(arg);
                        if (didHandle)
                        {
                            chatPanel.m_chatEntry.text = string.Empty;
                            chatPanel.HideInput();
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Represents the intended recipient of a message.
        /// </summary>
        [Flags]
        public enum ReceiverType
        {
            /// <summary>
            /// The message will never be displayed, but it will still be logged.
            /// </summary>
            None = 0b00,

            /// <summary>
            /// The message will be displayed in the first local player's chat panel.
            /// If the chat panel is not available, the message will be queued until it becomes available.
            /// </summary>
            PlayerOne = 0b01,

            /// <summary>
            /// The message will be displayed in the second local player's chat panel.
            /// If no chat panel is available, the message will be queued until one becomes available.
            /// If no player two chat panel is available at this time, the message will be dropped.
            /// </summary>
            PlayerTwo = 0b10,

            /// <summary>
            /// The message will be displayed in all local chat panels as soon as one becomes available.
            /// </summary>
            AllPlayers = 0b11,
        }

        /// <summary>
        /// Represents a single message to be sent to the chat panel.
        /// </summary>
        private sealed class Message
        {
            private readonly string _content;
            private readonly string _senderName;
            private readonly ReceiverType _receiver;

            /// <summary>
            /// Create a message that may be sent by the <see cref="ChatPanelManager"/>.
            /// </summary>
            /// <param name="content">A Unity UI string containing the contents of the message to be displayed.</param>
            /// <param name="senderName">The name of the sender to display before the message. May be <code>null</code> to not display a sender.</param>
            /// <param name="receiver">The chat panel to which to send the message.</param>
            public Message(string content, string senderName = null, ReceiverType receiver = ReceiverType.AllPlayers)
            {
                _content = content;
                _senderName = senderName;
                _receiver = receiver;
            }

            /// <summary>
            /// The Unity UI string containing the contents of the message to be displayed.
            /// </summary>
            public string Content => _content;

            /// <summary>
            /// The name of the sender to display before the message.
            /// </summary>
            public string SenderName => _senderName;

            /// <summary>
            /// The chat panel to which to send the message.
            /// </summary>
            public ReceiverType Receiver => _receiver;
        }

        [HarmonyPatch(typeof(ChatPanel), nameof(ChatPanel.SendChatMessage), new Type[] { })]
        private static class Patch_ChatPanel_SendChatMessage
        {
            private static bool Prefix(ChatPanel __instance)
            {
                return Instance.ChatPanel_SendChatMessage(__instance);
            }
        }
    }
}
