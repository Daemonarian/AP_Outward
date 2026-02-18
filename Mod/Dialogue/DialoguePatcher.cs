using System;
using System.Collections.Generic;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Dialogue.Builders.Actions;
using OutwardArchipelago.Dialogue.Builders.Conditions;
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

        private readonly HashSet<ulong> seenDialogueTrees = new();

        public void Awake()
        {
            RegisterAllPatches();
            Graph.onGraphDeserialized += OnGraphDeserialized;
        }

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
            // only patch dialogue trees when archipelago is enabled
            if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled)
            {
                // check if the graph is a dialogue tree
                if (graph is DialogueTreeExt tree)
                {
                    // DumpDialogueTree(tree);
                    PatchDialogueTree(tree);
                }
            }
        }
        public void PatchDialogueTree(DialogueTreeExt tree)
        {
            IDialoguePatchContext context = null;
            foreach (var patch in Patches.PatchesByDialogueTree(tree))
            {
                if (context == null)
                {
                    context = new DialoguePatchContext(tree);
                    //OutwardArchipelagoMod.Log.LogDebug($"Patching dialogue tree {context.TreeID}");
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
            // unique items

            Patches.Register(DialogueTreeID.Any, new ReplaceActionPatch
            {
                ActionPatch = new ReplaceItemRewardWithLocationCheckActionPatch
                {
                    ItemToLocation = APWorld.ItemToLocation,
                },
            });

            // Faction Pact checks

            Patches.Register(
                DialogueTreeID.RissaAberdeen_Neut_Prequest,
                new FactionPactGatekeepPatch
                {
                    ReplaceNodeID = 5,
                    Faction = APWorld.Faction.BlueChamber,
                    LocalizationKey = "dialogue.rissa.faction_pact_bc_required",
                    ActorName = "name_unpc_rissaaberdeen_01",
                });
            Patches.Register(
                DialogueTreeID.Calixa_Neut_Prequest,
                new FactionPactGatekeepPatch
                {
                    ReplaceNodeID = 1,
                    Faction = APWorld.Faction.HeroicKingdom,
                    LocalizationKey = "dialogue.calixa.faction_pact_hk_required",
                    ActorName = "name_unpc_calixa_01",
                });
            Patches.Register(
                DialogueTreeID.SimeonKing_Neut_Prequest,
                new FactionPactGatekeepPatch
                {
                    ReplaceNodeID = 1,
                    Faction = APWorld.Faction.HeroicKingdom,
                    LocalizationKey = "dialogue.calixa.faction_pact_hk_required",
                    ActorName = "name_unpc_calixa_01",
                });
            Patches.Register(
                DialogueTreeID.ElattAltar_Neut_Prequest,
                new FactionPactGatekeepPatch
                {
                    ReplaceNodeID = 34,
                    Faction = APWorld.Faction.HolyMission,
                    LocalizationKey = "dialogue.elatt.faction_pact_hm_required",
                    ActorName = "name_unpc_elatt_01",
                });
            Patches.Register(
                DialogueTreeID.Soroborean_HeadMaster_PQ1,
                new FactionPactGatekeepPatch
                {
                    ReplaceNodeID = 15,
                    Faction = APWorld.Faction.SoroborAcademy,
                    LocalizationKey = "dialogue.headmaster_salaberry.faction_pact_sa_required",
                    ActorName = "name_unpc_headmasterSalaberry_01",
                });

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
                    Locations = new[]
                    {
                        APWorld.Location.QuestParallelRustAndVengeance1,
                        APWorld.Location.QuestParallelRustAndVengeance2,
                        APWorld.Location.QuestParallelRustAndVengeance3,
                    },
                    NextNode = new OriginalNodeBuilder { NodeID = 13 }
                });

            // Minor Quests

            Patches.Register(
                DialogueTreeID.Merchant_BergAlchemist,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 7,
                    Location = APWorld.Location.QuestMinorAlchemyColdStone,
                    OtherAction = new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.SideQuests_AlchemistBerg },
                });

            Patches.Register(
                DialogueTreeID.Merchant_CierzoAlchemist,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 7,
                    Location = APWorld.Location.QuestMinorAlchemyCrystalPowder,
                });

            Patches.Register(
                DialogueTreeID.GoldLich_Neut_Initial,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 30,
                    Locations = new[]
                    {
                        APWorld.Location.QuestMinorBewareTheGoldLich1,
                        APWorld.Location.QuestMinorBewareTheGoldLich2,
                        APWorld.Location.QuestMinorBewareTheGoldLich3,
                        APWorld.Location.QuestMinorBewareTheGoldLich4,
                    },
                });

            Patches.Register(
                DialogueTreeID.JadeLich_Neut_Initial,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 22,
                    Locations = new[]
                    {
                        APWorld.Location.QuestMinorBewareTheJadeLich1,
                        APWorld.Location.QuestMinorBewareTheJadeLich2,
                        APWorld.Location.QuestMinorBewareTheJadeLich3,
                        APWorld.Location.QuestMinorBewareTheJadeLich4,
                    },
                });

            Patches.Register(
                DialogueTreeID.Cierzo_HelenTurnbull_Real,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 77,
                    Location = APWorld.Location.QuestMinorHelensFungus,
                    OtherAction = new RemoveItemActionBuilder { ItemID = MushroomShield },
                });

            Patches.Register(
                DialogueTreeID.TreasureHuntFinal,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 2,
                    Location = APWorld.Location.QuestMinorTreasureHunt,
                    OtherActions = new[]
                    {
                        new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.General_TsarAbraDock },
                        new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.PromptsComplete_TreasureHunt },
                    }
                });

            Patches.Register(
                DialogueTreeID.Purifier_MercantileProvost,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 9,
                    Location = APWorld.Location.QuestMinorScholarsRansom,
                    NextNode = new OriginalNodeBuilder { NodeID = 9 },
                });
            Patches.Register(
                DialogueTreeID.Purifier_MercantileProvost,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 17,
                    Location = APWorld.Location.QuestMinorScholarsRansom,
                    NextNode = new OriginalNodeBuilder { NodeID = 17 },
                });

            Patches.Register(
                DialogueTreeID.Soroborean_BloodMageTrigger,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 6,
                    Location = APWorld.Location.QuestMinorBloodyBusiness,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.SA_BloodMageQuestEnds },
                });

            // repeatable "Ledger" quests

            Patches.Register(
                DialogueTreeID.Merchant_BergGeneralStore,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 5,
                    Location = APWorld.Location.QuestMinorLedgerToBerg,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.PromptsComplete_CierzoGeneral },
                });

            Patches.Register(
                DialogueTreeID.Merchant_CierzoGeneralStore,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 4,
                    Location = APWorld.Location.QuestMinorLedgerToCierzo,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.PromptsComplete_LevantGeneral },
                });

            Patches.Register(
                DialogueTreeID.Merchant_LevantGeneralStore,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 6,
                    Location = APWorld.Location.QuestMinorLedgerToLevant,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.PromptsComplete_MonsoonGeneral },
                });

            Patches.Register(
                DialogueTreeID.Merchant_MonsoonGeneralStore,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 5,
                    Location = APWorld.Location.QuestMinorLedgerToMonsoon,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.PromptsComplete_BergGeneral },
                });

            // repeatable "Need" quests

            Patches.Register(
                DialogueTreeID.Merchant_BergKaziteAssassin,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 7,
                    Location = APWorld.Location.QuestMinorNeedBeastGolemScraps,
                });

            Patches.Register(
                DialogueTreeID.Merchant_CierzoFishmongerA,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 7,
                    Location = APWorld.Location.QuestMinorNeedCierzoCeviche,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.SideQuests_CompleteCook },
                });

            Patches.Register(
                DialogueTreeID.Merchant_BergFoodStore,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 7,
                    Location = APWorld.Location.QuestMinorNeedManticoreTail,
                });

            Patches.Register(
                DialogueTreeID.Merchant_LevantFoodStore,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 8,
                    Location = APWorld.Location.QuestMinorNeedSharkCartilage,
                });

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanCamp,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 19,
                    Location = APWorld.Location.QuestMinorNeedAngelFoodCake,
                });

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanArmor,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 19,
                    Location = APWorld.Location.QuestMinorNeedFireElementalParticles,
                });

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanFood,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 19,
                    Location = APWorld.Location.QuestMinorNeedFireElementalParticles,
                });

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanGeneral,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 19,
                    Location = APWorld.Location.QuestMinorNeedTourmaline,
                });

            Patches.Register(
                DialogueTreeID.Merchant_HarmattanWeapons,
                new InsertOneTimeLocationCheckPatch
                {
                    ReplaceNodeID = 19,
                    Location = APWorld.Location.QuestMinorNeedShieldGolemScrap,
                });

            // individual commissions

            Patches.Register(
                DialogueTreeID.Merchant_CierzoBlacksmith,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 59,
                    Location = APWorld.Location.CommissionBlueSandHelm,
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
                    Location = APWorld.Location.CommissionBlueSandArmor,
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
                    Location = APWorld.Location.CommissionBlueSandBoots,
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
                    Location = APWorld.Location.CommissionCopalHelm,
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
                    Location = APWorld.Location.CommissionCopalArmor,
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
                    Location = APWorld.Location.CommissionCopalBoots,
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
                    Location = APWorld.Location.CommissionPetrifiedWoodHelm,
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
                    Location = APWorld.Location.CommissionPetrifiedWoodArmor,
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
                    Location = APWorld.Location.CommissionPetrifiedWoodBoots,
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
                    Location = APWorld.Location.CommissionPalladiumHelm,
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
                    Location = APWorld.Location.CommissionPalladiumArmor,
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
                    Location = APWorld.Location.CommissionPalladiumBoots,
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
                    Location = APWorld.Location.CommissionTenebrousHelm,
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
                    Location = APWorld.Location.CommissionTenebrousArmor,
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
                    Location = APWorld.Location.CommissionTenebrousBoots,
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
                    Location = APWorld.Location.CommissionTsarHelm,
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
                    Location = APWorld.Location.CommissionTsarArmor,
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
                    Location = APWorld.Location.CommissionTsarBoots,
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
                    Location = APWorld.Location.CommissionAntiquePlateSallet,
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
                    Location = APWorld.Location.CommissionAntiquePlateGarb,
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
                    Location = APWorld.Location.CommissionAntiquePlateBoots,
                    OtherActions = new[]
                    {
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_HarmattanBlacksmithTimer },
                        new RemoveQuestEventActionBuilder { EventUID = OutwardQuestEvents.Crafting_HarmattanBlacksmithItemC },
                    }
                });

            // burac free skill
            Patches.Register(
                DialogueTreeID.Chersonese_Cierzo_BuracCarillon_Real,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 71,
                    Location = APWorld.Location.BuracFreeSkill,
                    OtherActions = new[]
                    {
                        new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.General_BuracGaveSkill },
                        new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.General_ReadyToBeTrained },
                    },
                    NextNode = new OriginalNodeBuilder { NodeID = 74 },
                });

            // acquire mana free skill
            var acquireManaFreeSkillPatch = new InsertLocationCheckPatch
            {
                ReplaceNodeID = 5,
                Location = APWorld.Location.WatcherFreeSkill,
                OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.General_ConfluxChoice },
                NextNode = new OriginalNodeBuilder { NodeID = 7 },
            };
            Patches.Register(DialogueTreeID.AcquireManaConflux, acquireManaFreeSkillPatch);
            Patches.Register(DialogueTreeID.AcquireManaSorobor, acquireManaFreeSkillPatch);

            // vendavel prisoner backstab
            Patches.Register(
                DialogueTreeID.PrisonerD_Neut_Vendavel,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 12,
                    Location = APWorld.Location.TrainVendavelPrisoner,
                    OtherActions = new IActionBuilder[]
                    {
                        new RemoveItemActionBuilder { ItemID = OutwardItem.ShivDagger },
                        new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.Vendavel_GaveShivToPrisoner },
                    }
                });

            // fix one-off skill trainer checks

            Patches.Register(
                DialogueTreeID.SagardBattleborn_HM_HallowPeacemaker,
                new ReplaceMultipleChoiceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 78 },
                    ChoiceIndex = 1,
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainSagardBattleborn, IsInverted = true },
                });
            Patches.Register(
                DialogueTreeID.SagardBattleborn_Neut_Prequest,
                new ReplaceMultipleChoiceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 16 },
                    ChoiceIndex = 1,
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainSagardBattleborn, IsInverted = true },
                });
            Patches.Register(
                DialogueTreeID.SagardBattleborn_Neut_Purifier,
                new ReplaceMultipleChoiceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 3 },
                    ChoiceIndex = 2,
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainSagardBattleborn, IsInverted = true },
                });
            Patches.Register(
                DialogueTreeID.SagardBattleborn_Neut_Initial,
                new ReplaceMultipleChoiceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 10 },
                    ChoiceIndex = 1,
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainSagardBattleborn, IsInverted = true },
                });
            Patches.Register(
                DialogueTreeID.SagardBattleborn_BC_AncestralPeacemaker,
                new ReplaceMultipleChoiceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 56 },
                    ChoiceIndex = 1,
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainSagardBattleborn, IsInverted = true },
                });
            Patches.Register(
                DialogueTreeID.SagardBattleborn_BC_MixedLegacies,
                new ReplaceMultipleChoiceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 15 },
                    ChoiceIndex = 1,
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainSagardBattleborn, IsInverted = true },
                });
            Patches.Register(
                DialogueTreeID.SagardBattleborn_BC_WhisperingBones,
                new ReplaceMultipleChoiceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 18 },
                    ChoiceIndex = 1,
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainSagardBattleborn, IsInverted = true },
                });

            Patches.Register(
                DialogueTreeID.Soeran_Neut_Initial,
                new ReplaceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 3 },
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainSoeran },
                });
            Patches.Register(
                DialogueTreeID.Soeran_Neut_Prequest,
                new ReplaceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 65 },
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainSoeran },
                });

            Patches.Register(
                DialogueTreeID.SimeonKing_HK_HeroPeacemaker,
                new ReplaceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 54 },
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainKingSimeon },
                });
            Patches.Register(
                DialogueTreeID.SimeonKing_HK_MouthToFeed,
                new ReplaceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 13 },
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainKingSimeon },
                });
            Patches.Register(
                DialogueTreeID.SimeonKing_HK_SandCorsair,
                new ReplaceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 3 },
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainKingSimeon },
                });
            Patches.Register(
                DialogueTreeID.SimeonKing_HK_TendFlame,
                new ReplaceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 4 },
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainKingSimeon },
                });
            Patches.Register(
                DialogueTreeID.SimeonKing_HM_HallowPeacemaker,
                new ReplaceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 41 },
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainKingSimeon },
                });
            Patches.Register(
                DialogueTreeID.SimeonKing_Neut_Initial,
                new ReplaceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 13 },
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainKingSimeon },
                });
            Patches.Register(
                DialogueTreeID.SimeonKing_Neut_Prequest,
                new ReplaceConditionPatch
                {
                    Node = new OriginalNodeBuilder { NodeID = 18 },
                    Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.TrainKingSimeon },
                });

            // wind altar location checks

            Patches.Register(
                DialogueTreeID.ChersPillar_Neut_COW,
                new InsertNodePatch
                {
                    ReplaceNode = new OriginalNodeBuilder { NodeID = 0 },
                    NewNode = new ConditionNodeBuilder
                    {
                        Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.WindAltarChersonese },
                        OnSuccess = new OriginalNodeBuilder { NodeID = 1 },
                        OnFailure = new ActionNodeBuilder
                        {
                            Action = new LocationCheckActionBuilder { Location = APWorld.Location.WindAltarChersonese },
                            NextNode = new OriginalNodeBuilder { NodeID = 4 },
                        },
                    },
                });
            Patches.Register(
                DialogueTreeID.EnmerkarPillar_Neut_COW,
                new InsertNodePatch
                {
                    ReplaceNode = new OriginalNodeBuilder { NodeID = 0 },
                    NewNode = new ConditionNodeBuilder
                    {
                        Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.WindAltarEnmerkarForest },
                        OnSuccess = new OriginalNodeBuilder { NodeID = 1 },
                        OnFailure = new ActionNodeBuilder
                        {
                            Action = new LocationCheckActionBuilder { Location = APWorld.Location.WindAltarEnmerkarForest },
                            NextNode = new OriginalNodeBuilder { NodeID = 4 },
                        },
                    },
                });
            Patches.Register(
                DialogueTreeID.AbrassPillar_Neut_COW,
                new InsertNodePatch
                {
                    ReplaceNode = new OriginalNodeBuilder { NodeID = 0 },
                    NewNode = new ConditionNodeBuilder
                    {
                        Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.WindAltarAbrassar },
                        OnSuccess = new OriginalNodeBuilder { NodeID = 1 },
                        OnFailure = new ActionNodeBuilder
                        {
                            Action = new LocationCheckActionBuilder { Location = APWorld.Location.WindAltarAbrassar },
                            NextNode = new OriginalNodeBuilder { NodeID = 4 },
                        },
                    },
                });
            Patches.Register(
                DialogueTreeID.MarshPillar_Neut_COW,
                new InsertNodePatch
                {
                    ReplaceNode = new OriginalNodeBuilder { NodeID = 0 },
                    NewNode = new ConditionNodeBuilder
                    {
                        Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.WindAltarHallowedMarsh },
                        OnSuccess = new OriginalNodeBuilder { NodeID = 1 },
                        OnFailure = new ActionNodeBuilder
                        {
                            Action = new LocationCheckActionBuilder { Location = APWorld.Location.WindAltarHallowedMarsh },
                            NextNode = new OriginalNodeBuilder { NodeID = 4 },
                        },
                    },
                });
            Patches.Register(
                DialogueTreeID.HarmattanPillar_Neut_COW,
                new InsertNodePatch
                {
                    ReplaceNode = new OriginalNodeBuilder { NodeID = 0 },
                    NewNode = new ConditionNodeBuilder
                    {
                        Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.WindAltarAntiquePlateau },
                        OnSuccess = new OriginalNodeBuilder { NodeID = 1 },
                        OnFailure = new ActionNodeBuilder
                        {
                            Action = new LocationCheckActionBuilder { Location = APWorld.Location.WindAltarAntiquePlateau },
                            NextNode = new OriginalNodeBuilder { NodeID = 4 },
                        },
                    },
                });
            Patches.Register(
                DialogueTreeID.CalderaPillar_Neut_COW,
                new InsertNodePatch
                {
                    ReplaceNode = new OriginalNodeBuilder { NodeID = 0 },
                    NewNode = new ConditionNodeBuilder
                    {
                        Condition = new LocationCheckConditionBuilder { Location = APWorld.Location.WindAltarCaldera },
                        OnSuccess = new OriginalNodeBuilder { NodeID = 5 },
                        OnFailure = new ActionNodeBuilder
                        {
                            Action = new LocationCheckActionBuilder { Location = APWorld.Location.WindAltarCaldera },
                            NextNode = new OriginalNodeBuilder { NodeID = 3 },
                        },
                    },
                });

            // friendly immaculate gift

            Patches.Register(
                DialogueTreeID.Chersonese_Immaculate_Real,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 29,
                    Location = APWorld.Location.FriendlyImmaculateChersonese,
                    NextNode = new ConditionNodeBuilder
                    {
                        Condition = new QuestEventConditionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateItems, MinStack = 2 },
                        OnSuccess = new OriginalNodeBuilder { NodeID = 32 },
                        OnFailure = new ActionNodeBuilder
                        {
                            Action = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateItems },
                            NextNode = new OriginalNodeBuilder { NodeID = 32 },
                        },
                    }
                });
            Patches.Register(
                DialogueTreeID.Emercar_Immaculate_Real,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 29,
                    Location = APWorld.Location.FriendlyImmaculateEnmerkarForest,
                    NextNode = new ConditionNodeBuilder
                    {
                        Condition = new QuestEventConditionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateItems, MinStack = 2 },
                        OnSuccess = new OriginalNodeBuilder { NodeID = 32 },
                        OnFailure = new ActionNodeBuilder
                        {
                            Action = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateItems },
                            NextNode = new OriginalNodeBuilder { NodeID = 32 },
                        },
                    }
                });
            Patches.Register(
                DialogueTreeID.Abrassar_Immaculate_Real,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 29,
                    Location = APWorld.Location.FriendlyImmaculateAbrassar,
                    NextNode = new ConditionNodeBuilder
                    {
                        Condition = new QuestEventConditionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateItems, MinStack = 2 },
                        OnSuccess = new OriginalNodeBuilder { NodeID = 32 },
                        OnFailure = new ActionNodeBuilder
                        {
                            Action = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateItems },
                            NextNode = new OriginalNodeBuilder { NodeID = 32 },
                        },
                    }
                });
            Patches.Register(
                DialogueTreeID.HallowedMarsh_Immaculate_Real,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 29,
                    Location = APWorld.Location.FriendlyImmaculateHallowedMarsh,
                    NextNode = new ConditionNodeBuilder
                    {
                        Condition = new QuestEventConditionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateItems, MinStack = 2 },
                        OnSuccess = new OriginalNodeBuilder { NodeID = 32 },
                        OnFailure = new ActionNodeBuilder
                        {
                            Action = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateItems },
                            NextNode = new OriginalNodeBuilder { NodeID = 32 },
                        },
                    }
                });
            Patches.Register(
                DialogueTreeID.AntiqueField_Immaculate_Real,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 17,
                    Location = APWorld.Location.FriendlyImmaculateAntiquePlateau,
                    OtherAction = new SendQuestEventActionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateAntiqueField },
                });
            Patches.Register(
                DialogueTreeID.Caldera_Immaculate_Real,
                new InsertLocationCheckPatch
                {
                    ReplaceNodeID = 17,
                    Location = APWorld.Location.FriendlyImmaculateCaldera,
                    NextNode = new OriginalNodeBuilder { NodeID = 19 },
                });

            // dreamer halberd always available

            var dreamerHalberdPatch = new InsertNodePatch
            {
                ReplaceNode = new OriginalNodeBuilder { NodeID = 0 },
                NewNode = new ConditionNodeBuilder
                {
                    Condition = new ConditionListBuilder
                    {
                        CheckMode = ConditionList.ConditionsCheckMode.AllTrueRequired,
                        Conditions = new List<IConditionBuilder>
                        {
                            new QuestEventConditionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateChersonese },
                            new QuestEventConditionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateEnmerkar },
                            new QuestEventConditionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateAbrassar },
                            new QuestEventConditionBuilder { EventUID = OutwardQuestEvents.SideQuests_ImmaculateHallowedMarsh},
                        },
                    },
                    OnSuccess = new OriginalNodeBuilder { NodeID = 1 },
                    OnFailure = new OriginalNodeBuilder { NodeID = 11 },
                }
            };
            Patches.Register(DialogueTreeID.Chersonese_Immaculate_Real, dreamerHalberdPatch);
            Patches.Register(DialogueTreeID.Emercar_Immaculate_Real, dreamerHalberdPatch);
            Patches.Register(DialogueTreeID.Abrassar_Immaculate_Real, dreamerHalberdPatch);
            Patches.Register(DialogueTreeID.HallowedMarsh_Immaculate_Real, dreamerHalberdPatch);

            var dreamerHalberdNoKillPatch = new InsertNodePatch
            {
                ReplaceNode = new OriginalNodeBuilder { NodeID = 5 },
                NewNode = new DescendantNodeBuilder { NodeID = 5 },
            };
            Patches.Register(DialogueTreeID.Chersonese_Immaculate_Real, dreamerHalberdNoKillPatch);
            Patches.Register(DialogueTreeID.Emercar_Immaculate_Real, dreamerHalberdNoKillPatch);
            Patches.Register(DialogueTreeID.Abrassar_Immaculate_Real, dreamerHalberdNoKillPatch);
            Patches.Register(DialogueTreeID.HallowedMarsh_Immaculate_Real, dreamerHalberdNoKillPatch);
        }
    }
}
