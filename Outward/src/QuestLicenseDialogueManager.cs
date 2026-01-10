using HarmonyLib;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutwardArchipelago
{
    public static class QuestLicenseDialogueManager
    {


        [HarmonyPatch(typeof(DialogueTree), nameof(DialogueTree.OnGraphStarted))]
        public class DialogueTree_OnGraphStarted
        {
            static void Prefix(DialogueTree __instance)
            {
                if (__instance.name == "Dialogue_RissaAberdeen_Neut_Prequest")
                {
                    if (__instance.primeNode as ConditionNode == null || (__instance.primeNode as ConditionNode).condition as Condition_QuestLicense == null)
                    {
                        var originalStartNode = __instance.primeNode;

                        var gateNode = __instance.AddNode<ConditionNode>();
                        gateNode.condition = new Condition_QuestLicense(1);

                        string text = "";
                        if (!LocalizationManager.Instance.TryGetLoc($"{Plugin.GUID}.dialogue.rissa.quest_license_1_required", out text))
                        {
                            text = $"[LOC] {Plugin.GUID}.dialogue.rissa.quest_license_1_required";
                        }

                        var rejectNode = __instance.AddNode<StatementNodeExt>();
                        rejectNode.actorName = __instance.actorParameters.FirstOrDefault()?.name ?? "Speaker";
                        rejectNode.statement = new Statement(text);

                        Connection.Create(gateNode, originalStartNode);
                        Connection.Create(gateNode, rejectNode);

                        __instance.primeNode = gateNode;
                    }
                }
            }
        }
    }
}
