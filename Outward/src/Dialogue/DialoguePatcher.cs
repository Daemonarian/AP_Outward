using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago.Data;
using OutwardArchipelago.Dialogue.Patches;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking.Types;

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

                try
                {
                    patch.ApplyPatch(context);
                }
                catch (Exception ex)
                {
                    OutwardArchipelagoMod.Log.LogError($"Failed to apply patch to dialogue tree: {ex}");
                }
            }
        }

        public static IDialoguePatch QuestLicenseGatekeep(int replaceNodeID, int minimumQuestLevel, string messageKey, string actorName = null, bool isInverted = false)
        {
            return new DialoguePatch(
                replaceNodeID,
                new DialoguePatchConditionNodeFactory(
                    new DialoguePatchQuestLicenseConditionFactory(minimumQuestLevel, isInverted),
                    new DialoguePatchOriginalNodeFactory(replaceNodeID),
                    new DialoguePatchStatementNodeFactory(
                        messageKey,
                        new DialoguePatchFinishNodeFactory(),
                        actorName)));
        }

        public static IDialoguePatch InsertLocationCheck(int replaceNodeID, IReadOnlyList<ArchipelagoLocationData> locations, IDialoguePatchNodeFactory afterNodeFactory)
        {
            return new DialoguePatch(
                replaceNodeID,
                new DialoguePatchActionNodeFactory(
                    new DialoguePatchLocationCheckActionFactory(locations),
                    afterNodeFactory));
        }

        public static IDialoguePatch InsertLocationCheck(int replaceNodeID, int toNodeID, IReadOnlyList<ArchipelagoLocationData> locations)
        {
            return InsertLocationCheck(replaceNodeID, locations, new DialoguePatchOriginalNodeFactory(toNodeID));
        }

        public static IDialoguePatch InsertLocationCheckAndRemoveQuestEvents(int replaceNodeID, IReadOnlyList<ArchipelagoLocationData> locations, IReadOnlyList<string> questEventUIDsToRemove, IDialoguePatchNodeFactory afterNodeFactory)
        {
            foreach(var questEventUID in questEventUIDsToRemove)
            {
                afterNodeFactory = new DialoguePatchActionNodeFactory(
                    new DialoguePatchRemoveQuestEventActionFactory(questEventUID),
                    afterNodeFactory);
            }

            return InsertLocationCheck(replaceNodeID, locations, afterNodeFactory);
        }

        public static IDialoguePatch InsertLocationCheckAndRemoveQuestEvents(int replaceNodeID, int toNodeID, IReadOnlyList<ArchipelagoLocationData> locations, IReadOnlyList<string> questEventUIDsToRemove)
        {
            return InsertLocationCheckAndRemoveQuestEvents(replaceNodeID, locations, questEventUIDsToRemove, new DialoguePatchOriginalNodeFactory(toNodeID));
        }

        public static IDialoguePatch InsertLocationCheckFirstTimeOnly(int replaceNodeID, ArchipelagoLocationData location, IDialoguePatchNodeFactory afterNodeFactory)
        {
            return new DialoguePatch(replaceNodeID, new DialoguePatchConditionNodeFactory(
                new DialoguePatchLocationCheckConditionFactory(location),
                new DialoguePatchOriginalNodeFactory(replaceNodeID),
                new DialoguePatchActionNodeFactory(
                    new DialoguePatchLocationCheckActionFactory(location),
                    afterNodeFactory)));
        }

        public static IDialoguePatch InsertLocationCheckFirstTimeOnly(int replaceNodeID, int toNodeID, ArchipelagoLocationData location)
        {
            return InsertLocationCheckFirstTimeOnly(replaceNodeID, location, new DialoguePatchOriginalNodeFactory(toNodeID));
        }

        private void RegisterAllPatches()
        {
            // Quest License checks

            Patches.Register(
                DialogueTreeID.RissaAberdeen_Neut_Prequest,
                QuestLicenseGatekeep(54, 1, "dialogue.rissa.quest_license_1_required"));
            Patches.Register(
                DialogueTreeID.Soroborean_MilitaryRecruiter_StartPQ1,
                QuestLicenseGatekeep(2, 1, "dialogue.recruiter.quest_license_1_required"));
            Patches.Register(
                DialogueTreeID.RissaAberdeen_BC_MixedLegacies,
                QuestLicenseGatekeep(3, 2, "dialogue.rissa.quest_license_2_required"));
            Patches.Register(
                DialogueTreeID.Cyrene_HK_TendTheFlame,
                QuestLicenseGatekeep(3, 2, "dialogue.cyrene.quest_license_2_required"));
            Patches.Register(
                DialogueTreeID.Ellinara_HM_Questions,
                QuestLicenseGatekeep(3, 2, "dialogue.ellinara.quest_license_2_required"));
            Patches.Register(
                DialogueTreeID.Soroborean_ArcaneDean_Q1,
                QuestLicenseGatekeep(2, 2, "dialogue.arcane_dean.quest_license_2_required"));
            Patches.Register(
                DialogueTreeID.Soroborean_EngineeringDean_Q1,
                QuestLicenseGatekeep(8, 2, "dialogue.engineering_dean.quest_license_2_required"));
            Patches.Register(
                DialogueTreeID.Soroborean_NaturalistDean_Q1,
                QuestLicenseGatekeep(8, 2, "dialogue.naturalist_dean.quest_license_2_required"));
            Patches.Register(
                DialogueTreeID.RissaAberdeen_BC_AshGiant,
                QuestLicenseGatekeep(3, 3, "dialogue.rissa.quest_license_3_required"));
            Patches.Register(
                DialogueTreeID.Cyrene_HK_SandCorsairs,
                QuestLicenseGatekeep(3, 3, "dialogue.cyrene.quest_license_3_required"));
            Patches.Register(
                DialogueTreeID.Ellinara_HM_Doubts,
                QuestLicenseGatekeep(3, 3, "dialogue.ellinara.quest_license_3_required"));
            Patches.Register(
                DialogueTreeID.Soroborean_MilitaryDean_Q2,
                QuestLicenseGatekeep(3, 3, "dialogue.military_dean.quest_license_3_required"));
            Patches.Register(
                DialogueTreeID.RissaAberdeen_BC_WhisperingBones,
                QuestLicenseGatekeep(3, 4, "dialogue.rissa.quest_license_4_required"));
            Patches.Register(
                DialogueTreeID.Calixa_HK_MouthToFeed,
                QuestLicenseGatekeep(4, 4, "dialogue.calixa.quest_license_4_required"));
            Patches.Register(
                DialogueTreeID.Ellinara_HM_Truth,
                QuestLicenseGatekeep(4, 4, "dialogue.ellinara.quest_license_4_required"));
            Patches.Register(
                DialogueTreeID.Soroborean_ArcaneDean_Q3,
                QuestLicenseGatekeep(1, 4, "dialogue.arcane_dean.quest_license_4_required"));
            Patches.Register(
                DialogueTreeID.RissaAberdeen_BC_AncestralPeacemaker,
                QuestLicenseGatekeep(4, 5, "dialogue.rissa.quest_license_5_required"));
            Patches.Register(
                DialogueTreeID.Calixa_HK_HeroPeacemaker,
                QuestLicenseGatekeep(4, 5, "dialogue.calixa.quest_license_5_required"));
            Patches.Register(
                DialogueTreeID.CardinalBourlamaque_HM_HallowPeacemaker,
                QuestLicenseGatekeep(4, 5, "dialogue.cardinal_bourlamaque.quest_license_5_required"));
            Patches.Register(
                DialogueTreeID.Soroborean_ArcaneDean_Q4,
                QuestLicenseGatekeep(1, 5, "dialogue.arcane_dean.quest_license_5_required"));
            Patches.Register(
                DialogueTreeID.Caldera_MessengerInn_Q0,
                QuestLicenseGatekeep(1, 6, "dialogue.messenger_inn.quest_license_6_required"));
            Patches.Register(
                DialogueTreeID.Caldera_Josef_BaseBuilding,
                QuestLicenseGatekeep(2, 7, "dialogue.josef.quest_license_7_required"));
            Patches.Register(
                DialogueTreeID.Caldera_Josef_BaseBuilding,
                QuestLicenseGatekeep(40, 8, "dialogue.josef.quest_license_8_required"));
            Patches.Register(
                DialogueTreeID.Caldera_Evangeline_BaseBuilding,
                QuestLicenseGatekeep(65, 9, "dialogue.evangeline.quest_license_9_required"));
            Patches.Register(
                DialogueTreeID.Caldera_Evangeline_BaseBuilding,
                QuestLicenseGatekeep(72, 10, "dialogue.evangeline.quest_license_10_required"));

            // Parallel Quests

            Patches.Register(
                DialogueTreeID.Soroborean_LichDying,
                InsertLocationCheck(11, 13, new[] {
                    ArchipelagoLocationData.QuestParallelRustAndVengeance1,
                    ArchipelagoLocationData.QuestParallelRustAndVengeance2,
                    ArchipelagoLocationData.QuestParallelRustAndVengeance3, }));

            // Minor Quests

            Patches.Register(
                DialogueTreeID.Merchant_BergAlchemist,
                InsertLocationCheckFirstTimeOnly(7, ArchipelagoLocationData.QuestMinorAlchemyColdStone,
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchRemoveQuestEventActionFactory(OutwardQuestEvents.SideQuests_AlchemistBerg),
                        new DialoguePatchOriginalNodeFactory(8))));

            Patches.Register(
                DialogueTreeID.Merchant_CierzoAlchemist,
                InsertLocationCheckFirstTimeOnly(7, 8, ArchipelagoLocationData.QuestMinorAlchemyCrystalPowder));

            Patches.Register(
                DialogueTreeID.Abrassar_BarrelMan_Real,
                InsertLocationCheck(23, 24, new[] { ArchipelagoLocationData.QuestMinorBarrelMan }));

            Patches.Register(
                DialogueTreeID.GoldLich_Neut_Initial,
                InsertLocationCheck(30, 31, new[] {
                    ArchipelagoLocationData.QuestMinorBewareTheGoldLich1,
                    ArchipelagoLocationData.QuestMinorBewareTheGoldLich2,
                    ArchipelagoLocationData.QuestMinorBewareTheGoldLich3,
                    ArchipelagoLocationData.QuestMinorBewareTheGoldLich4, }));

            Patches.Register(
                DialogueTreeID.JadeLich_Neut_Initial,
                InsertLocationCheck(22, 23, new[] {
                    ArchipelagoLocationData.QuestMinorBewareTheJadeLich1,
                    ArchipelagoLocationData.QuestMinorBewareTheJadeLich2,
                    ArchipelagoLocationData.QuestMinorBewareTheJadeLich3,
                    ArchipelagoLocationData.QuestMinorBewareTheJadeLich4, }));

            Patches.Register(
                DialogueTreeID.Cierzo_HelenTurnbull_Real,
                InsertLocationCheck(77, new[] { ArchipelagoLocationData.QuestMinorHelensFungus },
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchRemoveItemActionFactory(2300150),
                        new DialoguePatchOriginalNodeFactory(78))));

            Patches.Register(
                DialogueTreeID.Merchant_BergKaziteAssassin,
                InsertLocationCheckFirstTimeOnly(7, 8, ArchipelagoLocationData.QuestMinorNeedBeastGolemScraps));

            Patches.Register(
                DialogueTreeID.Emercar_UntertakerNecropolis_Real,
                InsertLocationCheck(10, new[] { ArchipelagoLocationData.QuestMinorSkullsForCremeuh },
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.SideQuests_NecropolisEnd),
                        new DialoguePatchActionNodeFactory(
                            new DialoguePatchRemoveItemActionFactory(6200160),
                            new DialoguePatchOriginalNodeFactory(11)))));

            Patches.Register(
                DialogueTreeID.StrangeApparitionFinal1,
                InsertLocationCheck(3, new[] { ArchipelagoLocationData.QuestMinorStrangeApparitions },
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.Artifacts_StatueReward),
                        new DialoguePatchOriginalNodeFactory(1))));
            Patches.Register(
                DialogueTreeID.StrangeApparitionFinal2,
                InsertLocationCheck(3, new[] { ArchipelagoLocationData.QuestMinorStrangeApparitions },
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.Artifacts_StatueReward),
                        new DialoguePatchOriginalNodeFactory(1))));
            Patches.Register(
                DialogueTreeID.StrangeApparitionFinal3,
                InsertLocationCheck(3, new[] { ArchipelagoLocationData.QuestMinorStrangeApparitions },
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.Artifacts_StatueReward),
                        new DialoguePatchOriginalNodeFactory(1))));
            Patches.Register(
                DialogueTreeID.StrangeApparitionFinal4,
                InsertLocationCheck(3, new[] { ArchipelagoLocationData.QuestMinorStrangeApparitions },
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.Artifacts_StatueReward),
                        new DialoguePatchOriginalNodeFactory(1))));

            Patches.Register(
                DialogueTreeID.TreasureHuntFinal,
                InsertLocationCheck(2, new[] { ArchipelagoLocationData.QuestMinorTreasureHunt },
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.General_TsarAbraDock),
                        new DialoguePatchActionNodeFactory(
                            new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.PromptsComplete_TreasureHunt),
                            new DialoguePatchOriginalNodeFactory(1)))));

            Patches.Register(
                DialogueTreeID.DefEd_WillOWisp_Altar,
                InsertLocationCheck(3, new[] { ArchipelagoLocationData.QuestMinorWilliamOfTheWisp },
                new DialoguePatchFinishNodeFactory()));

            Patches.Register(
                DialogueTreeID.Purifier_MercantileProvost,
                InsertLocationCheck(9, 9, new[] { ArchipelagoLocationData.QuestMinorScholarsRansom }));
            Patches.Register(
                DialogueTreeID.Purifier_MercantileProvost,
                InsertLocationCheck(17, 17, new[] { ArchipelagoLocationData.QuestMinorScholarsRansom }));

            Patches.Register(
                DialogueTreeID.Soroborean_BloodMageTrigger,
                InsertLocationCheck(6, new[] { ArchipelagoLocationData.QuestMinorBloodyBusiness },
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.SA_BloodMageQuestEnds),
                        new DialoguePatchFinishNodeFactory())));

            // repeatable "Ledger" quests

            Patches.Register(
                DialogueTreeID.Merchant_BergGeneralStore,
                InsertLocationCheckFirstTimeOnly(5, ArchipelagoLocationData.QuestMinorLedgerToBerg,
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.PromptsComplete_CierzoGeneral),
                        new DialoguePatchOriginalNodeFactory(6))));

            Patches.Register(
                DialogueTreeID.Merchant_CierzoGeneralStore,
                InsertLocationCheckFirstTimeOnly(4, ArchipelagoLocationData.QuestMinorLedgerToCierzo,
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.PromptsComplete_LevantGeneral),
                        new DialoguePatchOriginalNodeFactory(5))));

            Patches.Register(
                DialogueTreeID.Merchant_LevantGeneralStore,
                InsertLocationCheckFirstTimeOnly(6, ArchipelagoLocationData.QuestMinorLedgerToLevant,
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.PromptsComplete_MonsoonGeneral),
                        new DialoguePatchOriginalNodeFactory(7))));

            Patches.Register(
                DialogueTreeID.Merchant_MonsoonGeneralStore,
                InsertLocationCheckFirstTimeOnly(5, ArchipelagoLocationData.QuestMinorLedgerToMonsoon,
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.PromptsComplete_BergGeneral),
                        new DialoguePatchOriginalNodeFactory(6))));

            // repeatable "Need" quests

            Patches.Register(
                DialogueTreeID.Merchant_CierzoFishmongerA,
                InsertLocationCheckFirstTimeOnly(7, ArchipelagoLocationData.QuestMinorNeedCierzoCeviche,
                    new DialoguePatchActionNodeFactory(
                        new DialoguePatchSendQuestEventActionFactory(OutwardQuestEvents.SideQuests_CompleteCook),
                        new DialoguePatchOriginalNodeFactory(8))));

            Patches.Register(
                DialogueTreeID.Merchant_BergFoodStore,
                InsertLocationCheckFirstTimeOnly(7, 8, ArchipelagoLocationData.QuestMinorNeedManticoreTail));

            Patches.Register(
                DialogueTreeID.Merchant_LevantFoodStore,
                InsertLocationCheckFirstTimeOnly(8, 9, ArchipelagoLocationData.QuestMinorNeedSharkCartilage));

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanCamp,
                InsertLocationCheckFirstTimeOnly(19, 20, ArchipelagoLocationData.QuestMinorNeedAngelFoodCake));

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanArmor,
                InsertLocationCheckFirstTimeOnly(19, 20, ArchipelagoLocationData.QuestMinorNeedFireElementalParticles));

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanFood,
                InsertLocationCheckFirstTimeOnly(19, 20, ArchipelagoLocationData.QuestMinorNeedFireElementalParticles));

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanGeneral,
                InsertLocationCheckFirstTimeOnly(19, 20, ArchipelagoLocationData.QuestMinorNeedTourmaline));

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanWeapons,
                InsertLocationCheckFirstTimeOnly(19, 20, ArchipelagoLocationData.QuestMinorNeedShieldGolemScrap));

            // individual commissions

            Patches.Register(
                DialogueTreeID.Merchant_CierzoBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(59, 60,
                    new[] { ArchipelagoLocationData.CommissionBlueSandHelm },
                    new[] {
                        OutwardQuestEvents.Crafting_CierzoBlacksmithTimer,
                        OutwardQuestEvents.Crafting_CierzoBlacksmithItemA }));
            Patches.Register(
                DialogueTreeID.Merchant_CierzoBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(64, 60,
                    new[] { ArchipelagoLocationData.CommissionBlueSandArmor },
                    new[] {
                        OutwardQuestEvents.Crafting_CierzoBlacksmithTimer,
                        OutwardQuestEvents.Crafting_CierzoBlacksmithItemB }));
            Patches.Register(
                DialogueTreeID.Merchant_CierzoBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(66, 60,
                    new[] { ArchipelagoLocationData.CommissionBlueSandBoots },
                    new[] {
                        OutwardQuestEvents.Crafting_CierzoBlacksmithTimer,
                        OutwardQuestEvents.Crafting_CierzoBlacksmithItemC }));

            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(84, 85,
                    new[] { ArchipelagoLocationData.CommissionCopalHelm },
                    new[] {
                        OutwardQuestEvents.Crafting_BergBlacksmithTimer,
                        OutwardQuestEvents.Crafting_BergBlacksmithItemA }));
            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(89, 85,
                    new[] { ArchipelagoLocationData.CommissionCopalArmor },
                    new[] {
                        OutwardQuestEvents.Crafting_BergBlacksmithTimer,
                        OutwardQuestEvents.Crafting_BergBlacksmithItemB }));
            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(92, 85,
                    new[] { ArchipelagoLocationData.CommissionCopalBoots },
                    new[] {
                        OutwardQuestEvents.Crafting_BergBlacksmithTimer,
                        OutwardQuestEvents.Crafting_BergBlacksmithItemC }));
            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(95, 96,
                    new[] { ArchipelagoLocationData.CommissionPetrifiedWoodHelm },
                    new[] {
                        OutwardQuestEvents.Crafting_BergBlacksmithTimer,
                        OutwardQuestEvents.Crafting_BergBlacksmithItemD }));
            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(100, 96,
                    new[] { ArchipelagoLocationData.CommissionPetrifiedWoodArmor },
                    new[] {
                        OutwardQuestEvents.Crafting_BergBlacksmithTimer,
                        OutwardQuestEvents.Crafting_BergBlacksmithItemE }));
            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(102, 96,
                    new[] { ArchipelagoLocationData.CommissionPetrifiedWoodBoots },
                    new[] {
                        OutwardQuestEvents.Crafting_BergBlacksmithTimer,
                        OutwardQuestEvents.Crafting_BergBlacksmithItemF }));

            Patches.Register(
                DialogueTreeID.Merchant_MonsoonBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(55, 56,
                    new[] { ArchipelagoLocationData.CommissionPalladiumHelm },
                    new[] {
                        OutwardQuestEvents.Crafting_MonsoonBlacksmithTimer,
                        OutwardQuestEvents.Crafting_MonsoonBlacksmithItemA }));
            Patches.Register(
                DialogueTreeID.Merchant_MonsoonBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(60, 56,
                    new[] { ArchipelagoLocationData.CommissionPalladiumArmor },
                    new[] {
                        OutwardQuestEvents.Crafting_MonsoonBlacksmithTimer,
                        OutwardQuestEvents.Crafting_MonsoonBlacksmithItemB }));
            Patches.Register(
                DialogueTreeID.Merchant_MonsoonBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(62, 56,
                    new[] { ArchipelagoLocationData.CommissionPalladiumBoots },
                    new[] {
                        OutwardQuestEvents.Crafting_MonsoonBlacksmithTimer,
                        OutwardQuestEvents.Crafting_MonsoonBlacksmithItemC }));

            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(87, 88,
                    new[] { ArchipelagoLocationData.CommissionTenebrousHelm },
                    new[] {
                        OutwardQuestEvents.Crafting_LevantBlacksmithTimer,
                        OutwardQuestEvents.Crafting_LevantBlacksmithItemA }));
            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(92, 88,
                    new[] { ArchipelagoLocationData.CommissionTenebrousArmor },
                    new[] {
                        OutwardQuestEvents.Crafting_LevantBlacksmithTimer,
                        OutwardQuestEvents.Crafting_LevantBlacksmithItemB }));
            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(95, 88,
                    new[] { ArchipelagoLocationData.CommissionTenebrousBoots },
                    new[] {
                        OutwardQuestEvents.Crafting_LevantBlacksmithTimer,
                        OutwardQuestEvents.Crafting_LevantBlacksmithItemC }));
            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(98, 99,
                    new[] { ArchipelagoLocationData.CommissionTsarHelm },
                    new[] {
                        OutwardQuestEvents.Crafting_LevantBlacksmithTimer,
                        OutwardQuestEvents.Crafting_LevantBlacksmithItemD }));
            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(103, 99,
                    new[] { ArchipelagoLocationData.CommissionTsarArmor },
                    new[] {
                        OutwardQuestEvents.Crafting_LevantBlacksmithTimer,
                        OutwardQuestEvents.Crafting_LevantBlacksmithItemE }));
            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(105, 99,
                    new[] { ArchipelagoLocationData.CommissionTsarBoots },
                    new[] {
                        OutwardQuestEvents.Crafting_LevantBlacksmithTimer,
                        OutwardQuestEvents.Crafting_LevantBlacksmithItemF }));

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(58, 59,
                    new[] { ArchipelagoLocationData.CommissionAntiquePlateSallet },
                    new[] {
                        OutwardQuestEvents.Crafting_HarmattanBlacksmithTimer,
                        OutwardQuestEvents.Crafting_HarmattanBlacksmithItemA }));
            Patches.Register(
                DialogueTreeID.Merchant_HarmattanBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(63, 59,
                    new[] { ArchipelagoLocationData.CommissionAntiquePlateGarb },
                    new[] {
                        OutwardQuestEvents.Crafting_HarmattanBlacksmithTimer,
                        OutwardQuestEvents.Crafting_HarmattanBlacksmithItemB }));
            Patches.Register(
                DialogueTreeID.Merchant_HarmattanBlacksmith,
                InsertLocationCheckAndRemoveQuestEvents(65, 59,
                    new[] { ArchipelagoLocationData.CommissionAntiquePlateBoots },
                    new[] {
                        OutwardQuestEvents.Crafting_HarmattanBlacksmithTimer,
                        OutwardQuestEvents.Crafting_HarmattanBlacksmithItemC }));
        }

        private static string EscapeLabelString(string label)
        {
            return label.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
        }

        public static string DialogueTreeAsViz(DialogueTreeExt tree)
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
