using HarmonyLib;
using NodeCanvas.DialogueTrees;

namespace OutwardArchipelago
{
    [HarmonyPatch(typeof(DialogueTreeExt), nameof(DialogueTreeExt.OnGraphStarted))]
    internal class Patch_DialogueTreeExt_OnGraphStarted
    {
        private static void Prefix(DialogueTreeExt __instance)
        {
            OutwardArchipelagoMod.Log.LogDebug($"[DialogueTreeExt.OnGraphStarted] {__instance.name}");
        }
    }
}
