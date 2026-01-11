using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OutwardArchipelago
{
    public class DialoguePatcher
    {
        private static readonly Lazy<DialoguePatcher> _instance = new(() => new DialoguePatcher());
        public static DialoguePatcher Instance => _instance.Value;

        public static readonly IReadOnlyList<QuestLicenseGatekeepPatch> Edits = new List<QuestLicenseGatekeepPatch>
        {
            new("Dialogue_RissaAberdeen_Neut_Prequest",             54, "dialogue.rissa.quest_license_1_required",                 1),
            new("Dialogue_Soroborean_MilitaryRecruiter_StartPQ1",    2, "dialogue.recruiter.quest_license_1_required",             1),
            new("Dialogue_RissaAberdeen_BC_MixedLegacies",           3, "dialogue.rissa.quest_license_2_required",                 2),
            new("Dialogue_Cyrene_HK_TendTheFlame",                   3, "dialogue.cyrene.quest_license_2_required",                2),
            new("Dialogue_Ellinara_HM_Questions",                    3, "dialogue.ellinara.quest_license_2_required",              2),
            new("Dialogue_Soroborean_ArcaneDean_Q1",                 2, "dialogue.arcane_dean.quest_license_2_required",           2),
            new("Dialogue_Soroborean_EngineeringDean_Q1",            8, "dialogue.engineering_dean.quest_license_2_required",      2),
            new("Dialogue_Soroborean_NaturalistDean_Q1",             8, "dialogue.naturalist_dean.quest_license_2_required",       2),
            new("Dialogue_RissaAberdeen_BC_AshGiant",                3, "dialogue.rissa.quest_license_3_required",                 3),
            new("Dialogue_Cyrene_HK_SandCorsairs",                   3, "dialogue.cyrene.quest_license_3_required",                3),
            new("Dialogue_Ellinara_HM_Doubts",                       3, "dialogue.ellinara.quest_license_3_required",              3),
            new("Dialogue_Soroborean_MilitaryDean_Q2",               3, "dialogue.military_dean.quest_license_3_required",         3),
            new("Dialogue_RissaAberdeen_BC_WhisperingBones",         3, "dialogue.rissa.quest_license_4_required",                 4),
            new("Dialogue_Calixa_HK_MouthToFeed",                    4, "dialogue.calixa.quest_license_4_required",                4),
            new("Dialogue_Ellinara_HM_Truth",                        4, "dialogue.ellinara.quest_license_4_required",              4),
            new("Dialogue_Soroborean_ArcaneDean_Q3",                 1, "dialogue.arcane_dean.quest_license_4_required",           4),
            new("Dialogue_RissaAberdeen_BC_AncestralPeacemaker",     4, "dialogue.rissa.quest_license_5_required",                 5),
            new("Dialogue_Calixa_HK_HeroPeacemaker",                 4, "dialogue.calixa.quest_license_5_required",                5),
            new("Dialogue_CardinalBourlamaque_HM_HallowPeacemaker",  4, "dialogue.cardinal_bourlamaque.quest_license_5_required",  5),
            new("Dialogue_Soroborean_ArcaneDean_Q4",                 1, "dialogue.arcane_dean.quest_license_5_required",           5),
            new("Dialogue_Caldera_MessengerInn_Q0",                  1, "dialogue.messenger_inn.quest_license_6_required",         6),
            new("Dialogue_Caldera_Josef_BaseBuilding",               2, "dialogue.josef.quest_license_7_required",                 7),
            new("Dialogue_Caldera_Josef_BaseBuilding",              40, "dialogue.josef.quest_license_8_required",                 8),
            new("Dialogue_Caldera_Evangeline_BaseBuilding",         65, "dialogue.evangeline.quest_license_9_required",            9),
            new("Dialogue_Caldera_Evangeline_BaseBuilding",         72, "dialogue.evangeline.quest_license_10_required",          10),
        };

        private static readonly Lazy<IReadOnlyDictionary<string, IReadOnlyCollection<QuestLicenseGatekeepPatch>>> _editsByTreeName = new(() =>
        {
            var editsByTreeName = new Dictionary<string, List<QuestLicenseGatekeepPatch>>();
            foreach (var edit in Edits)
            {
                if (!editsByTreeName.ContainsKey(edit.TreeName))
                {
                    editsByTreeName[edit.TreeName] = new List<QuestLicenseGatekeepPatch>();
                }

                editsByTreeName[edit.TreeName].Add(edit);
            }

            return editsByTreeName.ToDictionary((pair) => pair.Key, (pair) => (IReadOnlyCollection<QuestLicenseGatekeepPatch>)pair.Value.AsReadOnly());
        });

        public static IReadOnlyDictionary<string, IReadOnlyCollection<QuestLicenseGatekeepPatch>> EditsByTreeName => _editsByTreeName.Value;

        public void Awake()
        {
            Graph.onGraphDeserialized += OnGraphDeserialized;
        }

        public void PatchDialogueTree(DialogueTreeExt tree)
        {
            if (EditsByTreeName.TryGetValue(tree.name, out var edits))
            {
                OutwardArchipelagoMod.Log.LogDebug($"Attempting to patch dialogue tree '{tree.name}' with Quest License gatekeepers...");

                // check if this dialogue tree has already been patched
                if (tree.allNodes.Any((node) => node is ConditionNode && (node as ConditionNode).condition is Condition_QuestLicense))
                {
                    OutwardArchipelagoMod.Log.LogDebug($"Dialogue tree {tree.name} has already been patched.");
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
                    OutwardArchipelagoMod.Log.LogDebug($"Applying quest license gatekeeper to node {edit.NodeID} in dialogue tree '{tree.name}'.");

                    var gatekeptNode = gatekeptNodesByOriginalID[edit.NodeID];
                    if (gatekeptNode == null)
                    {
                        OutwardArchipelagoMod.Log.LogError($"Failed to patch dialogue tree: could not find node {edit.NodeID} in dialgoue tree {edit.TreeName}");
                        continue;
                    }

                    var fullMessageKey = $"{OutwardArchipelagoMod.GUID}.{edit.MessageKey}";
                    if (!LocalizationManager.Instance.TryGetLoc(fullMessageKey, out var text))
                    {
                        OutwardArchipelagoMod.Log.LogError($"Could not find localization for message {fullMessageKey}");
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

        public void OnGraphDeserialized(Graph graph)
        {
            // only patch dialogue trees when we are hosting
            if (PhotonNetwork.isMasterClient)
            {
                // check if the graph is a dialogue tree
                if (graph is DialogueTreeExt tree)
                {
                    PatchDialogueTree(tree);
                }
            }
        }

        public class QuestLicenseGatekeepPatch
        {
            public string TreeName { get; private set; }
            public int NodeID { get; private set; }
            public string MessageKey { get; private set; }
            public int QuestLicenseCount { get; private set; }

            public QuestLicenseGatekeepPatch(string treeName, int nodeID, string messageKey, int questLicenseCount)
            {
                TreeName = treeName;
                NodeID = nodeID;
                MessageKey = messageKey;
                QuestLicenseCount = questLicenseCount;
            }
        }
    }
}
