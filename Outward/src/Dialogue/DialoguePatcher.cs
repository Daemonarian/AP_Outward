using System;
using System.Collections.Generic;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago.Data;
using OutwardArchipelago.Dialogue.Builders.Actions;
using OutwardArchipelago.Dialogue.Builders.Nodes;
using OutwardArchipelago.Dialogue.Patches;

namespace OutwardArchipelago.Dialogue
{
    internal class DialoguePatcher
    {
        private const int MushroomShield = 2300150;
        private const int BlueSkullEffigy = 6200160;
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
            foreach (var patch in Patches.PatchesByDialogueTree(tree))
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

        private void RegisterAllPatches()
        {
            // Quest License checks

            Patches.Register(
                DialogueTreeID.RissaAberdeen_Neut_Prequest,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 54,
                    MinimumQuestLevel = 1,
                    LocalizationKey = "dialogue.rissa.quest_license_1_required",
                });
            Patches.Register(
                DialogueTreeID.Soroborean_MilitaryRecruiter_StartPQ1,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 2,
                    MinimumQuestLevel = 1,
                    LocalizationKey = "dialogue.recruiter.quest_license_1_required",
                });
            Patches.Register(
                DialogueTreeID.RissaAberdeen_BC_MixedLegacies,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 3,
                    MinimumQuestLevel = 2,
                    LocalizationKey = "dialogue.rissa.quest_license_2_required",
                });
            Patches.Register(
                DialogueTreeID.Cyrene_HK_TendTheFlame,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 3,
                    MinimumQuestLevel = 2,
                    LocalizationKey = "dialogue.cyrene.quest_license_2_required",
                });
            Patches.Register(
                DialogueTreeID.Ellinara_HM_Questions,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 3,
                    MinimumQuestLevel = 2,
                    LocalizationKey = "dialogue.ellinara.quest_license_2_required",
                });
            Patches.Register(
                DialogueTreeID.Soroborean_ArcaneDean_Q1,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 2,
                    MinimumQuestLevel = 2,
                    LocalizationKey = "dialogue.arcane_dean.quest_license_2_required",
                });
            Patches.Register(
                DialogueTreeID.Soroborean_EngineeringDean_Q1,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 8,
                    MinimumQuestLevel = 2,
                    LocalizationKey = "dialogue.engineering_dean.quest_license_2_required",
                });
            Patches.Register(
                DialogueTreeID.Soroborean_NaturalistDean_Q1,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 8,
                    MinimumQuestLevel = 2,
                    LocalizationKey = "dialogue.naturalist_dean.quest_license_2_required",
                });
            Patches.Register(
                DialogueTreeID.RissaAberdeen_BC_AshGiant,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 3,
                    MinimumQuestLevel = 3,
                    LocalizationKey = "dialogue.rissa.quest_license_3_required",
                });
            Patches.Register(
                DialogueTreeID.Cyrene_HK_SandCorsairs,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 3,
                    MinimumQuestLevel = 3,
                    LocalizationKey = "dialogue.cyrene.quest_license_3_required",
                });
            Patches.Register(
                DialogueTreeID.Ellinara_HM_Doubts,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 3,
                    MinimumQuestLevel = 3,
                    LocalizationKey = "dialogue.ellinara.quest_license_3_required",
                });
            Patches.Register(
                DialogueTreeID.Soroborean_MilitaryDean_Q2,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 3,
                    MinimumQuestLevel = 3,
                    LocalizationKey = "dialogue.military_dean.quest_license_3_required",
                });
            Patches.Register(
                DialogueTreeID.RissaAberdeen_BC_WhisperingBones,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 3,
                    MinimumQuestLevel = 4,
                    LocalizationKey = "dialogue.rissa.quest_license_4_required",
                });
            Patches.Register(
                DialogueTreeID.Calixa_HK_MouthToFeed,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 4,
                    MinimumQuestLevel = 4,
                    LocalizationKey = "dialogue.calixa.quest_license_4_required",
                });
            Patches.Register(
                DialogueTreeID.Ellinara_HM_Truth,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 4,
                    MinimumQuestLevel = 4,
                    LocalizationKey = "dialogue.ellinara.quest_license_4_required",
                });
            Patches.Register(
                DialogueTreeID.Soroborean_ArcaneDean_Q3,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 1,
                    MinimumQuestLevel = 4,
                    LocalizationKey = "dialogue.arcane_dean.quest_license_4_required",
                });
            Patches.Register(
                DialogueTreeID.RissaAberdeen_BC_AncestralPeacemaker,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 4,
                    MinimumQuestLevel = 5,
                    LocalizationKey = "dialogue.rissa.quest_license_5_required",
                });
            Patches.Register(
                DialogueTreeID.Calixa_HK_HeroPeacemaker,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 4,
                    MinimumQuestLevel = 5,
                    LocalizationKey = "dialogue.calixa.quest_license_5_required",
                });
            Patches.Register(
                DialogueTreeID.CardinalBourlamaque_HM_HallowPeacemaker,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 4,
                    MinimumQuestLevel = 5,
                    LocalizationKey = "dialogue.cardinal_bourlamaque.quest_license_5_required",
                });
            Patches.Register(
                DialogueTreeID.Soroborean_ArcaneDean_Q4,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 1,
                    MinimumQuestLevel = 5,
                    LocalizationKey = "dialogue.arcane_dean.quest_license_5_required",
                });
            Patches.Register(
                DialogueTreeID.Caldera_MessengerInn_Q0,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 1,
                    MinimumQuestLevel = 6,
                    LocalizationKey = "dialogue.messenger_inn.quest_license_6_required",
                });
            Patches.Register(
                DialogueTreeID.Caldera_Josef_BaseBuilding,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 2,
                    MinimumQuestLevel = 7,
                    LocalizationKey = "dialogue.josef.quest_license_7_required",
                });
            Patches.Register(
                DialogueTreeID.Caldera_Josef_BaseBuilding,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 40,
                    MinimumQuestLevel = 8,
                    LocalizationKey = "dialogue.josef.quest_license_8_required",
                });
            Patches.Register(
                DialogueTreeID.Caldera_Evangeline_BaseBuilding,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 65,
                    MinimumQuestLevel = 9,
                    LocalizationKey = "dialogue.evangeline.quest_license_9_required",
                });
            Patches.Register(
                DialogueTreeID.Caldera_Evangeline_BaseBuilding,
                new QuestLicenseGatekeepPatch
                {
                    ReplaceNodeID = 72,
                    MinimumQuestLevel = 10,
                    LocalizationKey = "dialogue.evangeline.quest_license_10_required",
                });

            // Parallel Quests

            Patches.Register(
                DialogueTreeID.Soroborean_LichDying,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 11,
                    LocationIds = new[]
                    {
                        APWorldLocation.QuestParallelRustAndVengeance1,
                        APWorldLocation.QuestParallelRustAndVengeance2,
                        APWorldLocation.QuestParallelRustAndVengeance3,
                    },
                    NextNode = new OriginalNodeBuilder { NodeID = 13 }
                });

            // Minor Quests

            Patches.Register(
                DialogueTreeID.Merchant_BergAlchemist,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 7,
                    LocationId = APWorldLocation.QuestMinorAlchemyColdStone,
                    OtherAction = new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.SideQuests_AlchemistBerg },
                });

            Patches.Register(
                DialogueTreeID.Merchant_CierzoAlchemist,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 7,
                    LocationId = APWorldLocation.QuestMinorAlchemyCrystalPowder,
                });

            Patches.Register(
                DialogueTreeID.Abrassar_BarrelMan_Real,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 23,
                    LocationId = APWorldLocation.QuestMinorBarrelMan,
                });

            Patches.Register(
                DialogueTreeID.GoldLich_Neut_Initial,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 30,
                    LocationIds = new[]
                    {
                        APWorldLocation.QuestMinorBewareTheGoldLich1,
                        APWorldLocation.QuestMinorBewareTheGoldLich2,
                        APWorldLocation.QuestMinorBewareTheGoldLich3,
                        APWorldLocation.QuestMinorBewareTheGoldLich4,
                    },
                });

            Patches.Register(
                DialogueTreeID.JadeLich_Neut_Initial,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 22,
                    LocationIds = new[]
                    {
                        APWorldLocation.QuestMinorBewareTheJadeLich1,
                        APWorldLocation.QuestMinorBewareTheJadeLich2,
                        APWorldLocation.QuestMinorBewareTheJadeLich3,
                        APWorldLocation.QuestMinorBewareTheJadeLich4,
                    },
                });

            Patches.Register(
                DialogueTreeID.Cierzo_HelenTurnbull_Real,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 77,
                    LocationId = APWorldLocation.QuestMinorHelensFungus,
                    OtherAction = new RemoveItemActionBuilder { ItemID = MushroomShield },
                });

            Patches.Register(
                DialogueTreeID.Emercar_UntertakerNecropolis_Real,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 10,
                    LocationId = APWorldLocation.QuestMinorSkullsForCremeuh,
                    OtherAction = new RemoveItemActionBuilder
                    {
                        ItemID = BlueSkullEffigy,
                        Amount = 4,
                    },
                });

            var strangeApparitionsLocationCheckPatch = new InsertLocationCheckPatch
            {
                ReplaceNodeID = 3,
                LocationId = APWorldLocation.QuestMinorStrangeApparitions,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.Artifacts_StatueReward },
            };
            Patches.Register(DialogueTreeID.StrangeApparitionFinal1, strangeApparitionsLocationCheckPatch);
            Patches.Register(DialogueTreeID.StrangeApparitionFinal2, strangeApparitionsLocationCheckPatch);
            Patches.Register(DialogueTreeID.StrangeApparitionFinal3, strangeApparitionsLocationCheckPatch);
            Patches.Register(DialogueTreeID.StrangeApparitionFinal4, strangeApparitionsLocationCheckPatch);

            Patches.Register(
                DialogueTreeID.TreasureHuntFinal,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 2,
                    LocationId = APWorldLocation.QuestMinorTreasureHunt,
                    OtherActions = new[]
                    {
                        new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.General_TsarAbraDock },
                        new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.PromptsComplete_TreasureHunt },
                    }
                });

            Patches.Register(
                DialogueTreeID.DefEd_WillOWisp_Altar,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 3,
                    LocationId = APWorldLocation.QuestMinorWilliamOfTheWisp,
                });

            Patches.Register(
                DialogueTreeID.Purifier_MercantileProvost,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 9,
                    LocationId = APWorldLocation.QuestMinorScholarsRansom,
                    NextNode = new OriginalNodeBuilder { NodeID = 9 },
                });
            Patches.Register(
                DialogueTreeID.Purifier_MercantileProvost,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 17,
                    LocationId = APWorldLocation.QuestMinorScholarsRansom,
                    NextNode = new OriginalNodeBuilder { NodeID = 17 },
                });

            Patches.Register(
                DialogueTreeID.Soroborean_BloodMageTrigger,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 6,
                    LocationId = APWorldLocation.QuestMinorBloodyBusiness,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.SA_BloodMageQuestEnds },
                });

            // repeatable "Ledger" quests

            Patches.Register(
                DialogueTreeID.Merchant_BergGeneralStore,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 5,
                    LocationId = APWorldLocation.QuestMinorLedgerToBerg,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.PromptsComplete_CierzoGeneral },
                });

            Patches.Register(
                DialogueTreeID.Merchant_CierzoGeneralStore,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 4,
                    LocationId = APWorldLocation.QuestMinorLedgerToCierzo,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.PromptsComplete_LevantGeneral },
                });

            Patches.Register(
                DialogueTreeID.Merchant_LevantGeneralStore,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 6,
                    LocationId = APWorldLocation.QuestMinorLedgerToLevant,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.PromptsComplete_MonsoonGeneral },
                });

            Patches.Register(
                DialogueTreeID.Merchant_MonsoonGeneralStore,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 5,
                    LocationId = APWorldLocation.QuestMinorLedgerToMonsoon,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.PromptsComplete_BergGeneral },
                });

            // repeatable "Need" quests

            Patches.Register(
                DialogueTreeID.Merchant_BergKaziteAssassin,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 7,
                    LocationId = APWorldLocation.QuestMinorNeedBeastGolemScraps,
                });

            Patches.Register(
                DialogueTreeID.Merchant_CierzoFishmongerA,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 7,
                    LocationId = APWorldLocation.QuestMinorNeedCierzoCeviche,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.SideQuests_CompleteCook },
                });

            Patches.Register(
                DialogueTreeID.Merchant_BergFoodStore,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 7,
                    LocationId = APWorldLocation.QuestMinorNeedManticoreTail,
                });

            Patches.Register(
                DialogueTreeID.Merchant_LevantFoodStore,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 8,
                    LocationId = APWorldLocation.QuestMinorNeedSharkCartilage,
                });

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanCamp,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 19,
                    LocationId = APWorldLocation.QuestMinorNeedAngelFoodCake,
                });

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanArmor,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 19,
                    LocationId = APWorldLocation.QuestMinorNeedFireElementalParticles,
                });

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanFood,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 19,
                    LocationId = APWorldLocation.QuestMinorNeedFireElementalParticles,
                });

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanGeneral,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 19,
                    LocationId = APWorldLocation.QuestMinorNeedTourmaline,
                });

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanWeapons,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 19,
                    LocationId = APWorldLocation.QuestMinorNeedShieldGolemScrap,
                });

            // individual commissions

            Patches.Register(
                DialogueTreeID.Merchant_CierzoBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 59,
                    LocationId = APWorldLocation.CommissionBlueSandHelm,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_CierzoBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_CierzoBlacksmithItemA },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_CierzoBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 64,
                    LocationId = APWorldLocation.CommissionBlueSandArmor,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_CierzoBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_CierzoBlacksmithItemB },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_CierzoBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 66,
                    LocationId = APWorldLocation.CommissionBlueSandBoots,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_CierzoBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_CierzoBlacksmithItemC },
                    }
                });

            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 84,
                    LocationId = APWorldLocation.CommissionCopalHelm,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithItemA },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 89,
                    LocationId = APWorldLocation.CommissionCopalArmor,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithItemB },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 92,
                    LocationId = APWorldLocation.CommissionCopalBoots,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithItemC },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 95,
                    LocationId = APWorldLocation.CommissionPetrifiedWoodHelm,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithItemD },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 100,
                    LocationId = APWorldLocation.CommissionPetrifiedWoodArmor,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithItemE },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_BergBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 102,
                    LocationId = APWorldLocation.CommissionPetrifiedWoodBoots,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_BergBlacksmithItemF },
                    }
                });

            Patches.Register(
                DialogueTreeID.Merchant_MonsoonBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 55,
                    LocationId = APWorldLocation.CommissionPalladiumHelm,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_MonsoonBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_MonsoonBlacksmithItemA },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_MonsoonBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 60,
                    LocationId = APWorldLocation.CommissionPalladiumArmor,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_MonsoonBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_MonsoonBlacksmithItemB },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_MonsoonBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 62,
                    LocationId = APWorldLocation.CommissionPalladiumBoots,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_MonsoonBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_MonsoonBlacksmithItemC },
                    }
                });

            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 87,
                    LocationId = APWorldLocation.CommissionTenebrousHelm,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithItemA },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 92,
                    LocationId = APWorldLocation.CommissionTenebrousArmor,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithItemB },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 95,
                    LocationId = APWorldLocation.CommissionTenebrousBoots,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithItemC },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 98,
                    LocationId = APWorldLocation.CommissionTsarHelm,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithItemD },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 103,
                    LocationId = APWorldLocation.CommissionTsarArmor,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithItemE },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_LevantBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 105,
                    LocationId = APWorldLocation.CommissionTsarBoots,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_LevantBlacksmithItemF },
                    }
                });

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 58,
                    LocationId = APWorldLocation.CommissionAntiquePlateSallet,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_HarmattanBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_HarmattanBlacksmithItemA },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_HarmattanBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 63,
                    LocationId = APWorldLocation.CommissionAntiquePlateGarb,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_HarmattanBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_HarmattanBlacksmithItemB },
                    }
                });
            Patches.Register(
                DialogueTreeID.Merchant_HarmattanBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 65,
                    LocationId = APWorldLocation.CommissionAntiquePlateBoots,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_HarmattanBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_HarmattanBlacksmithItemC },
                    }
                });

            // unique items

            Patches.Register(DialogueTreeID.StrangeRustedSword, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnStrangeRustedSword,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.Artifacts_Brand },
            });

            var dreamerHalberdPatch = new InsertLocationCheckPatch
            {
                ReplaceNodeID = 6,
                LocationId = APWorldLocation.SpawnDreamerHalberd,
            };
            Patches.Register(DialogueTreeID.Chersonese_Immaculate_Real, dreamerHalberdPatch);
            Patches.Register(DialogueTreeID.HallowedMarsh_Immaculate_Real, dreamerHalberdPatch);
            Patches.Register(DialogueTreeID.Abrassar_Immaculate_Real, dreamerHalberdPatch);
            Patches.Register(DialogueTreeID.Emercar_Immaculate_Real, dreamerHalberdPatch);

            Patches.Register(DialogueTreeID.RuinedHalberd, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnRuinedHalberd,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.DLC2Artifacts_Duty },
            });

            Patches.Register(DialogueTreeID.MysteriousLongBlade, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnMysteriousLongBlade,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.DLC2Artifacts_GepBlade },
            });

            Patches.Register(DialogueTreeID.DepoweredBludgeon, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnDepoweredBludgeon,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.DLC2Artifacts_GhostParallel },
            });

            Patches.Register(DialogueTreeID.FossilizedGreataxe, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnFossilizedGreataxe,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.DLC2Artifacts_Grind },
            });

            Patches.Register(DialogueTreeID.MertonsFirepoker, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 1,
                LocationId = APWorldLocation.SpawnMertonsFirepoker,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.Artifacts_Murton },
            });

            Patches.Register(DialogueTreeID.CeremonialBow, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnCeremonialBow,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.DLC2Artifacts_Murmure },
            });

            Patches.Register(DialogueTreeID.PillarGreathammer, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnPillarGreathammer,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.Artifacts_PillarHammer },
            });

            Patches.Register(DialogueTreeID.Soroborean_BossesGiantScourgeOnDeath_ParallelQuest, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 25,
                LocationId = APWorldLocation.SpawnPorcelainFists,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.Harmadung_GiantReward },
            });

            Patches.Register(DialogueTreeID.WarmAxe, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnWarmAxe,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.DLC2Artifacts_Sandrose },
            });

            Patches.Register(DialogueTreeID.Caldera_OpenSealedMaceBox, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnSealedMace,
            });

            Patches.Register(DialogueTreeID.RustedSpear, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnRustedSpear,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.DLC2Artifacts_Shriek },
            });

            Patches.Register(DialogueTreeID.SunfallAxe, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnSunfallAxe,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.Artifacts_Sunfall },
            });

            Patches.Register(DialogueTreeID.ThriceWroughtHalberd, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnThriceWroughtHalberd,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.Artifacts_WroughtHalbert },
            });

            Patches.Register(DialogueTreeID.UnusualKnuckles, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 1,
                LocationId = APWorldLocation.SpawnUnusualKnuckles,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.DLC2Artifacts_Tokebakicit },
            });

            Patches.Register(DialogueTreeID.TsarFists, new InsertLocationCheckPatch
            {
                ReplaceNodeID = 2,
                LocationId = APWorldLocation.SpawnTsarFists,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.Harmadung_D2_K_RLReward },
            });
        }

        private readonly HashSet<ulong> seenDialogueTrees = new();

        public void DumpDialogueTree(DialogueTreeExt tree)
        {
            var treeID = DialogueTreeID.FromTree(tree);
            if (seenDialogueTrees.Add(treeID.Hash.Value))
            {
                OutwardArchipelagoMod.Log.LogDebug($"Dialogue tree deserialized {treeID}: {tree._serializedGraph}");
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
