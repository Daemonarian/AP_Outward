using System;
using HarmonyLib;
using OutwardArchipelago.Archipelago;
using UnityEngine.Events;

namespace OutwardArchipelago
{
    internal static class MainScreenWarning
    {
        private static bool doWarn = true;

        private static bool ShowArchipelagoNotConnectedWarning(MainScreen mainScreen, UnityAction acceptCallback)
        {
            if (doWarn && !ArchipelagoConnector.Instance.IsConnected)
            {
                var message = OutwardArchipelagoMod.Instance.GetLocalizedModString("notification.warn_start_game_without_connection");
                mainScreen.CharacterUI.MessagePanel.Show(
                    message,
                    string.Empty,
                    () =>
                    {
                        try
                        {
                            doWarn = false;
                            acceptCallback();
                        }
                        finally
                        {
                            doWarn = true;
                        }
                    },
                    () => { },
                    true,
                    -1f,
                    null);
                return false;
            }

            return true;
        }

        [HarmonyPatch(typeof(MainScreen), nameof(MainScreen.OnContinueClicked), new Type[] { })]
        private static class Patch_MainScreen_OnContinueClicked
        {
            private static bool Prefix(MainScreen __instance)
            {
                return ShowArchipelagoNotConnectedWarning(__instance, () => __instance.OnContinueClicked());
            }
        }

        [HarmonyPatch(typeof(MainScreen), nameof(MainScreen.OnNewGameClicked), new Type[] { })]
        private static class Patch_MainScreen_OnNewGameClicked
        {
            private static bool Prefix(MainScreen __instance)
            {
                return ShowArchipelagoNotConnectedWarning(__instance, () => __instance.OnNewGameClicked());
            }
        }
    }
}
