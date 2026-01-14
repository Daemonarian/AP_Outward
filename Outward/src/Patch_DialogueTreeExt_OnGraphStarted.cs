using HarmonyLib;
using NodeCanvas.DialogueTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
