using System;
using HarmonyLib;

namespace OutwardArchipelago.Patch
{
    [HarmonyPatch(typeof(ChatPanel), nameof(ChatPanel.SendChatMessage), new Type[] { })]
    internal static class ChatPanel_SendChatMessage
    {
        private static bool Prefix(ChatPanel __instance)
        {
            return ChatPanelManager.Instance.ChatPanel_SendChatMessage(__instance);
        }
    }
}
