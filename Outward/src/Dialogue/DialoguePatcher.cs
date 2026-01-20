using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace OutwardArchipelago.Dialogue
{
    internal class DialoguePatcher
    {
        private static readonly Lazy<DialoguePatcher> _instance = new(() => new DialoguePatcher());
        public static DialoguePatcher Instance => _instance.Value;

        public DialoguePatchCollection Patches = new();

        public void Awake()
        {
            RegisterAllPatches();
            Graph.onGraphDeserialized += OnGraphDeserialized;
        }

        private void RegisterAllPatches()
        {
            // Quest License checks

            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.RissaAberdeen_Neut_Prequest, 54, new Condition_QuestLicense(1), "dialogue.rissa.quest_license_1_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Soroborean_MilitaryRecruiter_StartPQ1, 2, new Condition_QuestLicense(1), "dialogue.recruiter.quest_license_1_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.RissaAberdeen_BC_MixedLegacies, 3, new Condition_QuestLicense(2), "dialogue.rissa.quest_license_2_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Cyrene_HK_TendTheFlame, 3, new Condition_QuestLicense(2), "dialogue.cyrene.quest_license_2_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Ellinara_HM_Questions, 3, new Condition_QuestLicense(2), "dialogue.ellinara.quest_license_2_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Soroborean_ArcaneDean_Q1, 2, new Condition_QuestLicense(2), "dialogue.arcane_dean.quest_license_2_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Soroborean_EngineeringDean_Q1, 8, new Condition_QuestLicense(2), "dialogue.engineering_dean.quest_license_2_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Soroborean_NaturalistDean_Q1, 8, new Condition_QuestLicense(2), "dialogue.naturalist_dean.quest_license_2_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.RissaAberdeen_BC_AshGiant, 3, new Condition_QuestLicense(3), "dialogue.rissa.quest_license_3_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Cyrene_HK_SandCorsairs, 3, new Condition_QuestLicense(3), "dialogue.cyrene.quest_license_3_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Ellinara_HM_Doubts, 3, new Condition_QuestLicense(3), "dialogue.ellinara.quest_license_3_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Soroborean_MilitaryDean_Q2, 3, new Condition_QuestLicense(3), "dialogue.military_dean.quest_license_3_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.RissaAberdeen_BC_WhisperingBones, 3, new Condition_QuestLicense(4), "dialogue.rissa.quest_license_4_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Calixa_HK_MouthToFeed, 4, new Condition_QuestLicense(4), "dialogue.calixa.quest_license_4_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Ellinara_HM_Truth, 4, new Condition_QuestLicense(4), "dialogue.ellinara.quest_license_4_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Soroborean_ArcaneDean_Q3, 1, new Condition_QuestLicense(4), "dialogue.arcane_dean.quest_license_4_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.RissaAberdeen_BC_AncestralPeacemaker, 4, new Condition_QuestLicense(5), "dialogue.rissa.quest_license_5_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Calixa_HK_HeroPeacemaker, 4, new Condition_QuestLicense(5), "dialogue.calixa.quest_license_5_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.CardinalBourlamaque_HM_HallowPeacemaker, 4, new Condition_QuestLicense(5), "dialogue.cardinal_bourlamaque.quest_license_5_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Soroborean_ArcaneDean_Q4, 1, new Condition_QuestLicense(5), "dialogue.arcane_dean.quest_license_5_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Caldera_MessengerInn_Q0, 1, new Condition_QuestLicense(6), "dialogue.messenger_inn.quest_license_6_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Caldera_Josef_BaseBuilding, 2, new Condition_QuestLicense(7), "dialogue.josef.quest_license_7_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Caldera_Josef_BaseBuilding, 40, new Condition_QuestLicense(8), "dialogue.josef.quest_license_8_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Caldera_Evangeline_BaseBuilding, 65, new Condition_QuestLicense(9), "dialogue.evangeline.quest_license_9_required"));
            Patches.Register(new GatekeepDialoguePatch(DialogueTreeID.Caldera_Evangeline_BaseBuilding, 72, new Condition_QuestLicense(10), "dialogue.evangeline.quest_license_10_required"));

            // Replace quest rewards

            Patches.Register(new ReplaceActionDialoguePatch(DialogueTreeID.Merchant_CierzoAlchemist, 7, new Action_CompleteLocationCheck(ArchipelagoLocationID.QUEST_SIDE_ALCHEMY_COLD_STONE)));
        }

        public void PatchDialogueTree(DialogueTreeExt tree)
        {
            IDialoguePatchContext context = null;
            foreach (IDialoguePatch patch in Patches.PatchesByDialogueTree(tree))
            {
                if (context == null)
                {
                    context = new DialoguePatchContext(tree);
                    OutwardArchipelagoMod.Log.LogDebug($"Patching dialogue tree {context.TreeID}");
                }

                patch.ApplyPatch(context);
            }

            if (context != null)
            {
                OutwardArchipelagoMod.Log.LogDebug($"Post patch tree {context.TreeID}: {DialogueTreeAsViz(tree)}");
            }
        }

        private static string EscapeLabelString(string label)
        {
            return label.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
        }

        public string DialogueTreeAsViz(DialogueTreeExt tree)
        {
            var sb = new StringBuilder();
            sb.Append($"digraph DialogueTree {{ ");

            sb.Append("Start [shape=doublecircle, label=\"Start\"]; ");
            foreach (var node in tree.allNodes)
            {
                var lsb = new StringBuilder();
                lsb.AppendLine($"ID={node.ID}");
                lsb.AppendLine($"UID={node.UID}");
                lsb.AppendLine($"Type={node.GetType().Name}");
                if (node is StatementNodeExt statementNode)
                {
                    lsb.AppendLine($"Actor={statementNode.actorName}");
                    lsb.AppendLine($"Statement={statementNode.statement?.text}");
                }
                else if (node is ConditionNode conditionNode)
                {
                    lsb.AppendLine($"ConditionType={conditionNode.condition?.GetType()?.Name}");
                    lsb.AppendLine($"ConditionInfo={conditionNode.condition?.info}");
                }
                else if (node is ActionNode actionNode)
                {
                    lsb.AppendLine($"ActionType={actionNode.action?.GetType()?.Name}");
                    lsb.AppendLine($"ActionInfo={actionNode.action?.info}");
                }
                else if (node is Jumper jumperNode)
                {
                    lsb.AppendLine($"TargetNodeID={jumperNode.sourceNode.ID}");
                    lsb.AppendLine($"TargetNodeUID={jumperNode.sourceNodeUID}");
                }
#pragma warning disable CS0618 // Type or member is obsolete
                else if (node is GoToNode goToNode)
#pragma warning restore CS0618 // Type or member is obsolete
                {
                    lsb.AppendLine($"TargetNodeID={goToNode._targetNode.ID}");
                    lsb.AppendLine($"TargetNodeUID={goToNode._targetNode.UID}");
                }

                sb.Append($"Node{node.ID} [label=\"{EscapeLabelString(lsb.ToString().Trim())}\"]; ");
            }

            if (tree.primeNode != null)
            {
                sb.Append($"Start -> Node{tree.primeNode.ID}; ");
            }

            foreach (var node in tree.allNodes)
            {
                if (node.outConnections != null)
                {
                    for (int i = 0; i < node.outConnections.Count; i++)
                    {
                        var outNode = node.outConnections[i]?.targetNode;
                        if (outNode != null)
                        {
                            var lsb = new StringBuilder();
                            if (node is ConditionNode)
                            {
                                if (i == 0)
                                {
                                    lsb.AppendLine("Yes");
                                }
                                else if (i == 1)
                                {
                                    lsb.AppendLine("No");
                                }
                                else
                                {
                                    lsb.AppendLine("??");
                                }
                            }
                            else if (node is MultipleChoiceNodeExt mcNode)
                            {
                                if (mcNode.availableChoices != null && i < mcNode.availableChoices.Count)
                                {
                                    lsb.AppendLine($"{i}: {mcNode.availableChoices[i]?.statement?.text}");
                                }
                                else
                                {
                                    lsb.AppendLine($"{i}: ??");
                                }
                            }

                            sb.Append($"Node{node.ID} -> Node{outNode.ID} [label=\"{EscapeLabelString(lsb.ToString().Trim())}\"]; ");
                        }
                    }
                }
            }

            sb.Append("}");

            return sb.ToString();
        }

        private readonly HashSet<ulong> seenDialogueTrees = new();

        public void DumpDialogueTree(DialogueTreeExt tree)
        {
            var treeID = DialogueTreeID.FromTree(tree);
            if (seenDialogueTrees.Add(treeID.Hash.Value))
            {
                var viz = DialogueTreeAsViz(tree);
                OutwardArchipelagoMod.Log.LogDebug($"Dialogue tree deserialized {treeID}: {viz}");
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
                    DumpDialogueTree(tree);
                    PatchDialogueTree(tree);
                }
            }
        }
    }
}
