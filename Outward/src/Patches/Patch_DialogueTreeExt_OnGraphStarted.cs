using HarmonyLib;
using NodeCanvas.DialogueTrees;
using OutwardArchipelago.Dialogue;

namespace OutwardArchipelago.Patches
{
    [HarmonyPatch(typeof(DialogueTreeExt), nameof(DialogueTreeExt.OnGraphStarted))]
    internal class Patch_DialogueTreeExt_OnGraphStarted
    {
        private static void Prefix(DialogueTreeExt __instance)
        {
            OutwardArchipelagoMod.Log.LogDebug($"[DialogueTreeExt.OnGraphStarted] {DialogueTreeID.FromTree(__instance)}");
        }
    }
}
