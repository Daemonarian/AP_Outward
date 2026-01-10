using HarmonyLib;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.XR;
using static MapMagic.ObjectPool;

namespace OutwardArchipelago
{
    public static class QuestLicenseDialogueManager
    {
        public static readonly IReadOnlyList<DialogueTreeQuestLicenseGatekeepInfo> Edits = new List<DialogueTreeQuestLicenseGatekeepInfo>
        {
            new("Dialogue_RissaAberdeen_Neut_Prequest",             54, "dialogue.rissa.quest_license_1_required",  1),
            new("Dialogue_RissaAberdeen_BC_MixedLegacies",           0, "dialogue.rissa.quest_license_2_required",  2),
            new("Dialogue_RissaAberdeen_BC_AshGiant",                0, "dialogue.rissa.quest_license_3_required",  3),
            new("Dialogue_RissaAberdeen_BC_WhisperingBones",         0, "dialogue.rissa.quest_license_4_required",  4),
            new("Dialogue_RissaAberdeen_BC_AncestralPeacemaker",     0, "dialogue.rissa.quest_license_5_required",  5),
            new("Dialogue_Cyrene_HK_TendTheFlame",                   0, "dialogue.rissa.quest_license_5_required",  2),
            new("Dialogue_Cyrene_HK_SandCorsairs",                   0, "dialogue.rissa.quest_license_5_required",  3),
            new("Dialogue_Calixa_HK_MouthToFeed",                    0, "dialogue.rissa.quest_license_5_required",  4),
            new("Dialogue_Calixa_HK_HeroPeacemaker",                 0, "dialogue.rissa.quest_license_5_required",  5),
            new("Dialogue_Ellinara_HM_Questions",                    0, "dialogue.rissa.quest_license_5_required",  2),
            new("Dialogue_Ellinara_HM_Doubts",                       0, "dialogue.rissa.quest_license_5_required",  3),
            new("Dialogue_Ellinara_HM_Truth",                        0, "dialogue.rissa.quest_license_5_required",  4),
            new("Dialogue_CardinalBourlamaque_HM_HallowPeacemaker",  0, "dialogue.rissa.quest_license_5_required",  5),
            new("Dialogue_Soroborean_MilitaryRecruiter_StartPQ1",    0, "dialogue.rissa.quest_license_5_required",  1),
            new("Dialogue_Soroborean_ArcaneDean_Q1",                 0, "dialogue.rissa.quest_license_5_required",  2),
            new("Dialogue_Soroborean_EngineeringDean_Q1",            0, "dialogue.rissa.quest_license_5_required",  2),
            new("Dialogue_Soroborean_NaturalistDean_Q1",             0, "dialogue.rissa.quest_license_5_required",  2),
            new("Dialogue_Soroborean_MilitaryDean_Q2",               0, "dialogue.rissa.quest_license_5_required",  3),
            new("Dialogue_Soroborean_ArcaneDean_Q3",                 0, "dialogue.rissa.quest_license_5_required",  4),
            new("Dialogue_Soroborean_ArcaneDean_Q4",                 0, "dialogue.rissa.quest_license_5_required",  5),
            new("Dialogue_Caldera_MessengerInn_Q0",                  0, "dialogue.rissa.quest_license_5_required",  6),
            new("Dialogue_Caldera_Josef_BaseBuilding",               0, "dialogue.rissa.quest_license_5_required",  7),
            new("Dialogue_Caldera_Josef_BaseBuilding",               0, "dialogue.rissa.quest_license_5_required",  8),
            new("Dialogue_Caldera_Evangeline_BaseBuilding",          0, "dialogue.rissa.quest_license_5_required",  9),
            new("Dialogue_Caldera_Evangeline_BaseBuilding",          0, "dialogue.rissa.quest_license_5_required", 10),
        };

        private static readonly Lazy<IReadOnlyDictionary<string, IReadOnlyCollection<DialogueTreeQuestLicenseGatekeepInfo>>> _editsByTreeName = new(() =>
        {
            var editsByTreeName = new Dictionary<string, List<DialogueTreeQuestLicenseGatekeepInfo>>();
            foreach (var edit in Edits)
            {
                if (!editsByTreeName.ContainsKey(edit.TreeName))
                {
                    editsByTreeName[edit.TreeName] = new List<DialogueTreeQuestLicenseGatekeepInfo>();
                }

                editsByTreeName[edit.TreeName].Add(edit);
            }

            return editsByTreeName.ToDictionary((pair) => pair.Key, (pair) => (IReadOnlyCollection<DialogueTreeQuestLicenseGatekeepInfo>)pair.Value.AsReadOnly());
        });

        public static IReadOnlyDictionary<string, IReadOnlyCollection<DialogueTreeQuestLicenseGatekeepInfo>> EditsByTreeName => _editsByTreeName.Value;

        public static void PatchDialogueTree(DialogueTree tree)
        {
            if (EditsByTreeName.TryGetValue(tree.name, out var edits))
            {
                Plugin.Log.LogDebug($"Attempting to patch dialogue tree {tree.name} with Quest License gatekeepers...");

                // check if this dialogue tree has already been patched
                if (tree.allNodes.Any((node) => node is ConditionNode && (node as ConditionNode).condition is Condition_QuestLicense))
                {
                    Plugin.Log.LogDebug($"Dialogue tree {tree.name} has already been patched.");
                    return;
                }

                // find all the nodes for each edit
                var gatekeptNodesByOriginalID = new Dictionary<int, Node>();
                foreach (var edit in edits)
                {
                    if (!gatekeptNodesByOriginalID.ContainsKey(edit.NodeID))
                    {
                        var gatekeptNode = tree.GetNodeWithID(edit.NodeID);
                        gatekeptNodesByOriginalID[edit.NodeID] = gatekeptNode;
                    }
                }

                foreach (var edit in edits)
                {
                    var gatekeptNode = gatekeptNodesByOriginalID[edit.NodeID];
                    if (gatekeptNode == null)
                    {
                        Plugin.Log.LogError($"Failed to patch dialogue tree: could not find node {edit.NodeID} in dialgoue tree {edit.TreeName}");
                        continue;
                    }

                    var fullMessageKey = $"{Plugin.GUID}.{edit.MessageKey}";
                    if (!LocalizationManager.Instance.TryGetLoc(fullMessageKey, out var text))
                    {
                        Plugin.Log.LogError($"Could not find localization for message {fullMessageKey}");
                        text = $"[LOC] {fullMessageKey}";
                    }

                    var gatekeeperNode = tree.AddNode<ConditionNode>();
                    gatekeeperNode.condition = new Condition_QuestLicense(edit.QuestLicenseCount);

                    var rejectionNode = tree.AddNode<StatementNodeExt>();
                    rejectionNode.actorName = tree.actorParameters.FirstOrDefault()?.name ?? "Speaker";
                    rejectionNode.statement = new Statement(text);

                    foreach (var connection in gatekeptNode.inConnections)
                    {
                        connection.targetNode = gatekeeperNode;
                        gatekeeperNode.inConnections.Add(connection);
                    }

                    gatekeptNode.inConnections.Clear();

                    Connection.Create(gatekeeperNode, gatekeptNode);
                    Connection.Create(gatekeeperNode, rejectionNode);

                    if (tree.primeNode.UID == gatekeptNode.UID)
                    {
                        tree.primeNode = gatekeeperNode;
                    }
                }
            }
        }

        public static void DumpDialogueTree(DialogueTree tree)
        {
            Plugin.Log.LogDebug($"DialogueTree: {tree.name}");
            var visited = new HashSet<int>();
            var toVisit = new Queue<Node>();
            toVisit.Enqueue(tree.primeNode);
            while (toVisit.Any())
            {
                var node = toVisit.Dequeue();
                if (visited.Contains(node.ID))
                {
                    continue;
                }

                visited.Add(node.ID);

                Plugin.Log.LogDebug($"  {node.name} / {node.customName} ({node.ID}) - {node.GetType()}");
                if (node is StatementNodeExt statementNode)
                {
                    Plugin.Log.LogDebug($"    statement = {statementNode.statement.text}");
                }
                else if (node is ConditionNode conditionNode)
                {
                    Plugin.Log.LogDebug($"    condition = {conditionNode.condition.info.Replace("\n", " ")}");
                }
                else if (node is ActionNode actionNode)
                {
                    Plugin.Log.LogDebug($"    action = {actionNode.action.info.Replace("\n", " ")}");
                }
                Plugin.Log.LogDebug($"    inConnection = {node.inConnections.Join((connection) => connection.targetNode.ID.ToString())}");
                Plugin.Log.LogDebug($"    outConnections = {node.outConnections.Join((connection) => connection.targetNode.ID.ToString())}");

                if (node is ConditionNode || node is ActionNode)
                {
                    foreach (var connection in node.outConnections)
                    {
                        toVisit.Enqueue(connection.targetNode);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(DialogueTree), nameof(DialogueTree.OnGraphStarted))]
        public static class DialogueTree_OnGraphStarted
        {
            static void Prefix(DialogueTree __instance)
            {
                DumpDialogueTree(__instance);
                PatchDialogueTree(__instance);
            }
        }

        public class DialogueTreeQuestLicenseGatekeepInfo
        {
            public string TreeName { get; private set; }
            public int NodeID { get; private set; }
            public string MessageKey { get; private set; }
            public int QuestLicenseCount { get; private set; }

            public DialogueTreeQuestLicenseGatekeepInfo(string treeName, int nodeID, string messageKey, int questLicenseCount)
            {
                TreeName = treeName;
                NodeID = nodeID;
                MessageKey = messageKey;
                QuestLicenseCount = questLicenseCount;
            }
        }
    }
}
