using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.Outward
{
    internal class OutwardAudio
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Sounds
        {
            [EnumMember(Value = "NONE")]
            NONE,
            [EnumMember(Value = "BGM_OutwardTheme")]
            BGM_OutwardTheme,
            [EnumMember(Value = "BGM_StandardCombat")]
            BGM_StandardCombat,
            [EnumMember(Value = "BGM_CierzoDay")]
            BGM_CierzoDay,
            [EnumMember(Value = "BGM_ChersoneseDay")]
            BGM_ChersoneseDay,
            [EnumMember(Value = "BGM_Dungeon2")]
            BGM_Dungeon2 = 6,
            [EnumMember(Value = "BGM_StandardCombat2")]
            BGM_StandardCombat2,
            [EnumMember(Value = "BGM_CierzoNight")]
            BGM_CierzoNight,
            [EnumMember(Value = "BGM_ChersoneseNight")]
            BGM_ChersoneseNight,
            [EnumMember(Value = "BGM_DungeonAmbiant")]
            BGM_DungeonAmbiant,
            [EnumMember(Value = "BGM_DungeonRuins")]
            BGM_DungeonRuins,
            [EnumMember(Value = "BGM_DungeonWild")]
            BGM_DungeonWild,
            [EnumMember(Value = "BGM_CombatAbrassar")]
            BGM_CombatAbrassar,
            [EnumMember(Value = "BGM_CombatChersonese")]
            BGM_CombatChersonese,
            [EnumMember(Value = "BGM_CombatEnmerkar")]
            BGM_CombatEnmerkar,
            [EnumMember(Value = "BGM_CombatHallowedMarsh")]
            BGM_CombatHallowedMarsh,
            [EnumMember(Value = "BGM_EventDanger")]
            BGM_EventDanger,
            [EnumMember(Value = "BGM_EventDramatic")]
            BGM_EventDramatic,
            [EnumMember(Value = "BGM_EventFriendly")]
            BGM_EventFriendly,
            [EnumMember(Value = "BGM_RegionAbrassar")]
            BGM_RegionAbrassar,
            [EnumMember(Value = "BGM_RegionAntiqueFields")]
            BGM_RegionAntiqueFields,
            [EnumMember(Value = "BGM_RegionChersonese")]
            BGM_RegionChersonese,
            [EnumMember(Value = "BGM_RegionChersoneseNIGHT")]
            BGM_RegionChersoneseNIGHT,
            [EnumMember(Value = "BGM_RegionEnmerkarForest")]
            BGM_RegionEnmerkarForest,
            [EnumMember(Value = "BGM_RegionHallowedMarsh")]
            BGM_RegionHallowedMarsh,
            [EnumMember(Value = "BGM_RegionHallowedMarshNIGHT")]
            BGM_RegionHallowedMarshNIGHT,
            [EnumMember(Value = "BGM_RegionKarburan")]
            BGM_RegionKarburan,
            [EnumMember(Value = "BGM_TownBerg")]
            BGM_TownBerg,
            [EnumMember(Value = "BGM_TownBergNIGHT")]
            BGM_TownBergNIGHT,
            [EnumMember(Value = "BGM_TownCierzo")]
            BGM_TownCierzo,
            [EnumMember(Value = "BGM_TownCierzoNIGHT")]
            BGM_TownCierzoNIGHT,
            [EnumMember(Value = "BGM_TownHarmattanNIGHT")]
            BGM_TownHarmattanNIGHT,
            [EnumMember(Value = "BGM_TownKaraburanCity")]
            BGM_TownKaraburanCity,
            [EnumMember(Value = "BGM_TownMarket")]
            BGM_TownMarket,
            [EnumMember(Value = "BGM_TownMonsoon")]
            BGM_TownMonsoon,
            [EnumMember(Value = "BGM_GeneralTitleScreen")]
            BGM_GeneralTitleScreen,
            [EnumMember(Value = "BGM_TownMonsoonNIGHT")]
            BGM_TownMonsoonNIGHT,
            [EnumMember(Value = "BGM_TownLevant")]
            BGM_TownLevant,
            [EnumMember(Value = "BGM_TownLevantNIGHT")]
            BGM_TownLevantNIGHT,
            [EnumMember(Value = "BGM_CombatBoss")]
            BGM_CombatBoss,
            [EnumMember(Value = "BGM_CombatMiniboss")]
            BGM_CombatMiniboss,
            [EnumMember(Value = "BGM_RegionEnmerkarForestNIGHT")]
            BGM_RegionEnmerkarForestNIGHT,
            [EnumMember(Value = "BGM_RegionAbrassarNIGHT")]
            BGM_RegionAbrassarNIGHT,
            [EnumMember(Value = "BGM_DungeonManmade")]
            BGM_DungeonManmade,
            [EnumMember(Value = "BGM_EventQuest")]
            BGM_EventQuest,
            [EnumMember(Value = "BGM_Empty")]
            BGM_Empty,
            [EnumMember(Value = "BGM_CombatAntiquePlateau")]
            BGM_CombatAntiquePlateau,
            [EnumMember(Value = "BGM_CombatBossDLC1")]
            BGM_CombatBossDLC1,
            [EnumMember(Value = "BGM_CombatDungeonAntique")]
            BGM_CombatDungeonAntique,
            [EnumMember(Value = "BGM_CombatDungeonFactory")]
            BGM_CombatDungeonFactory,
            [EnumMember(Value = "BGM_CombatMinibossDLC1")]
            BGM_CombatMinibossDLC1,
            [EnumMember(Value = "BGM_DungeonAntique")]
            BGM_DungeonAntique,
            [EnumMember(Value = "BGM_DungeonFactory")]
            BGM_DungeonFactory,
            [EnumMember(Value = "BGM_EventMystery")]
            BGM_EventMystery,
            [EnumMember(Value = "BGM_RegionAntiquePlateau")]
            BGM_RegionAntiquePlateau,
            [EnumMember(Value = "BGM_RegionAntiquePlateauNIGHT")]
            BGM_RegionAntiquePlateauNIGHT,
            [EnumMember(Value = "BGM_TownHarmattan")]
            BGM_TownHarmattan,
            [EnumMember(Value = "BGM_CalderaDay")]
            BGM_CalderaDay,
            [EnumMember(Value = "BGM_CalderaNight")]
            BGM_CalderaNight,
            [EnumMember(Value = "BGM_CombatBossDLC2")]
            BGM_CombatBossDLC2,
            [EnumMember(Value = "BGM_CombatCaldera")]
            BGM_CombatCaldera,
            [EnumMember(Value = "BGM_CombatDungeonCaldera")]
            BGM_CombatDungeonCaldera,
            [EnumMember(Value = "BGM_CombatDungeonVolcanic")]
            BGM_CombatDungeonVolcanic,
            [EnumMember(Value = "BGM_CombatMinibossDLC2")]
            BGM_CombatMinibossDLC2,
            [EnumMember(Value = "BGM_DungeonCaldera")]
            BGM_DungeonCaldera,
            [EnumMember(Value = "BGM_DungeonFinal")]
            BGM_DungeonFinal,
            [EnumMember(Value = "BGM_DungeonVolcanic")]
            BGM_DungeonVolcanic,
            [EnumMember(Value = "BGM_TownNewSirocco")]
            BGM_TownNewSirocco,
            [EnumMember(Value = "BGM_TownNewSiroccoNIGHT")]
            BGM_TownNewSiroccoNIGHT,
            [EnumMember(Value = "ENV_Morning")]
            ENV_Morning = 1000,
            [EnumMember(Value = "ENV_DayTime")]
            ENV_DayTime,
            [EnumMember(Value = "ENV_Evening")]
            ENV_Evening,
            [EnumMember(Value = "ENV_Night")]
            ENV_Night,
            [EnumMember(Value = "ENV_WaterWaves01")]
            ENV_WaterWaves01,
            [EnumMember(Value = "ENV_Wind")]
            ENV_Wind,
            [EnumMember(Value = "ENV_RainAverage")]
            ENV_RainAverage,
            [EnumMember(Value = "ENV_WoodCracking01")]
            ENV_WoodCracking01,
            [EnumMember(Value = "ENV_WoodCracking02")]
            ENV_WoodCracking02,
            [EnumMember(Value = "ENV_WoodCracking03")]
            ENV_WoodCracking03,
            [EnumMember(Value = "ENV_WoodCracking04")]
            ENV_WoodCracking04,
            [EnumMember(Value = "ENV_WoodCracking05")]
            ENV_WoodCracking05,
            [EnumMember(Value = "ENV_VigilSoundsLoop")]
            ENV_VigilSoundsLoop,
            [EnumMember(Value = "ENV_BlueSand_loop")]
            ENV_BlueSand_loop,
            [EnumMember(Value = "ENV_CicadaSoundAmbiance_01")]
            ENV_CicadaSoundAmbiance_01,
            [EnumMember(Value = "ENV_CicadaSoundAmbiance_02")]
            ENV_CicadaSoundAmbiance_02,
            [EnumMember(Value = "ENV_CrowInTheDistance01")]
            ENV_CrowInTheDistance01,
            [EnumMember(Value = "ENV_CrowInTheDistance02")]
            ENV_CrowInTheDistance02,
            [EnumMember(Value = "ENV_CrowInTheDistance03")]
            ENV_CrowInTheDistance03,
            [EnumMember(Value = "ENV_EchoingDropletInCave_Average")]
            ENV_EchoingDropletInCave_Average,
            [EnumMember(Value = "ENV_EchoingDropletInCave_High")]
            ENV_EchoingDropletInCave_High,
            [EnumMember(Value = "ENV_EchoingDropletInCave_Low")]
            ENV_EchoingDropletInCave_Low,
            [EnumMember(Value = "ENV_ElevatorSounds_In")]
            ENV_ElevatorSounds_In,
            [EnumMember(Value = "ENV_ElevatorSounds_Loop")]
            ENV_ElevatorSounds_Loop,
            [EnumMember(Value = "ENV_ElevatorSounds_Out")]
            ENV_ElevatorSounds_Out,
            [EnumMember(Value = "ENV_FishInWater01")]
            ENV_FishInWater01,
            [EnumMember(Value = "ENV_FishInWater02")]
            ENV_FishInWater02,
            [EnumMember(Value = "ENV_FishInWater03")]
            ENV_FishInWater03,
            [EnumMember(Value = "ENV_FishInWater04")]
            ENV_FishInWater04,
            [EnumMember(Value = "ENV_FliesBuzzingOnCarcass")]
            ENV_FliesBuzzingOnCarcass,
            [EnumMember(Value = "ENV_HallowedMarshFirefliesNightAmbience")]
            ENV_HallowedMarshFirefliesNightAmbience,
            [EnumMember(Value = "ENV_HallowedMarshMudBubbleBursting01")]
            ENV_HallowedMarshMudBubbleBursting01,
            [EnumMember(Value = "ENV_HallowedMarshMudBubbleBursting02")]
            ENV_HallowedMarshMudBubbleBursting02,
            [EnumMember(Value = "ENV_HallowedMarshMudBubbleBursting03")]
            ENV_HallowedMarshMudBubbleBursting03,
            [EnumMember(Value = "ENV_HallowedMarshMudBubbleBurstingLoop")]
            ENV_HallowedMarshMudBubbleBurstingLoop,
            [EnumMember(Value = "ENV_HeavyHummingNearGiantHornetNest")]
            ENV_HeavyHummingNearGiantHornetNest,
            [EnumMember(Value = "ENV_HeavyWoodCrack01")]
            ENV_HeavyWoodCrack01,
            [EnumMember(Value = "ENV_HeavyWoodCrack02")]
            ENV_HeavyWoodCrack02,
            [EnumMember(Value = "ENV_HeavyWoodCrack03")]
            ENV_HeavyWoodCrack03,
            [EnumMember(Value = "ENV_HeavyWoodCrack04")]
            ENV_HeavyWoodCrack04,
            [EnumMember(Value = "ENV_HeavyWoodCrack05")]
            ENV_HeavyWoodCrack05,
            [EnumMember(Value = "ENV_MachinerySounds_Loop")]
            ENV_MachinerySounds_Loop,
            [EnumMember(Value = "ENV_MetalWireDoorOpening")]
            ENV_MetalWireDoorOpening,
            [EnumMember(Value = "ENV_RainOnClothAverage")]
            ENV_RainOnClothAverage,
            [EnumMember(Value = "ENV_RainOnClothHeavy")]
            ENV_RainOnClothHeavy,
            [EnumMember(Value = "ENV_SandDustDroppingFromRoof01")]
            ENV_SandDustDroppingFromRoof01,
            [EnumMember(Value = "ENV_SandDustDroppingFromRoof02")]
            ENV_SandDustDroppingFromRoof02,
            [EnumMember(Value = "ENV_SandDustDroppingFromRoof03")]
            ENV_SandDustDroppingFromRoof03,
            [EnumMember(Value = "ENV_SandDustDroppingFromRoof04")]
            ENV_SandDustDroppingFromRoof04,
            [EnumMember(Value = "ENV_SuspendedWoodenBridgeMoveAndCrack01")]
            ENV_SuspendedWoodenBridgeMoveAndCrack01,
            [EnumMember(Value = "ENV_SuspendedWoodenBridgeMoveAndCrack02")]
            ENV_SuspendedWoodenBridgeMoveAndCrack02,
            [EnumMember(Value = "ENV_SuspendedWoodenBridgeMoveAndCrack03")]
            ENV_SuspendedWoodenBridgeMoveAndCrack03,
            [EnumMember(Value = "ENV_SuspendedWoodenBridgeMoveAndCrack04")]
            ENV_SuspendedWoodenBridgeMoveAndCrack04,
            [EnumMember(Value = "ENV_SuspendedWoodenBridgeMoveAndCrack05")]
            ENV_SuspendedWoodenBridgeMoveAndCrack05,
            [EnumMember(Value = "ENV_SuspendedWoodenBridgeMoveAndCrack06")]
            ENV_SuspendedWoodenBridgeMoveAndCrack06,
            [EnumMember(Value = "ENV_SuspendedWoodenBridgeMoveAndCrack07")]
            ENV_SuspendedWoodenBridgeMoveAndCrack07,
            [EnumMember(Value = "ENV_SuspendedWoodenBridgeMoveAndCrack08")]
            ENV_SuspendedWoodenBridgeMoveAndCrack08,
            [EnumMember(Value = "ENV_SuspendedWoodenBridgeMoveAndCrack09")]
            ENV_SuspendedWoodenBridgeMoveAndCrack09,
            [EnumMember(Value = "ENV_AirVent")]
            ENV_AirVent,
            [EnumMember(Value = "ENV_ChainSwinging01")]
            ENV_ChainSwinging01,
            [EnumMember(Value = "ENV_ChainSwinging02")]
            ENV_ChainSwinging02,
            [EnumMember(Value = "ENV_ChainSwinging03")]
            ENV_ChainSwinging03,
            [EnumMember(Value = "ENV_ChainSwinging04")]
            ENV_ChainSwinging04,
            [EnumMember(Value = "ENV_CorruptionFissuresLoop")]
            ENV_CorruptionFissuresLoop,
            [EnumMember(Value = "ENV_ExteriorSoundLoop01")]
            ENV_ExteriorSoundLoop01,
            [EnumMember(Value = "ENV_ExteriorSoundLoop02")]
            ENV_ExteriorSoundLoop02,
            [EnumMember(Value = "ENV_ExteriorSoundLoop03")]
            ENV_ExteriorSoundLoop03,
            [EnumMember(Value = "ENV_FlagSwinging01")]
            ENV_FlagSwinging01,
            [EnumMember(Value = "ENV_FlagSwinging02")]
            ENV_FlagSwinging02,
            [EnumMember(Value = "ENV_FlagSwinging03")]
            ENV_FlagSwinging03,
            [EnumMember(Value = "ENV_FlagSwinging04")]
            ENV_FlagSwinging04,
            [EnumMember(Value = "ENV_FrostWindBlow01")]
            ENV_FrostWindBlow01,
            [EnumMember(Value = "ENV_FrostWindBlow02")]
            ENV_FrostWindBlow02,
            [EnumMember(Value = "ENV_FrostWindBlow03")]
            ENV_FrostWindBlow03,
            [EnumMember(Value = "ENV_FrostWindBlow04")]
            ENV_FrostWindBlow04,
            [EnumMember(Value = "ENV_FrostWindBlow05")]
            ENV_FrostWindBlow05,
            [EnumMember(Value = "ENV_FrostWindLoop")]
            ENV_FrostWindLoop,
            [EnumMember(Value = "ENV_HeatWave01")]
            ENV_HeatWave01,
            [EnumMember(Value = "ENV_HeatWave02")]
            ENV_HeatWave02,
            [EnumMember(Value = "ENV_HeatWave03")]
            ENV_HeatWave03,
            [EnumMember(Value = "ENV_InsectsCrawling01")]
            ENV_InsectsCrawling01,
            [EnumMember(Value = "ENV_InsectsCrawling02")]
            ENV_InsectsCrawling02,
            [EnumMember(Value = "ENV_InsectsCrawling03")]
            ENV_InsectsCrawling03,
            [EnumMember(Value = "ENV_InsectsCrawling04")]
            ENV_InsectsCrawling04,
            [EnumMember(Value = "ENV_InsectsCrawling05")]
            ENV_InsectsCrawling05,
            [EnumMember(Value = "ENV_MagicSourceLoop")]
            ENV_MagicSourceLoop,
            [EnumMember(Value = "ENV_RiverStream01")]
            ENV_RiverStream01,
            [EnumMember(Value = "ENV_RiverStream02")]
            ENV_RiverStream02,
            [EnumMember(Value = "ENV_RockCrumbling01")]
            ENV_RockCrumbling01,
            [EnumMember(Value = "ENV_RockCrumbling02")]
            ENV_RockCrumbling02,
            [EnumMember(Value = "ENV_RockCrumbling03")]
            ENV_RockCrumbling03,
            [EnumMember(Value = "ENV_RockCrumbling04")]
            ENV_RockCrumbling04,
            [EnumMember(Value = "ENV_RockCrumbling05")]
            ENV_RockCrumbling05,
            [EnumMember(Value = "ENV_RockCrumbling06")]
            ENV_RockCrumbling06,
            [EnumMember(Value = "ENV_RockCrumbling07")]
            ENV_RockCrumbling07,
            [EnumMember(Value = "ENV_RockCrumbling08")]
            ENV_RockCrumbling08,
            [EnumMember(Value = "ENV_RockCrumbling09")]
            ENV_RockCrumbling09,
            [EnumMember(Value = "ENV_RopeSqueakSwinging01")]
            ENV_RopeSqueakSwinging01,
            [EnumMember(Value = "ENV_RopeSqueakSwinging02")]
            ENV_RopeSqueakSwinging02,
            [EnumMember(Value = "ENV_RopeSqueakSwinging03")]
            ENV_RopeSqueakSwinging03,
            [EnumMember(Value = "ENV_RopeSqueakSwinging04")]
            ENV_RopeSqueakSwinging04,
            [EnumMember(Value = "ENV_SandDustInTheWind01")]
            ENV_SandDustInTheWind01,
            [EnumMember(Value = "ENV_SandDustInTheWind02")]
            ENV_SandDustInTheWind02,
            [EnumMember(Value = "ENV_SandDustInTheWind03")]
            ENV_SandDustInTheWind03,
            [EnumMember(Value = "ENV_SandDustInTheWind04")]
            ENV_SandDustInTheWind04,
            [EnumMember(Value = "ENV_SandDustInTheWind05")]
            ENV_SandDustInTheWind05,
            [EnumMember(Value = "ENV_SandDustInTheWind06")]
            ENV_SandDustInTheWind06,
            [EnumMember(Value = "ENV_SandDustInTheWind07")]
            ENV_SandDustInTheWind07,
            [EnumMember(Value = "ENV_SandDustInTheWind08")]
            ENV_SandDustInTheWind08,
            [EnumMember(Value = "ENV_ShopSignsSwinging01")]
            ENV_ShopSignsSwinging01,
            [EnumMember(Value = "ENV_ShopSignsSwinging02")]
            ENV_ShopSignsSwinging02,
            [EnumMember(Value = "ENV_SlimyLoopingSound")]
            ENV_SlimyLoopingSound,
            [EnumMember(Value = "ENV_WaterCave01")]
            ENV_WaterCave01,
            [EnumMember(Value = "ENV_WaterStream01")]
            ENV_WaterStream01,
            [EnumMember(Value = "ENV_WaterTankMovement01")]
            ENV_WaterTankMovement01,
            [EnumMember(Value = "ENV_WaterTankMovement02")]
            ENV_WaterTankMovement02,
            [EnumMember(Value = "ENV_WaterTankMovement03")]
            ENV_WaterTankMovement03,
            [EnumMember(Value = "ENV_WaterTankMovement04")]
            ENV_WaterTankMovement04,
            [EnumMember(Value = "ENV_WaterTankMovement05")]
            ENV_WaterTankMovement05,
            [EnumMember(Value = "ENV_WaterTankMovement06")]
            ENV_WaterTankMovement06,
            [EnumMember(Value = "ENV_Cave_Cavern_AMB_Rumble_loop")]
            ENV_Cave_Cavern_AMB_Rumble_loop,
            [EnumMember(Value = "ENV_Dungeon_AMB_Rumble_loop")]
            ENV_Dungeon_AMB_Rumble_loop,
            [EnumMember(Value = "ENV_HeavyExplosion01")]
            ENV_HeavyExplosion01,
            [EnumMember(Value = "ENV_Blacksmith_HammerHit_01")]
            ENV_Blacksmith_HammerHit_01,
            [EnumMember(Value = "ENV_Blacksmith_HammerHit_02")]
            ENV_Blacksmith_HammerHit_02,
            [EnumMember(Value = "ENV_Blacksmith_HammerHit_03")]
            ENV_Blacksmith_HammerHit_03,
            [EnumMember(Value = "ENV_Blacksmith_HammerHit_04")]
            ENV_Blacksmith_HammerHit_04,
            [EnumMember(Value = "ENV_Blacksmith_HammerHit_05")]
            ENV_Blacksmith_HammerHit_05,
            [EnumMember(Value = "ENV_Blacksmith_HammerHit_06")]
            ENV_Blacksmith_HammerHit_06,
            [EnumMember(Value = "ENV_Blacksmith_HammerHit_07")]
            ENV_Blacksmith_HammerHit_07,
            [EnumMember(Value = "ENV_Blacksmith_HammerHit_08")]
            ENV_Blacksmith_HammerHit_08,
            [EnumMember(Value = "ENV_Geyser_Eruption_01")]
            ENV_Geyser_Eruption_01,
            [EnumMember(Value = "ENV_Geyser_Eruption_02")]
            ENV_Geyser_Eruption_02,
            [EnumMember(Value = "ENV_Geyser_Eruption_03")]
            ENV_Geyser_Eruption_03,
            [EnumMember(Value = "ENV_Geyser_Eruption_04")]
            ENV_Geyser_Eruption_04,
            [EnumMember(Value = "ENV_LiquidFlowinPipe_01")]
            ENV_LiquidFlowinPipe_01,
            [EnumMember(Value = "ENV_LiquidFlowinPipe_02")]
            ENV_LiquidFlowinPipe_02,
            [EnumMember(Value = "ENV_LiquidFlowinPipe_03")]
            ENV_LiquidFlowinPipe_03,
            [EnumMember(Value = "ENV_MagicMachinery_01")]
            ENV_MagicMachinery_01,
            [EnumMember(Value = "ENV_MagicMachinery_02")]
            ENV_MagicMachinery_02,
            [EnumMember(Value = "ENV_MagicMachinery_03")]
            ENV_MagicMachinery_03,
            [EnumMember(Value = "ENV_MagicMachinery_04")]
            ENV_MagicMachinery_04,
            [EnumMember(Value = "ENV_ScrewsAndMetalScrapFalling_01")]
            ENV_ScrewsAndMetalScrapFalling_01,
            [EnumMember(Value = "ENV_ScrewsAndMetalScrapFalling_02")]
            ENV_ScrewsAndMetalScrapFalling_02,
            [EnumMember(Value = "ENV_ScrewsAndMetalScrapFalling_03")]
            ENV_ScrewsAndMetalScrapFalling_03,
            [EnumMember(Value = "ENV_ScrewsAndMetalScrapFalling_04")]
            ENV_ScrewsAndMetalScrapFalling_04,
            [EnumMember(Value = "ENV_ScrewsAndMetalScrapFalling_05")]
            ENV_ScrewsAndMetalScrapFalling_05,
            [EnumMember(Value = "ENV_ScrewsAndMetalScrapFalling_06")]
            ENV_ScrewsAndMetalScrapFalling_06,
            [EnumMember(Value = "ENV_ScrewsAndMetalScrapFalling_07")]
            ENV_ScrewsAndMetalScrapFalling_07,
            [EnumMember(Value = "ENV_SteamBurst_01")]
            ENV_SteamBurst_01,
            [EnumMember(Value = "ENV_SteamBurst_02")]
            ENV_SteamBurst_02,
            [EnumMember(Value = "ENV_SteamBurst_03")]
            ENV_SteamBurst_03,
            [EnumMember(Value = "ENV_SteamLoop_01")]
            ENV_SteamLoop_01,
            [EnumMember(Value = "ENV_SteamLoop_02")]
            ENV_SteamLoop_02,
            [EnumMember(Value = "ENV_SteamLoop_03")]
            ENV_SteamLoop_03,
            [EnumMember(Value = "ENV_SteamLoop_04")]
            ENV_SteamLoop_04,
            [EnumMember(Value = "SFX_IMPACT_GooBlastHit1")]
            SFX_IMPACT_GooBlastHit1 = 10001,
            [EnumMember(Value = "SFX_IMPACT_GooBlastHit2")]
            SFX_IMPACT_GooBlastHit2,
            [EnumMember(Value = "SFX_IMPACT_GooBlastHit3")]
            SFX_IMPACT_GooBlastHit3,
            [EnumMember(Value = "SFX_IMPACT_GooBlastHit4")]
            SFX_IMPACT_GooBlastHit4,
            [EnumMember(Value = "SFX_IMPACT_GooBlastHit5")]
            SFX_IMPACT_GooBlastHit5,
            [EnumMember(Value = "SFX_IMPACT_GooBlastHit6")]
            SFX_IMPACT_GooBlastHit6,
            [EnumMember(Value = "SFX_IMPACT_FireLight")]
            SFX_IMPACT_FireLight,
            [EnumMember(Value = "SFX_IMPACT_FireMedium")]
            SFX_IMPACT_FireMedium,
            [EnumMember(Value = "SFX_IMPACT_FireHeavy")]
            SFX_IMPACT_FireHeavy,
            [EnumMember(Value = "SFX_IMPACT_LightWeaponOnFlesh1")]
            SFX_IMPACT_LightWeaponOnFlesh1,
            [EnumMember(Value = "SFX_IMPACT_LightWeaponOnFlesh2")]
            SFX_IMPACT_LightWeaponOnFlesh2,
            [EnumMember(Value = "SFX_IMPACT_LightWeaponOnFlesh3")]
            SFX_IMPACT_LightWeaponOnFlesh3,
            [EnumMember(Value = "SFX_IMPACT_LightWeaponOnFlesh4")]
            SFX_IMPACT_LightWeaponOnFlesh4,
            [EnumMember(Value = "SFX_IMPACT_LightWeaponOnFlesh5")]
            SFX_IMPACT_LightWeaponOnFlesh5,
            [EnumMember(Value = "SFX_IMPACT_LightWeaponOnFlesh6")]
            SFX_IMPACT_LightWeaponOnFlesh6,
            [EnumMember(Value = "SFX_IMPACT_SwordOnLeather1")]
            SFX_IMPACT_SwordOnLeather1,
            [EnumMember(Value = "SFX_IMPACT_SwordOnLeather2")]
            SFX_IMPACT_SwordOnLeather2,
            [EnumMember(Value = "SFX_IMPACT_SwordOnLeather3")]
            SFX_IMPACT_SwordOnLeather3,
            [EnumMember(Value = "SFX_IMPACT_SwordOnLeather4")]
            SFX_IMPACT_SwordOnLeather4,
            [EnumMember(Value = "SFX_IMPACT_SwordOnLeather5")]
            SFX_IMPACT_SwordOnLeather5,
            [EnumMember(Value = "SFX_IMPACT_SwordOnLeather6")]
            SFX_IMPACT_SwordOnLeather6,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalArmor1")]
            SFX_IMPACT_SwordOnMetalArmor1,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalArmor2")]
            SFX_IMPACT_SwordOnMetalArmor2,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalArmor3")]
            SFX_IMPACT_SwordOnMetalArmor3,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalArmor4")]
            SFX_IMPACT_SwordOnMetalArmor4,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalArmor5")]
            SFX_IMPACT_SwordOnMetalArmor5,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalArmor6")]
            SFX_IMPACT_SwordOnMetalArmor6,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalShield1")]
            SFX_IMPACT_SwordOnMetalShield1,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalShield2")]
            SFX_IMPACT_SwordOnMetalShield2,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalShield3")]
            SFX_IMPACT_SwordOnMetalShield3,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalShield4")]
            SFX_IMPACT_SwordOnMetalShield4,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalShield5")]
            SFX_IMPACT_SwordOnMetalShield5,
            [EnumMember(Value = "SFX_IMPACT_SwordOnMetalShield6")]
            SFX_IMPACT_SwordOnMetalShield6,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSword1")]
            SFX_IMPACT_SwordOnSword1,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSword2")]
            SFX_IMPACT_SwordOnSword2,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSword3")]
            SFX_IMPACT_SwordOnSword3,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSword4")]
            SFX_IMPACT_SwordOnSword4,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSword5")]
            SFX_IMPACT_SwordOnSword5,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSword6")]
            SFX_IMPACT_SwordOnSword6,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSwordHigh1")]
            SFX_IMPACT_SwordOnSwordHigh1 = 10041,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSwordHigh2")]
            SFX_IMPACT_SwordOnSwordHigh2,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSwordHigh3")]
            SFX_IMPACT_SwordOnSwordHigh3,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSwordHigh4")]
            SFX_IMPACT_SwordOnSwordHigh4,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSwordHigh5")]
            SFX_IMPACT_SwordOnSwordHigh5,
            [EnumMember(Value = "SFX_IMPACT_SwordOnSwordHigh6")]
            SFX_IMPACT_SwordOnSwordHigh6,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnCloth1")]
            SFX_IMPACT_WeaponOnCloth1,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnCloth2")]
            SFX_IMPACT_WeaponOnCloth2,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnCloth3")]
            SFX_IMPACT_WeaponOnCloth3,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnCloth4")]
            SFX_IMPACT_WeaponOnCloth4,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnCloth5")]
            SFX_IMPACT_WeaponOnCloth5,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnCloth6")]
            SFX_IMPACT_WeaponOnCloth6,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnFlesh1")]
            SFX_IMPACT_WeaponOnFlesh1,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnFlesh2")]
            SFX_IMPACT_WeaponOnFlesh2,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnFlesh3")]
            SFX_IMPACT_WeaponOnFlesh3,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnFlesh4")]
            SFX_IMPACT_WeaponOnFlesh4,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnFlesh5")]
            SFX_IMPACT_WeaponOnFlesh5,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnFlesh6")]
            SFX_IMPACT_WeaponOnFlesh6,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnLeather1")]
            SFX_IMPACT_WeaponOnLeather1,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnLeather2")]
            SFX_IMPACT_WeaponOnLeather2,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnLeather3")]
            SFX_IMPACT_WeaponOnLeather3,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnLeather4")]
            SFX_IMPACT_WeaponOnLeather4,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnLeather5")]
            SFX_IMPACT_WeaponOnLeather5,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnLeather6")]
            SFX_IMPACT_WeaponOnLeather6,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalArmor1")]
            SFX_IMPACT_WeaponOnMetalArmor1,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalArmor2")]
            SFX_IMPACT_WeaponOnMetalArmor2,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalArmor3")]
            SFX_IMPACT_WeaponOnMetalArmor3,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalArmor4")]
            SFX_IMPACT_WeaponOnMetalArmor4,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalArmor5")]
            SFX_IMPACT_WeaponOnMetalArmor5,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalArmor6")]
            SFX_IMPACT_WeaponOnMetalArmor6,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShield1")]
            SFX_IMPACT_WeaponOnMetalShield1,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShield2")]
            SFX_IMPACT_WeaponOnMetalShield2,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShield3")]
            SFX_IMPACT_WeaponOnMetalShield3,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShield4")]
            SFX_IMPACT_WeaponOnMetalShield4,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShield5")]
            SFX_IMPACT_WeaponOnMetalShield5,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShield6")]
            SFX_IMPACT_WeaponOnMetalShield6,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShieldLow1")]
            SFX_IMPACT_WeaponOnMetalShieldLow1,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShieldLow2")]
            SFX_IMPACT_WeaponOnMetalShieldLow2,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShieldLow3")]
            SFX_IMPACT_WeaponOnMetalShieldLow3,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShieldLow4")]
            SFX_IMPACT_WeaponOnMetalShieldLow4,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShieldLow5")]
            SFX_IMPACT_WeaponOnMetalShieldLow5,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnMetalShieldLow6")]
            SFX_IMPACT_WeaponOnMetalShieldLow6,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnStone1")]
            SFX_IMPACT_WeaponOnStone1,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnStone2")]
            SFX_IMPACT_WeaponOnStone2,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnStone3")]
            SFX_IMPACT_WeaponOnStone3,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnStone4")]
            SFX_IMPACT_WeaponOnStone4,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnStone5")]
            SFX_IMPACT_WeaponOnStone5,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnStone6")]
            SFX_IMPACT_WeaponOnStone6,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnWood1")]
            SFX_IMPACT_WeaponOnWood1,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnWood2")]
            SFX_IMPACT_WeaponOnWood2,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnWood3")]
            SFX_IMPACT_WeaponOnWood3,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnWood4")]
            SFX_IMPACT_WeaponOnWood4,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnWood5")]
            SFX_IMPACT_WeaponOnWood5,
            [EnumMember(Value = "SFX_IMPACT_WeaponOnWood6")]
            SFX_IMPACT_WeaponOnWood6,
            [EnumMember(Value = "SFX_STAB_SwordOnLeather1")]
            SFX_STAB_SwordOnLeather1,
            [EnumMember(Value = "SFX_STAB_SwordOnLeather2")]
            SFX_STAB_SwordOnLeather2,
            [EnumMember(Value = "SFX_STAB_SwordOnLeather3")]
            SFX_STAB_SwordOnLeather3,
            [EnumMember(Value = "SFX_STAB_SwordOnLeather4")]
            SFX_STAB_SwordOnLeather4,
            [EnumMember(Value = "SFX_STAB_SwordOnLeather5")]
            SFX_STAB_SwordOnLeather5,
            [EnumMember(Value = "SFX_STAB_SwordOnLeather6")]
            SFX_STAB_SwordOnLeather6,
            [EnumMember(Value = "SFX_STAB_SwordOnMetal1")]
            SFX_STAB_SwordOnMetal1,
            [EnumMember(Value = "SFX_STAB_SwordOnMetal2")]
            SFX_STAB_SwordOnMetal2,
            [EnumMember(Value = "SFX_STAB_SwordOnMetal3")]
            SFX_STAB_SwordOnMetal3,
            [EnumMember(Value = "SFX_STAB_SwordOnMetal4")]
            SFX_STAB_SwordOnMetal4,
            [EnumMember(Value = "SFX_STAB_SwordOnMetal5")]
            SFX_STAB_SwordOnMetal5,
            [EnumMember(Value = "SFX_STAB_SwordOnMetal6")]
            SFX_STAB_SwordOnMetal6,
            [EnumMember(Value = "SFX_FireCrackle1")]
            SFX_FireCrackle1,
            [EnumMember(Value = "SFX_FireCrackle2")]
            SFX_FireCrackle2,
            [EnumMember(Value = "SFX_FireChannel")]
            SFX_FireChannel = 10110,
            [EnumMember(Value = "SFX_FireThrowHeavy")]
            SFX_FireThrowHeavy,
            [EnumMember(Value = "SFX_FireThrowLight")]
            SFX_FireThrowLight,
            [EnumMember(Value = "SFX_WeaponUnsheath_1H")]
            SFX_WeaponUnsheath_1H,
            [EnumMember(Value = "SFX_WeaponUnsheath_2H")]
            SFX_WeaponUnsheath_2H,
            [EnumMember(Value = "SFX_WeaponSwing1")]
            SFX_WeaponSwing1,
            [EnumMember(Value = "SFX_WeaponSheath_1H")]
            SFX_WeaponSheath_1H,
            [EnumMember(Value = "SFX_WeaponSheath_2H")]
            SFX_WeaponSheath_2H,
            [EnumMember(Value = "SFX_FALL_ClothArmor")]
            SFX_FALL_ClothArmor = 10120,
            [EnumMember(Value = "SFX_FALL_MetalArmor")]
            SFX_FALL_MetalArmor = 10130,
            [EnumMember(Value = "SFX_FALL_Sword1")]
            SFX_FALL_Sword1 = 10135,
            [EnumMember(Value = "SFX_FALL_Sword2")]
            SFX_FALL_Sword2,
            [EnumMember(Value = "SFX_FALL_Weapon1")]
            SFX_FALL_Weapon1,
            [EnumMember(Value = "SFX_FALL_Weapon2")]
            SFX_FALL_Weapon2,
            [EnumMember(Value = "SFX_FALL_StoneArmor")]
            SFX_FALL_StoneArmor = 10140,
            [EnumMember(Value = "SFX_FALL_LeatherArmor")]
            SFX_FALL_LeatherArmor = 10150,
            [EnumMember(Value = "SFX_DODGE_ClothArmor")]
            SFX_DODGE_ClothArmor = 10160,
            [EnumMember(Value = "SFX_DODGE_MetalArmor")]
            SFX_DODGE_MetalArmor = 10170,
            [EnumMember(Value = "SFX_DODGE_StoneArmor")]
            SFX_DODGE_StoneArmor = 10180,
            [EnumMember(Value = "SFX_DODGE_LeatherArmor")]
            SFX_DODGE_LeatherArmor = 10190,
            [EnumMember(Value = "SFX_LEVER_Wood")]
            SFX_LEVER_Wood = 11010,
            [EnumMember(Value = "SFX_LEVER_Metal1")]
            SFX_LEVER_Metal1 = 11020,
            [EnumMember(Value = "SFX_LEVER_Metal2")]
            SFX_LEVER_Metal2,
            [EnumMember(Value = "SFX_LEVER_Metal3")]
            SFX_LEVER_Metal3,
            [EnumMember(Value = "SFX_LEVER_Metal4")]
            SFX_LEVER_Metal4,
            [EnumMember(Value = "SFX_LEVER_Metal5")]
            SFX_LEVER_Metal5,
            [EnumMember(Value = "SFX_DOOR_Wood")]
            SFX_DOOR_Wood = 11110,
            [EnumMember(Value = "SFX_DOOR_Metal")]
            SFX_DOOR_Metal = 11120,
            [EnumMember(Value = "SFX_DOOR_Stone")]
            SFX_DOOR_Stone = 11130,
            [EnumMember(Value = "SFX_BLOCK_Sword_1H")]
            SFX_BLOCK_Sword_1H = 11140,
            [EnumMember(Value = "SFX_BLOCK_Sword_2H")]
            SFX_BLOCK_Sword_2H = 11145,
            [EnumMember(Value = "SFX_BLOCK_Weapon_1H")]
            SFX_BLOCK_Weapon_1H = 11150,
            [EnumMember(Value = "SFX_BLOCK_Weapon_2H")]
            SFX_BLOCK_Weapon_2H = 11155,
            [EnumMember(Value = "SFX_StretchBow1")]
            SFX_StretchBow1 = 11160,
            [EnumMember(Value = "SFX_StretchBow2")]
            SFX_StretchBow2,
            [EnumMember(Value = "SFX_StretchBow3")]
            SFX_StretchBow3,
            [EnumMember(Value = "SFX_StretchBow4")]
            SFX_StretchBow4,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnFlesh1")]
            SFX_IMPACT_ArrowOnFlesh1 = 11170,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnFlesh2")]
            SFX_IMPACT_ArrowOnFlesh2,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnFlesh3")]
            SFX_IMPACT_ArrowOnFlesh3,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnFlesh4")]
            SFX_IMPACT_ArrowOnFlesh4,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnGround1")]
            SFX_IMPACT_ArrowOnGround1 = 11180,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnGround2")]
            SFX_IMPACT_ArrowOnGround2,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnGround3")]
            SFX_IMPACT_ArrowOnGround3,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnGround4")]
            SFX_IMPACT_ArrowOnGround4,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnMetal1")]
            SFX_IMPACT_ArrowOnMetal1 = 11190,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnMetal2")]
            SFX_IMPACT_ArrowOnMetal2,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnMetal3")]
            SFX_IMPACT_ArrowOnMetal3,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnMetal4")]
            SFX_IMPACT_ArrowOnMetal4,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnWood1")]
            SFX_IMPACT_ArrowOnWood1 = 11200,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnWood2")]
            SFX_IMPACT_ArrowOnWood2,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnWood3")]
            SFX_IMPACT_ArrowOnWood3,
            [EnumMember(Value = "SFX_IMPACT_ArrowOnWood4")]
            SFX_IMPACT_ArrowOnWood4,
            [EnumMember(Value = "SFX_IMPACT_KickOnCloth1")]
            SFX_IMPACT_KickOnCloth1 = 11210,
            [EnumMember(Value = "SFX_IMPACT_KickOnCloth2")]
            SFX_IMPACT_KickOnCloth2,
            [EnumMember(Value = "SFX_IMPACT_KickOnCloth3")]
            SFX_IMPACT_KickOnCloth3,
            [EnumMember(Value = "SFX_IMPACT_KickOnCloth4")]
            SFX_IMPACT_KickOnCloth4,
            [EnumMember(Value = "SFX_IMPACT_KickOnFlesh1")]
            SFX_IMPACT_KickOnFlesh1 = 11220,
            [EnumMember(Value = "SFX_IMPACT_KickOnFlesh2")]
            SFX_IMPACT_KickOnFlesh2,
            [EnumMember(Value = "SFX_IMPACT_KickOnFlesh3")]
            SFX_IMPACT_KickOnFlesh3,
            [EnumMember(Value = "SFX_IMPACT_KickOnFlesh4")]
            SFX_IMPACT_KickOnFlesh4,
            [EnumMember(Value = "SFX_IMPACT_KickOnLeather1")]
            SFX_IMPACT_KickOnLeather1 = 11230,
            [EnumMember(Value = "SFX_IMPACT_KickOnLeather2")]
            SFX_IMPACT_KickOnLeather2,
            [EnumMember(Value = "SFX_IMPACT_KickOnLeather3")]
            SFX_IMPACT_KickOnLeather3,
            [EnumMember(Value = "SFX_IMPACT_KickOnMetalArmor1")]
            SFX_IMPACT_KickOnMetalArmor1 = 11240,
            [EnumMember(Value = "SFX_IMPACT_KickOnMetalArmor2")]
            SFX_IMPACT_KickOnMetalArmor2,
            [EnumMember(Value = "SFX_IMPACT_KickOnMetalArmor3")]
            SFX_IMPACT_KickOnMetalArmor3,
            [EnumMember(Value = "SFX_IMPACT_KickOnMetalArmor4")]
            SFX_IMPACT_KickOnMetalArmor4,
            [EnumMember(Value = "SFX_IMPACT_KickOnMetalShield1")]
            SFX_IMPACT_KickOnMetalShield1 = 11250,
            [EnumMember(Value = "SFX_IMPACT_KickOnMetalShield2")]
            SFX_IMPACT_KickOnMetalShield2,
            [EnumMember(Value = "SFX_IMPACT_KickOnMetalShield3")]
            SFX_IMPACT_KickOnMetalShield3,
            [EnumMember(Value = "SFX_IMPACT_KickOnMetalShield4")]
            SFX_IMPACT_KickOnMetalShield4,
            [EnumMember(Value = "SFX_IMPACT_KickOnStone1")]
            SFX_IMPACT_KickOnStone1 = 11260,
            [EnumMember(Value = "SFX_IMPACT_KickOnStone2")]
            SFX_IMPACT_KickOnStone2,
            [EnumMember(Value = "SFX_IMPACT_KickOnStone3")]
            SFX_IMPACT_KickOnStone3,
            [EnumMember(Value = "SFX_IMPACT_KickOnStone4")]
            SFX_IMPACT_KickOnStone4,
            [EnumMember(Value = "SFX_IMPACT_KickOnWood1")]
            SFX_IMPACT_KickOnWood1 = 11270,
            [EnumMember(Value = "SFX_IMPACT_KickOnWood2")]
            SFX_IMPACT_KickOnWood2,
            [EnumMember(Value = "SFX_IMPACT_KickOnWood3")]
            SFX_IMPACT_KickOnWood3,
            [EnumMember(Value = "SFX_IMPACT_KickOnWood4")]
            SFX_IMPACT_KickOnWood4,
            [EnumMember(Value = "SFX_IMPACT_SwordOnWood1")]
            SFX_IMPACT_SwordOnWood1 = 11320,
            [EnumMember(Value = "SFX_IMPACT_SwordOnWood2")]
            SFX_IMPACT_SwordOnWood2,
            [EnumMember(Value = "SFX_IMPACT_SwordOnWood3")]
            SFX_IMPACT_SwordOnWood3,
            [EnumMember(Value = "SFX_IMPACT_SwordOnWood4")]
            SFX_IMPACT_SwordOnWood4,
            [EnumMember(Value = "SFX_IMPACT_SwordOnHollowWood1")]
            SFX_IMPACT_SwordOnHollowWood1 = 11330,
            [EnumMember(Value = "SFX_IMPACT_SwordOnHollowWood2")]
            SFX_IMPACT_SwordOnHollowWood2,
            [EnumMember(Value = "SFX_IMPACT_SwordOnHollowWood3")]
            SFX_IMPACT_SwordOnHollowWood3,
            [EnumMember(Value = "SFX_IMPACT_SwordOnHollowWood4")]
            SFX_IMPACT_SwordOnHollowWood4,
            [EnumMember(Value = "SFX_IMPACT_SwordOnStone1")]
            SFX_IMPACT_SwordOnStone1 = 11340,
            [EnumMember(Value = "SFX_IMPACT_SwordOnStone2")]
            SFX_IMPACT_SwordOnStone2,
            [EnumMember(Value = "SFX_IMPACT_SwordOnStone3")]
            SFX_IMPACT_SwordOnStone3,
            [EnumMember(Value = "SFX_IMPACT_SwordOnStone4")]
            SFX_IMPACT_SwordOnStone4,
            [EnumMember(Value = "SFX_EQUIP_Weapon_1H")]
            SFX_EQUIP_Weapon_1H = 12000,
            [EnumMember(Value = "SFX_EQUIP_Weapon_2H")]
            SFX_EQUIP_Weapon_2H,
            [EnumMember(Value = "SFX_UNEQUIP_Weapon_1H")]
            SFX_UNEQUIP_Weapon_1H = 12050,
            [EnumMember(Value = "SFX_UNEQUIP_Weapon_2H")]
            SFX_UNEQUIP_Weapon_2H,
            [EnumMember(Value = "SFX_USE_DrinkPotion")]
            SFX_USE_DrinkPotion = 12100,
            [EnumMember(Value = "SFX_PickupItem")]
            SFX_PickupItem = 12200,
            [EnumMember(Value = "SFX_StartFire")]
            SFX_StartFire = 12250,
            [EnumMember(Value = "SFX_CampFireLoop")]
            SFX_CampFireLoop,
            [EnumMember(Value = "SFX_SKILL_FireStoneIn")]
            SFX_SKILL_FireStoneIn = 12300,
            [EnumMember(Value = "SFX_SKILL_FireStoneOut")]
            SFX_SKILL_FireStoneOut,
            [EnumMember(Value = "SFX_SKILL_FireStoneLoop")]
            SFX_SKILL_FireStoneLoop,
            [EnumMember(Value = "SFX_SKILL_Spark")]
            SFX_SKILL_Spark,
            [EnumMember(Value = "SFX_SKILL_Teleport")]
            SFX_SKILL_Teleport,
            [EnumMember(Value = "SFX_SHOOT_Arrow1")]
            SFX_SHOOT_Arrow1 = 12500,
            [EnumMember(Value = "SFX_SHOOT_Arrow2")]
            SFX_SHOOT_Arrow2,
            [EnumMember(Value = "SFX_SHOOT_Arrow3")]
            SFX_SHOOT_Arrow3,
            [EnumMember(Value = "SFX_SHOOT_Arrow4")]
            SFX_SHOOT_Arrow4,
            [EnumMember(Value = "SFX_SWIPE_Kick1")]
            SFX_SWIPE_Kick1 = 12540,
            [EnumMember(Value = "SFX_SWIPE_Kick2")]
            SFX_SWIPE_Kick2,
            [EnumMember(Value = "SFX_SWIPE_Kick3")]
            SFX_SWIPE_Kick3,
            [EnumMember(Value = "SFX_SWIPE_Kick4")]
            SFX_SWIPE_Kick4,
            [EnumMember(Value = "SFX_SWING_SwordAttack_1H1")]
            SFX_SWING_SwordAttack_1H1 = 12550,
            [EnumMember(Value = "SFX_SWING_SwordAttack_1H2")]
            SFX_SWING_SwordAttack_1H2,
            [EnumMember(Value = "SFX_SWING_SwordAttack_1H3")]
            SFX_SWING_SwordAttack_1H3,
            [EnumMember(Value = "SFX_SWING_SwordAttack_1H4")]
            SFX_SWING_SwordAttack_1H4,
            [EnumMember(Value = "SFX_SWING_SwordComboAttack_1H")]
            SFX_SWING_SwordComboAttack_1H = 12560,
            [EnumMember(Value = "SFX_SWING_SwordFencingAttack_1H1")]
            SFX_SWING_SwordFencingAttack_1H1 = 12570,
            [EnumMember(Value = "SFX_SWING_SwordFencingAttack_1H2")]
            SFX_SWING_SwordFencingAttack_1H2,
            [EnumMember(Value = "SFX_SWING_SwordHeavyAttack_1H1")]
            SFX_SWING_SwordHeavyAttack_1H1 = 12575,
            [EnumMember(Value = "SFX_SWING_SwordHeavyAttack_1H2")]
            SFX_SWING_SwordHeavyAttack_1H2,
            [EnumMember(Value = "SFX_SWING_WeaponFencingAttack_1H1")]
            SFX_SWING_WeaponFencingAttack_1H1 = 12670,
            [EnumMember(Value = "SFX_SWING_WeaponFencingAttack_1H2")]
            SFX_SWING_WeaponFencingAttack_1H2,
            [EnumMember(Value = "SFX_SWING_SwordAttack_2H1")]
            SFX_SWING_SwordAttack_2H1 = 12750,
            [EnumMember(Value = "SFX_SWING_SwordAttack_2H2")]
            SFX_SWING_SwordAttack_2H2,
            [EnumMember(Value = "SFX_SWING_SwordAttack_2H3")]
            SFX_SWING_SwordAttack_2H3,
            [EnumMember(Value = "SFX_SWING_SwordAttack_2H4")]
            SFX_SWING_SwordAttack_2H4,
            [EnumMember(Value = "SFX_SWING_SwordComboAttack_2H")]
            SFX_SWING_SwordComboAttack_2H = 12760,
            [EnumMember(Value = "SFX_SWING_SwordFencingAttack_2H1")]
            SFX_SWING_SwordFencingAttack_2H1 = 12770,
            [EnumMember(Value = "SFX_SWING_SwordFencingAttack_2H2")]
            SFX_SWING_SwordFencingAttack_2H2,
            [EnumMember(Value = "SFX_SWING_SwordHeavyAttack_2H1")]
            SFX_SWING_SwordHeavyAttack_2H1 = 12775,
            [EnumMember(Value = "SFX_SWING_SwordHeavyAttack_2H2")]
            SFX_SWING_SwordHeavyAttack_2H2,
            [EnumMember(Value = "SFX_SWING_WeaponAttack_2H1")]
            SFX_SWING_WeaponAttack_2H1 = 12850,
            [EnumMember(Value = "SFX_SWING_WeaponAttack_2H2")]
            SFX_SWING_WeaponAttack_2H2,
            [EnumMember(Value = "SFX_SWING_WeaponAttack_2H3")]
            SFX_SWING_WeaponAttack_2H3,
            [EnumMember(Value = "SFX_SWING_WeaponFencingAttack_2H1")]
            SFX_SWING_WeaponFencingAttack_2H1 = 12870,
            [EnumMember(Value = "SFX_SWING_WeaponFencingAttack_2H2")]
            SFX_SWING_WeaponFencingAttack_2H2,
            [EnumMember(Value = "SFX_SKILL_AirSigil_Preparation")]
            SFX_SKILL_AirSigil_Preparation = 12880,
            [EnumMember(Value = "SFX_SKILL_AirSigil_Release")]
            SFX_SKILL_AirSigil_Release,
            [EnumMember(Value = "SFX_SKILL_AnkleBlow")]
            SFX_SKILL_AnkleBlow = 12891,
            [EnumMember(Value = "SFX_SKILL_BloodBullet_Impact")]
            SFX_SKILL_BloodBullet_Impact = 12901,
            [EnumMember(Value = "SFX_SKILL_BloodBullet_Preparation")]
            SFX_SKILL_BloodBullet_Preparation,
            [EnumMember(Value = "SFX_SKILL_BoonSpell")]
            SFX_SKILL_BoonSpell,
            [EnumMember(Value = "SFX_SKILL_Brace")]
            SFX_SKILL_Brace = 12910,
            [EnumMember(Value = "SFX_SKILL_CallToElement")]
            SFX_SKILL_CallToElement = 12920,
            [EnumMember(Value = "SFX_SKILL_Counter")]
            SFX_SKILL_Counter = 12930,
            [EnumMember(Value = "SFX_SKILL_DeflectionStrike_HeavyPiercing")]
            SFX_SKILL_DeflectionStrike_HeavyPiercing = 12940,
            [EnumMember(Value = "SFX_SKILL_DeflectionStrike_Whoosh")]
            SFX_SKILL_DeflectionStrike_Whoosh = 12950,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileDecay_Impact01")]
            SFX_SKILL_ElemantalProjectileDecay_Impact01 = 12960,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileDecay_Impact02")]
            SFX_SKILL_ElemantalProjectileDecay_Impact02,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileDecay_Shot")]
            SFX_SKILL_ElemantalProjectileDecay_Shot = 12963,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileElectric_Impact01")]
            SFX_SKILL_ElemantalProjectileElectric_Impact01 = 12970,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileElectric_Impact02")]
            SFX_SKILL_ElemantalProjectileElectric_Impact02,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileElectric_Impact03")]
            SFX_SKILL_ElemantalProjectileElectric_Impact03,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileElectric_Shot")]
            SFX_SKILL_ElemantalProjectileElectric_Shot,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileEthereal_Impact01")]
            SFX_SKILL_ElemantalProjectileEthereal_Impact01,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileEthereal_Impact02")]
            SFX_SKILL_ElemantalProjectileEthereal_Impact02,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileEthereal_Shot")]
            SFX_SKILL_ElemantalProjectileEthereal_Shot,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileFire_Impact01")]
            SFX_SKILL_ElemantalProjectileFire_Impact01 = 12980,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileFire_Impact02")]
            SFX_SKILL_ElemantalProjectileFire_Impact02,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileFire_Shot")]
            SFX_SKILL_ElemantalProjectileFire_Shot,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileIce_Impact01")]
            SFX_SKILL_ElemantalProjectileIce_Impact01 = 12990,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileIce_Impact02")]
            SFX_SKILL_ElemantalProjectileIce_Impact02,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileIce_Impact03")]
            SFX_SKILL_ElemantalProjectileIce_Impact03,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileIce_Shot")]
            SFX_SKILL_ElemantalProjectileIce_Shot,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileWind_Impact01")]
            SFX_SKILL_ElemantalProjectileWind_Impact01 = 13000,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileWind_Impact02")]
            SFX_SKILL_ElemantalProjectileWind_Impact02,
            [EnumMember(Value = "SFX_SKILL_ElemantalProjectileWind_Shot")]
            SFX_SKILL_ElemantalProjectileWind_Shot,
            [EnumMember(Value = "SFX_SKILL_EvasionShoot")]
            SFX_SKILL_EvasionShoot = 13010,
            [EnumMember(Value = "SFX_SKILL_ExploitStab")]
            SFX_SKILL_ExploitStab = 13020,
            [EnumMember(Value = "SFX_SKILL_FinishingBlow")]
            SFX_SKILL_FinishingBlow = 13030,
            [EnumMember(Value = "SFX_SKILL_FirePistol")]
            SFX_SKILL_FirePistol = 13040,
            [EnumMember(Value = "SFX_SKILL_FireSigile_Preparation")]
            SFX_SKILL_FireSigile_Preparation,
            [EnumMember(Value = "SFX_SKILL_FireSigile_Release")]
            SFX_SKILL_FireSigile_Release,
            [EnumMember(Value = "SFX_SKILL_FlameThrower_In")]
            SFX_SKILL_FlameThrower_In = 13050,
            [EnumMember(Value = "SFX_SKILL_FlameThrower_Loop")]
            SFX_SKILL_FlameThrower_Loop,
            [EnumMember(Value = "SFX_SKILL_FlameThrower_Out")]
            SFX_SKILL_FlameThrower_Out,
            [EnumMember(Value = "SFX_SKILL_Focus")]
            SFX_SKILL_Focus = 13060,
            [EnumMember(Value = "SFX_SKILL_ForceBlow_In")]
            SFX_SKILL_ForceBlow_In = 13070,
            [EnumMember(Value = "SFX_SKILL_ForceBlow_Loop")]
            SFX_SKILL_ForceBlow_Loop,
            [EnumMember(Value = "SFX_SKILL_ForceBlow_Out")]
            SFX_SKILL_ForceBlow_Out,
            [EnumMember(Value = "SFX_SKILL_ForceBubble_In")]
            SFX_SKILL_ForceBubble_In = 13080,
            [EnumMember(Value = "SFX_SKILL_ForceBubble_Loop")]
            SFX_SKILL_ForceBubble_Loop,
            [EnumMember(Value = "SFX_SKILL_ForceBubble_Out")]
            SFX_SKILL_ForceBubble_Out,
            [EnumMember(Value = "SFX_SKILL_ForceRaise")]
            SFX_SKILL_ForceRaise = 13090,
            [EnumMember(Value = "SFX_SKILL_FrostBullet_Impact")]
            SFX_SKILL_FrostBullet_Impact = 13100,
            [EnumMember(Value = "SFX_SKILL_FrostBullet_Preparation")]
            SFX_SKILL_FrostBullet_Preparation,
            [EnumMember(Value = "SFX_SKILL_FrostSigil_Preparation")]
            SFX_SKILL_FrostSigil_Preparation = 13120,
            [EnumMember(Value = "SFX_SKILL_FrostSigil_Release")]
            SFX_SKILL_FrostSigil_Release,
            [EnumMember(Value = "SFX_SKILL_GongStrike_Preparation")]
            SFX_SKILL_GongStrike_Preparation = 13130,
            [EnumMember(Value = "SFX_SKILL_GongStrike_Release")]
            SFX_SKILL_GongStrike_Release,
            [EnumMember(Value = "SFX_SKILL_Juggernaut_PhysicalCharge")]
            SFX_SKILL_Juggernaut_PhysicalCharge = 13140,
            [EnumMember(Value = "SFX_SKILL_Juggernaut_ReleaseWhoosh")]
            SFX_SKILL_Juggernaut_ReleaseWhoosh,
            [EnumMember(Value = "SFX_SKILL_LeapAttack_Impact")]
            SFX_SKILL_LeapAttack_Impact = 13150,
            [EnumMember(Value = "SFX_SKILL_LeapAttack_Whoosh")]
            SFX_SKILL_LeapAttack_Whoosh,
            [EnumMember(Value = "SFX_SKILL_MaceFill")]
            SFX_SKILL_MaceFill = 13160,
            [EnumMember(Value = "SFX_SKILL_Mist")]
            SFX_SKILL_Mist = 13170,
            [EnumMember(Value = "SFX_SKILL_MoonSwipe_PhysicalCharge")]
            SFX_SKILL_MoonSwipe_PhysicalCharge = 13180,
            [EnumMember(Value = "SFX_SKILL_MoonSwipe_PierceShot")]
            SFX_SKILL_MoonSwipe_PierceShot,
            [EnumMember(Value = "SFX_SKILL_MoonSwipe_Whoosh")]
            SFX_SKILL_MoonSwipe_Whoosh,
            [EnumMember(Value = "SFX_SKILL_MultitargetStrike")]
            SFX_SKILL_MultitargetStrike = 13190,
            [EnumMember(Value = "SFX_SKILL_PommelCounter")]
            SFX_SKILL_PommelCounter = 13200,
            [EnumMember(Value = "SFX_SKILL_Possessed")]
            SFX_SKILL_Possessed = 13210,
            [EnumMember(Value = "SFX_SKILL_PreciseStrike_PhysicalCharge")]
            SFX_SKILL_PreciseStrike_PhysicalCharge = 13220,
            [EnumMember(Value = "SFX_SKILL_PreciseStrike_WhooshImpact")]
            SFX_SKILL_PreciseStrike_WhooshImpact,
            [EnumMember(Value = "SFX_SKILL_PushKick")]
            SFX_SKILL_PushKick = 13230,
            [EnumMember(Value = "SFX_SKILL_Rage")]
            SFX_SKILL_Rage = 13240,
            [EnumMember(Value = "SFX_SKILL_ReloadGun")]
            SFX_SKILL_ReloadGun = 13250,
            [EnumMember(Value = "SFX_SKILL_RevealSoul")]
            SFX_SKILL_RevealSoul = 13260,
            [EnumMember(Value = "SFX_SKILL_RuneSpell")]
            SFX_SKILL_RuneSpell = 13270,
            [EnumMember(Value = "SFX_SKILL_SavageStrike")]
            SFX_SKILL_SavageStrike = 13280,
            [EnumMember(Value = "SFX_SKILL_SerpentParry")]
            SFX_SKILL_SerpentParry = 13290,
            [EnumMember(Value = "SFX_SKILL_Shakram")]
            SFX_SKILL_Shakram = 13300,
            [EnumMember(Value = "SFX_SKILL_ShieldAbsorb_Impact")]
            SFX_SKILL_ShieldAbsorb_Impact = 13310,
            [EnumMember(Value = "SFX_SKILL_ShieldAbsorb_Spell")]
            SFX_SKILL_ShieldAbsorb_Spell,
            [EnumMember(Value = "SFX_SKILL_ShieldCharge_PhysicalCharge")]
            SFX_SKILL_ShieldCharge_PhysicalCharge,
            [EnumMember(Value = "SFX_SKILL_ShieldCharge_Whoosh")]
            SFX_SKILL_ShieldCharge_Whoosh,
            [EnumMember(Value = "SFX_SKILL_SniperShot")]
            SFX_SKILL_SniperShot = 13320,
            [EnumMember(Value = "SFX_SKILL_SpellBind")]
            SFX_SKILL_SpellBind = 13330,
            [EnumMember(Value = "SFX_SKILL_Swarm")]
            SFX_SKILL_Swarm = 13340,
            [EnumMember(Value = "SFX_SKILL_SweepKick01")]
            SFX_SKILL_SweepKick01 = 13350,
            [EnumMember(Value = "SFX_SKILL_SweepKick02")]
            SFX_SKILL_SweepKick02,
            [EnumMember(Value = "SFX_SKILL_ThrowLantern")]
            SFX_SKILL_ThrowLantern = 13360,
            [EnumMember(Value = "SFX_SKILL_ViolentStab")]
            SFX_SKILL_ViolentStab = 13370,
            [EnumMember(Value = "SFX_SKILL_Warm")]
            SFX_SKILL_Warm = 13380,
            [EnumMember(Value = "SFX_INTERACTION_OpenContainerGeneric")]
            SFX_INTERACTION_OpenContainerGeneric = 15000,
            [EnumMember(Value = "SFX_INTERACTION_OpenContainerChest")]
            SFX_INTERACTION_OpenContainerChest,
            [EnumMember(Value = "SFX_INTERACTION_GatherPlant")]
            SFX_INTERACTION_GatherPlant = 15020,
            [EnumMember(Value = "SFX_INTERACTION_GatherWood")]
            SFX_INTERACTION_GatherWood,
            [EnumMember(Value = "SFX_INTERACTION_GatherMining")]
            SFX_INTERACTION_GatherMining,
            [EnumMember(Value = "SFX_INTERACTION_GatherFish")]
            SFX_INTERACTION_GatherFish,
            [EnumMember(Value = "SFX_Deploy_AlchemyKit_Close")]
            SFX_Deploy_AlchemyKit_Close = 15030,
            [EnumMember(Value = "SFX_Deploy_AlchemyKit_Open")]
            SFX_Deploy_AlchemyKit_Open,
            [EnumMember(Value = "SFX_Deploy_BedRoll_Close")]
            SFX_Deploy_BedRoll_Close,
            [EnumMember(Value = "SFX_Deploy_BedRoll_GoSleep")]
            SFX_Deploy_BedRoll_GoSleep,
            [EnumMember(Value = "SFX_Deploy_BedRoll_Open")]
            SFX_Deploy_BedRoll_Open,
            [EnumMember(Value = "SFX_Deploy_CookingPot_Close")]
            SFX_Deploy_CookingPot_Close,
            [EnumMember(Value = "SFX_Deploy_CookingPot_Open")]
            SFX_Deploy_CookingPot_Open,
            [EnumMember(Value = "SFX_Deploy_EnchantingPedestal_Close")]
            SFX_Deploy_EnchantingPedestal_Close,
            [EnumMember(Value = "SFX_Deploy_EnchantingPedestal_Open")]
            SFX_Deploy_EnchantingPedestal_Open,
            [EnumMember(Value = "SFX_Deploy_EnchantingPilar_Close")]
            SFX_Deploy_EnchantingPilar_Close,
            [EnumMember(Value = "SFX_Deploy_EnchantingPilar_Open")]
            SFX_Deploy_EnchantingPilar_Open,
            [EnumMember(Value = "SFX_Deploy_FireCamp_Open")]
            SFX_Deploy_FireCamp_Open,
            [EnumMember(Value = "SFX_Deploy_Mage_Tent_Close")]
            SFX_Deploy_Mage_Tent_Close,
            [EnumMember(Value = "SFX_Deploy_Mage_Tent_Open")]
            SFX_Deploy_Mage_Tent_Open,
            [EnumMember(Value = "SFX_Deploy_Plant_Tent_Open")]
            SFX_Deploy_Plant_Tent_Open,
            [EnumMember(Value = "SFX_Deploy_ScourgeCoccon_Open")]
            SFX_Deploy_ScourgeCoccon_Open,
            [EnumMember(Value = "SFX_Deploy_Simple_Tent_Open")]
            SFX_Deploy_Simple_Tent_Open,
            [EnumMember(Value = "SFX_Deploy_Simple_Tent_Close")]
            SFX_Deploy_Simple_Tent_Close,
            [EnumMember(Value = "SFX_Deploy_TripwireTrap_Close")]
            SFX_Deploy_TripwireTrap_Close,
            [EnumMember(Value = "SFX_Deploy_TripwireTrap_Open")]
            SFX_Deploy_TripwireTrap_Open,
            [EnumMember(Value = "SFX_SKILL_DrumSkill_Deploy")]
            SFX_SKILL_DrumSkill_Deploy = 15060,
            [EnumMember(Value = "SFX_SKILL_DrumSkill_NurturingEcho_Dance")]
            SFX_SKILL_DrumSkill_NurturingEcho_Dance,
            [EnumMember(Value = "SFX_SKILL_DrumSkill_NurturingEcho_Explode")]
            SFX_SKILL_DrumSkill_NurturingEcho_Explode,
            [EnumMember(Value = "SFX_SKILL_DrumSkill_Reverberation")]
            SFX_SKILL_DrumSkill_Reverberation,
            [EnumMember(Value = "SFX_SKILL_DrumSkill_Diassemble")]
            SFX_SKILL_DrumSkill_Diassemble,
            [EnumMember(Value = "SFX_SKILL_SkyChime_Deploy")]
            SFX_SKILL_SkyChime_Deploy,
            [EnumMember(Value = "SFX_SKILL_SkyChime_Diassemble")]
            SFX_SKILL_SkyChime_Diassemble,
            [EnumMember(Value = "SFX_SKILL_SkyChime_NurturingEcho_Dance")]
            SFX_SKILL_SkyChime_NurturingEcho_Dance,
            [EnumMember(Value = "SFX_SKILL_SkyChime_NurturingEcho_Explode")]
            SFX_SKILL_SkyChime_NurturingEcho_Explode,
            [EnumMember(Value = "SFX_SKILL_SkyChime_Reverberation")]
            SFX_SKILL_SkyChime_Reverberation,
            [EnumMember(Value = "SFX_IMPACT_GuitarAxeA")]
            SFX_IMPACT_GuitarAxeA = 15080,
            [EnumMember(Value = "SFX_IMPACT_GuitarAxeB")]
            SFX_IMPACT_GuitarAxeB,
            [EnumMember(Value = "SFX_IMPACT_GuitarAxeC")]
            SFX_IMPACT_GuitarAxeC,
            [EnumMember(Value = "SFX_IMPACT_GuitarAxeD")]
            SFX_IMPACT_GuitarAxeD,
            [EnumMember(Value = "SFX_SWING_GuitarAxeA")]
            SFX_SWING_GuitarAxeA,
            [EnumMember(Value = "SFX_SWING_GuitarAxeB")]
            SFX_SWING_GuitarAxeB,
            [EnumMember(Value = "SFX_SWING_GuitarAxeC")]
            SFX_SWING_GuitarAxeC,
            [EnumMember(Value = "SFX_IMPACT_TuningForkA")]
            SFX_IMPACT_TuningForkA,
            [EnumMember(Value = "SFX_IMPACT_TuningForkB")]
            SFX_IMPACT_TuningForkB,
            [EnumMember(Value = "SFX_IMPACT_TuningForkC")]
            SFX_IMPACT_TuningForkC,
            [EnumMember(Value = "SFX_SWING_TuningForkA")]
            SFX_SWING_TuningForkA,
            [EnumMember(Value = "SFX_SWING_TuningForkB")]
            SFX_SWING_TuningForkB,
            [EnumMember(Value = "SFX_SWING_TuningForkC")]
            SFX_SWING_TuningForkC,
            [EnumMember(Value = "SFX_SWING_GuitarAxeAwakenedA")]
            SFX_SWING_GuitarAxeAwakenedA,
            [EnumMember(Value = "SFX_SWING_GuitarAxeAwakenedB")]
            SFX_SWING_GuitarAxeAwakenedB,
            [EnumMember(Value = "SFX_SWING_GuitarAxeAwakenedC")]
            SFX_SWING_GuitarAxeAwakenedC,
            [EnumMember(Value = "SFX_SWING_TuningForkAwakenedA")]
            SFX_SWING_TuningForkAwakenedA,
            [EnumMember(Value = "SFX_SWING_TuningForkAwakenedB")]
            SFX_SWING_TuningForkAwakenedB,
            [EnumMember(Value = "SFX_SWING_TuningForkAwakenedC")]
            SFX_SWING_TuningForkAwakenedC,
            [EnumMember(Value = "FT_WoodHollow1")]
            FT_WoodHollow1 = 20000,
            [EnumMember(Value = "FT_WoodHollow2")]
            FT_WoodHollow2,
            [EnumMember(Value = "FT_WoodHollow3")]
            FT_WoodHollow3,
            [EnumMember(Value = "FT_WoodHollow4")]
            FT_WoodHollow4,
            [EnumMember(Value = "FT_WoodHollow5")]
            FT_WoodHollow5,
            [EnumMember(Value = "FT_WoodHollow6")]
            FT_WoodHollow6,
            [EnumMember(Value = "FT_WoodHollow7")]
            FT_WoodHollow7,
            [EnumMember(Value = "FT_WoodHollow8")]
            FT_WoodHollow8,
            [EnumMember(Value = "FT_Metal1")]
            FT_Metal1,
            [EnumMember(Value = "FT_Metal2")]
            FT_Metal2,
            [EnumMember(Value = "FT_Metal3")]
            FT_Metal3,
            [EnumMember(Value = "FT_Metal4")]
            FT_Metal4,
            [EnumMember(Value = "FT_Metal5")]
            FT_Metal5,
            [EnumMember(Value = "FT_Metal6")]
            FT_Metal6,
            [EnumMember(Value = "FT_Metal7")]
            FT_Metal7,
            [EnumMember(Value = "FT_Metal8")]
            FT_Metal8,
            [EnumMember(Value = "FT_WoodHollowStairs1")]
            FT_WoodHollowStairs1,
            [EnumMember(Value = "FT_WoodHollowStairs2")]
            FT_WoodHollowStairs2,
            [EnumMember(Value = "FT_WoodHollowStairs3")]
            FT_WoodHollowStairs3,
            [EnumMember(Value = "FT_WoodHollowStairs4")]
            FT_WoodHollowStairs4,
            [EnumMember(Value = "FT_WoodHollowStairs5")]
            FT_WoodHollowStairs5,
            [EnumMember(Value = "FT_WoodHollowStairs6")]
            FT_WoodHollowStairs6,
            [EnumMember(Value = "FT_WoodHollowStairs7")]
            FT_WoodHollowStairs7,
            [EnumMember(Value = "FT_WoodHollowStairs8")]
            FT_WoodHollowStairs8,
            [EnumMember(Value = "FT_WoodSolid1")]
            FT_WoodSolid1,
            [EnumMember(Value = "FT_WoodSolid2")]
            FT_WoodSolid2,
            [EnumMember(Value = "FT_WoodSolid3")]
            FT_WoodSolid3,
            [EnumMember(Value = "FT_WoodSolid4")]
            FT_WoodSolid4,
            [EnumMember(Value = "FT_WoodSolid5")]
            FT_WoodSolid5,
            [EnumMember(Value = "FT_WoodSolid6")]
            FT_WoodSolid6,
            [EnumMember(Value = "FT_WoodSolid7")]
            FT_WoodSolid7,
            [EnumMember(Value = "FT_WoodSolid8")]
            FT_WoodSolid8,
            [EnumMember(Value = "FT_Concrete1")]
            FT_Concrete1,
            [EnumMember(Value = "FT_Concrete2")]
            FT_Concrete2,
            [EnumMember(Value = "FT_Concrete3")]
            FT_Concrete3,
            [EnumMember(Value = "FT_Concrete4")]
            FT_Concrete4,
            [EnumMember(Value = "FT_Concrete5")]
            FT_Concrete5,
            [EnumMember(Value = "FT_Concrete6")]
            FT_Concrete6,
            [EnumMember(Value = "FT_Concrete7")]
            FT_Concrete7,
            [EnumMember(Value = "FT_Concrete8")]
            FT_Concrete8,
            [EnumMember(Value = "FT_Stone1")]
            FT_Stone1,
            [EnumMember(Value = "FT_Stone2")]
            FT_Stone2,
            [EnumMember(Value = "FT_Stone3")]
            FT_Stone3,
            [EnumMember(Value = "FT_Stone4")]
            FT_Stone4,
            [EnumMember(Value = "FT_Stone5")]
            FT_Stone5,
            [EnumMember(Value = "FT_Stone6")]
            FT_Stone6,
            [EnumMember(Value = "FT_Stone7")]
            FT_Stone7,
            [EnumMember(Value = "FT_Stone8")]
            FT_Stone8,
            [EnumMember(Value = "FT_Dirt1")]
            FT_Dirt1,
            [EnumMember(Value = "FT_Dirt2")]
            FT_Dirt2,
            [EnumMember(Value = "FT_Dirt3")]
            FT_Dirt3,
            [EnumMember(Value = "FT_Dirt4")]
            FT_Dirt4,
            [EnumMember(Value = "FT_Dirt5")]
            FT_Dirt5,
            [EnumMember(Value = "FT_Dirt6")]
            FT_Dirt6,
            [EnumMember(Value = "FT_Dirt7")]
            FT_Dirt7,
            [EnumMember(Value = "FT_Dirt8")]
            FT_Dirt8,
            [EnumMember(Value = "FT_Sand1")]
            FT_Sand1,
            [EnumMember(Value = "FT_Sand2")]
            FT_Sand2,
            [EnumMember(Value = "FT_Sand3")]
            FT_Sand3,
            [EnumMember(Value = "FT_Sand4")]
            FT_Sand4,
            [EnumMember(Value = "FT_Sand5")]
            FT_Sand5,
            [EnumMember(Value = "FT_Sand6")]
            FT_Sand6,
            [EnumMember(Value = "FT_Sand7")]
            FT_Sand7,
            [EnumMember(Value = "FT_Sand8")]
            FT_Sand8,
            [EnumMember(Value = "FT_Gravel1")]
            FT_Gravel1,
            [EnumMember(Value = "FT_Gravel2")]
            FT_Gravel2,
            [EnumMember(Value = "FT_Gravel3")]
            FT_Gravel3,
            [EnumMember(Value = "FT_Gravel4")]
            FT_Gravel4,
            [EnumMember(Value = "FT_Gravel5")]
            FT_Gravel5,
            [EnumMember(Value = "FT_Gravel6")]
            FT_Gravel6,
            [EnumMember(Value = "FT_Gravel7")]
            FT_Gravel7,
            [EnumMember(Value = "FT_Gravel8")]
            FT_Gravel8,
            [EnumMember(Value = "FT_Water1")]
            FT_Water1,
            [EnumMember(Value = "FT_Water2")]
            FT_Water2,
            [EnumMember(Value = "FT_Water3")]
            FT_Water3,
            [EnumMember(Value = "FT_Water4")]
            FT_Water4,
            [EnumMember(Value = "FT_Water5")]
            FT_Water5,
            [EnumMember(Value = "FT_Water6")]
            FT_Water6,
            [EnumMember(Value = "FT_Water7")]
            FT_Water7,
            [EnumMember(Value = "FT_Water8")]
            FT_Water8,
            [EnumMember(Value = "FT_WaterDeep1")]
            FT_WaterDeep1,
            [EnumMember(Value = "FT_WaterDeep2")]
            FT_WaterDeep2,
            [EnumMember(Value = "FT_WaterDeep3")]
            FT_WaterDeep3,
            [EnumMember(Value = "FT_WaterDeep4")]
            FT_WaterDeep4,
            [EnumMember(Value = "FT_WaterLight1")]
            FT_WaterLight1,
            [EnumMember(Value = "FT_WaterLight2")]
            FT_WaterLight2,
            [EnumMember(Value = "FT_WaterLight3")]
            FT_WaterLight3,
            [EnumMember(Value = "FT_WaterLight4")]
            FT_WaterLight4,
            [EnumMember(Value = "FT_WaterLight5")]
            FT_WaterLight5,
            [EnumMember(Value = "FT_WaterLight6")]
            FT_WaterLight6,
            [EnumMember(Value = "FT_WaterLight7")]
            FT_WaterLight7,
            [EnumMember(Value = "FT_WaterLight8")]
            FT_WaterLight8,
            [EnumMember(Value = "FT_Grass1")]
            FT_Grass1,
            [EnumMember(Value = "FT_Grass2")]
            FT_Grass2,
            [EnumMember(Value = "FT_Grass3")]
            FT_Grass3,
            [EnumMember(Value = "FT_Grass4")]
            FT_Grass4,
            [EnumMember(Value = "FT_Grass5")]
            FT_Grass5,
            [EnumMember(Value = "FT_Grass6")]
            FT_Grass6,
            [EnumMember(Value = "FT_Grass7")]
            FT_Grass7,
            [EnumMember(Value = "FT_Grass8")]
            FT_Grass8,
            [EnumMember(Value = "FT_TallGrass1")]
            FT_TallGrass1,
            [EnumMember(Value = "FT_TallGrass2")]
            FT_TallGrass2,
            [EnumMember(Value = "FT_GrassLight1")]
            FT_GrassLight1,
            [EnumMember(Value = "FT_GrassLight2")]
            FT_GrassLight2,
            [EnumMember(Value = "FT_GrassLight3")]
            FT_GrassLight3,
            [EnumMember(Value = "FT_GrassLight4")]
            FT_GrassLight4,
            [EnumMember(Value = "FT_GrassLight5")]
            FT_GrassLight5,
            [EnumMember(Value = "FT_GrassLight6")]
            FT_GrassLight6,
            [EnumMember(Value = "FT_GrassLight7")]
            FT_GrassLight7,
            [EnumMember(Value = "FT_GrassLight8")]
            FT_GrassLight8,
            [EnumMember(Value = "FT_GEARSTEP_MetalArmor1")]
            FT_GEARSTEP_MetalArmor1,
            [EnumMember(Value = "FT_GEARSTEP_MetalArmor2")]
            FT_GEARSTEP_MetalArmor2,
            [EnumMember(Value = "FT_GEARSTEP_MetalArmor3")]
            FT_GEARSTEP_MetalArmor3,
            [EnumMember(Value = "FT_GEARSTEP_MetalArmor4")]
            FT_GEARSTEP_MetalArmor4,
            [EnumMember(Value = "FT_GEARSTEP_MetalArmor5")]
            FT_GEARSTEP_MetalArmor5,
            [EnumMember(Value = "FT_GEARSTEP_MetalArmor6")]
            FT_GEARSTEP_MetalArmor6,
            [EnumMember(Value = "FT_GEARSTEP_LeatherArmor1")]
            FT_GEARSTEP_LeatherArmor1 = 20120,
            [EnumMember(Value = "FT_GEARSTEP_LeatherArmor2")]
            FT_GEARSTEP_LeatherArmor2,
            [EnumMember(Value = "FT_GEARSTEP_LeatherArmor3")]
            FT_GEARSTEP_LeatherArmor3,
            [EnumMember(Value = "FT_GEARSTEP_LeatherArmor4")]
            FT_GEARSTEP_LeatherArmor4,
            [EnumMember(Value = "FT_GEARSTEP_LeatherArmor5")]
            FT_GEARSTEP_LeatherArmor5,
            [EnumMember(Value = "FT_GEARSTEP_LeatherArmor6")]
            FT_GEARSTEP_LeatherArmor6,
            [EnumMember(Value = "FT_GEARSTEP_StoneArmor1")]
            FT_GEARSTEP_StoneArmor1 = 20131,
            [EnumMember(Value = "FT_GEARSTEP_StoneArmor2")]
            FT_GEARSTEP_StoneArmor2,
            [EnumMember(Value = "FT_GEARSTEP_StoneArmor3")]
            FT_GEARSTEP_StoneArmor3,
            [EnumMember(Value = "FT_GEARSTEP_StoneArmor4")]
            FT_GEARSTEP_StoneArmor4,
            [EnumMember(Value = "FT_GEARSTEP_StoneArmor5")]
            FT_GEARSTEP_StoneArmor5,
            [EnumMember(Value = "FT_GEARSTEP_StoneArmor6")]
            FT_GEARSTEP_StoneArmor6,
            [EnumMember(Value = "FT_GEARSTEP_ClothArmor1")]
            FT_GEARSTEP_ClothArmor1 = 20141,
            [EnumMember(Value = "FT_GEARSTEP_ClothArmor2")]
            FT_GEARSTEP_ClothArmor2,
            [EnumMember(Value = "FT_GEARSTEP_ClothArmor3")]
            FT_GEARSTEP_ClothArmor3,
            [EnumMember(Value = "FT_GEARSTEP_ClothArmor4")]
            FT_GEARSTEP_ClothArmor4,
            [EnumMember(Value = "FT_GEARSTEP_ClothArmor5")]
            FT_GEARSTEP_ClothArmor5,
            [EnumMember(Value = "FT_GEARSTEP_ClothArmor6")]
            FT_GEARSTEP_ClothArmor6,
            [EnumMember(Value = "LOC_SPCH_Test1")]
            LOC_SPCH_Test1 = 30000,
            [EnumMember(Value = "LOC_EXCL_Test1")]
            LOC_EXCL_Test1 = 40000,
            [EnumMember(Value = "LOC_EXCL_LichGreet")]
            LOC_EXCL_LichGreet,
            [EnumMember(Value = "LOC_EXCL_LichBye")]
            LOC_EXCL_LichBye,
            [EnumMember(Value = "LOC_EXCL_LichListen04")]
            LOC_EXCL_LichListen04,
            [EnumMember(Value = "LOC_EXCL_LichListen05")]
            LOC_EXCL_LichListen05,
            [EnumMember(Value = "LOC_EXCL_LichListen06")]
            LOC_EXCL_LichListen06,
            [EnumMember(Value = "LOC_EXCL_Immac03")]
            LOC_EXCL_Immac03,
            [EnumMember(Value = "LOC_EXCL_Immac05")]
            LOC_EXCL_Immac05,
            [EnumMember(Value = "LOC_EXCL_Mumble_HostileMan03")]
            LOC_EXCL_Mumble_HostileMan03,
            [EnumMember(Value = "LOC_EXCL_Laughter_HostileMan02")]
            LOC_EXCL_Laughter_HostileMan02,
            [EnumMember(Value = "LOC_EXCL_Mumble_QuietWoman01")]
            LOC_EXCL_Mumble_QuietWoman01,
            [EnumMember(Value = "LOC_EXCL_Mumble_QuietWoman02")]
            LOC_EXCL_Mumble_QuietWoman02,
            [EnumMember(Value = "LOC_EXCL_Mumble_QuietWoman03")]
            LOC_EXCL_Mumble_QuietWoman03,
            [EnumMember(Value = "LOC_EXCL_FemaleHurt03")]
            LOC_EXCL_FemaleHurt03,
            [EnumMember(Value = "LOC_EXCL_FemaleVomiting02")]
            LOC_EXCL_FemaleVomiting02,
            [EnumMember(Value = "LOC_EXCL_Laughter_Myriade")]
            LOC_EXCL_Laughter_Myriade,
            [EnumMember(Value = "LOC_EXCL_Sad_Myriade")]
            LOC_EXCL_Sad_Myriade,
            [EnumMember(Value = "CS_Ankylosaurus_Death")]
            CS_Ankylosaurus_Death = 50000,
            [EnumMember(Value = "CS_Ankylosaurus_Footstep1")]
            CS_Ankylosaurus_Footstep1,
            [EnumMember(Value = "CS_Ankylosaurus_Footstep2")]
            CS_Ankylosaurus_Footstep2,
            [EnumMember(Value = "CS_Ankylosaurus_Footstep3")]
            CS_Ankylosaurus_Footstep3,
            [EnumMember(Value = "CS_Ankylosaurus_Footstep4")]
            CS_Ankylosaurus_Footstep4,
            [EnumMember(Value = "CS_Ankylosaurus_Roar")]
            CS_Ankylosaurus_Roar,
            [EnumMember(Value = "CS_Compy_Death")]
            CS_Compy_Death = 50010,
            [EnumMember(Value = "CS_Compy_Footstep1")]
            CS_Compy_Footstep1,
            [EnumMember(Value = "CS_Compy_Footstep2")]
            CS_Compy_Footstep2,
            [EnumMember(Value = "CS_Compy_Footstep3")]
            CS_Compy_Footstep3,
            [EnumMember(Value = "CS_Compy_Footstep4")]
            CS_Compy_Footstep4,
            [EnumMember(Value = "CS_Compy_Roar")]
            CS_Compy_Roar,
            [EnumMember(Value = "CS_Dilophosaurus_Death")]
            CS_Dilophosaurus_Death = 50020,
            [EnumMember(Value = "CS_Dilophosaurus_Footstep1")]
            CS_Dilophosaurus_Footstep1,
            [EnumMember(Value = "CS_Dilophosaurus_Footstep2")]
            CS_Dilophosaurus_Footstep2,
            [EnumMember(Value = "CS_Dilophosaurus_Footstep3")]
            CS_Dilophosaurus_Footstep3,
            [EnumMember(Value = "CS_Dilophosaurus_Footstep4")]
            CS_Dilophosaurus_Footstep4,
            [EnumMember(Value = "CS_Dilophosaurus_Roar")]
            CS_Dilophosaurus_Roar,
            [EnumMember(Value = "CS_Jeckyl_Death")]
            CS_Jeckyl_Death = 50030,
            [EnumMember(Value = "CS_Jeckyl_Footstep1")]
            CS_Jeckyl_Footstep1,
            [EnumMember(Value = "CS_Jeckyl_Footstep2")]
            CS_Jeckyl_Footstep2,
            [EnumMember(Value = "CS_Jeckyl_Footstep3")]
            CS_Jeckyl_Footstep3,
            [EnumMember(Value = "CS_Jeckyl_Footstep4")]
            CS_Jeckyl_Footstep4,
            [EnumMember(Value = "CS_Jeckyl_Roar")]
            CS_Jeckyl_Roar,
            [EnumMember(Value = "CS_Kruger_Death")]
            CS_Kruger_Death = 50040,
            [EnumMember(Value = "CS_Kruger_Footstep1")]
            CS_Kruger_Footstep1,
            [EnumMember(Value = "CS_Kruger_Footstep2")]
            CS_Kruger_Footstep2,
            [EnumMember(Value = "CS_Kruger_Footstep3")]
            CS_Kruger_Footstep3,
            [EnumMember(Value = "CS_Kruger_Footstep4")]
            CS_Kruger_Footstep4,
            [EnumMember(Value = "CS_Kruger_Roar")]
            CS_Kruger_Roar,
            [EnumMember(Value = "CS_Kruger_RoarLong")]
            CS_Kruger_RoarLong,
            [EnumMember(Value = "CS_Namor_Death")]
            CS_Namor_Death = 50050,
            [EnumMember(Value = "CS_Namor_Footstep1")]
            CS_Namor_Footstep1,
            [EnumMember(Value = "CS_Namor_Footstep2")]
            CS_Namor_Footstep2,
            [EnumMember(Value = "CS_Namor_Footstep3")]
            CS_Namor_Footstep3,
            [EnumMember(Value = "CS_Namor_Footstep4")]
            CS_Namor_Footstep4,
            [EnumMember(Value = "CS_Namor_Roar")]
            CS_Namor_Roar,
            [EnumMember(Value = "CS_Raptor_Death")]
            CS_Raptor_Death = 50060,
            [EnumMember(Value = "CS_Raptor_Footstep1")]
            CS_Raptor_Footstep1,
            [EnumMember(Value = "CS_Raptor_Footstep2")]
            CS_Raptor_Footstep2,
            [EnumMember(Value = "CS_Raptor_Footstep3")]
            CS_Raptor_Footstep3,
            [EnumMember(Value = "CS_Raptor_Footstep4")]
            CS_Raptor_Footstep4,
            [EnumMember(Value = "CS_Raptor_Roar")]
            CS_Raptor_Roar,
            [EnumMember(Value = "CS_Rham_Death")]
            CS_Rham_Death = 50070,
            [EnumMember(Value = "CS_Rham_WingFlap1")]
            CS_Rham_WingFlap1,
            [EnumMember(Value = "CS_Rham_WingFlap2")]
            CS_Rham_WingFlap2,
            [EnumMember(Value = "CS_Rham_WingFlap3")]
            CS_Rham_WingFlap3,
            [EnumMember(Value = "CS_Rham_WingFlap4")]
            CS_Rham_WingFlap4,
            [EnumMember(Value = "CS_Rham_Roar")]
            CS_Rham_Roar,
            [EnumMember(Value = "CS_TRex_Death")]
            CS_TRex_Death = 50080,
            [EnumMember(Value = "CS_TRex_Footstep1")]
            CS_TRex_Footstep1,
            [EnumMember(Value = "CS_TRex_Footstep2")]
            CS_TRex_Footstep2,
            [EnumMember(Value = "CS_TRex_Footstep3")]
            CS_TRex_Footstep3,
            [EnumMember(Value = "CS_TRex_Footstep4")]
            CS_TRex_Footstep4,
            [EnumMember(Value = "CS_TRex_Roar")]
            CS_TRex_Roar,
            [EnumMember(Value = "CS_TRex_RoarKill")]
            CS_TRex_RoarKill,
            [EnumMember(Value = "CS_Triceratops_Death")]
            CS_Triceratops_Death = 50090,
            [EnumMember(Value = "CS_Triceratops_Footstep1")]
            CS_Triceratops_Footstep1,
            [EnumMember(Value = "CS_Triceratops_Footstep2")]
            CS_Triceratops_Footstep2,
            [EnumMember(Value = "CS_Triceratops_Footstep3")]
            CS_Triceratops_Footstep3,
            [EnumMember(Value = "CS_Triceratops_Footstep4")]
            CS_Triceratops_Footstep4,
            [EnumMember(Value = "CS_Triceratops_Roar")]
            CS_Triceratops_Roar,
            [EnumMember(Value = "CS_Hound_AttackBit1")]
            CS_Hound_AttackBit1 = 50100,
            [EnumMember(Value = "CS_Hound_AttackBit2")]
            CS_Hound_AttackBit2,
            [EnumMember(Value = "CS_Hound_AttackBit3")]
            CS_Hound_AttackBit3,
            [EnumMember(Value = "CS_Hound_AttackCall1")]
            CS_Hound_AttackCall1,
            [EnumMember(Value = "CS_Hound_AttackCall2")]
            CS_Hound_AttackCall2,
            [EnumMember(Value = "CS_Hound_BodyFall1")]
            CS_Hound_BodyFall1,
            [EnumMember(Value = "CS_Hound_BodyFall2")]
            CS_Hound_BodyFall2,
            [EnumMember(Value = "CS_Hound_Call1")]
            CS_Hound_Call1,
            [EnumMember(Value = "CS_Hound_Call2")]
            CS_Hound_Call2,
            [EnumMember(Value = "CS_Hound_Call3")]
            CS_Hound_Call3,
            [EnumMember(Value = "CS_Hound_DodgeCall1")]
            CS_Hound_DodgeCall1,
            [EnumMember(Value = "CS_Hound_DodgeCall2")]
            CS_Hound_DodgeCall2,
            [EnumMember(Value = "CS_Hound_FootLand1")]
            CS_Hound_FootLand1,
            [EnumMember(Value = "CS_Hound_FootLand2")]
            CS_Hound_FootLand2,
            [EnumMember(Value = "CS_Hound_FootLand3")]
            CS_Hound_FootLand3,
            [EnumMember(Value = "CS_Hound_FootLand4")]
            CS_Hound_FootLand4,
            [EnumMember(Value = "CS_Hound_FootStep1")]
            CS_Hound_FootStep1,
            [EnumMember(Value = "CS_Hound_FootStep2")]
            CS_Hound_FootStep2,
            [EnumMember(Value = "CS_Hound_FootStep3")]
            CS_Hound_FootStep3,
            [EnumMember(Value = "CS_Hound_FootStep4")]
            CS_Hound_FootStep4,
            [EnumMember(Value = "CS_Hound_FootStep5")]
            CS_Hound_FootStep5,
            [EnumMember(Value = "CS_Hound_FootStep6")]
            CS_Hound_FootStep6,
            [EnumMember(Value = "CS_Hound_HeavyHurt1")]
            CS_Hound_HeavyHurt1,
            [EnumMember(Value = "CS_Hound_HeavyHurt2")]
            CS_Hound_HeavyHurt2,
            [EnumMember(Value = "CS_Hound_Hit1")]
            CS_Hound_Hit1,
            [EnumMember(Value = "CS_Hound_Hit2")]
            CS_Hound_Hit2,
            [EnumMember(Value = "CS_Hound_Hit3")]
            CS_Hound_Hit3,
            [EnumMember(Value = "CS_Hound_Hurt1")]
            CS_Hound_Hurt1,
            [EnumMember(Value = "CS_Hound_Hurt2")]
            CS_Hound_Hurt2,
            [EnumMember(Value = "CS_Hound_Hurt3")]
            CS_Hound_Hurt3,
            [EnumMember(Value = "CS_Hound_Laught1")]
            CS_Hound_Laught1,
            [EnumMember(Value = "CS_Hound_Laught2")]
            CS_Hound_Laught2,
            [EnumMember(Value = "CS_Hound_Laught3")]
            CS_Hound_Laught3,
            [EnumMember(Value = "CS_Hound_Laught4")]
            CS_Hound_Laught4,
            [EnumMember(Value = "CS_Hound_Pounce1")]
            CS_Hound_Pounce1,
            [EnumMember(Value = "CS_Hound_Pounce2")]
            CS_Hound_Pounce2,
            [EnumMember(Value = "CS_Hound_PounceJump1")]
            CS_Hound_PounceJump1,
            [EnumMember(Value = "CS_Hound_PounceJump2")]
            CS_Hound_PounceJump2,
            [EnumMember(Value = "CS_Hound_WhooshJump1")]
            CS_Hound_WhooshJump1,
            [EnumMember(Value = "CS_Hound_WhooshJump2")]
            CS_Hound_WhooshJump2,
            [EnumMember(Value = "CS_Hound_WhooshJump3")]
            CS_Hound_WhooshJump3,
            [EnumMember(Value = "CS_AssBug_AttackA1")]
            CS_AssBug_AttackA1 = 50150,
            [EnumMember(Value = "CS_AssBug_AttackA2")]
            CS_AssBug_AttackA2,
            [EnumMember(Value = "CS_AssBug_AttackA3")]
            CS_AssBug_AttackA3,
            [EnumMember(Value = "CS_AssBug_AttackA4")]
            CS_AssBug_AttackA4,
            [EnumMember(Value = "CS_AssBug_AttackA5")]
            CS_AssBug_AttackA5,
            [EnumMember(Value = "CS_AssBug_AttackB1")]
            CS_AssBug_AttackB1,
            [EnumMember(Value = "CS_AssBug_AttackB2")]
            CS_AssBug_AttackB2,
            [EnumMember(Value = "CS_AssBug_AttackB3")]
            CS_AssBug_AttackB3,
            [EnumMember(Value = "CS_AssBug_AttackB4")]
            CS_AssBug_AttackB4,
            [EnumMember(Value = "CS_AssBug_AttackB_Grunt1")]
            CS_AssBug_AttackB_Grunt1,
            [EnumMember(Value = "CS_AssBug_AttackB_Grunt2")]
            CS_AssBug_AttackB_Grunt2,
            [EnumMember(Value = "CS_AssBug_AttackB_Grunt3")]
            CS_AssBug_AttackB_Grunt3,
            [EnumMember(Value = "CS_AssBug_Attack_Scream1")]
            CS_AssBug_Attack_Scream1,
            [EnumMember(Value = "CS_AssBug_Attack_Scream2")]
            CS_AssBug_Attack_Scream2,
            [EnumMember(Value = "CS_AssBug_Attack_Scream3")]
            CS_AssBug_Attack_Scream3,
            [EnumMember(Value = "CS_AssBug_Attack_Scream4")]
            CS_AssBug_Attack_Scream4,
            [EnumMember(Value = "CS_AssBug_BigScream1")]
            CS_AssBug_BigScream1,
            [EnumMember(Value = "CS_AssBug_BigScream2")]
            CS_AssBug_BigScream2,
            [EnumMember(Value = "CS_AssBug_BodyFall1")]
            CS_AssBug_BodyFall1,
            [EnumMember(Value = "CS_AssBug_BodyFall2")]
            CS_AssBug_BodyFall2,
            [EnumMember(Value = "CS_AssBug_Footstep1")]
            CS_AssBug_Footstep1,
            [EnumMember(Value = "CS_AssBug_Footstep2")]
            CS_AssBug_Footstep2,
            [EnumMember(Value = "CS_AssBug_Footstep3")]
            CS_AssBug_Footstep3,
            [EnumMember(Value = "CS_AssBug_Footstep4")]
            CS_AssBug_Footstep4,
            [EnumMember(Value = "CS_AssBug_Footstep5")]
            CS_AssBug_Footstep5,
            [EnumMember(Value = "CS_AssBug_Footstep6")]
            CS_AssBug_Footstep6,
            [EnumMember(Value = "CS_AssBug_Footstep7")]
            CS_AssBug_Footstep7,
            [EnumMember(Value = "CS_AssBug_Footstep8")]
            CS_AssBug_Footstep8,
            [EnumMember(Value = "CS_AssBug_Hurt1")]
            CS_AssBug_Hurt1,
            [EnumMember(Value = "CS_AssBug_Hurt2")]
            CS_AssBug_Hurt2,
            [EnumMember(Value = "CS_AssBug_Hurt3")]
            CS_AssBug_Hurt3,
            [EnumMember(Value = "CS_AssBug_Hurt4")]
            CS_AssBug_Hurt4,
            [EnumMember(Value = "CS_AssBug_KnockDownFF1")]
            CS_AssBug_KnockDownFF1,
            [EnumMember(Value = "CS_AssBug_KnockDownFF2")]
            CS_AssBug_KnockDownFF2,
            [EnumMember(Value = "CS_AssBug_KnockDownFF3")]
            CS_AssBug_KnockDownFF3,
            [EnumMember(Value = "CS_AssBug_KnockDownFF_Grunt1")]
            CS_AssBug_KnockDownFF_Grunt1,
            [EnumMember(Value = "CS_AssBug_KnockDownFF_Grunt2")]
            CS_AssBug_KnockDownFF_Grunt2,
            [EnumMember(Value = "CS_AssBug_KnockDownFF_Grunt3")]
            CS_AssBug_KnockDownFF_Grunt3,
            [EnumMember(Value = "CS_AssBug_Knocked1")]
            CS_AssBug_Knocked1,
            [EnumMember(Value = "CS_AssBug_Knocked2")]
            CS_AssBug_Knocked2,
            [EnumMember(Value = "CS_AssBug_Knocked3")]
            CS_AssBug_Knocked3,
            [EnumMember(Value = "CS_AssBug_Knocked4")]
            CS_AssBug_Knocked4,
            [EnumMember(Value = "CS_AssBug_Knocked5")]
            CS_AssBug_Knocked5,
            [EnumMember(Value = "CS_AssBug_Knocked6")]
            CS_AssBug_Knocked6,
            [EnumMember(Value = "CS_Beetle_Attack_Grunt1")]
            CS_Beetle_Attack_Grunt1 = 50200,
            [EnumMember(Value = "CS_Beetle_Attack_Grunt2")]
            CS_Beetle_Attack_Grunt2,
            [EnumMember(Value = "CS_Beetle_Attack_Grunt3")]
            CS_Beetle_Attack_Grunt3,
            [EnumMember(Value = "CS_Beetle_Attack_Whoosh1")]
            CS_Beetle_Attack_Whoosh1,
            [EnumMember(Value = "CS_Beetle_Attack_Whoosh2")]
            CS_Beetle_Attack_Whoosh2,
            [EnumMember(Value = "CS_Beetle_Attack_Whoosh3")]
            CS_Beetle_Attack_Whoosh3,
            [EnumMember(Value = "CS_Beetle_Attack_Whoosh4")]
            CS_Beetle_Attack_Whoosh4,
            [EnumMember(Value = "CS_Beetle_AttackDouble_Grunt1")]
            CS_Beetle_AttackDouble_Grunt1,
            [EnumMember(Value = "CS_Beetle_AttackDouble_Grunt2")]
            CS_Beetle_AttackDouble_Grunt2,
            [EnumMember(Value = "CS_Beetle_AttackDouble_Whoosh1")]
            CS_Beetle_AttackDouble_Whoosh1,
            [EnumMember(Value = "CS_Beetle_AttackDouble_Whoosh2")]
            CS_Beetle_AttackDouble_Whoosh2,
            [EnumMember(Value = "CS_Beetle_AttackDouble_Whoosh3")]
            CS_Beetle_AttackDouble_Whoosh3,
            [EnumMember(Value = "CS_Beetle_AttackDouble_WingFlap1")]
            CS_Beetle_AttackDouble_WingFlap1,
            [EnumMember(Value = "CS_Beetle_AttackDouble_WingFlap2")]
            CS_Beetle_AttackDouble_WingFlap2,
            [EnumMember(Value = "CS_Beetle_AttackDouble_WingFlap3")]
            CS_Beetle_AttackDouble_WingFlap3,
            [EnumMember(Value = "CS_Beetle_BodyFall1")]
            CS_Beetle_BodyFall1,
            [EnumMember(Value = "CS_Beetle_BodyFall2")]
            CS_Beetle_BodyFall2,
            [EnumMember(Value = "CS_Beetle_BodyFall3")]
            CS_Beetle_BodyFall3,
            [EnumMember(Value = "CS_Beetle_Footstep1")]
            CS_Beetle_Footstep1,
            [EnumMember(Value = "CS_Beetle_Footstep2")]
            CS_Beetle_Footstep2,
            [EnumMember(Value = "CS_Beetle_Footstep3")]
            CS_Beetle_Footstep3,
            [EnumMember(Value = "CS_Beetle_Footstep4")]
            CS_Beetle_Footstep4,
            [EnumMember(Value = "CS_Beetle_Footstep5")]
            CS_Beetle_Footstep5,
            [EnumMember(Value = "CS_Beetle_Footstep6")]
            CS_Beetle_Footstep6,
            [EnumMember(Value = "CS_Beetle_Footstep7")]
            CS_Beetle_Footstep7,
            [EnumMember(Value = "CS_Beetle_Footstep8")]
            CS_Beetle_Footstep8,
            [EnumMember(Value = "CS_Beetle_HeavyHurt_Grunt1")]
            CS_Beetle_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_Beetle_HeavyHurt_Grunt2")]
            CS_Beetle_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_Beetle_HeavyHurt_Grunt3")]
            CS_Beetle_HeavyHurt_Grunt3,
            [EnumMember(Value = "CS_Beetle_Hurt_Grunt1")]
            CS_Beetle_Hurt_Grunt1,
            [EnumMember(Value = "CS_Beetle_Hurt_Grunt2")]
            CS_Beetle_Hurt_Grunt2,
            [EnumMember(Value = "CS_Beetle_Hurt_Grunt3")]
            CS_Beetle_Hurt_Grunt3,
            [EnumMember(Value = "CS_Beetle_Hurt_Grunt4")]
            CS_Beetle_Hurt_Grunt4,
            [EnumMember(Value = "CS_Beetle_HurtHit1")]
            CS_Beetle_HurtHit1,
            [EnumMember(Value = "CS_Beetle_HurtHit2")]
            CS_Beetle_HurtHit2,
            [EnumMember(Value = "CS_Beetle_HurtHit3")]
            CS_Beetle_HurtHit3,
            [EnumMember(Value = "CS_Beetle_HurtHit4")]
            CS_Beetle_HurtHit4,
            [EnumMember(Value = "CS_Beetle_IdleWalking")]
            CS_Beetle_IdleWalking,
            [EnumMember(Value = "CS_Beetle_RangeAttack1")]
            CS_Beetle_RangeAttack1,
            [EnumMember(Value = "CS_Beetle_RangeAttack2")]
            CS_Beetle_RangeAttack2,
            [EnumMember(Value = "CS_Beetle_RangeAttack_Grunt1")]
            CS_Beetle_RangeAttack_Grunt1,
            [EnumMember(Value = "CS_Beetle_RangeAttack_Grunt2")]
            CS_Beetle_RangeAttack_Grunt2,
            [EnumMember(Value = "CS_Beetle_RangeAttackHeavy1")]
            CS_Beetle_RangeAttackHeavy1,
            [EnumMember(Value = "CS_Beetle_RangeAttackHeavy2")]
            CS_Beetle_RangeAttackHeavy2,
            [EnumMember(Value = "CS_Beetle_RangeAttackHeavy_Growl1")]
            CS_Beetle_RangeAttackHeavy_Growl1,
            [EnumMember(Value = "CS_Beetle_RangeAttackHeavy_Growl2")]
            CS_Beetle_RangeAttackHeavy_Growl2,
            [EnumMember(Value = "CS_Beetle_RangeAttackHeavy_Growl3")]
            CS_Beetle_RangeAttackHeavy_Growl3,
            [EnumMember(Value = "CS_Beetle_RollingOnBack1")]
            CS_Beetle_RollingOnBack1,
            [EnumMember(Value = "CS_Beetle_RollingOnBack_VO1")]
            CS_Beetle_RollingOnBack_VO1,
            [EnumMember(Value = "CS_Beetle_RollingOnBack_VO2")]
            CS_Beetle_RollingOnBack_VO2,
            [EnumMember(Value = "CS_Beetle_RollingOnBack_VO3")]
            CS_Beetle_RollingOnBack_VO3,
            [EnumMember(Value = "CS_HiveLord_Attack_BigGrunt1")]
            CS_HiveLord_Attack_BigGrunt1,
            [EnumMember(Value = "CS_HiveLord_Attack_BigGrunt2")]
            CS_HiveLord_Attack_BigGrunt2,
            [EnumMember(Value = "CS_HiveLord_Attack_BigGrunt3")]
            CS_HiveLord_Attack_BigGrunt3,
            [EnumMember(Value = "CS_HiveLord_Attack_Grunt1")]
            CS_HiveLord_Attack_Grunt1,
            [EnumMember(Value = "CS_HiveLord_Attack_Grunt2")]
            CS_HiveLord_Attack_Grunt2,
            [EnumMember(Value = "CS_HiveLord_Attack_Grunt3")]
            CS_HiveLord_Attack_Grunt3,
            [EnumMember(Value = "CS_HiveLord_Attack_Grunt4")]
            CS_HiveLord_Attack_Grunt4,
            [EnumMember(Value = "CS_HiveLord_Attack_Grunt5")]
            CS_HiveLord_Attack_Grunt5,
            [EnumMember(Value = "CS_HiveLord_Attack_HeavyWhoosh1")]
            CS_HiveLord_Attack_HeavyWhoosh1,
            [EnumMember(Value = "CS_HiveLord_Attack_HeavyWhoosh2")]
            CS_HiveLord_Attack_HeavyWhoosh2,
            [EnumMember(Value = "CS_HiveLord_Attack_HeavyWhoosh3")]
            CS_HiveLord_Attack_HeavyWhoosh3,
            [EnumMember(Value = "CS_HiveLord_Attack_Whoosh1")]
            CS_HiveLord_Attack_Whoosh1,
            [EnumMember(Value = "CS_HiveLord_Attack_Whoosh2")]
            CS_HiveLord_Attack_Whoosh2,
            [EnumMember(Value = "CS_HiveLord_Attack_Whoosh3")]
            CS_HiveLord_Attack_Whoosh3,
            [EnumMember(Value = "CS_HiveLord_Attack_Whoosh4")]
            CS_HiveLord_Attack_Whoosh4,
            [EnumMember(Value = "CS_HiveLord_BodyFall1")]
            CS_HiveLord_BodyFall1,
            [EnumMember(Value = "CS_HiveLord_BodyFall2")]
            CS_HiveLord_BodyFall2,
            [EnumMember(Value = "CS_HiveLord_BodyFall3")]
            CS_HiveLord_BodyFall3,
            [EnumMember(Value = "CS_HiveLord_Footstep_Run1")]
            CS_HiveLord_Footstep_Run1,
            [EnumMember(Value = "CS_HiveLord_Footstep_Run2")]
            CS_HiveLord_Footstep_Run2,
            [EnumMember(Value = "CS_HiveLord_Footstep_Run3")]
            CS_HiveLord_Footstep_Run3,
            [EnumMember(Value = "CS_HiveLord_Footstep_Run4")]
            CS_HiveLord_Footstep_Run4,
            [EnumMember(Value = "CS_HiveLord_Footstep_Run5")]
            CS_HiveLord_Footstep_Run5,
            [EnumMember(Value = "CS_HiveLord_Footstep_Run6")]
            CS_HiveLord_Footstep_Run6,
            [EnumMember(Value = "CS_HiveLord_Footstep_Walk1")]
            CS_HiveLord_Footstep_Walk1,
            [EnumMember(Value = "CS_HiveLord_Footstep_Walk2")]
            CS_HiveLord_Footstep_Walk2,
            [EnumMember(Value = "CS_HiveLord_Footstep_Walk3")]
            CS_HiveLord_Footstep_Walk3,
            [EnumMember(Value = "CS_HiveLord_Footstep_Walk4")]
            CS_HiveLord_Footstep_Walk4,
            [EnumMember(Value = "CS_HiveLord_Footstep_Walk5")]
            CS_HiveLord_Footstep_Walk5,
            [EnumMember(Value = "CS_HiveLord_Footstep_Walk6")]
            CS_HiveLord_Footstep_Walk6,
            [EnumMember(Value = "CS_HiveLord_Hurt_Grunt1")]
            CS_HiveLord_Hurt_Grunt1,
            [EnumMember(Value = "CS_HiveLord_Hurt_Grunt2")]
            CS_HiveLord_Hurt_Grunt2,
            [EnumMember(Value = "CS_HiveLord_Hurt_Grunt3")]
            CS_HiveLord_Hurt_Grunt3,
            [EnumMember(Value = "CS_HiveLord_Hurt_Grunt4")]
            CS_HiveLord_Hurt_Grunt4,
            [EnumMember(Value = "CS_HiveLord_HurtHit1")]
            CS_HiveLord_HurtHit1,
            [EnumMember(Value = "CS_HiveLord_HurtHit2")]
            CS_HiveLord_HurtHit2,
            [EnumMember(Value = "CS_HiveLord_HurtHit3")]
            CS_HiveLord_HurtHit3,
            [EnumMember(Value = "CS_HiveLord_SwarmAttack_Scream1")]
            CS_HiveLord_SwarmAttack_Scream1,
            [EnumMember(Value = "CS_HiveLord_SwarmAttack_Scream2")]
            CS_HiveLord_SwarmAttack_Scream2,
            [EnumMember(Value = "CS_Phytosaur_AttackGoreA_Grunt1")]
            CS_Phytosaur_AttackGoreA_Grunt1,
            [EnumMember(Value = "CS_Phytosaur_AttackGoreA_Grunt2")]
            CS_Phytosaur_AttackGoreA_Grunt2,
            [EnumMember(Value = "CS_Phytosaur_AttackGoreB_Grunt1")]
            CS_Phytosaur_AttackGoreB_Grunt1,
            [EnumMember(Value = "CS_Phytosaur_AttackGoreB_Grunt2")]
            CS_Phytosaur_AttackGoreB_Grunt2,
            [EnumMember(Value = "CS_Phytosaur_AttackGoreB_Grunt3")]
            CS_Phytosaur_AttackGoreB_Grunt3,
            [EnumMember(Value = "CS_Phytosaur_AttackPolen_Grunt1")]
            CS_Phytosaur_AttackPolen_Grunt1,
            [EnumMember(Value = "CS_Phytosaur_AttackPolen_Grunt2")]
            CS_Phytosaur_AttackPolen_Grunt2,
            [EnumMember(Value = "CS_Phytosaur_AttackPolen_Grunt3")]
            CS_Phytosaur_AttackPolen_Grunt3,
            [EnumMember(Value = "CS_Phytosaur_AttackPolen_ShakeWhoosh")]
            CS_Phytosaur_AttackPolen_ShakeWhoosh,
            [EnumMember(Value = "CS_Phytosaur_AttackSpecial_Whoosh1")]
            CS_Phytosaur_AttackSpecial_Whoosh1,
            [EnumMember(Value = "CS_Phytosaur_AttackSpecial_Whoosh2")]
            CS_Phytosaur_AttackSpecial_Whoosh2,
            [EnumMember(Value = "CS_Phytosaur_AttackSpecial_Whoosh3")]
            CS_Phytosaur_AttackSpecial_Whoosh3,
            [EnumMember(Value = "CS_Phytosaur_AttackSpecial_Whoosh4")]
            CS_Phytosaur_AttackSpecial_Whoosh4,
            [EnumMember(Value = "CS_Phytosaur_AttackSpecial_Grunt1")]
            CS_Phytosaur_AttackSpecial_Grunt1,
            [EnumMember(Value = "CS_Phytosaur_AttackSpecial_Grunt2")]
            CS_Phytosaur_AttackSpecial_Grunt2,
            [EnumMember(Value = "CS_Phytosaur_Attack_Whoosh1")]
            CS_Phytosaur_Attack_Whoosh1,
            [EnumMember(Value = "CS_Phytosaur_Attack_Whoosh2")]
            CS_Phytosaur_Attack_Whoosh2,
            [EnumMember(Value = "CS_Phytosaur_Attack_Whoosh3")]
            CS_Phytosaur_Attack_Whoosh3,
            [EnumMember(Value = "CS_Phytosaur_Attack_Whoosh4")]
            CS_Phytosaur_Attack_Whoosh4,
            [EnumMember(Value = "CS_Phytosaur_BodyFall1")]
            CS_Phytosaur_BodyFall1,
            [EnumMember(Value = "CS_Phytosaur_BodyFall2")]
            CS_Phytosaur_BodyFall2,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Run1")]
            CS_Phytosaur_Footstep_Run1,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Run2")]
            CS_Phytosaur_Footstep_Run2,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Run3")]
            CS_Phytosaur_Footstep_Run3,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Run4")]
            CS_Phytosaur_Footstep_Run4,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Run5")]
            CS_Phytosaur_Footstep_Run5,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Run6")]
            CS_Phytosaur_Footstep_Run6,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Walk1")]
            CS_Phytosaur_Footstep_Walk1,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Walk2")]
            CS_Phytosaur_Footstep_Walk2,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Walk3")]
            CS_Phytosaur_Footstep_Walk3,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Walk4")]
            CS_Phytosaur_Footstep_Walk4,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Walk5")]
            CS_Phytosaur_Footstep_Walk5,
            [EnumMember(Value = "CS_Phytosaur_Footstep_Walk6")]
            CS_Phytosaur_Footstep_Walk6,
            [EnumMember(Value = "CS_Phytosaur_HurtHit1")]
            CS_Phytosaur_HurtHit1,
            [EnumMember(Value = "CS_Phytosaur_HurtHit2")]
            CS_Phytosaur_HurtHit2,
            [EnumMember(Value = "CS_Phytosaur_HurtHit3")]
            CS_Phytosaur_HurtHit3,
            [EnumMember(Value = "CS_Phytosaur_HurtHit4")]
            CS_Phytosaur_HurtHit4,
            [EnumMember(Value = "CS_Phytosaur_Hurt_Grunt1")]
            CS_Phytosaur_Hurt_Grunt1,
            [EnumMember(Value = "CS_Phytosaur_Hurt_Grunt2")]
            CS_Phytosaur_Hurt_Grunt2,
            [EnumMember(Value = "CS_Phytosaur_Hurt_Grunt3")]
            CS_Phytosaur_Hurt_Grunt3,
            [EnumMember(Value = "CS_Phytosaur_Hurt_Grunt4")]
            CS_Phytosaur_Hurt_Grunt4,
            [EnumMember(Value = "CS_Phytosaur_Hurt_Grunt5")]
            CS_Phytosaur_Hurt_Grunt5,
            [EnumMember(Value = "CS_Phytosaur_Hurt_Grunt6")]
            CS_Phytosaur_Hurt_Grunt6,
            [EnumMember(Value = "CS_Phytosaur_Hurt_Grunt7")]
            CS_Phytosaur_Hurt_Grunt7,
            [EnumMember(Value = "CS_Phytosaur_RunBreath")]
            CS_Phytosaur_RunBreath,
            [EnumMember(Value = "CS_Phytosaur_Scream1")]
            CS_Phytosaur_Scream1,
            [EnumMember(Value = "CS_Phytosaur_Scream2")]
            CS_Phytosaur_Scream2,
            [EnumMember(Value = "CS_Phytosaur_Scream3")]
            CS_Phytosaur_Scream3,
            [EnumMember(Value = "CS_Phytosaur_Scream4")]
            CS_Phytosaur_Scream4,
            [EnumMember(Value = "CS_Shrimp_Attack_Grunt1")]
            CS_Shrimp_Attack_Grunt1 = 50340,
            [EnumMember(Value = "CS_Shrimp_Attack_Grunt2")]
            CS_Shrimp_Attack_Grunt2,
            [EnumMember(Value = "CS_Shrimp_Attack_Grunt3")]
            CS_Shrimp_Attack_Grunt3,
            [EnumMember(Value = "CS_Shrimp_AttackSpecial_Grunt1")]
            CS_Shrimp_AttackSpecial_Grunt1,
            [EnumMember(Value = "CS_Shrimp_AttackSpecial_Grunt2")]
            CS_Shrimp_AttackSpecial_Grunt2,
            [EnumMember(Value = "CS_Shrimp_AttackSpecial_Grunt3")]
            CS_Shrimp_AttackSpecial_Grunt3,
            [EnumMember(Value = "CS_Shrimp_AttackSpecial_Whoosh1")]
            CS_Shrimp_AttackSpecial_Whoosh1,
            [EnumMember(Value = "CS_Shrimp_AttackSpecial_Whoosh2")]
            CS_Shrimp_AttackSpecial_Whoosh2,
            [EnumMember(Value = "CS_Shrimp_AttackSpecial_Whoosh3")]
            CS_Shrimp_AttackSpecial_Whoosh3,
            [EnumMember(Value = "CS_Shrimp_Attack_Whoosh1")]
            CS_Shrimp_Attack_Whoosh1,
            [EnumMember(Value = "CS_Shrimp_Attack_Whoosh2")]
            CS_Shrimp_Attack_Whoosh2,
            [EnumMember(Value = "CS_Shrimp_Attack_Whoosh3")]
            CS_Shrimp_Attack_Whoosh3,
            [EnumMember(Value = "CS_Shrimp_BodyFall1")]
            CS_Shrimp_BodyFall1,
            [EnumMember(Value = "CS_Shrimp_BodyFall2")]
            CS_Shrimp_BodyFall2,
            [EnumMember(Value = "CS_Shrimp_Footstep1")]
            CS_Shrimp_Footstep1,
            [EnumMember(Value = "CS_Shrimp_Footstep2")]
            CS_Shrimp_Footstep2,
            [EnumMember(Value = "CS_Shrimp_Footstep3")]
            CS_Shrimp_Footstep3,
            [EnumMember(Value = "CS_Shrimp_Footstep4")]
            CS_Shrimp_Footstep4,
            [EnumMember(Value = "CS_Shrimp_Footstep5")]
            CS_Shrimp_Footstep5,
            [EnumMember(Value = "CS_Shrimp_Footstep6")]
            CS_Shrimp_Footstep6,
            [EnumMember(Value = "CS_Shrimp_Footstep7")]
            CS_Shrimp_Footstep7,
            [EnumMember(Value = "CS_Shrimp_Footstep8")]
            CS_Shrimp_Footstep8,
            [EnumMember(Value = "CS_Shrimp_Hurt_Grunt1")]
            CS_Shrimp_Hurt_Grunt1,
            [EnumMember(Value = "CS_Shrimp_Hurt_Grunt2")]
            CS_Shrimp_Hurt_Grunt2,
            [EnumMember(Value = "CS_Shrimp_Hurt_Grunt3")]
            CS_Shrimp_Hurt_Grunt3,
            [EnumMember(Value = "CS_Shrimp_Hurt_Grunt4")]
            CS_Shrimp_Hurt_Grunt4,
            [EnumMember(Value = "CS_Shrimp_HurtHit1")]
            CS_Shrimp_HurtHit1,
            [EnumMember(Value = "CS_Shrimp_HurtHit2")]
            CS_Shrimp_HurtHit2,
            [EnumMember(Value = "CS_Shrimp_HurtHit3")]
            CS_Shrimp_HurtHit3,
            [EnumMember(Value = "CS_Shrimp_Scream1")]
            CS_Shrimp_Scream1,
            [EnumMember(Value = "CS_Shrimp_Scream2")]
            CS_Shrimp_Scream2,
            [EnumMember(Value = "CS_Shrimp_Scream3")]
            CS_Shrimp_Scream3,
            [EnumMember(Value = "CS_Shrimp_Scream4")]
            CS_Shrimp_Scream4,
            [EnumMember(Value = "CS_Bird_Eating1")]
            CS_Bird_Eating1 = 50380,
            [EnumMember(Value = "CS_Bird_Eating2")]
            CS_Bird_Eating2,
            [EnumMember(Value = "CS_Bird_Eating3")]
            CS_Bird_Eating3,
            [EnumMember(Value = "CS_Bird_WingFlap1")]
            CS_Bird_WingFlap1,
            [EnumMember(Value = "CS_Bird_WingFlap2")]
            CS_Bird_WingFlap2,
            [EnumMember(Value = "CS_Bird_WingFlap3")]
            CS_Bird_WingFlap3,
            [EnumMember(Value = "CS_Bird_WingFlap4")]
            CS_Bird_WingFlap4,
            [EnumMember(Value = "CS_Bird_WingFlap5")]
            CS_Bird_WingFlap5,
            [EnumMember(Value = "CS_Bird_WingFlap6")]
            CS_Bird_WingFlap6,
            [EnumMember(Value = "CS_Bird_WingFlap_Loop")]
            CS_Bird_WingFlap_Loop,
            [EnumMember(Value = "CS_Human_Attack_Grunt1")]
            CS_Human_Attack_Grunt1 = 50400,
            [EnumMember(Value = "CS_Human_Attack_Grunt2")]
            CS_Human_Attack_Grunt2,
            [EnumMember(Value = "CS_Human_Attack_Grunt3")]
            CS_Human_Attack_Grunt3,
            [EnumMember(Value = "CS_Human_Attack_Grunt4")]
            CS_Human_Attack_Grunt4,
            [EnumMember(Value = "CS_Human_Attack_Grunt5")]
            CS_Human_Attack_Grunt5,
            [EnumMember(Value = "CS_Human_Attack_Grunt6")]
            CS_Human_Attack_Grunt6,
            [EnumMember(Value = "CS_Human_Attack_Grunt7")]
            CS_Human_Attack_Grunt7,
            [EnumMember(Value = "CS_Human_HeavyAttack_Grunt1")]
            CS_Human_HeavyAttack_Grunt1,
            [EnumMember(Value = "CS_Human_HeavyAttack_Grunt2")]
            CS_Human_HeavyAttack_Grunt2,
            [EnumMember(Value = "CS_Human_HeavyAttack_Grunt3")]
            CS_Human_HeavyAttack_Grunt3,
            [EnumMember(Value = "CS_Human_HeavyAttack_Grunt4")]
            CS_Human_HeavyAttack_Grunt4,
            [EnumMember(Value = "CS_Human_HeavyAttack_Grunt5")]
            CS_Human_HeavyAttack_Grunt5,
            [EnumMember(Value = "CS_Human_HeavyAttack_Grunt6")]
            CS_Human_HeavyAttack_Grunt6,
            [EnumMember(Value = "CS_Human_HeavyAttack_Grunt7")]
            CS_Human_HeavyAttack_Grunt7,
            [EnumMember(Value = "CS_Human_HeavyHurt_Grunt1")]
            CS_Human_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_Human_HeavyHurt_Grunt2")]
            CS_Human_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_Human_HeavyHurt_Grunt3")]
            CS_Human_HeavyHurt_Grunt3,
            [EnumMember(Value = "CS_Human_HeavyHurt_Grunt4")]
            CS_Human_HeavyHurt_Grunt4,
            [EnumMember(Value = "CS_Human_HeavyHurt_Grunt5")]
            CS_Human_HeavyHurt_Grunt5,
            [EnumMember(Value = "CS_Human_HeavyHurt_Grunt6")]
            CS_Human_HeavyHurt_Grunt6,
            [EnumMember(Value = "CS_Human_HeavyHurt_Grunt7")]
            CS_Human_HeavyHurt_Grunt7,
            [EnumMember(Value = "CS_Human_Hurt_Grunt1")]
            CS_Human_Hurt_Grunt1,
            [EnumMember(Value = "CS_Human_Hurt_Grunt2")]
            CS_Human_Hurt_Grunt2,
            [EnumMember(Value = "CS_Human_Hurt_Grunt3")]
            CS_Human_Hurt_Grunt3,
            [EnumMember(Value = "CS_Human_Hurt_Grunt4")]
            CS_Human_Hurt_Grunt4,
            [EnumMember(Value = "CS_Human_Hurt_Grunt5")]
            CS_Human_Hurt_Grunt5,
            [EnumMember(Value = "CS_Human_Hurt_Grunt6")]
            CS_Human_Hurt_Grunt6,
            [EnumMember(Value = "CS_Human_Hurt_Grunt7")]
            CS_Human_Hurt_Grunt7,
            [EnumMember(Value = "CS_Human_Yelling1")]
            CS_Human_Yelling1,
            [EnumMember(Value = "CS_Human_Yelling2")]
            CS_Human_Yelling2,
            [EnumMember(Value = "CS_Human_Yelling3")]
            CS_Human_Yelling3,
            [EnumMember(Value = "CS_Human_Yelling4")]
            CS_Human_Yelling4,
            [EnumMember(Value = "CS_Human_Yelling5")]
            CS_Human_Yelling5,
            [EnumMember(Value = "CS_Human_Yelling6")]
            CS_Human_Yelling6,
            [EnumMember(Value = "CS_ForgeGolem_BodyFall1")]
            CS_ForgeGolem_BodyFall1 = 50440,
            [EnumMember(Value = "CS_ForgeGolem_BodyFall2")]
            CS_ForgeGolem_BodyFall2,
            [EnumMember(Value = "CS_ForgeGolem_BodyFall3")]
            CS_ForgeGolem_BodyFall3,
            [EnumMember(Value = "CS_ForgeGolem_Footstep1")]
            CS_ForgeGolem_Footstep1,
            [EnumMember(Value = "CS_ForgeGolem_Footstep2")]
            CS_ForgeGolem_Footstep2,
            [EnumMember(Value = "CS_ForgeGolem_Footstep3")]
            CS_ForgeGolem_Footstep3,
            [EnumMember(Value = "CS_ForgeGolem_Footstep4")]
            CS_ForgeGolem_Footstep4,
            [EnumMember(Value = "CS_ForgeGolem_Footstep5")]
            CS_ForgeGolem_Footstep5,
            [EnumMember(Value = "CS_ForgeGolem_Footstep6")]
            CS_ForgeGolem_Footstep6,
            [EnumMember(Value = "CS_ForgeGolem_Footstep7")]
            CS_ForgeGolem_Footstep7,
            [EnumMember(Value = "CS_ForgeGolem_Footstep8")]
            CS_ForgeGolem_Footstep8,
            [EnumMember(Value = "CS_ForgeGolem_HeavyHurt_Grunt1")]
            CS_ForgeGolem_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_ForgeGolem_HeavyHurt_Grunt2")]
            CS_ForgeGolem_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_ForgeGolem_HeavyHurt_Grunt3")]
            CS_ForgeGolem_HeavyHurt_Grunt3,
            [EnumMember(Value = "CS_ForgeGolem_HeavyHurt_Grunt4")]
            CS_ForgeGolem_HeavyHurt_Grunt4,
            [EnumMember(Value = "CS_ForgeGolem_HurtHit1")]
            CS_ForgeGolem_HurtHit1,
            [EnumMember(Value = "CS_ForgeGolem_HurtHit2")]
            CS_ForgeGolem_HurtHit2,
            [EnumMember(Value = "CS_ForgeGolem_HurtHit3")]
            CS_ForgeGolem_HurtHit3,
            [EnumMember(Value = "CS_ForgeGolem_Hurt_Grunt1")]
            CS_ForgeGolem_Hurt_Grunt1,
            [EnumMember(Value = "CS_ForgeGolem_Hurt_Grunt2")]
            CS_ForgeGolem_Hurt_Grunt2,
            [EnumMember(Value = "CS_ForgeGolem_Hurt_Grunt3")]
            CS_ForgeGolem_Hurt_Grunt3,
            [EnumMember(Value = "CS_ForgeGolem_Hurt_Grunt4")]
            CS_ForgeGolem_Hurt_Grunt4,
            [EnumMember(Value = "CS_ForgeGolem_Hurt_Grunt5")]
            CS_ForgeGolem_Hurt_Grunt5,
            [EnumMember(Value = "CS_ForgeGolem_Scream1")]
            CS_ForgeGolem_Scream1,
            [EnumMember(Value = "CS_ForgeGolem_Scream2")]
            CS_ForgeGolem_Scream2,
            [EnumMember(Value = "CS_ForgeGolem_Scream3")]
            CS_ForgeGolem_Scream3,
            [EnumMember(Value = "CS_ForgeGolem_SpitCall_Grunt1")]
            CS_ForgeGolem_SpitCall_Grunt1,
            [EnumMember(Value = "CS_ForgeGolem_SpitCall_Grunt2")]
            CS_ForgeGolem_SpitCall_Grunt2,
            [EnumMember(Value = "CS_ForgeGolem_SpitCall_Grunt3")]
            CS_ForgeGolem_SpitCall_Grunt3,
            [EnumMember(Value = "CS_ForgeGolem_Attack_Grount1")]
            CS_ForgeGolem_Attack_Grount1,
            [EnumMember(Value = "CS_ForgeGolem_Attack_Grount2")]
            CS_ForgeGolem_Attack_Grount2,
            [EnumMember(Value = "CS_ForgeGolem_Attack_Grount3")]
            CS_ForgeGolem_Attack_Grount3,
            [EnumMember(Value = "CS_ForgeGolem_SpitCall_Grunt4")]
            CS_ForgeGolem_SpitCall_Grunt4,
            [EnumMember(Value = "CS_ForgeGolem_Attack_Whoosh1")]
            CS_ForgeGolem_Attack_Whoosh1,
            [EnumMember(Value = "CS_ForgeGolem_Attack_Whoosh2")]
            CS_ForgeGolem_Attack_Whoosh2,
            [EnumMember(Value = "CS_ForgeGolem_Attack_Whoosh3")]
            CS_ForgeGolem_Attack_Whoosh3,
            [EnumMember(Value = "CS_ForgeGolem_Attack_Whoosh4")]
            CS_ForgeGolem_Attack_Whoosh4,
            [EnumMember(Value = "CS_Giant_Attack_Grunt0")]
            CS_Giant_Attack_Grunt0 = 50480,
            [EnumMember(Value = "CS_Giant_Footstep_Run1")]
            CS_Giant_Footstep_Run1,
            [EnumMember(Value = "CS_Giant_Footstep_Run2")]
            CS_Giant_Footstep_Run2,
            [EnumMember(Value = "CS_Giant_Footstep_Run3")]
            CS_Giant_Footstep_Run3,
            [EnumMember(Value = "CS_Giant_Footstep_Run4")]
            CS_Giant_Footstep_Run4,
            [EnumMember(Value = "CS_Giant_Footstep_Run5")]
            CS_Giant_Footstep_Run5,
            [EnumMember(Value = "CS_Giant_Footstep_Run6")]
            CS_Giant_Footstep_Run6,
            [EnumMember(Value = "CS_Giant_Footstep_Walk1")]
            CS_Giant_Footstep_Walk1,
            [EnumMember(Value = "CS_Giant_Footstep_Walk2")]
            CS_Giant_Footstep_Walk2,
            [EnumMember(Value = "CS_Giant_Footstep_Walk3")]
            CS_Giant_Footstep_Walk3,
            [EnumMember(Value = "CS_Giant_Footstep_Walk4")]
            CS_Giant_Footstep_Walk4,
            [EnumMember(Value = "CS_Giant_Footstep_Walk5")]
            CS_Giant_Footstep_Walk5,
            [EnumMember(Value = "CS_Giant_Footstep_Walk6")]
            CS_Giant_Footstep_Walk6,
            [EnumMember(Value = "CS_Giant_HeavyAttack_Whoosh1")]
            CS_Giant_HeavyAttack_Whoosh1,
            [EnumMember(Value = "CS_Giant_HeavyAttack_Whoosh2")]
            CS_Giant_HeavyAttack_Whoosh2,
            [EnumMember(Value = "CS_Giant_HeavyAttack_Whoosh3")]
            CS_Giant_HeavyAttack_Whoosh3,
            [EnumMember(Value = "CS_Giant_HeavyAttack_Whoosh4")]
            CS_Giant_HeavyAttack_Whoosh4,
            [EnumMember(Value = "CS_Giant_HeavyHurt_Grunt1")]
            CS_Giant_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_Giant_HeavyHurt_Grunt2")]
            CS_Giant_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_Giant_HeavyImpact_Ground1")]
            CS_Giant_HeavyImpact_Ground1,
            [EnumMember(Value = "CS_Giant_HeavyImpact_Ground2")]
            CS_Giant_HeavyImpact_Ground2,
            [EnumMember(Value = "CS_Giant_HeavyImpact_Ground3")]
            CS_Giant_HeavyImpact_Ground3,
            [EnumMember(Value = "CS_Giant_Hurt_Grunt1")]
            CS_Giant_Hurt_Grunt1,
            [EnumMember(Value = "CS_Giant_Hurt_Grunt2")]
            CS_Giant_Hurt_Grunt2,
            [EnumMember(Value = "CS_Giant_Hurt_Grunt3")]
            CS_Giant_Hurt_Grunt3,
            [EnumMember(Value = "CS_Giant_Hurt_Grunt4")]
            CS_Giant_Hurt_Grunt4,
            [EnumMember(Value = "CS_Giant_KnockDown1")]
            CS_Giant_KnockDown1,
            [EnumMember(Value = "CS_Giant_KnockDown2")]
            CS_Giant_KnockDown2,
            [EnumMember(Value = "CS_Giant_Attack_Grunt1")]
            CS_Giant_Attack_Grunt1,
            [EnumMember(Value = "CS_Giant_Attack_Grunt2")]
            CS_Giant_Attack_Grunt2,
            [EnumMember(Value = "CS_Giant_Attack_Grunt3")]
            CS_Giant_Attack_Grunt3,
            [EnumMember(Value = "CS_Giant_Attack_Grunt4")]
            CS_Giant_Attack_Grunt4,
            [EnumMember(Value = "CS_Giant_Attack_Grunt5")]
            CS_Giant_Attack_Grunt5,
            [EnumMember(Value = "CS_Giant_Attack_Whoosh1")]
            CS_Giant_Attack_Whoosh1,
            [EnumMember(Value = "CS_Giant_Attack_Whoosh2")]
            CS_Giant_Attack_Whoosh2,
            [EnumMember(Value = "CS_Giant_Attack_Whoosh3")]
            CS_Giant_Attack_Whoosh3,
            [EnumMember(Value = "CS_Giant_Attack_Whoosh4")]
            CS_Giant_Attack_Whoosh4,
            [EnumMember(Value = "CS_Giant_Attack_Whoosh5")]
            CS_Giant_Attack_Whoosh5,
            [EnumMember(Value = "CS_Giant_FastAttack_Grunt1")]
            CS_Giant_FastAttack_Grunt1,
            [EnumMember(Value = "CS_Giant_FastAttack_Grunt2")]
            CS_Giant_FastAttack_Grunt2,
            [EnumMember(Value = "CS_Giant_SpecialAttack1_Grunt1")]
            CS_Giant_SpecialAttack1_Grunt1,
            [EnumMember(Value = "CS_Giant_SpecialAttack1_Grunt2")]
            CS_Giant_SpecialAttack1_Grunt2,
            [EnumMember(Value = "CS_Giant_SpecialAttack1_Grunt3")]
            CS_Giant_SpecialAttack1_Grunt3,
            [EnumMember(Value = "CS_Giant_SpecialAttack2_Grunt1")]
            CS_Giant_SpecialAttack2_Grunt1,
            [EnumMember(Value = "CS_Giant_SpecialAttack2_Grunt2")]
            CS_Giant_SpecialAttack2_Grunt2,
            [EnumMember(Value = "CS_Giant_SpecialAttack2_Grunt3")]
            CS_Giant_SpecialAttack2_Grunt3,
            [EnumMember(Value = "CS_HiveMan_Attack_Grunt1")]
            CS_HiveMan_Attack_Grunt1 = 50530,
            [EnumMember(Value = "CS_HiveMan_Attack_Grunt2")]
            CS_HiveMan_Attack_Grunt2,
            [EnumMember(Value = "CS_HiveMan_Attack_Grunt3")]
            CS_HiveMan_Attack_Grunt3,
            [EnumMember(Value = "CS_HiveMan_Attack_Grunt4")]
            CS_HiveMan_Attack_Grunt4,
            [EnumMember(Value = "CS_HiveMan_Attack_Grunt5")]
            CS_HiveMan_Attack_Grunt5,
            [EnumMember(Value = "CS_HiveMan_Footstep1")]
            CS_HiveMan_Footstep1,
            [EnumMember(Value = "CS_HiveMan_Footstep2")]
            CS_HiveMan_Footstep2,
            [EnumMember(Value = "CS_HiveMan_Footstep3")]
            CS_HiveMan_Footstep3,
            [EnumMember(Value = "CS_HiveMan_Footstep4")]
            CS_HiveMan_Footstep4,
            [EnumMember(Value = "CS_HiveMan_Footstep5")]
            CS_HiveMan_Footstep5,
            [EnumMember(Value = "CS_HiveMan_Footstep6")]
            CS_HiveMan_Footstep6,
            [EnumMember(Value = "CS_HiveMan_HeavyAttack_Whoosh1")]
            CS_HiveMan_HeavyAttack_Whoosh1,
            [EnumMember(Value = "CS_HiveMan_HeavyAttack_Whoosh2")]
            CS_HiveMan_HeavyAttack_Whoosh2,
            [EnumMember(Value = "CS_HiveMan_HeavyAttack_Whoosh3")]
            CS_HiveMan_HeavyAttack_Whoosh3,
            [EnumMember(Value = "CS_HiveMan_HeavyHurt_Grunt1")]
            CS_HiveMan_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_HiveMan_HeavyHurt_Grunt2")]
            CS_HiveMan_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_HiveMan_HeavyHurt_Grunt3")]
            CS_HiveMan_HeavyHurt_Grunt3,
            [EnumMember(Value = "CS_HiveMan_Hurt_Grunt1")]
            CS_HiveMan_Hurt_Grunt1,
            [EnumMember(Value = "CS_HiveMan_Hurt_Grunt2")]
            CS_HiveMan_Hurt_Grunt2,
            [EnumMember(Value = "CS_HiveMan_Hurt_Grunt3")]
            CS_HiveMan_Hurt_Grunt3,
            [EnumMember(Value = "CS_HiveMan_HurtHit1")]
            CS_HiveMan_HurtHit1,
            [EnumMember(Value = "CS_HiveMan_HurtHit2")]
            CS_HiveMan_HurtHit2,
            [EnumMember(Value = "CS_HiveMan_HurtHit3")]
            CS_HiveMan_HurtHit3,
            [EnumMember(Value = "CS_HiveMan_HurtHit4")]
            CS_HiveMan_HurtHit4,
            [EnumMember(Value = "CS_HiveMan_KnockDown1")]
            CS_HiveMan_KnockDown1,
            [EnumMember(Value = "CS_HiveMan_KnockDown2")]
            CS_HiveMan_KnockDown2,
            [EnumMember(Value = "CS_HiveMan_KnockDown3")]
            CS_HiveMan_KnockDown3,
            [EnumMember(Value = "CS_HiveMan_MultipleAttack_Whoosh1")]
            CS_HiveMan_MultipleAttack_Whoosh1,
            [EnumMember(Value = "CS_HiveMan_MultipleAttack_Whoosh2")]
            CS_HiveMan_MultipleAttack_Whoosh2,
            [EnumMember(Value = "CS_HiveMan_MultipleAttack_Whoosh3")]
            CS_HiveMan_MultipleAttack_Whoosh3,
            [EnumMember(Value = "CS_HiveMan_AttackHitGround1")]
            CS_HiveMan_AttackHitGround1,
            [EnumMember(Value = "CS_HiveMan_AttackHitGround2")]
            CS_HiveMan_AttackHitGround2,
            [EnumMember(Value = "CS_HiveMan_AttackSnatch1")]
            CS_HiveMan_AttackSnatch1,
            [EnumMember(Value = "CS_HiveMan_AttackSnatch2")]
            CS_HiveMan_AttackSnatch2,
            [EnumMember(Value = "CS_HiveMan_Attack_HornetSwarm_loop")]
            CS_HiveMan_Attack_HornetSwarm_loop,
            [EnumMember(Value = "CS_Illuminator_Attack_Grunt1")]
            CS_Illuminator_Attack_Grunt1 = 50570,
            [EnumMember(Value = "CS_Illuminator_Attack_Grunt2")]
            CS_Illuminator_Attack_Grunt2,
            [EnumMember(Value = "CS_Illuminator_Attack_Grunt3")]
            CS_Illuminator_Attack_Grunt3,
            [EnumMember(Value = "CS_Illuminator_Attack_Whoosh1")]
            CS_Illuminator_Attack_Whoosh1,
            [EnumMember(Value = "CS_Illuminator_Attack_Whoosh2")]
            CS_Illuminator_Attack_Whoosh2,
            [EnumMember(Value = "CS_Illuminator_Attack_Whoosh3")]
            CS_Illuminator_Attack_Whoosh3,
            [EnumMember(Value = "CS_Illuminator_Footstep1")]
            CS_Illuminator_Footstep1,
            [EnumMember(Value = "CS_Illuminator_Footstep2")]
            CS_Illuminator_Footstep2,
            [EnumMember(Value = "CS_Illuminator_Footstep3")]
            CS_Illuminator_Footstep3,
            [EnumMember(Value = "CS_Illuminator_Footstep4")]
            CS_Illuminator_Footstep4,
            [EnumMember(Value = "CS_Illuminator_Footstep5")]
            CS_Illuminator_Footstep5,
            [EnumMember(Value = "CS_Illuminator_Footstep6")]
            CS_Illuminator_Footstep6,
            [EnumMember(Value = "CS_Manticore_Attack_Whoosh1")]
            CS_Manticore_Attack_Whoosh1 = 50590,
            [EnumMember(Value = "CS_Manticore_Attack_Whoosh2")]
            CS_Manticore_Attack_Whoosh2,
            [EnumMember(Value = "CS_Manticore_Attack_Whoosh3")]
            CS_Manticore_Attack_Whoosh3,
            [EnumMember(Value = "CS_Manticore_BiteAttack_Grunt1")]
            CS_Manticore_BiteAttack_Grunt1,
            [EnumMember(Value = "CS_Manticore_BiteAttack_Grunt2")]
            CS_Manticore_BiteAttack_Grunt2,
            [EnumMember(Value = "CS_Manticore_BiteAttack_Grunt3")]
            CS_Manticore_BiteAttack_Grunt3,
            [EnumMember(Value = "CS_Manticore_BiteAttack_Grunt4")]
            CS_Manticore_BiteAttack_Grunt4,
            [EnumMember(Value = "CS_Manticore_Dodge_Grunt1")]
            CS_Manticore_Dodge_Grunt1,
            [EnumMember(Value = "CS_Manticore_Dodge_Grunt2")]
            CS_Manticore_Dodge_Grunt2,
            [EnumMember(Value = "CS_Manticore_Dodge_Grunt3")]
            CS_Manticore_Dodge_Grunt3,
            [EnumMember(Value = "CS_Manticore_Dodge_Grunt4")]
            CS_Manticore_Dodge_Grunt4,
            [EnumMember(Value = "CS_Manticore_Footstep1")]
            CS_Manticore_Footstep1,
            [EnumMember(Value = "CS_Manticore_Footstep2")]
            CS_Manticore_Footstep2,
            [EnumMember(Value = "CS_Manticore_Footstep3")]
            CS_Manticore_Footstep3,
            [EnumMember(Value = "CS_Manticore_Footstep4")]
            CS_Manticore_Footstep4,
            [EnumMember(Value = "CS_Manticore_Footstep5")]
            CS_Manticore_Footstep5,
            [EnumMember(Value = "CS_Manticore_Footstep6")]
            CS_Manticore_Footstep6,
            [EnumMember(Value = "CS_Manticore_HeavyAttack_Grunt1")]
            CS_Manticore_HeavyAttack_Grunt1,
            [EnumMember(Value = "CS_Manticore_HeavyAttack_Grunt2")]
            CS_Manticore_HeavyAttack_Grunt2,
            [EnumMember(Value = "CS_Manticore_HeavyAttack_Grunt3")]
            CS_Manticore_HeavyAttack_Grunt3,
            [EnumMember(Value = "CS_Manticore_HeavyAttack_Whoosh1")]
            CS_Manticore_HeavyAttack_Whoosh1,
            [EnumMember(Value = "CS_Manticore_HeavyAttack_Whoosh2")]
            CS_Manticore_HeavyAttack_Whoosh2,
            [EnumMember(Value = "CS_Manticore_HeavyAttack_Whoosh3")]
            CS_Manticore_HeavyAttack_Whoosh3,
            [EnumMember(Value = "CS_Manticore_HeavyAttack_Whoosh4")]
            CS_Manticore_HeavyAttack_Whoosh4,
            [EnumMember(Value = "CS_Manticore_HeavyAttack_Whoosh5")]
            CS_Manticore_HeavyAttack_Whoosh5,
            [EnumMember(Value = "CS_Manticore_HeavyAttack_Whoosh6")]
            CS_Manticore_HeavyAttack_Whoosh6,
            [EnumMember(Value = "CS_Manticore_HeavyHurt_Grunt1")]
            CS_Manticore_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_Manticore_HeavyHurt_Grunt2")]
            CS_Manticore_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_Manticore_HeavyHurt_Grunt3")]
            CS_Manticore_HeavyHurt_Grunt3,
            [EnumMember(Value = "CS_Manticore_HeavyHurt_Grunt4")]
            CS_Manticore_HeavyHurt_Grunt4,
            [EnumMember(Value = "CS_Manticore_HornAttack_Grunt1")]
            CS_Manticore_HornAttack_Grunt1,
            [EnumMember(Value = "CS_Manticore_HornAttack_Grunt2")]
            CS_Manticore_HornAttack_Grunt2,
            [EnumMember(Value = "CS_Manticore_HornAttack_Grunt3")]
            CS_Manticore_HornAttack_Grunt3,
            [EnumMember(Value = "CS_Manticore_HornAttack_Grunt4")]
            CS_Manticore_HornAttack_Grunt4,
            [EnumMember(Value = "CS_Manticore_HornAttack_Grunt5")]
            CS_Manticore_HornAttack_Grunt5,
            [EnumMember(Value = "CS_Manticore_HornAttack_Grunt6")]
            CS_Manticore_HornAttack_Grunt6,
            [EnumMember(Value = "CS_Manticore_HornAttack_Whoosh1")]
            CS_Manticore_HornAttack_Whoosh1,
            [EnumMember(Value = "CS_Manticore_HornAttack_Whoosh2")]
            CS_Manticore_HornAttack_Whoosh2,
            [EnumMember(Value = "CS_Manticore_HornAttack_Whoosh3")]
            CS_Manticore_HornAttack_Whoosh3,
            [EnumMember(Value = "CS_Manticore_HornAttack_Whoosh4")]
            CS_Manticore_HornAttack_Whoosh4,
            [EnumMember(Value = "CS_Manticore_Hurt_Grunt1")]
            CS_Manticore_Hurt_Grunt1,
            [EnumMember(Value = "CS_Manticore_Hurt_Grunt2")]
            CS_Manticore_Hurt_Grunt2,
            [EnumMember(Value = "CS_Manticore_Hurt_Grunt3")]
            CS_Manticore_Hurt_Grunt3,
            [EnumMember(Value = "CS_Manticore_Hurt_Grunt4")]
            CS_Manticore_Hurt_Grunt4,
            [EnumMember(Value = "CS_Manticore_Idle_Growl1")]
            CS_Manticore_Idle_Growl1,
            [EnumMember(Value = "CS_Manticore_Idle_Growl2")]
            CS_Manticore_Idle_Growl2,
            [EnumMember(Value = "CS_Manticore_Idle_Growl3")]
            CS_Manticore_Idle_Growl3,
            [EnumMember(Value = "CS_Manticore_Land_Footstep1")]
            CS_Manticore_Land_Footstep1,
            [EnumMember(Value = "CS_Manticore_Land_Footstep2")]
            CS_Manticore_Land_Footstep2,
            [EnumMember(Value = "CS_Manticore_Land_Footstep3")]
            CS_Manticore_Land_Footstep3,
            [EnumMember(Value = "CS_Manticore_Land_Footstep4")]
            CS_Manticore_Land_Footstep4,
            [EnumMember(Value = "CS_Manticore_Attack_Grunt1")]
            CS_Manticore_Attack_Grunt1,
            [EnumMember(Value = "CS_Manticore_Attack_Grunt2")]
            CS_Manticore_Attack_Grunt2,
            [EnumMember(Value = "CS_Manticore_Attack_Grunt3")]
            CS_Manticore_Attack_Grunt3,
            [EnumMember(Value = "CS_Manticore_TailProjectils_Shot1")]
            CS_Manticore_TailProjectils_Shot1,
            [EnumMember(Value = "CS_Manticore_TailProjectils_Shot2")]
            CS_Manticore_TailProjectils_Shot2,
            [EnumMember(Value = "CS_Manticore_TailProjectils_Shot3")]
            CS_Manticore_TailProjectils_Shot3,
            [EnumMember(Value = "CS_Manticore_TailProjectils_Shot4")]
            CS_Manticore_TailProjectils_Shot4,
            [EnumMember(Value = "CS_Manticore_TailProjectils_Shot5")]
            CS_Manticore_TailProjectils_Shot5,
            [EnumMember(Value = "CS_Manticore_TailProjectils_Shot6")]
            CS_Manticore_TailProjectils_Shot6,
            [EnumMember(Value = "CS_Mantis_Attack_Grunt1")]
            CS_Mantis_Attack_Grunt1,
            [EnumMember(Value = "CS_Mantis_Attack_Grunt2")]
            CS_Mantis_Attack_Grunt2,
            [EnumMember(Value = "CS_Mantis_Attack_Grunt3")]
            CS_Mantis_Attack_Grunt3,
            [EnumMember(Value = "CS_Mantis_Attack_Grunt4")]
            CS_Mantis_Attack_Grunt4,
            [EnumMember(Value = "CS_Mantis_BodyFall1")]
            CS_Mantis_BodyFall1,
            [EnumMember(Value = "CS_Mantis_BodyFall2")]
            CS_Mantis_BodyFall2,
            [EnumMember(Value = "CS_Mantis_Footstep1")]
            CS_Mantis_Footstep1,
            [EnumMember(Value = "CS_Mantis_Footstep2")]
            CS_Mantis_Footstep2,
            [EnumMember(Value = "CS_Mantis_Footstep3")]
            CS_Mantis_Footstep3,
            [EnumMember(Value = "CS_Mantis_Footstep4")]
            CS_Mantis_Footstep4,
            [EnumMember(Value = "CS_Mantis_Footstep5")]
            CS_Mantis_Footstep5,
            [EnumMember(Value = "CS_Mantis_Footstep6")]
            CS_Mantis_Footstep6,
            [EnumMember(Value = "CS_Mantis_Footstep7")]
            CS_Mantis_Footstep7,
            [EnumMember(Value = "CS_Mantis_Footstep8")]
            CS_Mantis_Footstep8,
            [EnumMember(Value = "CS_Mantis_HeavyHit_Hurt1")]
            CS_Mantis_HeavyHit_Hurt1,
            [EnumMember(Value = "CS_Mantis_HeavyHit_Hurt2")]
            CS_Mantis_HeavyHit_Hurt2,
            [EnumMember(Value = "CS_Mantis_HeavyHit_Hurt3")]
            CS_Mantis_HeavyHit_Hurt3,
            [EnumMember(Value = "CS_Mantis_Hurt_Scream1")]
            CS_Mantis_Hurt_Scream1,
            [EnumMember(Value = "CS_Mantis_Hurt_Scream2")]
            CS_Mantis_Hurt_Scream2,
            [EnumMember(Value = "CS_Mantis_Hurt_Scream3")]
            CS_Mantis_Hurt_Scream3,
            [EnumMember(Value = "CS_Mantis_Hurt_Scream4")]
            CS_Mantis_Hurt_Scream4,
            [EnumMember(Value = "CS_Mantis_Hit_Hurt1")]
            CS_Mantis_Hit_Hurt1,
            [EnumMember(Value = "CS_Mantis_Hit_Hurt2")]
            CS_Mantis_Hit_Hurt2,
            [EnumMember(Value = "CS_Mantis_Hit_Hurt3")]
            CS_Mantis_Hit_Hurt3,
            [EnumMember(Value = "CS_Mantis_Hit_Hurt4")]
            CS_Mantis_Hit_Hurt4,
            [EnumMember(Value = "CS_Mantis_KnockDown1")]
            CS_Mantis_KnockDown1,
            [EnumMember(Value = "CS_Mantis_KnockDown2")]
            CS_Mantis_KnockDown2,
            [EnumMember(Value = "CS_Mantis_KnockDown3")]
            CS_Mantis_KnockDown3,
            [EnumMember(Value = "CS_Mantis_ManaBlast_Grunt1")]
            CS_Mantis_ManaBlast_Grunt1,
            [EnumMember(Value = "CS_Mantis_ManaBlast_Grunt2")]
            CS_Mantis_ManaBlast_Grunt2,
            [EnumMember(Value = "CS_Mantis_ManaBlast_Grunt3")]
            CS_Mantis_ManaBlast_Grunt3,
            [EnumMember(Value = "CS_Mantis_ManaBlast_Whoosh1")]
            CS_Mantis_ManaBlast_Whoosh1,
            [EnumMember(Value = "CS_Mantis_ManaBlast_Whoosh2")]
            CS_Mantis_ManaBlast_Whoosh2,
            [EnumMember(Value = "CS_Mantis_ManaBlast_Whoosh3")]
            CS_Mantis_ManaBlast_Whoosh3,
            [EnumMember(Value = "CS_Mantis_ManaBlast_Whoosh4")]
            CS_Mantis_ManaBlast_Whoosh4,
            [EnumMember(Value = "CS_Mantis_RunAttack_Grunt1")]
            CS_Mantis_RunAttack_Grunt1,
            [EnumMember(Value = "CS_Mantis_RunAttack_Grunt2")]
            CS_Mantis_RunAttack_Grunt2,
            [EnumMember(Value = "CS_Mantis_RunAttack_Grunt3")]
            CS_Mantis_RunAttack_Grunt3,
            [EnumMember(Value = "CS_Mantis_SpecialAttack_Grunt1")]
            CS_Mantis_SpecialAttack_Grunt1,
            [EnumMember(Value = "CS_Mantis_SpecialAttack_Grunt2")]
            CS_Mantis_SpecialAttack_Grunt2,
            [EnumMember(Value = "CS_Mantis_SpecialAttack_Grunt3")]
            CS_Mantis_SpecialAttack_Grunt3,
            [EnumMember(Value = "CS_ObsidienElmt_BodyFall1")]
            CS_ObsidienElmt_BodyFall1 = 50700,
            [EnumMember(Value = "CS_ObsidienElmt_BodyFall2")]
            CS_ObsidienElmt_BodyFall2,
            [EnumMember(Value = "CS_ObsidienElmt_BodyFall3")]
            CS_ObsidienElmt_BodyFall3,
            [EnumMember(Value = "CS_ObsidienElmt_Footstep1")]
            CS_ObsidienElmt_Footstep1,
            [EnumMember(Value = "CS_ObsidienElmt_Footstep2")]
            CS_ObsidienElmt_Footstep2,
            [EnumMember(Value = "CS_ObsidienElmt_Footstep3")]
            CS_ObsidienElmt_Footstep3,
            [EnumMember(Value = "CS_ObsidienElmt_Footstep4")]
            CS_ObsidienElmt_Footstep4,
            [EnumMember(Value = "CS_ObsidienElmt_Footstep5")]
            CS_ObsidienElmt_Footstep5,
            [EnumMember(Value = "CS_ObsidienElmt_Footstep6")]
            CS_ObsidienElmt_Footstep6,
            [EnumMember(Value = "CS_ObsidienElmt_HeavyHurt_Grunt1")]
            CS_ObsidienElmt_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_ObsidienElmt_HeavyHurt_Grunt2")]
            CS_ObsidienElmt_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_ObsidienElmt_HeavyHurt_Grunt3")]
            CS_ObsidienElmt_HeavyHurt_Grunt3,
            [EnumMember(Value = "CS_ObsidienElmt_Hurt_Grunt1")]
            CS_ObsidienElmt_Hurt_Grunt1,
            [EnumMember(Value = "CS_ObsidienElmt_Hurt_Grunt2")]
            CS_ObsidienElmt_Hurt_Grunt2,
            [EnumMember(Value = "CS_ObsidienElmt_Hurt_Grunt3")]
            CS_ObsidienElmt_Hurt_Grunt3,
            [EnumMember(Value = "CS_ObsidienElmt_HurtHit1")]
            CS_ObsidienElmt_HurtHit1,
            [EnumMember(Value = "CS_ObsidienElmt_HurtHit2")]
            CS_ObsidienElmt_HurtHit2,
            [EnumMember(Value = "CS_ObsidienElmt_HurtHit3")]
            CS_ObsidienElmt_HurtHit3,
            [EnumMember(Value = "CS_ObsidienElmt_Attack_Grunt1")]
            CS_ObsidienElmt_Attack_Grunt1,
            [EnumMember(Value = "CS_ObsidienElmt_Attack_Grunt2")]
            CS_ObsidienElmt_Attack_Grunt2,
            [EnumMember(Value = "CS_ObsidienElmt_Attack_Grunt3")]
            CS_ObsidienElmt_Attack_Grunt3,
            [EnumMember(Value = "CS_ObsidienElmt_Attack_Whoosh1")]
            CS_ObsidienElmt_Attack_Whoosh1,
            [EnumMember(Value = "CS_ObsidienElmt_Attack_Whoosh2")]
            CS_ObsidienElmt_Attack_Whoosh2,
            [EnumMember(Value = "CS_ObsidienElmt_Attack_Whoosh3")]
            CS_ObsidienElmt_Attack_Whoosh3,
            [EnumMember(Value = "CS_ObsidienElmt_RunBack_In")]
            CS_ObsidienElmt_RunBack_In = 50725,
            [EnumMember(Value = "CS_ObsidienElmt_RunBack_Loop")]
            CS_ObsidienElmt_RunBack_Loop,
            [EnumMember(Value = "CS_ObsidienElmt_RunBack_Out")]
            CS_ObsidienElmt_RunBack_Out,
            [EnumMember(Value = "CS_ObsidienElmt_SpecialAttack_Grunt1")]
            CS_ObsidienElmt_SpecialAttack_Grunt1,
            [EnumMember(Value = "CS_ObsidienElmt_SpecialAttack_Grunt2")]
            CS_ObsidienElmt_SpecialAttack_Grunt2,
            [EnumMember(Value = "CS_ObsidienElmt_SpecialAttack_Grunt3")]
            CS_ObsidienElmt_SpecialAttack_Grunt3,
            [EnumMember(Value = "CS_ObsidienElmt_SpecialAttack_Whoosh1")]
            CS_ObsidienElmt_SpecialAttack_Whoosh1,
            [EnumMember(Value = "CS_ObsidienElmt_SpecialAttack_Whoosh2")]
            CS_ObsidienElmt_SpecialAttack_Whoosh2,
            [EnumMember(Value = "CS_ObsidienElmt_SpecialAttack_Whoosh3")]
            CS_ObsidienElmt_SpecialAttack_Whoosh3,
            [EnumMember(Value = "CS_ObsidienElmt_Sprint_In")]
            CS_ObsidienElmt_Sprint_In,
            [EnumMember(Value = "CS_ObsidienElmt_Sprint_Loop")]
            CS_ObsidienElmt_Sprint_Loop,
            [EnumMember(Value = "CS_ObsidienElmt_Sprint_Out")]
            CS_ObsidienElmt_Sprint_Out,
            [EnumMember(Value = "CS_ShellHorror_Attack_Grunt1")]
            CS_ShellHorror_Attack_Grunt1 = 50740,
            [EnumMember(Value = "CS_ShellHorror_Attack_Grunt2")]
            CS_ShellHorror_Attack_Grunt2,
            [EnumMember(Value = "CS_ShellHorror_Attack_Grunt3")]
            CS_ShellHorror_Attack_Grunt3,
            [EnumMember(Value = "CS_ShellHorror_Attack_Grunt4")]
            CS_ShellHorror_Attack_Grunt4,
            [EnumMember(Value = "CS_ShellHorror_Attack_Grunt5")]
            CS_ShellHorror_Attack_Grunt5,
            [EnumMember(Value = "CS_ShellHorror_Attack_Grunt6")]
            CS_ShellHorror_Attack_Grunt6,
            [EnumMember(Value = "CS_ShellHorror_HeavyAttack_Grunt1")]
            CS_ShellHorror_HeavyAttack_Grunt1,
            [EnumMember(Value = "CS_ShellHorror_HeavyAttack_Grunt2")]
            CS_ShellHorror_HeavyAttack_Grunt2,
            [EnumMember(Value = "CS_ShellHorror_HeavyAttack_Grunt3")]
            CS_ShellHorror_HeavyAttack_Grunt3,
            [EnumMember(Value = "CS_ShellHorror_Bodyfall1")]
            CS_ShellHorror_Bodyfall1,
            [EnumMember(Value = "CS_ShellHorror_Bodyfall2")]
            CS_ShellHorror_Bodyfall2,
            [EnumMember(Value = "CS_ShellHorror_Footstep1")]
            CS_ShellHorror_Footstep1,
            [EnumMember(Value = "CS_ShellHorror_Footstep2")]
            CS_ShellHorror_Footstep2,
            [EnumMember(Value = "CS_ShellHorror_Footstep3")]
            CS_ShellHorror_Footstep3,
            [EnumMember(Value = "CS_ShellHorror_Footstep4")]
            CS_ShellHorror_Footstep4,
            [EnumMember(Value = "CS_ShellHorror_Footstep5")]
            CS_ShellHorror_Footstep5,
            [EnumMember(Value = "CS_ShellHorror_Footstep6")]
            CS_ShellHorror_Footstep6,
            [EnumMember(Value = "CS_ShellHorror_Walk_Footstep1")]
            CS_ShellHorror_Walk_Footstep1,
            [EnumMember(Value = "CS_ShellHorror_Walk_Footstep2")]
            CS_ShellHorror_Walk_Footstep2,
            [EnumMember(Value = "CS_ShellHorror_Walk_Footstep3")]
            CS_ShellHorror_Walk_Footstep3,
            [EnumMember(Value = "CS_ShellHorror_Walk_Footstep4")]
            CS_ShellHorror_Walk_Footstep4,
            [EnumMember(Value = "CS_ShellHorror_Walk_Footstep5")]
            CS_ShellHorror_Walk_Footstep5,
            [EnumMember(Value = "CS_ShellHorror_Walk_Footstep6")]
            CS_ShellHorror_Walk_Footstep6,
            [EnumMember(Value = "CS_ShellHorror_HeavyAttack_Whoosh1")]
            CS_ShellHorror_HeavyAttack_Whoosh1,
            [EnumMember(Value = "CS_ShellHorror_HeavyAttack_Whoosh2")]
            CS_ShellHorror_HeavyAttack_Whoosh2,
            [EnumMember(Value = "CS_ShellHorror_HeavyAttack_Whoosh3")]
            CS_ShellHorror_HeavyAttack_Whoosh3,
            [EnumMember(Value = "CS_ShellHorror_HitGround1")]
            CS_ShellHorror_HitGround1,
            [EnumMember(Value = "CS_ShellHorror_HitGround2")]
            CS_ShellHorror_HitGround2,
            [EnumMember(Value = "CS_ShellHorror_HitGround3")]
            CS_ShellHorror_HitGround3,
            [EnumMember(Value = "CS_ShellHorror_HitGround4")]
            CS_ShellHorror_HitGround4,
            [EnumMember(Value = "CS_ShellHorror_HeavyHurt_Grunt1")]
            CS_ShellHorror_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_ShellHorror_HeavyHurt_Grunt2")]
            CS_ShellHorror_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_ShellHorror_HurtHit1")]
            CS_ShellHorror_HurtHit1,
            [EnumMember(Value = "CS_ShellHorror_HurtHit2")]
            CS_ShellHorror_HurtHit2,
            [EnumMember(Value = "CS_ShellHorror_HurtHit3")]
            CS_ShellHorror_HurtHit3,
            [EnumMember(Value = "CS_ShellHorror_HurtHit4")]
            CS_ShellHorror_HurtHit4,
            [EnumMember(Value = "CS_ShellHorror_HurtHit_Grunt1")]
            CS_ShellHorror_HurtHit_Grunt1,
            [EnumMember(Value = "CS_ShellHorror_HurtHit_Grunt2")]
            CS_ShellHorror_HurtHit_Grunt2,
            [EnumMember(Value = "CS_ShellHorror_HurtHit_Grunt3")]
            CS_ShellHorror_HurtHit_Grunt3,
            [EnumMember(Value = "CS_ShellHorror_HurtHit_Grunt4")]
            CS_ShellHorror_HurtHit_Grunt4,
            [EnumMember(Value = "CS_ShellHorror_RunAttack_Whoosh1")]
            CS_ShellHorror_RunAttack_Whoosh1,
            [EnumMember(Value = "CS_ShellHorror_RunAttack_Whoosh2")]
            CS_ShellHorror_RunAttack_Whoosh2,
            [EnumMember(Value = "CS_ShellHorror_RunAttack_Whoosh3")]
            CS_ShellHorror_RunAttack_Whoosh3,
            [EnumMember(Value = "CS_ShellHorror_Spell_Grunt1")]
            CS_ShellHorror_Spell_Grunt1,
            [EnumMember(Value = "CS_ShellHorror_Spell_Grunt2")]
            CS_ShellHorror_Spell_Grunt2,
            [EnumMember(Value = "CS_ShellHorror_Spell_Grunt3")]
            CS_ShellHorror_Spell_Grunt3,
            [EnumMember(Value = "CS_ShellHorror_Spell_Grunt4")]
            CS_ShellHorror_Spell_Grunt4,
            [EnumMember(Value = "CS_SpecterMelee_SpecialAttackA_Whoosh1")]
            CS_SpecterMelee_SpecialAttackA_Whoosh1 = 50790,
            [EnumMember(Value = "CS_SpecterMelee_SpecialAttackA_Whoosh2")]
            CS_SpecterMelee_SpecialAttackA_Whoosh2,
            [EnumMember(Value = "CS_SpecterMelee_SpecialAttackB_Whoosh1")]
            CS_SpecterMelee_SpecialAttackB_Whoosh1,
            [EnumMember(Value = "CS_SpecterMelee_SpecialAttackB_Whoosh2")]
            CS_SpecterMelee_SpecialAttackB_Whoosh2,
            [EnumMember(Value = "CS_SpecterMelee_SpecialAttackB_Whoosh3")]
            CS_SpecterMelee_SpecialAttackB_Whoosh3,
            [EnumMember(Value = "CS_SpecterMelee_Bodyfall1")]
            CS_SpecterMelee_Bodyfall1,
            [EnumMember(Value = "CS_SpecterMelee_Bodyfall2")]
            CS_SpecterMelee_Bodyfall2,
            [EnumMember(Value = "CS_SpecterMelee_Bodyfall3")]
            CS_SpecterMelee_Bodyfall3,
            [EnumMember(Value = "CS_SpecterMelee_HitHurt1")]
            CS_SpecterMelee_HitHurt1,
            [EnumMember(Value = "CS_SpecterMelee_HitHurt2")]
            CS_SpecterMelee_HitHurt2,
            [EnumMember(Value = "CS_SpecterMelee_HitHurt3")]
            CS_SpecterMelee_HitHurt3,
            [EnumMember(Value = "CS_SpecterMelee_Footstep1")]
            CS_SpecterMelee_Footstep1,
            [EnumMember(Value = "CS_SpecterMelee_Footstep2")]
            CS_SpecterMelee_Footstep2,
            [EnumMember(Value = "CS_SpecterMelee_Footstep3")]
            CS_SpecterMelee_Footstep3,
            [EnumMember(Value = "CS_SpecterMelee_Footstep4")]
            CS_SpecterMelee_Footstep4,
            [EnumMember(Value = "CS_SpecterMelee_Footstep5")]
            CS_SpecterMelee_Footstep5,
            [EnumMember(Value = "CS_SpecterMelee_Footstep6")]
            CS_SpecterMelee_Footstep6,
            [EnumMember(Value = "CS_SpecterMelee_Attack_Grunt1")]
            CS_SpecterMelee_Attack_Grunt1,
            [EnumMember(Value = "CS_SpecterMelee_Attack_Grunt2")]
            CS_SpecterMelee_Attack_Grunt2,
            [EnumMember(Value = "CS_SpecterMelee_Attack_Grunt3")]
            CS_SpecterMelee_Attack_Grunt3,
            [EnumMember(Value = "CS_SpecterMelee_HeavyHurt_Grunt1")]
            CS_SpecterMelee_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_SpecterMelee_HeavyHurt_Grunt2")]
            CS_SpecterMelee_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_SpecterMelee_HeavyHurt_Grunt3")]
            CS_SpecterMelee_HeavyHurt_Grunt3,
            [EnumMember(Value = "CS_SpecterMelee_HurtHit_Grunt1")]
            CS_SpecterMelee_HurtHit_Grunt1,
            [EnumMember(Value = "CS_SpecterMelee_HurtHit_Grunt2")]
            CS_SpecterMelee_HurtHit_Grunt2,
            [EnumMember(Value = "CS_SpecterMelee_HurtHit_Grunt3")]
            CS_SpecterMelee_HurtHit_Grunt3,
            [EnumMember(Value = "CS_SpecterMelee_Attack_Whoosh1")]
            CS_SpecterMelee_Attack_Whoosh1,
            [EnumMember(Value = "CS_SpecterMelee_Attack_Whoosh2")]
            CS_SpecterMelee_Attack_Whoosh2,
            [EnumMember(Value = "CS_SpecterMelee_Attack_Whoosh3")]
            CS_SpecterMelee_Attack_Whoosh3,
            [EnumMember(Value = "CS_SpecterMelee_SpecialAttackA_Grunt1")]
            CS_SpecterMelee_SpecialAttackA_Grunt1,
            [EnumMember(Value = "CS_SpecterMelee_SpecialAttackA_Grunt2")]
            CS_SpecterMelee_SpecialAttackA_Grunt2,
            [EnumMember(Value = "CS_SpecterMelee_SpecialAttackB_Grunt1")]
            CS_SpecterMelee_SpecialAttackB_Grunt1,
            [EnumMember(Value = "CS_SpecterMelee_SpecialAttackB_Grunt2")]
            CS_SpecterMelee_SpecialAttackB_Grunt2,
            [EnumMember(Value = "CS_SpecterMelee_SpecialAttackB_Grunt3")]
            CS_SpecterMelee_SpecialAttackB_Grunt3,
            [EnumMember(Value = "CS_BeastGolem_Attack3_Grunt1")]
            CS_BeastGolem_Attack3_Grunt1 = 50830,
            [EnumMember(Value = "CS_BeastGolem_Attack3_Grunt2")]
            CS_BeastGolem_Attack3_Grunt2,
            [EnumMember(Value = "CS_BeastGolem_Attack3_Grunt3")]
            CS_BeastGolem_Attack3_Grunt3,
            [EnumMember(Value = "CS_BeastGolem_Attack3_Whoosh1")]
            CS_BeastGolem_Attack3_Whoosh1,
            [EnumMember(Value = "CS_BeastGolem_Attack3_Whoosh2")]
            CS_BeastGolem_Attack3_Whoosh2,
            [EnumMember(Value = "CS_BeastGolem_Attack3_whoosh3")]
            CS_BeastGolem_Attack3_whoosh3,
            [EnumMember(Value = "CS_BeastGolem_Attack_Grunt1")]
            CS_BeastGolem_Attack_Grunt1,
            [EnumMember(Value = "CS_BeastGolem_Attack_Grunt2")]
            CS_BeastGolem_Attack_Grunt2,
            [EnumMember(Value = "CS_BeastGolem_Attack_Grunt3")]
            CS_BeastGolem_Attack_Grunt3,
            [EnumMember(Value = "CS_BeastGolem_Bodyfall1")]
            CS_BeastGolem_Bodyfall1,
            [EnumMember(Value = "CS_BeastGolem_Bodyfall2")]
            CS_BeastGolem_Bodyfall2,
            [EnumMember(Value = "CS_BeastGolem_Bodyfall3")]
            CS_BeastGolem_Bodyfall3,
            [EnumMember(Value = "CS_BeastGolem_FootStep1")]
            CS_BeastGolem_FootStep1,
            [EnumMember(Value = "CS_BeastGolem_FootStep2")]
            CS_BeastGolem_FootStep2,
            [EnumMember(Value = "CS_BeastGolem_FootStep3")]
            CS_BeastGolem_FootStep3,
            [EnumMember(Value = "CS_BeastGolem_FootStep4")]
            CS_BeastGolem_FootStep4,
            [EnumMember(Value = "CS_BeastGolem_FootStep5")]
            CS_BeastGolem_FootStep5,
            [EnumMember(Value = "CS_BeastGolem_FootStep6")]
            CS_BeastGolem_FootStep6,
            [EnumMember(Value = "CS_BeastGolem_KnockBackHurt_Grunt1")]
            CS_BeastGolem_KnockBackHurt_Grunt1,
            [EnumMember(Value = "CS_BeastGolem_KnockBackHurt_Grunt2")]
            CS_BeastGolem_KnockBackHurt_Grunt2,
            [EnumMember(Value = "CS_BeastGolem_KnockBackHurt_Grunt3")]
            CS_BeastGolem_KnockBackHurt_Grunt3,
            [EnumMember(Value = "CS_BeastGolem_KnockDownHurt_Grunt1")]
            CS_BeastGolem_KnockDownHurt_Grunt1,
            [EnumMember(Value = "CS_BeastGolem_KnockDownHurt_Grunt2")]
            CS_BeastGolem_KnockDownHurt_Grunt2,
            [EnumMember(Value = "CS_BeastGolem_KnockDownHurt_Grunt3")]
            CS_BeastGolem_KnockDownHurt_Grunt3,
            [EnumMember(Value = "CS_BeastGolem_SideJump_Whoosh1")]
            CS_BeastGolem_SideJump_Whoosh1,
            [EnumMember(Value = "CS_BeastGolem_SideJump_Whoosh2")]
            CS_BeastGolem_SideJump_Whoosh2,
            [EnumMember(Value = "CS_BeastGolem_SideJump_Whoosh3")]
            CS_BeastGolem_SideJump_Whoosh3,
            [EnumMember(Value = "CS_BladeDancer_Attack_Grunt1")]
            CS_BladeDancer_Attack_Grunt1 = 50860,
            [EnumMember(Value = "CS_BladeDancer_Attack_Grunt2")]
            CS_BladeDancer_Attack_Grunt2,
            [EnumMember(Value = "CS_BladeDancer_Attack_Grunt3")]
            CS_BladeDancer_Attack_Grunt3,
            [EnumMember(Value = "CS_BladeDancer_Attack_Whoosh1")]
            CS_BladeDancer_Attack_Whoosh1,
            [EnumMember(Value = "CS_BladeDancer_Attack_Whoosh2")]
            CS_BladeDancer_Attack_Whoosh2,
            [EnumMember(Value = "CS_BladeDancer_Attack_Whoosh3")]
            CS_BladeDancer_Attack_Whoosh3,
            [EnumMember(Value = "CS_BladeDancer_AttackSpecial1")]
            CS_BladeDancer_AttackSpecial1,
            [EnumMember(Value = "CS_BladeDancer_AttackSpecial2")]
            CS_BladeDancer_AttackSpecial2,
            [EnumMember(Value = "CS_BladeDancer_AttackSpecial3")]
            CS_BladeDancer_AttackSpecial3,
            [EnumMember(Value = "CS_BladeDancer_AttackSpell_Grunt1")]
            CS_BladeDancer_AttackSpell_Grunt1,
            [EnumMember(Value = "CS_BladeDancer_AttackSpell_Grunt2")]
            CS_BladeDancer_AttackSpell_Grunt2,
            [EnumMember(Value = "CS_BladeDancer_AttackSpell_Whoosh1")]
            CS_BladeDancer_AttackSpell_Whoosh1,
            [EnumMember(Value = "CS_BladeDancer_AttackSpell_Whoosh2")]
            CS_BladeDancer_AttackSpell_Whoosh2,
            [EnumMember(Value = "CS_BladeDancer_Bodyfall1")]
            CS_BladeDancer_Bodyfall1,
            [EnumMember(Value = "CS_BladeDancer_Bodyfall2")]
            CS_BladeDancer_Bodyfall2,
            [EnumMember(Value = "CS_BladeDancer_Bodyfall3")]
            CS_BladeDancer_Bodyfall3,
            [EnumMember(Value = "CS_BladeDancer_Footstep1")]
            CS_BladeDancer_Footstep1,
            [EnumMember(Value = "CS_BladeDancer_Footstep2")]
            CS_BladeDancer_Footstep2,
            [EnumMember(Value = "CS_BladeDancer_Footstep3")]
            CS_BladeDancer_Footstep3,
            [EnumMember(Value = "CS_BladeDancer_Footstep4")]
            CS_BladeDancer_Footstep4,
            [EnumMember(Value = "CS_BladeDancer_Footstep5")]
            CS_BladeDancer_Footstep5,
            [EnumMember(Value = "CS_BladeDancer_Footstep6")]
            CS_BladeDancer_Footstep6,
            [EnumMember(Value = "CS_BladeDancer_Footstep7")]
            CS_BladeDancer_Footstep7,
            [EnumMember(Value = "CS_BladeDancer_Footstep8")]
            CS_BladeDancer_Footstep8,
            [EnumMember(Value = "CS_BladeDancer_KnockBack_Grunt1")]
            CS_BladeDancer_KnockBack_Grunt1,
            [EnumMember(Value = "CS_BladeDancer_KnockBack_Grunt2")]
            CS_BladeDancer_KnockBack_Grunt2,
            [EnumMember(Value = "CS_BladeDancer_KnockBack_Grunt3")]
            CS_BladeDancer_KnockBack_Grunt3,
            [EnumMember(Value = "CS_BladeDancer_RunningAttack_Whoosh1")]
            CS_BladeDancer_RunningAttack_Whoosh1,
            [EnumMember(Value = "CS_BladeDancer_RunningAttack_Whoosh2")]
            CS_BladeDancer_RunningAttack_Whoosh2,
            [EnumMember(Value = "CS_BurningMan_Attack1")]
            CS_BurningMan_Attack1 = 50890,
            [EnumMember(Value = "CS_BurningMan_Attack2")]
            CS_BurningMan_Attack2,
            [EnumMember(Value = "CS_BurningMan_Attack3")]
            CS_BurningMan_Attack3,
            [EnumMember(Value = "CS_BurningMan_Attack_Whoosh1")]
            CS_BurningMan_Attack_Whoosh1,
            [EnumMember(Value = "CS_BurningMan_Attack_Whoosh2")]
            CS_BurningMan_Attack_Whoosh2,
            [EnumMember(Value = "CS_BurningMan_Attack_Whoosh3")]
            CS_BurningMan_Attack_Whoosh3,
            [EnumMember(Value = "CS_BurningMan_FootStep1")]
            CS_BurningMan_FootStep1,
            [EnumMember(Value = "CS_BurningMan_FootStep2")]
            CS_BurningMan_FootStep2,
            [EnumMember(Value = "CS_BurningMan_FootStep3")]
            CS_BurningMan_FootStep3,
            [EnumMember(Value = "CS_BurningMan_FootStep4")]
            CS_BurningMan_FootStep4,
            [EnumMember(Value = "CS_BurningMan_FootStep5")]
            CS_BurningMan_FootStep5,
            [EnumMember(Value = "CS_BurningMan_FootStep6")]
            CS_BurningMan_FootStep6,
            [EnumMember(Value = "CS_BurningMan_FootStep7")]
            CS_BurningMan_FootStep7,
            [EnumMember(Value = "CS_BurningMan_RunningLoop")]
            CS_BurningMan_RunningLoop,
            [EnumMember(Value = "CS_HiveLord_Attack_HornetGrount1")]
            CS_HiveLord_Attack_HornetGrount1 = 50910,
            [EnumMember(Value = "CS_HiveLord_Attack_HornetGrount2")]
            CS_HiveLord_Attack_HornetGrount2,
            [EnumMember(Value = "CS_HiveLord_Attack_HornetGrount3")]
            CS_HiveLord_Attack_HornetGrount3,
            [EnumMember(Value = "CS_HiveLord_Attack_HornetGrount4")]
            CS_HiveLord_Attack_HornetGrount4,
            [EnumMember(Value = "CS_HiveLord_Attack_HornetGrount5")]
            CS_HiveLord_Attack_HornetGrount5,
            [EnumMember(Value = "CS_HiveLord_Attack_HornetGrount6")]
            CS_HiveLord_Attack_HornetGrount6,
            [EnumMember(Value = "CS_HiveLord_Attack_HornetGrount7")]
            CS_HiveLord_Attack_HornetGrount7,
            [EnumMember(Value = "CS_HiveLord_Attack_HornetGrount8")]
            CS_HiveLord_Attack_HornetGrount8,
            [EnumMember(Value = "CS_HiveLord_Attack_HornetSwarm_loop")]
            CS_HiveLord_Attack_HornetSwarm_loop,
            [EnumMember(Value = "CS_Ghost_Attack_Grunt1")]
            CS_Ghost_Attack_Grunt1 = 50920,
            [EnumMember(Value = "CS_Ghost_Attack_Grunt2")]
            CS_Ghost_Attack_Grunt2,
            [EnumMember(Value = "CS_Ghost_Attack_Grunt3")]
            CS_Ghost_Attack_Grunt3,
            [EnumMember(Value = "CS_Ghost_Dead_Grunt1")]
            CS_Ghost_Dead_Grunt1,
            [EnumMember(Value = "CS_Ghost_Dead_Grunt2")]
            CS_Ghost_Dead_Grunt2,
            [EnumMember(Value = "CS_Ghost_Dead_Grunt3")]
            CS_Ghost_Dead_Grunt3,
            [EnumMember(Value = "CS_Ghost_Hurt_Grunt1")]
            CS_Ghost_Hurt_Grunt1,
            [EnumMember(Value = "CS_Ghost_Hurt_Grunt2")]
            CS_Ghost_Hurt_Grunt2,
            [EnumMember(Value = "CS_Ghost_Hurt_Grunt3")]
            CS_Ghost_Hurt_Grunt3,
            [EnumMember(Value = "CS_Immaculate_Attack_Grunt1")]
            CS_Immaculate_Attack_Grunt1 = 50930,
            [EnumMember(Value = "CS_Immaculate_Attack_Grunt2")]
            CS_Immaculate_Attack_Grunt2,
            [EnumMember(Value = "CS_Immaculate_Attack_Grunt3")]
            CS_Immaculate_Attack_Grunt3,
            [EnumMember(Value = "CS_Immaculate_Attack_Whoosh1")]
            CS_Immaculate_Attack_Whoosh1,
            [EnumMember(Value = "CS_Immaculate_Attack_Whoosh2")]
            CS_Immaculate_Attack_Whoosh2,
            [EnumMember(Value = "CS_Immaculate_Attack_Whoosh3")]
            CS_Immaculate_Attack_Whoosh3,
            [EnumMember(Value = "CS_Immaculate_BodyFall1")]
            CS_Immaculate_BodyFall1,
            [EnumMember(Value = "CS_Immaculate_BodyFall2")]
            CS_Immaculate_BodyFall2,
            [EnumMember(Value = "CS_Immaculate_BodyFall3")]
            CS_Immaculate_BodyFall3,
            [EnumMember(Value = "CS_Immaculate_Footstep1")]
            CS_Immaculate_Footstep1,
            [EnumMember(Value = "CS_Immaculate_Footstep2")]
            CS_Immaculate_Footstep2,
            [EnumMember(Value = "CS_Immaculate_Footstep3")]
            CS_Immaculate_Footstep3,
            [EnumMember(Value = "CS_Immaculate_Footstep4")]
            CS_Immaculate_Footstep4,
            [EnumMember(Value = "CS_Immaculate_Footstep5")]
            CS_Immaculate_Footstep5,
            [EnumMember(Value = "CS_Immaculate_Footstep6")]
            CS_Immaculate_Footstep6,
            [EnumMember(Value = "CS_Immaculate_HeavyHurt_Grunt1")]
            CS_Immaculate_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_Immaculate_HeavyHurt_Grunt2")]
            CS_Immaculate_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_Immaculate_HeavyHurt_Grunt3")]
            CS_Immaculate_HeavyHurt_Grunt3,
            [EnumMember(Value = "CS_Immaculate_Hurt_Grunt1")]
            CS_Immaculate_Hurt_Grunt1,
            [EnumMember(Value = "CS_Immaculate_Hurt_Grunt2")]
            CS_Immaculate_Hurt_Grunt2,
            [EnumMember(Value = "CS_Immaculate_Hurt_Grunt3")]
            CS_Immaculate_Hurt_Grunt3,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack1_Grunt1")]
            CS_Immaculate_SpecialAttack1_Grunt1,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack1_Grunt2")]
            CS_Immaculate_SpecialAttack1_Grunt2,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack1_Grunt3")]
            CS_Immaculate_SpecialAttack1_Grunt3,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack1_Whoosh1")]
            CS_Immaculate_SpecialAttack1_Whoosh1,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack1_Whoosh2")]
            CS_Immaculate_SpecialAttack1_Whoosh2,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack1_Whoosh3")]
            CS_Immaculate_SpecialAttack1_Whoosh3,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack2_Whoosh1")]
            CS_Immaculate_SpecialAttack2_Whoosh1,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack2_Whoosh2")]
            CS_Immaculate_SpecialAttack2_Whoosh2,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack2_Whoosh3")]
            CS_Immaculate_SpecialAttack2_Whoosh3,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack_Grunt1")]
            CS_Immaculate_SpecialAttack_Grunt1,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack_Grunt2")]
            CS_Immaculate_SpecialAttack_Grunt2,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack_Grunt3")]
            CS_Immaculate_SpecialAttack_Grunt3,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack_Whoosh1")]
            CS_Immaculate_SpecialAttack_Whoosh1,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack_Whoosh2")]
            CS_Immaculate_SpecialAttack_Whoosh2,
            [EnumMember(Value = "CS_Immaculate_SpecialAttack_Whoosh3")]
            CS_Immaculate_SpecialAttack_Whoosh3,
            [EnumMember(Value = "CS_LichDark_Attack_LongRange_Grunt1")]
            CS_LichDark_Attack_LongRange_Grunt1 = 50970,
            [EnumMember(Value = "CS_LichDark_Attack_LongRange_Grunt2")]
            CS_LichDark_Attack_LongRange_Grunt2,
            [EnumMember(Value = "CS_LichDark_Attack_LongRange_Grunt3")]
            CS_LichDark_Attack_LongRange_Grunt3,
            [EnumMember(Value = "CS_LichDark_Attack_LongRange_Power1")]
            CS_LichDark_Attack_LongRange_Power1,
            [EnumMember(Value = "CS_LichDark_Attack_LongRange_Power2")]
            CS_LichDark_Attack_LongRange_Power2,
            [EnumMember(Value = "CS_LichDark_Attack_LongRange_Power3")]
            CS_LichDark_Attack_LongRange_Power3,
            [EnumMember(Value = "CS_LichDark_Idle_Loop")]
            CS_LichDark_Idle_Loop,
            [EnumMember(Value = "CS_LichDark_Run_Loop")]
            CS_LichDark_Run_Loop,
            [EnumMember(Value = "CS_LichGold_Attack_Grunt1")]
            CS_LichGold_Attack_Grunt1 = 50980,
            [EnumMember(Value = "CS_LichGold_Attack_Grunt2")]
            CS_LichGold_Attack_Grunt2,
            [EnumMember(Value = "CS_LichGold_Attack_Grunt3")]
            CS_LichGold_Attack_Grunt3,
            [EnumMember(Value = "CS_LichGold_Attack_Whoosh1")]
            CS_LichGold_Attack_Whoosh1,
            [EnumMember(Value = "CS_LichGold_Attack_Whoosh2")]
            CS_LichGold_Attack_Whoosh2,
            [EnumMember(Value = "CS_LichGold_Dodge_Grunt1")]
            CS_LichGold_Dodge_Grunt1,
            [EnumMember(Value = "CS_LichGold_Dodge_Grunt2")]
            CS_LichGold_Dodge_Grunt2,
            [EnumMember(Value = "CS_LichGold_Dodge_Whoosh1")]
            CS_LichGold_Dodge_Whoosh1,
            [EnumMember(Value = "CS_LichGold_Dodge_Whoosh2")]
            CS_LichGold_Dodge_Whoosh2,
            [EnumMember(Value = "CS_LichGold_Hurt_Grunt1")]
            CS_LichGold_Hurt_Grunt1,
            [EnumMember(Value = "CS_LichGold_Hurt_Grunt2")]
            CS_LichGold_Hurt_Grunt2,
            [EnumMember(Value = "CS_LichGold_Hurt_Grunt3")]
            CS_LichGold_Hurt_Grunt3,
            [EnumMember(Value = "CS_LichGold_Spell1_Grunt1")]
            CS_LichGold_Spell1_Grunt1,
            [EnumMember(Value = "CS_LichGold_Spell1_Grunt2")]
            CS_LichGold_Spell1_Grunt2,
            [EnumMember(Value = "CS_LichGold_Spell1_Whoosh1")]
            CS_LichGold_Spell1_Whoosh1,
            [EnumMember(Value = "CS_LichGold_Spell1_Whoosh2")]
            CS_LichGold_Spell1_Whoosh2,
            [EnumMember(Value = "CS_LichGold_Spell1_Whoosh3")]
            CS_LichGold_Spell1_Whoosh3,
            [EnumMember(Value = "CS_LichGold_Spell2_Grount1")]
            CS_LichGold_Spell2_Grount1,
            [EnumMember(Value = "CS_LichGold_Spell2_Grount2")]
            CS_LichGold_Spell2_Grount2,
            [EnumMember(Value = "CS_LichGold_Spell2_Grount3")]
            CS_LichGold_Spell2_Grount3,
            [EnumMember(Value = "CS_LichGold_Spell2_Whoosh1")]
            CS_LichGold_Spell2_Whoosh1,
            [EnumMember(Value = "CS_LichGold_Spell2_Whoosh2")]
            CS_LichGold_Spell2_Whoosh2,
            [EnumMember(Value = "CS_LichGold_Spell2_Whoosh3")]
            CS_LichGold_Spell2_Whoosh3,
            [EnumMember(Value = "CS_LichGold_Spell3_Grount1")]
            CS_LichGold_Spell3_Grount1,
            [EnumMember(Value = "CS_LichGold_Spell3_Grount2")]
            CS_LichGold_Spell3_Grount2,
            [EnumMember(Value = "CS_LichGold_Spell3_Grount3")]
            CS_LichGold_Spell3_Grount3,
            [EnumMember(Value = "CS_LichGold_Spell3_Magic1")]
            CS_LichGold_Spell3_Magic1,
            [EnumMember(Value = "CS_LichGold_Spell3_Magic2")]
            CS_LichGold_Spell3_Magic2,
            [EnumMember(Value = "CS_LichGold_Spell3_Magic3")]
            CS_LichGold_Spell3_Magic3,
            [EnumMember(Value = "CS_LichGold_StaffBolt_Ground1")]
            CS_LichGold_StaffBolt_Ground1,
            [EnumMember(Value = "CS_LichGold_StaffBolt_Ground2")]
            CS_LichGold_StaffBolt_Ground2,
            [EnumMember(Value = "CS_LichGold_StaffBolt_Ground3")]
            CS_LichGold_StaffBolt_Ground3,
            [EnumMember(Value = "CS_LichGold_StaffBolt_Whoosh1")]
            CS_LichGold_StaffBolt_Whoosh1,
            [EnumMember(Value = "CS_LichGold_StaffBolt_Whoosh2")]
            CS_LichGold_StaffBolt_Whoosh2,
            [EnumMember(Value = "CS_Deer_Attack_Grunt1")]
            CS_Deer_Attack_Grunt1 = 51020,
            [EnumMember(Value = "CS_Deer_Attack_Grunt2")]
            CS_Deer_Attack_Grunt2,
            [EnumMember(Value = "CS_Deer_Attack_Grunt3")]
            CS_Deer_Attack_Grunt3,
            [EnumMember(Value = "CS_Deer_Attack_Whoosh1")]
            CS_Deer_Attack_Whoosh1,
            [EnumMember(Value = "CS_Deer_Attack_Whoosh2")]
            CS_Deer_Attack_Whoosh2,
            [EnumMember(Value = "CS_Deer_Attack_Whoosh3")]
            CS_Deer_Attack_Whoosh3,
            [EnumMember(Value = "CS_Deer_BodyFall1")]
            CS_Deer_BodyFall1,
            [EnumMember(Value = "CS_Deer_BodyFall2")]
            CS_Deer_BodyFall2,
            [EnumMember(Value = "CS_Deer_Call1")]
            CS_Deer_Call1,
            [EnumMember(Value = "CS_Deer_Call2")]
            CS_Deer_Call2,
            [EnumMember(Value = "CS_Deer_Foostep1")]
            CS_Deer_Foostep1,
            [EnumMember(Value = "CS_Deer_Foostep2")]
            CS_Deer_Foostep2,
            [EnumMember(Value = "CS_Deer_Foostep3")]
            CS_Deer_Foostep3,
            [EnumMember(Value = "CS_Deer_Foostep4")]
            CS_Deer_Foostep4,
            [EnumMember(Value = "CS_Deer_Foostep5")]
            CS_Deer_Foostep5,
            [EnumMember(Value = "CS_Deer_Foostep6")]
            CS_Deer_Foostep6,
            [EnumMember(Value = "CS_Deer_Foostep7")]
            CS_Deer_Foostep7,
            [EnumMember(Value = "CS_Deer_Foostep8")]
            CS_Deer_Foostep8,
            [EnumMember(Value = "CS_Deer_Foostep9")]
            CS_Deer_Foostep9,
            [EnumMember(Value = "CS_Deer_Foostep10")]
            CS_Deer_Foostep10,
            [EnumMember(Value = "CS_Deer_Foostep11")]
            CS_Deer_Foostep11,
            [EnumMember(Value = "CS_Deer_Foostep12")]
            CS_Deer_Foostep12,
            [EnumMember(Value = "CS_Deer_Hurt_Grount1")]
            CS_Deer_Hurt_Grount1,
            [EnumMember(Value = "CS_Deer_Hurt_Grount2")]
            CS_Deer_Hurt_Grount2,
            [EnumMember(Value = "CS_Deer_Hurt_Grount3")]
            CS_Deer_Hurt_Grount3,
            [EnumMember(Value = "CS_Deer_Hurt_hit1")]
            CS_Deer_Hurt_hit1,
            [EnumMember(Value = "CS_Deer_Hurt_hit2")]
            CS_Deer_Hurt_hit2,
            [EnumMember(Value = "CS_Deer_Hurt_hit3")]
            CS_Deer_Hurt_hit3,
            [EnumMember(Value = "CS_Specter_Range_ShotLongRange")]
            CS_Specter_Range_ShotLongRange = 51050,
            [EnumMember(Value = "CS_Specter_Range_ShotWide1")]
            CS_Specter_Range_ShotWide1,
            [EnumMember(Value = "CS_Specter_Range_ShotWide2")]
            CS_Specter_Range_ShotWide2,
            [EnumMember(Value = "CS_Specter_Range_ShotWide3")]
            CS_Specter_Range_ShotWide3,
            [EnumMember(Value = "CS_Specter_Range_WeaponLoad")]
            CS_Specter_Range_WeaponLoad,
            [EnumMember(Value = "CS_Steakosaur_BiteAttack_Grunt1")]
            CS_Steakosaur_BiteAttack_Grunt1 = 51060,
            [EnumMember(Value = "CS_Steakosaur_BiteAttack_Grunt2")]
            CS_Steakosaur_BiteAttack_Grunt2,
            [EnumMember(Value = "CS_Steakosaur_BiteAttack_Grunt3")]
            CS_Steakosaur_BiteAttack_Grunt3,
            [EnumMember(Value = "CS_Steakosaur_BiteAttack_Grunt4")]
            CS_Steakosaur_BiteAttack_Grunt4,
            [EnumMember(Value = "CS_Steakosaur_BiteAttack_Whoosh1")]
            CS_Steakosaur_BiteAttack_Whoosh1,
            [EnumMember(Value = "CS_Steakosaur_BiteAttack_Whoosh2")]
            CS_Steakosaur_BiteAttack_Whoosh2,
            [EnumMember(Value = "CS_Steakosaur_BiteAttack_Whoosh3")]
            CS_Steakosaur_BiteAttack_Whoosh3,
            [EnumMember(Value = "CS_Steakosaur_BiteAttack_Whoosh4")]
            CS_Steakosaur_BiteAttack_Whoosh4,
            [EnumMember(Value = "CS_Steakosaur_BodyFall1")]
            CS_Steakosaur_BodyFall1,
            [EnumMember(Value = "CS_Steakosaur_BodyFall2")]
            CS_Steakosaur_BodyFall2,
            [EnumMember(Value = "CS_Steakosaur_BodyFall3")]
            CS_Steakosaur_BodyFall3,
            [EnumMember(Value = "CS_Steakosaur_Footstep1")]
            CS_Steakosaur_Footstep1,
            [EnumMember(Value = "CS_Steakosaur_Footstep2")]
            CS_Steakosaur_Footstep2,
            [EnumMember(Value = "CS_Steakosaur_Footstep3")]
            CS_Steakosaur_Footstep3,
            [EnumMember(Value = "CS_Steakosaur_Footstep4")]
            CS_Steakosaur_Footstep4,
            [EnumMember(Value = "CS_Steakosaur_Footstep5")]
            CS_Steakosaur_Footstep5,
            [EnumMember(Value = "CS_Steakosaur_Footstep6")]
            CS_Steakosaur_Footstep6,
            [EnumMember(Value = "CS_Steakosaur_HowlCall1")]
            CS_Steakosaur_HowlCall1,
            [EnumMember(Value = "CS_Steakosaur_HowlCall2")]
            CS_Steakosaur_HowlCall2,
            [EnumMember(Value = "CS_Steakosaur_HowlCall3")]
            CS_Steakosaur_HowlCall3,
            [EnumMember(Value = "CS_Steakosaur_HurtGrount1")]
            CS_Steakosaur_HurtGrount1,
            [EnumMember(Value = "CS_Steakosaur_HurtGrount2")]
            CS_Steakosaur_HurtGrount2,
            [EnumMember(Value = "CS_Steakosaur_HurtGrount3")]
            CS_Steakosaur_HurtGrount3,
            [EnumMember(Value = "CS_Steakosaur_RunAttack_Grount1")]
            CS_Steakosaur_RunAttack_Grount1,
            [EnumMember(Value = "CS_Steakosaur_RunAttack_Grount2")]
            CS_Steakosaur_RunAttack_Grount2,
            [EnumMember(Value = "CS_Steakosaur_RunAttack_Grount3")]
            CS_Steakosaur_RunAttack_Grount3,
            [EnumMember(Value = "CS_Troglodyte_CastSpell_Grunt1")]
            CS_Troglodyte_CastSpell_Grunt1 = 51090,
            [EnumMember(Value = "CS_Troglodyte_CastSpell_Grunt2")]
            CS_Troglodyte_CastSpell_Grunt2,
            [EnumMember(Value = "CS_Troglodyte_CastSpell_Grunt3")]
            CS_Troglodyte_CastSpell_Grunt3,
            [EnumMember(Value = "CS_Troglodyte_HeavyHurt_Grount1")]
            CS_Troglodyte_HeavyHurt_Grount1,
            [EnumMember(Value = "CS_Troglodyte_HeavyHurt_Grount2")]
            CS_Troglodyte_HeavyHurt_Grount2,
            [EnumMember(Value = "CS_Troglodyte_HeavyHurt_Grount3")]
            CS_Troglodyte_HeavyHurt_Grount3,
            [EnumMember(Value = "CS_Troglodyte_HeavyHurt_Grount4")]
            CS_Troglodyte_HeavyHurt_Grount4,
            [EnumMember(Value = "CS_Troglodyte_Hurt_Grount1")]
            CS_Troglodyte_Hurt_Grount1,
            [EnumMember(Value = "CS_Troglodyte_Hurt_Grount2")]
            CS_Troglodyte_Hurt_Grount2,
            [EnumMember(Value = "CS_Troglodyte_Hurt_Grount3")]
            CS_Troglodyte_Hurt_Grount3,
            [EnumMember(Value = "CS_Troglodyte_Hurt_Grount4")]
            CS_Troglodyte_Hurt_Grount4,
            [EnumMember(Value = "CS_Troglodyte_Hurt_Grount5")]
            CS_Troglodyte_Hurt_Grount5,
            [EnumMember(Value = "CS_Troglodyte_MaceNormalAttack1_Grunt1")]
            CS_Troglodyte_MaceNormalAttack1_Grunt1,
            [EnumMember(Value = "CS_Troglodyte_MaceNormalAttack1_Grunt2")]
            CS_Troglodyte_MaceNormalAttack1_Grunt2,
            [EnumMember(Value = "CS_Troglodyte_MaceNormalAttack1_Grunt3")]
            CS_Troglodyte_MaceNormalAttack1_Grunt3,
            [EnumMember(Value = "CS_Troglodyte_MaceNormalAttack2_Grunt1")]
            CS_Troglodyte_MaceNormalAttack2_Grunt1,
            [EnumMember(Value = "CS_Troglodyte_MaceNormalAttack2_Grunt2")]
            CS_Troglodyte_MaceNormalAttack2_Grunt2,
            [EnumMember(Value = "CS_Troglodyte_MaceNormalAttack2_Grunt3")]
            CS_Troglodyte_MaceNormalAttack2_Grunt3,
            [EnumMember(Value = "CS_Troglodyte_PushKick_Grount1")]
            CS_Troglodyte_PushKick_Grount1,
            [EnumMember(Value = "CS_Troglodyte_PushKick_Grount2")]
            CS_Troglodyte_PushKick_Grount2,
            [EnumMember(Value = "CS_Troglodyte_PushKick_Grount3")]
            CS_Troglodyte_PushKick_Grount3,
            [EnumMember(Value = "CS_Troglodyte_PushKick_Grount4")]
            CS_Troglodyte_PushKick_Grount4,
            [EnumMember(Value = "CS_Troglodyte_SpecialAttack1_Grunt1")]
            CS_Troglodyte_SpecialAttack1_Grunt1,
            [EnumMember(Value = "CS_Troglodyte_SpecialAttack1_Grunt2")]
            CS_Troglodyte_SpecialAttack1_Grunt2,
            [EnumMember(Value = "CS_Troglodyte_SpecialAttack1_Grunt3")]
            CS_Troglodyte_SpecialAttack1_Grunt3,
            [EnumMember(Value = "CS_Troglodyte_SpecialAttack2_Grunt1")]
            CS_Troglodyte_SpecialAttack2_Grunt1,
            [EnumMember(Value = "CS_Troglodyte_SpecialAttack2_Grunt2")]
            CS_Troglodyte_SpecialAttack2_Grunt2,
            [EnumMember(Value = "CS_Troglodyte_SpecialAttack2_Grunt3")]
            CS_Troglodyte_SpecialAttack2_Grunt3,
            [EnumMember(Value = "CS_Troglodyte_Taunt_Grunt1")]
            CS_Troglodyte_Taunt_Grunt1,
            [EnumMember(Value = "CS_Troglodyte_Taunt_Grunt2")]
            CS_Troglodyte_Taunt_Grunt2,
            [EnumMember(Value = "CS_Troglodyte_Taunt_Grunt3")]
            CS_Troglodyte_Taunt_Grunt3,
            [EnumMember(Value = "CS_Troglodyte_Taunt_Grunt4")]
            CS_Troglodyte_Taunt_Grunt4,
            [EnumMember(Value = "CS_Troglodyte_ThrowGrenade_Grunt1")]
            CS_Troglodyte_ThrowGrenade_Grunt1,
            [EnumMember(Value = "CS_Troglodyte_ThrowGrenade_Grunt2")]
            CS_Troglodyte_ThrowGrenade_Grunt2,
            [EnumMember(Value = "CS_Troglodyte_ThrowGrenade_Grunt3")]
            CS_Troglodyte_ThrowGrenade_Grunt3,
            [EnumMember(Value = "CS_Troglodyte_Cast_Spell1")]
            CS_Troglodyte_Cast_Spell1,
            [EnumMember(Value = "CS_Troglodyte_Cast_Spell2")]
            CS_Troglodyte_Cast_Spell2,
            [EnumMember(Value = "CS_Troglodyte_Throw_Grenade1")]
            CS_Troglodyte_Throw_Grenade1,
            [EnumMember(Value = "CS_Troglodyte_Throw_Grenade2")]
            CS_Troglodyte_Throw_Grenade2,
            [EnumMember(Value = "CS_Troglodyte_Throw_Grenade3")]
            CS_Troglodyte_Throw_Grenade3,
            [EnumMember(Value = "CS_Tuanosaur_Attack_Whoosh1")]
            CS_Tuanosaur_Attack_Whoosh1,
            [EnumMember(Value = "CS_Tuanosaur_Attack_Whoosh2")]
            CS_Tuanosaur_Attack_Whoosh2,
            [EnumMember(Value = "CS_Tuanosaur_Attack_Whoosh3")]
            CS_Tuanosaur_Attack_Whoosh3,
            [EnumMember(Value = "CS_Tuanosaur_Attack_Whoosh4")]
            CS_Tuanosaur_Attack_Whoosh4,
            [EnumMember(Value = "CS_Tuanosaur_AttackBite_Grunt1")]
            CS_Tuanosaur_AttackBite_Grunt1,
            [EnumMember(Value = "CS_Tuanosaur_AttackBite_Grunt2")]
            CS_Tuanosaur_AttackBite_Grunt2,
            [EnumMember(Value = "CS_Tuanosaur_AttackBite_Grunt3")]
            CS_Tuanosaur_AttackBite_Grunt3,
            [EnumMember(Value = "CS_Tuanosaur_AttackBite_Whoosh1")]
            CS_Tuanosaur_AttackBite_Whoosh1,
            [EnumMember(Value = "CS_Tuanosaur_AttackBite_Whoosh2")]
            CS_Tuanosaur_AttackBite_Whoosh2,
            [EnumMember(Value = "CS_Tuanosaur_AttackBite_Whoosh3")]
            CS_Tuanosaur_AttackBite_Whoosh3,
            [EnumMember(Value = "CS_Tuanosaur_AttackBreathForward_Grunt1")]
            CS_Tuanosaur_AttackBreathForward_Grunt1,
            [EnumMember(Value = "CS_Tuanosaur_AttackBreathForward_Grunt2")]
            CS_Tuanosaur_AttackBreathForward_Grunt2,
            [EnumMember(Value = "CS_Tuanosaur_AttackBreathForward_Grunt3")]
            CS_Tuanosaur_AttackBreathForward_Grunt3,
            [EnumMember(Value = "CS_Tuanosaur_AttackBreathSwipe_Grunt1")]
            CS_Tuanosaur_AttackBreathSwipe_Grunt1,
            [EnumMember(Value = "CS_Tuanosaur_AttackBreathSwipe_Grunt2")]
            CS_Tuanosaur_AttackBreathSwipe_Grunt2,
            [EnumMember(Value = "CS_Tuanosaur_AttackBreathSwipe_Grunt3")]
            CS_Tuanosaur_AttackBreathSwipe_Grunt3,
            [EnumMember(Value = "CS_Tuanosaur_AttackClawSlash_Grunt1")]
            CS_Tuanosaur_AttackClawSlash_Grunt1,
            [EnumMember(Value = "CS_Tuanosaur_AttackClawSlash_Grunt2")]
            CS_Tuanosaur_AttackClawSlash_Grunt2,
            [EnumMember(Value = "CS_Tuanosaur_AttackClawSlash_Grunt3")]
            CS_Tuanosaur_AttackClawSlash_Grunt3,
            [EnumMember(Value = "CS_Tuanosaur_AttackDash_Grunt1")]
            CS_Tuanosaur_AttackDash_Grunt1,
            [EnumMember(Value = "CS_Tuanosaur_AttackDash_Grunt2")]
            CS_Tuanosaur_AttackDash_Grunt2,
            [EnumMember(Value = "CS_Tuanosaur_AttackDash_Grunt3")]
            CS_Tuanosaur_AttackDash_Grunt3,
            [EnumMember(Value = "CS_Tuanosaur_AttackSpit_Grunt1")]
            CS_Tuanosaur_AttackSpit_Grunt1,
            [EnumMember(Value = "CS_Tuanosaur_AttackSpit_Grunt2")]
            CS_Tuanosaur_AttackSpit_Grunt2,
            [EnumMember(Value = "CS_Tuanosaur_Bodyfall1")]
            CS_Tuanosaur_Bodyfall1,
            [EnumMember(Value = "CS_Tuanosaur_Bodyfall2")]
            CS_Tuanosaur_Bodyfall2,
            [EnumMember(Value = "CS_Tuanosaur_Bodyfall3")]
            CS_Tuanosaur_Bodyfall3,
            [EnumMember(Value = "CS_Tuanosaur_Dodge_Grunt1")]
            CS_Tuanosaur_Dodge_Grunt1,
            [EnumMember(Value = "CS_Tuanosaur_Dodge_Grunt2")]
            CS_Tuanosaur_Dodge_Grunt2,
            [EnumMember(Value = "CS_Tuanosaur_Dodge_Grunt3")]
            CS_Tuanosaur_Dodge_Grunt3,
            [EnumMember(Value = "CS_Tuanosaur_Dodge_Whoosh1")]
            CS_Tuanosaur_Dodge_Whoosh1,
            [EnumMember(Value = "CS_Tuanosaur_Dodge_Whoosh2")]
            CS_Tuanosaur_Dodge_Whoosh2,
            [EnumMember(Value = "CS_Tuanosaur_Dodge_Whoosh3")]
            CS_Tuanosaur_Dodge_Whoosh3,
            [EnumMember(Value = "CS_Tuanosaur_Footstep1")]
            CS_Tuanosaur_Footstep1,
            [EnumMember(Value = "CS_Tuanosaur_Footstep2")]
            CS_Tuanosaur_Footstep2,
            [EnumMember(Value = "CS_Tuanosaur_Footstep3")]
            CS_Tuanosaur_Footstep3,
            [EnumMember(Value = "CS_Tuanosaur_Footstep4")]
            CS_Tuanosaur_Footstep4,
            [EnumMember(Value = "CS_Tuanosaur_Footstep5")]
            CS_Tuanosaur_Footstep5,
            [EnumMember(Value = "CS_Tuanosaur_Footstep6")]
            CS_Tuanosaur_Footstep6,
            [EnumMember(Value = "CS_Tuanosaur_HeavyHurt_Grunt1")]
            CS_Tuanosaur_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_Tuanosaur_HeavyHurt_Grunt2")]
            CS_Tuanosaur_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_Tuanosaur_HeavyHurt_Grunt3")]
            CS_Tuanosaur_HeavyHurt_Grunt3,
            [EnumMember(Value = "CS_Tuanosaur_Hurt_Grunt1")]
            CS_Tuanosaur_Hurt_Grunt1,
            [EnumMember(Value = "CS_Tuanosaur_Hurt_Grunt2")]
            CS_Tuanosaur_Hurt_Grunt2,
            [EnumMember(Value = "CS_Tuanosaur_Hurt_Grunt3")]
            CS_Tuanosaur_Hurt_Grunt3,
            [EnumMember(Value = "CS_Wendigo_Dodge_Grunt1")]
            CS_Wendigo_Dodge_Grunt1 = 51180,
            [EnumMember(Value = "CS_Wendigo_Dodge_Grunt2")]
            CS_Wendigo_Dodge_Grunt2,
            [EnumMember(Value = "CS_Wendigo_Dodge_Grunt3")]
            CS_Wendigo_Dodge_Grunt3,
            [EnumMember(Value = "CS_Wendigo_HeavyHurt_Grunt1")]
            CS_Wendigo_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_Wendigo_HeavyHurt_Grunt2")]
            CS_Wendigo_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_Wendigo_HeavyHurt_Grunt3")]
            CS_Wendigo_HeavyHurt_Grunt3,
            [EnumMember(Value = "CS_Wendigo_Hurt_Grunt1")]
            CS_Wendigo_Hurt_Grunt1,
            [EnumMember(Value = "CS_Wendigo_Hurt_Grunt2")]
            CS_Wendigo_Hurt_Grunt2,
            [EnumMember(Value = "CS_Wendigo_Hurt_Grunt3")]
            CS_Wendigo_Hurt_Grunt3,
            [EnumMember(Value = "CS_Wendigo_NormalAttack_Grunt1")]
            CS_Wendigo_NormalAttack_Grunt1,
            [EnumMember(Value = "CS_Wendigo_NormalAttack_Grunt2")]
            CS_Wendigo_NormalAttack_Grunt2,
            [EnumMember(Value = "CS_Wendigo_NormalAttack_Grunt3")]
            CS_Wendigo_NormalAttack_Grunt3,
            [EnumMember(Value = "CS_Wendigo_NormalAttack_Grunt4")]
            CS_Wendigo_NormalAttack_Grunt4,
            [EnumMember(Value = "CS_Wendigo_NormalAttack_Grunt5")]
            CS_Wendigo_NormalAttack_Grunt5,
            [EnumMember(Value = "CS_Wendigo_NormalAttack_Grunt6")]
            CS_Wendigo_NormalAttack_Grunt6,
            [EnumMember(Value = "CS_Wendigo_NormalAttack_Grunt7")]
            CS_Wendigo_NormalAttack_Grunt7,
            [EnumMember(Value = "CS_Wendigo_NormalAttack_Grunt8")]
            CS_Wendigo_NormalAttack_Grunt8,
            [EnumMember(Value = "CS_Wendigo_NormalAttack_Grunt9")]
            CS_Wendigo_NormalAttack_Grunt9,
            [EnumMember(Value = "CS_Wendigo_NormalAttack_Grunt10")]
            CS_Wendigo_NormalAttack_Grunt10,
            [EnumMember(Value = "CS_Wendigo_SpecialAttack1_Grunt1")]
            CS_Wendigo_SpecialAttack1_Grunt1,
            [EnumMember(Value = "CS_Wendigo_SpecialAttack1_Grunt2")]
            CS_Wendigo_SpecialAttack1_Grunt2,
            [EnumMember(Value = "CS_Wendigo_SpecialAttack1_Grunt3")]
            CS_Wendigo_SpecialAttack1_Grunt3,
            [EnumMember(Value = "CS_Wendigo_SpecialAttack2_Grunt1")]
            CS_Wendigo_SpecialAttack2_Grunt1,
            [EnumMember(Value = "CS_Wendigo_SpecialAttack2_Grunt2")]
            CS_Wendigo_SpecialAttack2_Grunt2,
            [EnumMember(Value = "CS_Wendigo_SpecialAttack2_Grunt3")]
            CS_Wendigo_SpecialAttack2_Grunt3,
            [EnumMember(Value = "CS_Wendigo_Spell_Grunt1")]
            CS_Wendigo_Spell_Grunt1,
            [EnumMember(Value = "CS_Wendigo_Spell_Grunt2")]
            CS_Wendigo_Spell_Grunt2,
            [EnumMember(Value = "CS_Wendigo_Spell_Grunt3")]
            CS_Wendigo_Spell_Grunt3,
            [EnumMember(Value = "CS_FemaleCharactersAttackGrunt01")]
            CS_FemaleCharactersAttackGrunt01,
            [EnumMember(Value = "CS_FemaleCharactersAttackGrunt02")]
            CS_FemaleCharactersAttackGrunt02,
            [EnumMember(Value = "CS_FemaleCharactersAttackGrunt03")]
            CS_FemaleCharactersAttackGrunt03,
            [EnumMember(Value = "CS_FemaleCharactersAttackGrunt04")]
            CS_FemaleCharactersAttackGrunt04,
            [EnumMember(Value = "CS_FemaleCharactersAttackGrunt05")]
            CS_FemaleCharactersAttackGrunt05,
            [EnumMember(Value = "CS_FemaleCharactersAttackGrunt06")]
            CS_FemaleCharactersAttackGrunt06,
            [EnumMember(Value = "CS_FemaleCharactersAttackGrunt07")]
            CS_FemaleCharactersAttackGrunt07,
            [EnumMember(Value = "CS_FemaleCharactersAttackShout01")]
            CS_FemaleCharactersAttackShout01,
            [EnumMember(Value = "CS_FemaleCharactersAttackShout02")]
            CS_FemaleCharactersAttackShout02,
            [EnumMember(Value = "CS_FemaleCharactersExhausted01")]
            CS_FemaleCharactersExhausted01,
            [EnumMember(Value = "CS_FemaleCharactersExhausted02")]
            CS_FemaleCharactersExhausted02,
            [EnumMember(Value = "CS_FemaleCharactersFreezing01")]
            CS_FemaleCharactersFreezing01,
            [EnumMember(Value = "CS_FemaleCharactersFreezing02")]
            CS_FemaleCharactersFreezing02,
            [EnumMember(Value = "CS_FemaleCharactersHeavyHurt01")]
            CS_FemaleCharactersHeavyHurt01,
            [EnumMember(Value = "CS_FemaleCharactersHeavyHurt02")]
            CS_FemaleCharactersHeavyHurt02,
            [EnumMember(Value = "CS_FemaleCharactersHeavyHurt03")]
            CS_FemaleCharactersHeavyHurt03,
            [EnumMember(Value = "CS_FemaleCharactersHeavyHurt04")]
            CS_FemaleCharactersHeavyHurt04,
            [EnumMember(Value = "CS_FemaleCharactersHurt01")]
            CS_FemaleCharactersHurt01,
            [EnumMember(Value = "CS_FemaleCharactersHurt02")]
            CS_FemaleCharactersHurt02,
            [EnumMember(Value = "CS_FemaleCharactersHurt03")]
            CS_FemaleCharactersHurt03,
            [EnumMember(Value = "CS_FemaleCharactersHurt04")]
            CS_FemaleCharactersHurt04,
            [EnumMember(Value = "CS_FemaleCharactersOverheating01")]
            CS_FemaleCharactersOverheating01,
            [EnumMember(Value = "CS_FemaleCharactersOverheating02")]
            CS_FemaleCharactersOverheating02,
            [EnumMember(Value = "CS_FemaleCharactersSleepingLoop")]
            CS_FemaleCharactersSleepingLoop,
            [EnumMember(Value = "CS_FemaleCharactersTired01")]
            CS_FemaleCharactersTired01,
            [EnumMember(Value = "CS_FemaleCharactersTired02")]
            CS_FemaleCharactersTired02,
            [EnumMember(Value = "CS_FemaleCharactersVomiting01")]
            CS_FemaleCharactersVomiting01,
            [EnumMember(Value = "CS_FemaleCharactersVomiting02")]
            CS_FemaleCharactersVomiting02,
            [EnumMember(Value = "CS_CrescentSharkAttackBiteGrunt01")]
            CS_CrescentSharkAttackBiteGrunt01,
            [EnumMember(Value = "CS_CrescentSharkAttackBiteGrunt02")]
            CS_CrescentSharkAttackBiteGrunt02,
            [EnumMember(Value = "CS_CrescentSharkAttackBiteGrunt03")]
            CS_CrescentSharkAttackBiteGrunt03,
            [EnumMember(Value = "CS_CrescentSharkAttackWaterMove01")]
            CS_CrescentSharkAttackWaterMove01,
            [EnumMember(Value = "CS_CrescentSharkAttackWaterMove02")]
            CS_CrescentSharkAttackWaterMove02,
            [EnumMember(Value = "CS_CrescentSharkAttackWaterMove03")]
            CS_CrescentSharkAttackWaterMove03,
            [EnumMember(Value = "CS_CrescentSharkAttackWaterMove04")]
            CS_CrescentSharkAttackWaterMove04,
            [EnumMember(Value = "CS_CrescentSharkAttackWaterMove05")]
            CS_CrescentSharkAttackWaterMove05,
            [EnumMember(Value = "CS_CrescentSharkAttackWaterMove06")]
            CS_CrescentSharkAttackWaterMove06,
            [EnumMember(Value = "CS_CrescentSharkAttack2BiteGrunt01")]
            CS_CrescentSharkAttack2BiteGrunt01,
            [EnumMember(Value = "CS_CrescentSharkAttack2BiteGrunt02")]
            CS_CrescentSharkAttack2BiteGrunt02,
            [EnumMember(Value = "CS_CrescentSharkAttack2BiteGrunt03")]
            CS_CrescentSharkAttack2BiteGrunt03,
            [EnumMember(Value = "CS_CrescentSharkAttack2ElecWhoohs01")]
            CS_CrescentSharkAttack2ElecWhoohs01,
            [EnumMember(Value = "CS_CrescentSharkAttack2ElecWhoohs02")]
            CS_CrescentSharkAttack2ElecWhoohs02,
            [EnumMember(Value = "CS_CrescentSharkAttack2ElecWhoohs03")]
            CS_CrescentSharkAttack2ElecWhoohs03,
            [EnumMember(Value = "CS_CrescentSharkElectricityLoop01")]
            CS_CrescentSharkElectricityLoop01,
            [EnumMember(Value = "CS_CrescentSharkElectricityLoop02")]
            CS_CrescentSharkElectricityLoop02,
            [EnumMember(Value = "CS_CrescentSharkHurtElectricityHit01")]
            CS_CrescentSharkHurtElectricityHit01,
            [EnumMember(Value = "CS_CrescentSharkHurtElectricityHit02")]
            CS_CrescentSharkHurtElectricityHit02,
            [EnumMember(Value = "CS_CrescentSharkHurtElectricityHit03")]
            CS_CrescentSharkHurtElectricityHit03,
            [EnumMember(Value = "CS_CrescentSharkHurtGrunt01")]
            CS_CrescentSharkHurtGrunt01,
            [EnumMember(Value = "CS_CrescentSharkHurtGrunt02")]
            CS_CrescentSharkHurtGrunt02,
            [EnumMember(Value = "CS_CrescentSharkHurtGrunt03")]
            CS_CrescentSharkHurtGrunt03,
            [EnumMember(Value = "CS_CrescentSharkHurtHit01")]
            CS_CrescentSharkHurtHit01,
            [EnumMember(Value = "CS_CrescentSharkHurtHit02")]
            CS_CrescentSharkHurtHit02,
            [EnumMember(Value = "CS_CrescentSharkHurtHit03")]
            CS_CrescentSharkHurtHit03,
            [EnumMember(Value = "CS_CrescentSharkKnockedDownWakeUp01")]
            CS_CrescentSharkKnockedDownWakeUp01,
            [EnumMember(Value = "CS_CrescentSharkKnockedDownWakeUp02")]
            CS_CrescentSharkKnockedDownWakeUp02,
            [EnumMember(Value = "CS_CrescentSharkKnockedDownWakeUp03")]
            CS_CrescentSharkKnockedDownWakeUp03,
            [EnumMember(Value = "CS_Golem_Attack3Spin_Whoosh1")]
            CS_Golem_Attack3Spin_Whoosh1 = 51270,
            [EnumMember(Value = "CS_Golem_Attack3Spin_Whoosh2")]
            CS_Golem_Attack3Spin_Whoosh2,
            [EnumMember(Value = "CS_Golem_BodyFall1")]
            CS_Golem_BodyFall1,
            [EnumMember(Value = "CS_Golem_BodyFall2")]
            CS_Golem_BodyFall2,
            [EnumMember(Value = "CS_Golem_BodyFall3")]
            CS_Golem_BodyFall3,
            [EnumMember(Value = "CS_Golem_Footstep1")]
            CS_Golem_Footstep1,
            [EnumMember(Value = "CS_Golem_Footstep2")]
            CS_Golem_Footstep2,
            [EnumMember(Value = "CS_Golem_Footstep3")]
            CS_Golem_Footstep3,
            [EnumMember(Value = "CS_Golem_Footstep4")]
            CS_Golem_Footstep4,
            [EnumMember(Value = "CS_Golem_Footstep5")]
            CS_Golem_Footstep5,
            [EnumMember(Value = "CS_Golem_Footstep6")]
            CS_Golem_Footstep6,
            [EnumMember(Value = "CS_Golem_HeavyAttack_Whoosh1")]
            CS_Golem_HeavyAttack_Whoosh1,
            [EnumMember(Value = "CS_Golem_HeavyAttack_Whoosh2")]
            CS_Golem_HeavyAttack_Whoosh2,
            [EnumMember(Value = "CS_Golem_HeavyAttackFence_Whoosh1")]
            CS_Golem_HeavyAttackFence_Whoosh1,
            [EnumMember(Value = "CS_Golem_HeavyAttackFence_Whoosh2")]
            CS_Golem_HeavyAttackFence_Whoosh2,
            [EnumMember(Value = "CS_Golem_Hurt_Hit1")]
            CS_Golem_Hurt_Hit1,
            [EnumMember(Value = "CS_Golem_Hurt_Hit2")]
            CS_Golem_Hurt_Hit2,
            [EnumMember(Value = "CS_Golem_Hurt_Hit3")]
            CS_Golem_Hurt_Hit3,
            [EnumMember(Value = "CS_Golem_NormalAttack2_Whoosh1")]
            CS_Golem_NormalAttack2_Whoosh1,
            [EnumMember(Value = "CS_Golem_NormalAttack2_Whoosh2")]
            CS_Golem_NormalAttack2_Whoosh2,
            [EnumMember(Value = "CS_Golem_NormalAttack_Whoosh1")]
            CS_Golem_NormalAttack_Whoosh1,
            [EnumMember(Value = "CS_Golem_NormalAttack_Whoosh2")]
            CS_Golem_NormalAttack_Whoosh2,
            [EnumMember(Value = "CS_Golem_RunWind_Loop")]
            CS_Golem_RunWind_Loop,
            [EnumMember(Value = "CS_Golem_Attack_Grunt1")]
            CS_Golem_Attack_Grunt1 = 51300,
            [EnumMember(Value = "CS_Golem_Attack_Grunt2")]
            CS_Golem_Attack_Grunt2,
            [EnumMember(Value = "CS_Golem_Attack_Grunt3")]
            CS_Golem_Attack_Grunt3,
            [EnumMember(Value = "CS_Golem_HeavyAttack_Grunt1")]
            CS_Golem_HeavyAttack_Grunt1,
            [EnumMember(Value = "CS_Golem_HeavyAttack_Grunt2")]
            CS_Golem_HeavyAttack_Grunt2,
            [EnumMember(Value = "CS_Golem_HeavyAttack_Grunt3")]
            CS_Golem_HeavyAttack_Grunt3,
            [EnumMember(Value = "CS_Golem_Dodge_Whoosh1")]
            CS_Golem_Dodge_Whoosh1,
            [EnumMember(Value = "CS_Golem_Dodge_Whoosh2")]
            CS_Golem_Dodge_Whoosh2,
            [EnumMember(Value = "CS_PearlBird_Call1")]
            CS_PearlBird_Call1 = 51310,
            [EnumMember(Value = "CS_PearlBird_Call2")]
            CS_PearlBird_Call2,
            [EnumMember(Value = "CS_PearlBird_Call3")]
            CS_PearlBird_Call3,
            [EnumMember(Value = "CS_PearlBird_HeavyHurt_Grunt1")]
            CS_PearlBird_HeavyHurt_Grunt1,
            [EnumMember(Value = "CS_PearlBird_HeavyHurt_Grunt2")]
            CS_PearlBird_HeavyHurt_Grunt2,
            [EnumMember(Value = "CS_PearlBird_HeavyHurt_Grunt3")]
            CS_PearlBird_HeavyHurt_Grunt3,
            [EnumMember(Value = "CS_PearlBird_Hurt_Grunt1")]
            CS_PearlBird_Hurt_Grunt1,
            [EnumMember(Value = "CS_PearlBird_Hurt_Grunt2")]
            CS_PearlBird_Hurt_Grunt2,
            [EnumMember(Value = "CS_PearlBird_Hurt_Grunt3")]
            CS_PearlBird_Hurt_Grunt3,
            [EnumMember(Value = "CS_PearlBird_Idle_Call1")]
            CS_PearlBird_Idle_Call1,
            [EnumMember(Value = "CS_PearlBird_Idle_Call2")]
            CS_PearlBird_Idle_Call2,
            [EnumMember(Value = "CS_PearlBird_Idle_Call3")]
            CS_PearlBird_Idle_Call3,
            [EnumMember(Value = "CS_PearlBird_Idle_Call4")]
            CS_PearlBird_Idle_Call4,
            [EnumMember(Value = "CS_Calixa_Attack_Grunt1")]
            CS_Calixa_Attack_Grunt1 = 51330,
            [EnumMember(Value = "CS_Calixa_Attack_Grunt2")]
            CS_Calixa_Attack_Grunt2,
            [EnumMember(Value = "CS_Calixa_Attack_Grunt3")]
            CS_Calixa_Attack_Grunt3,
            [EnumMember(Value = "CS_Calixa_Dodge_Grunt1")]
            CS_Calixa_Dodge_Grunt1,
            [EnumMember(Value = "CS_Calixa_Dodge_Grunt2")]
            CS_Calixa_Dodge_Grunt2,
            [EnumMember(Value = "CS_Calixa_Hurt_Grunt1")]
            CS_Calixa_Hurt_Grunt1,
            [EnumMember(Value = "CS_Calixa_Hurt_Grunt2")]
            CS_Calixa_Hurt_Grunt2,
            [EnumMember(Value = "CS_Calixa_Hurt_Grunt3")]
            CS_Calixa_Hurt_Grunt3,
            [EnumMember(Value = "CS_Calixa_Spell1_Grunt1")]
            CS_Calixa_Spell1_Grunt1,
            [EnumMember(Value = "CS_Calixa_Spell1_Grunt2")]
            CS_Calixa_Spell1_Grunt2,
            [EnumMember(Value = "CS_Calixa_Spell2_Grunt1")]
            CS_Calixa_Spell2_Grunt1,
            [EnumMember(Value = "CS_Calixa_Spell2_Grunt2")]
            CS_Calixa_Spell2_Grunt2,
            [EnumMember(Value = "CS_Calixa_Spell2_Grunt3")]
            CS_Calixa_Spell2_Grunt3,
            [EnumMember(Value = "CS_Pypla_AttackGrunt_01")]
            CS_Pypla_AttackGrunt_01 = 51350,
            [EnumMember(Value = "CS_Pypla_AttackGrunt_02")]
            CS_Pypla_AttackGrunt_02,
            [EnumMember(Value = "CS_Pypla_AttackGrunt_03")]
            CS_Pypla_AttackGrunt_03,
            [EnumMember(Value = "CS_Pypla_AttackGrunt_04")]
            CS_Pypla_AttackGrunt_04,
            [EnumMember(Value = "CS_Pypla_AttackGrunt_05")]
            CS_Pypla_AttackGrunt_05,
            [EnumMember(Value = "CS_Pypla_AttackGrunt_Rage_01")]
            CS_Pypla_AttackGrunt_Rage_01,
            [EnumMember(Value = "CS_Pypla_AttackGrunt_Rage_02")]
            CS_Pypla_AttackGrunt_Rage_02,
            [EnumMember(Value = "CS_Pypla_AttackGrunt_Rage_03")]
            CS_Pypla_AttackGrunt_Rage_03,
            [EnumMember(Value = "CS_Pypla_AttackSwing_01")]
            CS_Pypla_AttackSwing_01,
            [EnumMember(Value = "CS_Pypla_AttackSwing_02")]
            CS_Pypla_AttackSwing_02,
            [EnumMember(Value = "CS_Pypla_AttackSwing_03")]
            CS_Pypla_AttackSwing_03,
            [EnumMember(Value = "CS_Pypla_AttackSwing_04")]
            CS_Pypla_AttackSwing_04,
            [EnumMember(Value = "CS_Pypla_Bite_01")]
            CS_Pypla_Bite_01,
            [EnumMember(Value = "CS_Pypla_Bite_02")]
            CS_Pypla_Bite_02,
            [EnumMember(Value = "CS_Pypla_Bite_03")]
            CS_Pypla_Bite_03,
            [EnumMember(Value = "CS_Pypla_Bite_04")]
            CS_Pypla_Bite_04,
            [EnumMember(Value = "CS_Pypla_Bite_05")]
            CS_Pypla_Bite_05,
            [EnumMember(Value = "CS_Pypla_Bite_06")]
            CS_Pypla_Bite_06,
            [EnumMember(Value = "CS_Pypla_BodyFall_01")]
            CS_Pypla_BodyFall_01,
            [EnumMember(Value = "CS_Pypla_BodyFall_02")]
            CS_Pypla_BodyFall_02,
            [EnumMember(Value = "CS_Pypla_BodyFall_03")]
            CS_Pypla_BodyFall_03,
            [EnumMember(Value = "CS_Pypla_DodgeAttackSwing_01")]
            CS_Pypla_DodgeAttackSwing_01,
            [EnumMember(Value = "CS_Pypla_DodgeAttackSwing_02")]
            CS_Pypla_DodgeAttackSwing_02,
            [EnumMember(Value = "CS_Pypla_DodgeAttackSwing_03")]
            CS_Pypla_DodgeAttackSwing_03,
            [EnumMember(Value = "CS_Pypla_DodgeAttackSwing_04")]
            CS_Pypla_DodgeAttackSwing_04,
            [EnumMember(Value = "CS_Pypla_HurtGrunt_01")]
            CS_Pypla_HurtGrunt_01,
            [EnumMember(Value = "CS_Pypla_HurtGrunt_02")]
            CS_Pypla_HurtGrunt_02,
            [EnumMember(Value = "CS_Pypla_HurtGrunt_03")]
            CS_Pypla_HurtGrunt_03,
            [EnumMember(Value = "CS_Pypla_HurtGrunt_05")]
            CS_Pypla_HurtGrunt_05 = 51379,
            [EnumMember(Value = "CS_Pypla_HurtGrunt_07")]
            CS_Pypla_HurtGrunt_07 = 51381,
            [EnumMember(Value = "CS_Pypla_HurtImpact_01")]
            CS_Pypla_HurtImpact_01,
            [EnumMember(Value = "CS_Pypla_HurtImpact_02")]
            CS_Pypla_HurtImpact_02,
            [EnumMember(Value = "CS_Pypla_HurtImpact_03")]
            CS_Pypla_HurtImpact_03,
            [EnumMember(Value = "CS_Pypla_SpecialAttackGrunt_01")]
            CS_Pypla_SpecialAttackGrunt_01,
            [EnumMember(Value = "CS_Pypla_SpecialAttackGrunt_02")]
            CS_Pypla_SpecialAttackGrunt_02,
            [EnumMember(Value = "CS_Pypla_SpecialAttackGrunt_03")]
            CS_Pypla_SpecialAttackGrunt_03,
            [EnumMember(Value = "CS_Pypla_SpecialAttackSwing_01")]
            CS_Pypla_SpecialAttackSwing_01,
            [EnumMember(Value = "CS_Pypla_SpecialAttackSwing_02")]
            CS_Pypla_SpecialAttackSwing_02,
            [EnumMember(Value = "CS_Pypla_SpecialAttackSwing_03")]
            CS_Pypla_SpecialAttackSwing_03,
            [EnumMember(Value = "CS_Pypla_Talk_01")]
            CS_Pypla_Talk_01,
            [EnumMember(Value = "CS_Pypla_Talk_02")]
            CS_Pypla_Talk_02,
            [EnumMember(Value = "CS_Pypla_Talk_03")]
            CS_Pypla_Talk_03,
            [EnumMember(Value = "CS_Pypla_Talk_04")]
            CS_Pypla_Talk_04,
            [EnumMember(Value = "CS_Elemental_CorruptionElement_Loop")]
            CS_Elemental_CorruptionElement_Loop = 51400,
            [EnumMember(Value = "CS_Elemental_DecayElement_Loop")]
            CS_Elemental_DecayElement_Loop,
            [EnumMember(Value = "CS_Elemental_ElectricityElement_Loop")]
            CS_Elemental_ElectricityElement_Loop,
            [EnumMember(Value = "CS_Elemental_EtheralElement_Loop")]
            CS_Elemental_EtheralElement_Loop,
            [EnumMember(Value = "CS_Elemental_Hurt_Impact_01")]
            CS_Elemental_Hurt_Impact_01,
            [EnumMember(Value = "CS_Elemental_Hurt_Impact_02")]
            CS_Elemental_Hurt_Impact_02,
            [EnumMember(Value = "CS_Elemental_Hurt_Impact_03")]
            CS_Elemental_Hurt_Impact_03,
            [EnumMember(Value = "CS_Elemental_IceElement_Loop")]
            CS_Elemental_IceElement_Loop,
            [EnumMember(Value = "CS_Elemental_LightElement_Loop")]
            CS_Elemental_LightElement_Loop,
            [EnumMember(Value = "CS_Elemental_LightningElement_Loop")]
            CS_Elemental_LightningElement_Loop,
            [EnumMember(Value = "CS_Elemental_OrganicMoves_01")]
            CS_Elemental_OrganicMoves_01,
            [EnumMember(Value = "CS_Elemental_OrganicMoves_02")]
            CS_Elemental_OrganicMoves_02,
            [EnumMember(Value = "CS_Elemental_OrganicMoves_03")]
            CS_Elemental_OrganicMoves_03,
            [EnumMember(Value = "CS_Elemental_OrganicMoves_04")]
            CS_Elemental_OrganicMoves_04,
            [EnumMember(Value = "CS_Elemental_OrganicMoves_05")]
            CS_Elemental_OrganicMoves_05,
            [EnumMember(Value = "CS_Elemental_SpellBoon_01")]
            CS_Elemental_SpellBoon_01,
            [EnumMember(Value = "CS_Elemental_SpellBoon_02")]
            CS_Elemental_SpellBoon_02,
            [EnumMember(Value = "CS_Elemental_SpellBoon_03")]
            CS_Elemental_SpellBoon_03,
            [EnumMember(Value = "CS_Elemental_SpellProjectileMove_01")]
            CS_Elemental_SpellProjectileMove_01,
            [EnumMember(Value = "CS_Elemental_SpellProjectileMove_02")]
            CS_Elemental_SpellProjectileMove_02,
            [EnumMember(Value = "CS_Elemental_SpellProjectileMove_03")]
            CS_Elemental_SpellProjectileMove_03,
            [EnumMember(Value = "CS_Grotesque_AttackGrunt_01")]
            CS_Grotesque_AttackGrunt_01 = 51430,
            [EnumMember(Value = "CS_Grotesque_AttackGrunt_02")]
            CS_Grotesque_AttackGrunt_02,
            [EnumMember(Value = "CS_Grotesque_AttackGrunt_03")]
            CS_Grotesque_AttackGrunt_03,
            [EnumMember(Value = "CS_Grotesque_AttackSwings_01")]
            CS_Grotesque_AttackSwings_01,
            [EnumMember(Value = "CS_Grotesque_AttackSwings_02")]
            CS_Grotesque_AttackSwings_02,
            [EnumMember(Value = "CS_Grotesque_AttackSwings_03")]
            CS_Grotesque_AttackSwings_03,
            [EnumMember(Value = "CS_Grotesque_AttackSwings_04")]
            CS_Grotesque_AttackSwings_04,
            [EnumMember(Value = "CS_Grotesque_AttackSwings_05")]
            CS_Grotesque_AttackSwings_05,
            [EnumMember(Value = "CS_Grotesque_AttackSwings_06")]
            CS_Grotesque_AttackSwings_06,
            [EnumMember(Value = "CS_Grotesque_AttackSwings_Heavy_01")]
            CS_Grotesque_AttackSwings_Heavy_01,
            [EnumMember(Value = "CS_Grotesque_AttackSwings_Heavy_02")]
            CS_Grotesque_AttackSwings_Heavy_02,
            [EnumMember(Value = "CS_Grotesque_Footsteps_01")]
            CS_Grotesque_Footsteps_01,
            [EnumMember(Value = "CS_Grotesque_Footsteps_02")]
            CS_Grotesque_Footsteps_02,
            [EnumMember(Value = "CS_Grotesque_Footsteps_03")]
            CS_Grotesque_Footsteps_03,
            [EnumMember(Value = "CS_Grotesque_Footsteps_04")]
            CS_Grotesque_Footsteps_04,
            [EnumMember(Value = "CS_Grotesque_Footsteps_05")]
            CS_Grotesque_Footsteps_05,
            [EnumMember(Value = "CS_Grotesque_Footsteps_06")]
            CS_Grotesque_Footsteps_06,
            [EnumMember(Value = "CS_Grotesque_HeavyAttack_01")]
            CS_Grotesque_HeavyAttack_01,
            [EnumMember(Value = "CS_Grotesque_HeavyAttack_02")]
            CS_Grotesque_HeavyAttack_02,
            [EnumMember(Value = "CS_Grotesque_HurtGrunt_01")]
            CS_Grotesque_HurtGrunt_01,
            [EnumMember(Value = "CS_Grotesque_HurtGrunt_02")]
            CS_Grotesque_HurtGrunt_02,
            [EnumMember(Value = "CS_Grotesque_HurtGrunt_03")]
            CS_Grotesque_HurtGrunt_03,
            [EnumMember(Value = "CS_Grotesque_KnockHurt_01")]
            CS_Grotesque_KnockHurt_01,
            [EnumMember(Value = "CS_Grotesque_KnockHurt_02")]
            CS_Grotesque_KnockHurt_02,
            [EnumMember(Value = "CS_Grotesque_KnockHurt_03")]
            CS_Grotesque_KnockHurt_03,
            [EnumMember(Value = "CS_Grotesque_LongAttackGrunt_01")]
            CS_Grotesque_LongAttackGrunt_01,
            [EnumMember(Value = "CS_Grotesque_LongAttackGrunt_02")]
            CS_Grotesque_LongAttackGrunt_02,
            [EnumMember(Value = "CS_Grotesque_Snort_01")]
            CS_Grotesque_Snort_01,
            [EnumMember(Value = "CS_Grotesque_Snort_02")]
            CS_Grotesque_Snort_02,
            [EnumMember(Value = "CS_Grotesque_SpikeDrag_Walk_01")]
            CS_Grotesque_SpikeDrag_Walk_01,
            [EnumMember(Value = "CS_Grotesque_SpikeDrag_Walk_02")]
            CS_Grotesque_SpikeDrag_Walk_02,
            [EnumMember(Value = "CS_Grotesque_SpikeDrag_Walk_03")]
            CS_Grotesque_SpikeDrag_Walk_03,
            [EnumMember(Value = "CS_Grotesque_SpikeDrag_Walk_04")]
            CS_Grotesque_SpikeDrag_Walk_04,
            [EnumMember(Value = "CS_Grotesque_SpikeDrag_Walk_05")]
            CS_Grotesque_SpikeDrag_Walk_05,
            [EnumMember(Value = "CS_Grotesque_SpikeDrag_Walk_06")]
            CS_Grotesque_SpikeDrag_Walk_06,
            [EnumMember(Value = "CS_Grotesque_SpikeImpact_Ground_01")]
            CS_Grotesque_SpikeImpact_Ground_01,
            [EnumMember(Value = "CS_Grotesque_SpikeImpact_Ground_02")]
            CS_Grotesque_SpikeImpact_Ground_02,
            [EnumMember(Value = "CS_Grotesque_SpikeImpact_Ground_03")]
            CS_Grotesque_SpikeImpact_Ground_03,
            [EnumMember(Value = "CS_Grotesque_SpikeImpact_Ground_04")]
            CS_Grotesque_SpikeImpact_Ground_04,
            [EnumMember(Value = "CS_Grotesque_SpikeImpact_Ground_05")]
            CS_Grotesque_SpikeImpact_Ground_05,
            [EnumMember(Value = "CS_Grotesque_SpikeImpact_Ground_06")]
            CS_Grotesque_SpikeImpact_Ground_06,
            [EnumMember(Value = "CS_Grotesque_TentacleMoves_Walk_01")]
            CS_Grotesque_TentacleMoves_Walk_01,
            [EnumMember(Value = "CS_Grotesque_TentacleMoves_Walk_02")]
            CS_Grotesque_TentacleMoves_Walk_02,
            [EnumMember(Value = "CS_Grotesque_TentacleMoves_Walk_03")]
            CS_Grotesque_TentacleMoves_Walk_03,
            [EnumMember(Value = "CS_Grotesque_TentacleMoves_Walk_04")]
            CS_Grotesque_TentacleMoves_Walk_04,
            [EnumMember(Value = "CS_Grotesque_TentacleMoves_Walk_05")]
            CS_Grotesque_TentacleMoves_Walk_05,
            [EnumMember(Value = "CS_Boozu_Scream_01")]
            CS_Boozu_Scream_01 = 51480,
            [EnumMember(Value = "CS_Boozu_Scream_02")]
            CS_Boozu_Scream_02,
            [EnumMember(Value = "CS_Boozu_ActionWhoohs_01")]
            CS_Boozu_ActionWhoohs_01,
            [EnumMember(Value = "CS_Boozu_ActionWhoohs_02")]
            CS_Boozu_ActionWhoohs_02,
            [EnumMember(Value = "CS_Boozu_ActionWhoohs_03")]
            CS_Boozu_ActionWhoohs_03,
            [EnumMember(Value = "CS_Boozu_ActionWhoohs_04")]
            CS_Boozu_ActionWhoohs_04,
            [EnumMember(Value = "CS_Boozu_ActionWhoohs_05")]
            CS_Boozu_ActionWhoohs_05,
            [EnumMember(Value = "CS_Boozu_ActionWhoohs_06")]
            CS_Boozu_ActionWhoohs_06,
            [EnumMember(Value = "CS_Boozu_AttackGrunt_01")]
            CS_Boozu_AttackGrunt_01,
            [EnumMember(Value = "CS_Boozu_AttackGrunt_02")]
            CS_Boozu_AttackGrunt_02,
            [EnumMember(Value = "CS_Boozu_AttackGrunt_03")]
            CS_Boozu_AttackGrunt_03,
            [EnumMember(Value = "CS_Boozu_AttackGrunt_04")]
            CS_Boozu_AttackGrunt_04,
            [EnumMember(Value = "CS_Boozu_AttackGrunt_05")]
            CS_Boozu_AttackGrunt_05,
            [EnumMember(Value = "CS_Boozu_BigImpact_01")]
            CS_Boozu_BigImpact_01,
            [EnumMember(Value = "CS_Boozu_BigImpact_02")]
            CS_Boozu_BigImpact_02,
            [EnumMember(Value = "CS_Boozu_BigImpact_03")]
            CS_Boozu_BigImpact_03,
            [EnumMember(Value = "CS_Boozu_ChargeGrunt_01")]
            CS_Boozu_ChargeGrunt_01,
            [EnumMember(Value = "CS_Boozu_ChargeGrunt_02")]
            CS_Boozu_ChargeGrunt_02,
            [EnumMember(Value = "CS_Boozu_ChargeGrunt_03")]
            CS_Boozu_ChargeGrunt_03,
            [EnumMember(Value = "CS_Boozu_Contamination_01")]
            CS_Boozu_Contamination_01,
            [EnumMember(Value = "CS_Boozu_Contamination_02")]
            CS_Boozu_Contamination_02,
            [EnumMember(Value = "CS_Boozu_Contamination_03")]
            CS_Boozu_Contamination_03,
            [EnumMember(Value = "CS_Boozu_Footstep_01")]
            CS_Boozu_Footstep_01,
            [EnumMember(Value = "CS_Boozu_Footstep_02")]
            CS_Boozu_Footstep_02,
            [EnumMember(Value = "CS_Boozu_Footstep_03")]
            CS_Boozu_Footstep_03,
            [EnumMember(Value = "CS_Boozu_Footstep_04")]
            CS_Boozu_Footstep_04,
            [EnumMember(Value = "CS_Boozu_Footstep_05")]
            CS_Boozu_Footstep_05,
            [EnumMember(Value = "CS_Boozu_Footstep_06")]
            CS_Boozu_Footstep_06,
            [EnumMember(Value = "CS_Boozu_Hurt_Dead_Grunt_01")]
            CS_Boozu_Hurt_Dead_Grunt_01,
            [EnumMember(Value = "CS_Boozu_Hurt_Dead_Grunt_02")]
            CS_Boozu_Hurt_Dead_Grunt_02,
            [EnumMember(Value = "CS_Boozu_Hurt_Dead_Grunt_03")]
            CS_Boozu_Hurt_Dead_Grunt_03,
            [EnumMember(Value = "CS_Boozu_RageGrunt_01")]
            CS_Boozu_RageGrunt_01,
            [EnumMember(Value = "CS_Boozu_RageGrunt_02")]
            CS_Boozu_RageGrunt_02,
            [EnumMember(Value = "CS_Boozu_Shake_01")]
            CS_Boozu_Shake_01,
            [EnumMember(Value = "CS_Boozu_Shake_02")]
            CS_Boozu_Shake_02,
            [EnumMember(Value = "CS_Boozu_Shake_03")]
            CS_Boozu_Shake_03,
            [EnumMember(Value = "CS_Boozu_Snort_01")]
            CS_Boozu_Snort_01,
            [EnumMember(Value = "CS_Boozu_Snort_02")]
            CS_Boozu_Snort_02,
            [EnumMember(Value = "CS_Boozu_Snort_03")]
            CS_Boozu_Snort_03,
            [EnumMember(Value = "CS_GolemShielded_ActionBreath_01")]
            CS_GolemShielded_ActionBreath_01 = 51520,
            [EnumMember(Value = "CS_GolemShielded_ActionBreath_02")]
            CS_GolemShielded_ActionBreath_02,
            [EnumMember(Value = "CS_GolemShielded_ActionBreath_03")]
            CS_GolemShielded_ActionBreath_03,
            [EnumMember(Value = "CS_GolemShielded_ActionBreath_04")]
            CS_GolemShielded_ActionBreath_04,
            [EnumMember(Value = "CS_GolemShielded_ActionBreath_05")]
            CS_GolemShielded_ActionBreath_05,
            [EnumMember(Value = "CS_GolemShielded_ActionBreath_06")]
            CS_GolemShielded_ActionBreath_06,
            [EnumMember(Value = "CS_GolemShielded_ActionBreath_07")]
            CS_GolemShielded_ActionBreath_07,
            [EnumMember(Value = "CS_GolemShielded_ActionBreath_08")]
            CS_GolemShielded_ActionBreath_08,
            [EnumMember(Value = "CS_GolemShielded_ActionBreath_09")]
            CS_GolemShielded_ActionBreath_09,
            [EnumMember(Value = "CS_GolemShielded_ActionBreath_10")]
            CS_GolemShielded_ActionBreath_10,
            [EnumMember(Value = "CS_GolemShielded_ActionBreath_11")]
            CS_GolemShielded_ActionBreath_11,
            [EnumMember(Value = "CS_GolemShielded_AttackBuildUp_01")]
            CS_GolemShielded_AttackBuildUp_01,
            [EnumMember(Value = "CS_GolemShielded_AttackBuildUp_02")]
            CS_GolemShielded_AttackBuildUp_02,
            [EnumMember(Value = "CS_GolemShielded_AttackBuildUp_03")]
            CS_GolemShielded_AttackBuildUp_03,
            [EnumMember(Value = "CS_GolemShielded_AttackBuildUp_04")]
            CS_GolemShielded_AttackBuildUp_04,
            [EnumMember(Value = "CS_GolemShielded_AttackBuildUp_05")]
            CS_GolemShielded_AttackBuildUp_05,
            [EnumMember(Value = "CS_GolemShielded_AttackBuildUp_06")]
            CS_GolemShielded_AttackBuildUp_06,
            [EnumMember(Value = "CS_GolemShielded_AttackBuildUp_07")]
            CS_GolemShielded_AttackBuildUp_07,
            [EnumMember(Value = "CS_GolemShielded_AttackBuildUp_08")]
            CS_GolemShielded_AttackBuildUp_08,
            [EnumMember(Value = "CS_GolemShielded_AttackBuildUp_09")]
            CS_GolemShielded_AttackBuildUp_09,
            [EnumMember(Value = "CS_GolemShielded_AttackGrunt_01")]
            CS_GolemShielded_AttackGrunt_01,
            [EnumMember(Value = "CS_GolemShielded_AttackGrunt_02")]
            CS_GolemShielded_AttackGrunt_02,
            [EnumMember(Value = "CS_GolemShielded_AttackGrunt_03")]
            CS_GolemShielded_AttackGrunt_03,
            [EnumMember(Value = "CS_GolemShielded_AttackGrunt_04")]
            CS_GolemShielded_AttackGrunt_04,
            [EnumMember(Value = "CS_GolemShielded_AttackGrunt_05")]
            CS_GolemShielded_AttackGrunt_05,
            [EnumMember(Value = "CS_GolemShielded_AttackGrunt_06")]
            CS_GolemShielded_AttackGrunt_06,
            [EnumMember(Value = "CS_GolemShielded_AttackGrunt_07")]
            CS_GolemShielded_AttackGrunt_07,
            [EnumMember(Value = "CS_GolemShielded_AttackGrunt_08")]
            CS_GolemShielded_AttackGrunt_08,
            [EnumMember(Value = "CS_GolemShielded_AttackGrunt_09")]
            CS_GolemShielded_AttackGrunt_09,
            [EnumMember(Value = "CS_GolemShielded_AttackGrunt_10")]
            CS_GolemShielded_AttackGrunt_10,
            [EnumMember(Value = "CS_GolemShielded_AttackGrunt_11")]
            CS_GolemShielded_AttackGrunt_11,
            [EnumMember(Value = "CS_GolemShielded_AttackShot_Explosion_01")]
            CS_GolemShielded_AttackShot_Explosion_01,
            [EnumMember(Value = "CS_GolemShielded_AttackShot_Explosion_02")]
            CS_GolemShielded_AttackShot_Explosion_02,
            [EnumMember(Value = "CS_GolemShielded_AttackShot_Explosion_03")]
            CS_GolemShielded_AttackShot_Explosion_03,
            [EnumMember(Value = "CS_GolemShielded_AttackShot_Explosion_04")]
            CS_GolemShielded_AttackShot_Explosion_04,
            [EnumMember(Value = "CS_GolemShielded_AttackShot_Explosion_05")]
            CS_GolemShielded_AttackShot_Explosion_05,
            [EnumMember(Value = "CS_GolemShielded_AttackShot_Explosion_06")]
            CS_GolemShielded_AttackShot_Explosion_06,
            [EnumMember(Value = "CS_GolemShielded_AttackShot_Explosion_07")]
            CS_GolemShielded_AttackShot_Explosion_07,
            [EnumMember(Value = "CS_GolemShielded_AttackShot_Explosion_08")]
            CS_GolemShielded_AttackShot_Explosion_08,
            [EnumMember(Value = "CS_GolemShielded_HurtGrunt_01")]
            CS_GolemShielded_HurtGrunt_01,
            [EnumMember(Value = "CS_GolemShielded_HurtGrunt_02")]
            CS_GolemShielded_HurtGrunt_02,
            [EnumMember(Value = "CS_GolemShielded_HurtGrunt_03")]
            CS_GolemShielded_HurtGrunt_03,
            [EnumMember(Value = "CS_GolemShielded_HurtGrunt_04")]
            CS_GolemShielded_HurtGrunt_04,
            [EnumMember(Value = "CS_GolemShielded_MoveClose_01")]
            CS_GolemShielded_MoveClose_01,
            [EnumMember(Value = "CS_GolemShielded_MoveClose_02")]
            CS_GolemShielded_MoveClose_02,
            [EnumMember(Value = "CS_GolemShielded_ShieldImpact_01")]
            CS_GolemShielded_ShieldImpact_01,
            [EnumMember(Value = "CS_GolemShielded_ShieldImpact_02")]
            CS_GolemShielded_ShieldImpact_02,
            [EnumMember(Value = "CS_GolemShielded_ShieldImpact_03")]
            CS_GolemShielded_ShieldImpact_03,
            [EnumMember(Value = "CS_GolemShielded_ShieldImpact_04")]
            CS_GolemShielded_ShieldImpact_04,
            [EnumMember(Value = "CS_Pure_Illuminator_Attack_Swing_01")]
            CS_Pure_Illuminator_Attack_Swing_01 = 51570,
            [EnumMember(Value = "CS_Pure_Illuminator_Attack_Swing_02")]
            CS_Pure_Illuminator_Attack_Swing_02,
            [EnumMember(Value = "CS_Pure_Illuminator_Attack_Swing_03")]
            CS_Pure_Illuminator_Attack_Swing_03,
            [EnumMember(Value = "CS_Pure_Illuminator_Attack_Swing_04")]
            CS_Pure_Illuminator_Attack_Swing_04,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackDouble_Grunt_01")]
            CS_Pure_Illuminator_AttackDouble_Grunt_01,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackDouble_Grunt_02")]
            CS_Pure_Illuminator_AttackDouble_Grunt_02,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackGrunt_01")]
            CS_Pure_Illuminator_AttackGrunt_01,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackGrunt_02")]
            CS_Pure_Illuminator_AttackGrunt_02,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackGrunt_03")]
            CS_Pure_Illuminator_AttackGrunt_03,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackGrunt_04")]
            CS_Pure_Illuminator_AttackGrunt_04,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackGrunt_05")]
            CS_Pure_Illuminator_AttackGrunt_05,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackGrunt_06")]
            CS_Pure_Illuminator_AttackGrunt_06,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackGrunt_07")]
            CS_Pure_Illuminator_AttackGrunt_07,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackGrunt_08")]
            CS_Pure_Illuminator_AttackGrunt_08,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackSnort_01")]
            CS_Pure_Illuminator_AttackSnort_01,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackSnort_02")]
            CS_Pure_Illuminator_AttackSnort_02,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackSnort_03")]
            CS_Pure_Illuminator_AttackSnort_03,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackSnort_04")]
            CS_Pure_Illuminator_AttackSnort_04,
            [EnumMember(Value = "CS_Pure_Illuminator_AttackSnort_05")]
            CS_Pure_Illuminator_AttackSnort_05,
            [EnumMember(Value = "CS_Pure_Illuminator_FistImpact_01")]
            CS_Pure_Illuminator_FistImpact_01,
            [EnumMember(Value = "CS_Pure_Illuminator_FistImpact_02")]
            CS_Pure_Illuminator_FistImpact_02,
            [EnumMember(Value = "CS_Pure_Illuminator_FistImpact_03")]
            CS_Pure_Illuminator_FistImpact_03,
            [EnumMember(Value = "CS_Pure_Illuminator_Footsteps_02")]
            CS_Pure_Illuminator_Footsteps_02,
            [EnumMember(Value = "CS_Pure_Illuminator_Footsteps_03")]
            CS_Pure_Illuminator_Footsteps_03,
            [EnumMember(Value = "CS_Pure_Illuminator_Footsteps_04")]
            CS_Pure_Illuminator_Footsteps_04,
            [EnumMember(Value = "CS_Pure_Illuminator_Footsteps_05")]
            CS_Pure_Illuminator_Footsteps_05,
            [EnumMember(Value = "CS_Pure_Illuminator_Footsteps_06")]
            CS_Pure_Illuminator_Footsteps_06,
            [EnumMember(Value = "CS_Pure_Illuminator_Footsteps_07")]
            CS_Pure_Illuminator_Footsteps_07,
            [EnumMember(Value = "CS_Pure_Illuminator_HeavyImpactGround_01")]
            CS_Pure_Illuminator_HeavyImpactGround_01,
            [EnumMember(Value = "CS_Pure_Illuminator_HeavyImpactGround_02")]
            CS_Pure_Illuminator_HeavyImpactGround_02,
            [EnumMember(Value = "CS_Pure_Illuminator_HeavyImpactGround_03")]
            CS_Pure_Illuminator_HeavyImpactGround_03,
            [EnumMember(Value = "CS_Pure_Illuminator_HurtGrunt_01")]
            CS_Pure_Illuminator_HurtGrunt_01,
            [EnumMember(Value = "CS_Pure_Illuminator_HurtGrunt_02")]
            CS_Pure_Illuminator_HurtGrunt_02,
            [EnumMember(Value = "CS_Pure_Illuminator_HurtGrunt_03")]
            CS_Pure_Illuminator_HurtGrunt_03,
            [EnumMember(Value = "CS_Pure_Illuminator_HurtGrunt_04")]
            CS_Pure_Illuminator_HurtGrunt_04,
            [EnumMember(Value = "CS_Pure_Illuminator_HurtGrunt_05")]
            CS_Pure_Illuminator_HurtGrunt_05,
            [EnumMember(Value = "CS_Pure_Illuminator_HurtGrunt_06")]
            CS_Pure_Illuminator_HurtGrunt_06,
            [EnumMember(Value = "CS_LichRust_Command_AsTram_01")]
            CS_LichRust_Command_AsTram_01 = 51610,
            [EnumMember(Value = "CS_LichRust_Command_AsTram_02")]
            CS_LichRust_Command_AsTram_02,
            [EnumMember(Value = "CS_LichRust_Command_AsTram_03")]
            CS_LichRust_Command_AsTram_03,
            [EnumMember(Value = "CS_LichRust_Command_DraGoRe_01")]
            CS_LichRust_Command_DraGoRe_01,
            [EnumMember(Value = "CS_LichRust_Command_DraGoRe_02")]
            CS_LichRust_Command_DraGoRe_02,
            [EnumMember(Value = "CS_LichRust_Command_DraGoRe_03")]
            CS_LichRust_Command_DraGoRe_03,
            [EnumMember(Value = "CS_LichRust_Command_FuGoLeMas_01")]
            CS_LichRust_Command_FuGoLeMas_01,
            [EnumMember(Value = "CS_LichRust_Command_FuGoLeMas_02")]
            CS_LichRust_Command_FuGoLeMas_02,
            [EnumMember(Value = "CS_LichRust_Command_GorTis_01")]
            CS_LichRust_Command_GorTis_01,
            [EnumMember(Value = "CS_LichRust_Command_GorTis_02")]
            CS_LichRust_Command_GorTis_02,
            [EnumMember(Value = "CS_LichRust_Command_OrTraDas_01")]
            CS_LichRust_Command_OrTraDas_01,
            [EnumMember(Value = "CS_LichRust_Command_OrTraDas_02")]
            CS_LichRust_Command_OrTraDas_02,
            [EnumMember(Value = "CS_LichRust_Command_Spell_01")]
            CS_LichRust_Command_Spell_01,
            [EnumMember(Value = "CS_LichRust_Command_Spell_02")]
            CS_LichRust_Command_Spell_02,
            [EnumMember(Value = "CS_LichRust_Command_Spell_03")]
            CS_LichRust_Command_Spell_03,
            [EnumMember(Value = "CS_LichRust_Command_Spell_04")]
            CS_LichRust_Command_Spell_04,
            [EnumMember(Value = "CS_LichRust_Spell")]
            CS_LichRust_Spell,
            [EnumMember(Value = "CS_LichRust_TeleportIn_01")]
            CS_LichRust_TeleportIn_01,
            [EnumMember(Value = "CS_LichRust_TeleportIn_02")]
            CS_LichRust_TeleportIn_02,
            [EnumMember(Value = "CS_LichRust_TeleportIn_03")]
            CS_LichRust_TeleportIn_03,
            [EnumMember(Value = "CS_LichRust_TeleportOut_01")]
            CS_LichRust_TeleportOut_01,
            [EnumMember(Value = "CS_LichRust_TeleportOut_02")]
            CS_LichRust_TeleportOut_02,
            [EnumMember(Value = "CS_LichRust_TeleportOut_03")]
            CS_LichRust_TeleportOut_03,
            [EnumMember(Value = "CS_LichRust_Dialogue_01")]
            CS_LichRust_Dialogue_01,
            [EnumMember(Value = "CS_LichRust_Dialogue_02")]
            CS_LichRust_Dialogue_02,
            [EnumMember(Value = "CS_LichRust_Dialogue_03")]
            CS_LichRust_Dialogue_03,
            [EnumMember(Value = "CS_LichRust_Dialogue_04")]
            CS_LichRust_Dialogue_04,
            [EnumMember(Value = "CS_LichRust_Dialogue_05")]
            CS_LichRust_Dialogue_05,
            [EnumMember(Value = "CS_LichRust_HurtGrunt_01")]
            CS_LichRust_HurtGrunt_01,
            [EnumMember(Value = "CS_LichRust_HurtGrunt_02")]
            CS_LichRust_HurtGrunt_02,
            [EnumMember(Value = "CS_LichRust_HurtGrunt_03")]
            CS_LichRust_HurtGrunt_03,
            [EnumMember(Value = "CS_LichRust_HurtGrunt_04")]
            CS_LichRust_HurtGrunt_04,
            [EnumMember(Value = "CS_Broken_Golem_Attack_Grunt_01")]
            CS_Broken_Golem_Attack_Grunt_01 = 51660,
            [EnumMember(Value = "CS_Broken_Golem_Attack_Grunt_02")]
            CS_Broken_Golem_Attack_Grunt_02,
            [EnumMember(Value = "CS_Broken_Golem_Attack_Grunt_03")]
            CS_Broken_Golem_Attack_Grunt_03,
            [EnumMember(Value = "CS_Broken_Golem_Bodyfall_01")]
            CS_Broken_Golem_Bodyfall_01,
            [EnumMember(Value = "CS_Broken_Golem_Bodyfall_02")]
            CS_Broken_Golem_Bodyfall_02,
            [EnumMember(Value = "CS_Broken_Golem_Bodyfall_03")]
            CS_Broken_Golem_Bodyfall_03,
            [EnumMember(Value = "CS_Broken_Golem_Disfonction_01")]
            CS_Broken_Golem_Disfonction_01,
            [EnumMember(Value = "CS_Broken_Golem_Disfonction_02")]
            CS_Broken_Golem_Disfonction_02,
            [EnumMember(Value = "CS_Broken_Golem_Disfonction_03")]
            CS_Broken_Golem_Disfonction_03,
            [EnumMember(Value = "CS_Broken_Golem_Dodge_Whoosh_01")]
            CS_Broken_Golem_Dodge_Whoosh_01,
            [EnumMember(Value = "CS_Broken_Golem_Dodge_Whoosh_02")]
            CS_Broken_Golem_Dodge_Whoosh_02,
            [EnumMember(Value = "CS_Broken_Golem_Heavy_Attack_Whoosh_01")]
            CS_Broken_Golem_Heavy_Attack_Whoosh_01,
            [EnumMember(Value = "CS_Broken_Golem_Heavy_Attack_Whoosh_02")]
            CS_Broken_Golem_Heavy_Attack_Whoosh_02,
            [EnumMember(Value = "CS_Broken_Golem_Heavy_Attack_Fence_Whoosh_01")]
            CS_Broken_Golem_Heavy_Attack_Fence_Whoosh_01,
            [EnumMember(Value = "CS_Broken_Golem_Heavy_Attack_Fence_Whoosh_02")]
            CS_Broken_Golem_Heavy_Attack_Fence_Whoosh_02,
            [EnumMember(Value = "CS_Broken_Golem_Heavy_Attack_Grunt_01")]
            CS_Broken_Golem_Heavy_Attack_Grunt_01,
            [EnumMember(Value = "CS_Broken_Golem_Heavy_Attack_Grunt_02")]
            CS_Broken_Golem_Heavy_Attack_Grunt_02,
            [EnumMember(Value = "CS_Broken_Golem_Heavy_Attack_Grunt_03")]
            CS_Broken_Golem_Heavy_Attack_Grunt_03,
            [EnumMember(Value = "CS_Broken_Golem_Heavy_attack_Whoosh_01")]
            CS_Broken_Golem_Heavy_attack_Whoosh_01,
            [EnumMember(Value = "CS_Broken_Golem_Heavy_attack_Whoosh_02")]
            CS_Broken_Golem_Heavy_attack_Whoosh_02,
            [EnumMember(Value = "CS_Broken_Golem_Heavy_Attack3_Spin_Whoosh_01")]
            CS_Broken_Golem_Heavy_Attack3_Spin_Whoosh_01,
            [EnumMember(Value = "CS_Broken_Golem_Heavy_Attack3_Spin_Whoosh_02")]
            CS_Broken_Golem_Heavy_Attack3_Spin_Whoosh_02,
            [EnumMember(Value = "CS_Broken_Golem_Hurt_Hit_01")]
            CS_Broken_Golem_Hurt_Hit_01,
            [EnumMember(Value = "CS_Broken_Golem_Hurt_Hit_02")]
            CS_Broken_Golem_Hurt_Hit_02,
            [EnumMember(Value = "CS_Broken_Golem_Hurt_Hit_03")]
            CS_Broken_Golem_Hurt_Hit_03,
            [EnumMember(Value = "CS_Broken_Golem_Normal_Attack2_Whoosh_01")]
            CS_Broken_Golem_Normal_Attack2_Whoosh_01,
            [EnumMember(Value = "CS_Broken_Golem_Normal_Attack2_Whoosh_02")]
            CS_Broken_Golem_Normal_Attack2_Whoosh_02,
            [EnumMember(Value = "CS_Broken_Golem_Run_Wind_Loop")]
            CS_Broken_Golem_Run_Wind_Loop,
            [EnumMember(Value = "CS_Broken_Golem_Attack_Run_Whoosh_01")]
            CS_Broken_Golem_Attack_Run_Whoosh_01,
            [EnumMember(Value = "CS_Broken_Golem_Attack_Run_Whoosh_02")]
            CS_Broken_Golem_Attack_Run_Whoosh_02,
            [EnumMember(Value = "CS_Broken_Golem_Attack_Run_Whoosh_03")]
            CS_Broken_Golem_Attack_Run_Whoosh_03,
            [EnumMember(Value = "CS_Broken_Golem_Footstep_01")]
            CS_Broken_Golem_Footstep_01,
            [EnumMember(Value = "CS_Broken_Golem_Footstep_02")]
            CS_Broken_Golem_Footstep_02,
            [EnumMember(Value = "CS_Broken_Golem_Footstep_03")]
            CS_Broken_Golem_Footstep_03,
            [EnumMember(Value = "CS_Broken_Golem_Footstep_04")]
            CS_Broken_Golem_Footstep_04,
            [EnumMember(Value = "CS_Broken_Golem_Footstep_05")]
            CS_Broken_Golem_Footstep_05,
            [EnumMember(Value = "CS_Broken_Golem_Footstep_06")]
            CS_Broken_Golem_Footstep_06,
            [EnumMember(Value = "CS_Broken_Golem_Normal_Attack_Whoosh_01")]
            CS_Broken_Golem_Normal_Attack_Whoosh_01,
            [EnumMember(Value = "CS_Broken_Golem_Normal_Attack_Whoosh_02")]
            CS_Broken_Golem_Normal_Attack_Whoosh_02,
            [EnumMember(Value = "CS_GiantHorror_ArmReconnect_01")]
            CS_GiantHorror_ArmReconnect_01 = 51700,
            [EnumMember(Value = "CS_GiantHorror_ArmReconnect_02")]
            CS_GiantHorror_ArmReconnect_02,
            [EnumMember(Value = "CS_GiantHorror_ArmSpread_01")]
            CS_GiantHorror_ArmSpread_01,
            [EnumMember(Value = "CS_GiantHorror_ArmSpread_02")]
            CS_GiantHorror_ArmSpread_02,
            [EnumMember(Value = "CS_GiantHorror_Attack_Grunt_01")]
            CS_GiantHorror_Attack_Grunt_01,
            [EnumMember(Value = "CS_GiantHorror_Attack_Grunt_02")]
            CS_GiantHorror_Attack_Grunt_02,
            [EnumMember(Value = "CS_GiantHorror_Attack_Grunt_03")]
            CS_GiantHorror_Attack_Grunt_03,
            [EnumMember(Value = "CS_GiantHorror_Attack_Grunt_04")]
            CS_GiantHorror_Attack_Grunt_04,
            [EnumMember(Value = "CS_GiantHorror_Attack_Scream_01")]
            CS_GiantHorror_Attack_Scream_01,
            [EnumMember(Value = "CS_GiantHorror_Attack_Scream_02")]
            CS_GiantHorror_Attack_Scream_02,
            [EnumMember(Value = "CS_GiantHorror_Attack_Snort_01")]
            CS_GiantHorror_Attack_Snort_01,
            [EnumMember(Value = "CS_GiantHorror_Attack_Snort_02")]
            CS_GiantHorror_Attack_Snort_02,
            [EnumMember(Value = "CS_GiantHorror_Attack_Snort_03")]
            CS_GiantHorror_Attack_Snort_03,
            [EnumMember(Value = "CS_GiantHorror_Attack_Snort_04")]
            CS_GiantHorror_Attack_Snort_04,
            [EnumMember(Value = "CS_GiantHorror_Attack_Swing_Whoosh_01")]
            CS_GiantHorror_Attack_Swing_Whoosh_01,
            [EnumMember(Value = "CS_GiantHorror_Attack_Swing_Whoosh_02")]
            CS_GiantHorror_Attack_Swing_Whoosh_02,
            [EnumMember(Value = "CS_GiantHorror_Attack_Swing_Whoosh_03")]
            CS_GiantHorror_Attack_Swing_Whoosh_03,
            [EnumMember(Value = "CS_GiantHorror_Attack_Swing_Whoosh_04")]
            CS_GiantHorror_Attack_Swing_Whoosh_04,
            [EnumMember(Value = "CS_GiantHorror_Hurt_Grunt_01")]
            CS_GiantHorror_Hurt_Grunt_01,
            [EnumMember(Value = "CS_GiantHorror_Hurt_Grunt_02")]
            CS_GiantHorror_Hurt_Grunt_02,
            [EnumMember(Value = "CS_GiantHorror_Hurt_Grunt_03")]
            CS_GiantHorror_Hurt_Grunt_03,
            [EnumMember(Value = "CS_GiantHorror_ShoulderDisconnect_Detach_01")]
            CS_GiantHorror_ShoulderDisconnect_Detach_01,
            [EnumMember(Value = "CS_GiantHorror_ShoulderDisconnect_Detach_02")]
            CS_GiantHorror_ShoulderDisconnect_Detach_02,
            [EnumMember(Value = "CS_SupremeShell_Attack_Grunt_01")]
            CS_SupremeShell_Attack_Grunt_01 = 51730,
            [EnumMember(Value = "CS_SupremeShell_Attack_Grunt_02")]
            CS_SupremeShell_Attack_Grunt_02,
            [EnumMember(Value = "CS_SupremeShell_Attack_Grunt_03")]
            CS_SupremeShell_Attack_Grunt_03,
            [EnumMember(Value = "CS_SupremeShell_Attack_Grunt_04")]
            CS_SupremeShell_Attack_Grunt_04,
            [EnumMember(Value = "CS_SupremeShell_Attack_Grunt_05")]
            CS_SupremeShell_Attack_Grunt_05,
            [EnumMember(Value = "CS_SupremeShell_AttackSwingWhoosh_01")]
            CS_SupremeShell_AttackSwingWhoosh_01,
            [EnumMember(Value = "CS_SupremeShell_AttackSwingWhoosh_02")]
            CS_SupremeShell_AttackSwingWhoosh_02,
            [EnumMember(Value = "CS_SupremeShell_AttackSwingWhoosh_03")]
            CS_SupremeShell_AttackSwingWhoosh_03,
            [EnumMember(Value = "CS_SupremeShell_AttackSwingWhoosh_04")]
            CS_SupremeShell_AttackSwingWhoosh_04,
            [EnumMember(Value = "CS_SupremeShell_AttackSwingWhoosh_05")]
            CS_SupremeShell_AttackSwingWhoosh_05,
            [EnumMember(Value = "CS_SupremeShell_AttackSwingWhoosh_06")]
            CS_SupremeShell_AttackSwingWhoosh_06,
            [EnumMember(Value = "CS_SupremeShell_AttackSwingWhoosh_07")]
            CS_SupremeShell_AttackSwingWhoosh_07,
            [EnumMember(Value = "CS_SupremeShell_AttackSwingWhoosh_Heavy_01")]
            CS_SupremeShell_AttackSwingWhoosh_Heavy_01,
            [EnumMember(Value = "CS_SupremeShell_AttackSwingWhoosh_Heavy_02")]
            CS_SupremeShell_AttackSwingWhoosh_Heavy_02,
            [EnumMember(Value = "CS_SupremeShell_BodyFall_01")]
            CS_SupremeShell_BodyFall_01,
            [EnumMember(Value = "CS_SupremeShell_BodyFall_02")]
            CS_SupremeShell_BodyFall_02,
            [EnumMember(Value = "CS_SupremeShell_BodyFall_03")]
            CS_SupremeShell_BodyFall_03,
            [EnumMember(Value = "CS_SupremeShell_BodyFall_Raise_01")]
            CS_SupremeShell_BodyFall_Raise_01,
            [EnumMember(Value = "CS_SupremeShell_BodyFall_Raise_02")]
            CS_SupremeShell_BodyFall_Raise_02,
            [EnumMember(Value = "CS_SupremeShell_BodyFall_Raise_03")]
            CS_SupremeShell_BodyFall_Raise_03,
            [EnumMember(Value = "CS_SupremeShell_Fist_Impact_01")]
            CS_SupremeShell_Fist_Impact_01,
            [EnumMember(Value = "CS_SupremeShell_Fist_Impact_02")]
            CS_SupremeShell_Fist_Impact_02,
            [EnumMember(Value = "CS_SupremeShell_Fist_Impact_03")]
            CS_SupremeShell_Fist_Impact_03,
            [EnumMember(Value = "CS_SupremeShell_Fist_Impact_04")]
            CS_SupremeShell_Fist_Impact_04,
            [EnumMember(Value = "CS_SupremeShell_Fist_Impact_05")]
            CS_SupremeShell_Fist_Impact_05,
            [EnumMember(Value = "CS_SupremeShell_Fist_Impact_06")]
            CS_SupremeShell_Fist_Impact_06,
            [EnumMember(Value = "CS_SupremeShell_Footsteps_01")]
            CS_SupremeShell_Footsteps_01,
            [EnumMember(Value = "CS_SupremeShell_Footsteps_02")]
            CS_SupremeShell_Footsteps_02,
            [EnumMember(Value = "CS_SupremeShell_Footsteps_03")]
            CS_SupremeShell_Footsteps_03,
            [EnumMember(Value = "CS_SupremeShell_Footsteps_04")]
            CS_SupremeShell_Footsteps_04,
            [EnumMember(Value = "CS_SupremeShell_Footsteps_05")]
            CS_SupremeShell_Footsteps_05,
            [EnumMember(Value = "CS_SupremeShell_Footsteps_06")]
            CS_SupremeShell_Footsteps_06,
            [EnumMember(Value = "CS_SupremeShell_HeavyImpact_01")]
            CS_SupremeShell_HeavyImpact_01,
            [EnumMember(Value = "CS_SupremeShell_HeavyImpact_02")]
            CS_SupremeShell_HeavyImpact_02,
            [EnumMember(Value = "CS_SupremeShell_HeavyImpact_03")]
            CS_SupremeShell_HeavyImpact_03,
            [EnumMember(Value = "CS_SupremeShell_HeavyImpact_04")]
            CS_SupremeShell_HeavyImpact_04,
            [EnumMember(Value = "CS_SupremeShell_HeavyImpactGround_01")]
            CS_SupremeShell_HeavyImpactGround_01,
            [EnumMember(Value = "CS_SupremeShell_HeavyImpactGround_02")]
            CS_SupremeShell_HeavyImpactGround_02,
            [EnumMember(Value = "CS_SupremeShell_HurtGrunt_01")]
            CS_SupremeShell_HurtGrunt_01,
            [EnumMember(Value = "CS_SupremeShell_HurtGrunt_02")]
            CS_SupremeShell_HurtGrunt_02,
            [EnumMember(Value = "CS_SupremeShell_HurtGrunt_03")]
            CS_SupremeShell_HurtGrunt_03,
            [EnumMember(Value = "CS_SupremeShell_HurtImpact_01")]
            CS_SupremeShell_HurtImpact_01,
            [EnumMember(Value = "CS_SupremeShell_HurtImpact_02")]
            CS_SupremeShell_HurtImpact_02,
            [EnumMember(Value = "CS_SupremeShell_HurtImpact_03")]
            CS_SupremeShell_HurtImpact_03,
            [EnumMember(Value = "CS_SupremeShell_Idle_OpenHead_01")]
            CS_SupremeShell_Idle_OpenHead_01,
            [EnumMember(Value = "CS_SupremeShell_Idle_OpenHead_02")]
            CS_SupremeShell_Idle_OpenHead_02,
            [EnumMember(Value = "CS_SupremeShell_Rage_Scream_01")]
            CS_SupremeShell_Rage_Scream_01,
            [EnumMember(Value = "CS_SupremeShell_Rage_Scream_02")]
            CS_SupremeShell_Rage_Scream_02,
            [EnumMember(Value = "CS_SupremeShell_RageScream_01")]
            CS_SupremeShell_RageScream_01,
            [EnumMember(Value = "CS_SupremeShell_RageScream_02")]
            CS_SupremeShell_RageScream_02,
            [EnumMember(Value = "CS_SupremeShell_RageScream_03")]
            CS_SupremeShell_RageScream_03,
            [EnumMember(Value = "CS_SupremeShell_Raise_01")]
            CS_SupremeShell_Raise_01,
            [EnumMember(Value = "CS_SupremeShell_Raise_02")]
            CS_SupremeShell_Raise_02,
            [EnumMember(Value = "CS_SupremeShell_Raise_03")]
            CS_SupremeShell_Raise_03,
            [EnumMember(Value = "CS_SupremeShell_ShockWave_Rumble_01")]
            CS_SupremeShell_ShockWave_Rumble_01,
            [EnumMember(Value = "CS_SupremeShell_ShockWave_Rumble_02")]
            CS_SupremeShell_ShockWave_Rumble_02,
            [EnumMember(Value = "CS_SupremeShell_SkillLaser_Shot_01")]
            CS_SupremeShell_SkillLaser_Shot_01,
            [EnumMember(Value = "CS_SupremeShell_SkillLaser_Shot_02")]
            CS_SupremeShell_SkillLaser_Shot_02,
            [EnumMember(Value = "CS_SupremeShell_SkillLaser_Shot_Loop")]
            CS_SupremeShell_SkillLaser_Shot_Loop,
            [EnumMember(Value = "CS_SupremeShell_Snort_01")]
            CS_SupremeShell_Snort_01,
            [EnumMember(Value = "CS_SupremeShell_Snort_02")]
            CS_SupremeShell_Snort_02,
            [EnumMember(Value = "CS_SupremeShell_Snort_03")]
            CS_SupremeShell_Snort_03,
            [EnumMember(Value = "CS_Bloody_Beast_Bite_Attack_Grunt_01")]
            CS_Bloody_Beast_Bite_Attack_Grunt_01 = 51801,
            [EnumMember(Value = "CS_Bloody_Beast_Bite_Attack_Grunt_02")]
            CS_Bloody_Beast_Bite_Attack_Grunt_02,
            [EnumMember(Value = "CS_Bloody_Beast_Bite_Attack_Grunt_03")]
            CS_Bloody_Beast_Bite_Attack_Grunt_03,
            [EnumMember(Value = "CS_Bloody_Beast_Bite_Attack_Grunt_04")]
            CS_Bloody_Beast_Bite_Attack_Grunt_04,
            [EnumMember(Value = "CS_Bloody_Beast_Howl_Call_01")]
            CS_Bloody_Beast_Howl_Call_01,
            [EnumMember(Value = "CS_Bloody_Beast_Howl_Call_02")]
            CS_Bloody_Beast_Howl_Call_02,
            [EnumMember(Value = "CS_Bloody_Beast_Howl_Call_03")]
            CS_Bloody_Beast_Howl_Call_03,
            [EnumMember(Value = "CS_Bloody_Beast_Hurt_Grunt_01")]
            CS_Bloody_Beast_Hurt_Grunt_01,
            [EnumMember(Value = "CS_Bloody_Beast_Hurt_Grunt_02")]
            CS_Bloody_Beast_Hurt_Grunt_02,
            [EnumMember(Value = "CS_Bloody_Beast_Hurt_Grunt_03")]
            CS_Bloody_Beast_Hurt_Grunt_03,
            [EnumMember(Value = "CS_Bloody_Beast_Run_Attack_Grunt_01")]
            CS_Bloody_Beast_Run_Attack_Grunt_01,
            [EnumMember(Value = "CS_Bloody_Beast_Run_Attack_Grunt_02")]
            CS_Bloody_Beast_Run_Attack_Grunt_02,
            [EnumMember(Value = "CS_Bloody_Beast_Run_Attack_Grunt_03")]
            CS_Bloody_Beast_Run_Attack_Grunt_03,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Attack_Grunt_01")]
            CS_Broken_Beast_Golem_Attack_Grunt_01 = 51820,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Attack_Grunt_02")]
            CS_Broken_Beast_Golem_Attack_Grunt_02,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Attack_Grunt_03")]
            CS_Broken_Beast_Golem_Attack_Grunt_03,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Attack_Short_Grunt_01")]
            CS_Broken_Beast_Golem_Attack_Short_Grunt_01,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Attack_Short_Grunt_02")]
            CS_Broken_Beast_Golem_Attack_Short_Grunt_02,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Attack_Short_Grunt_03")]
            CS_Broken_Beast_Golem_Attack_Short_Grunt_03,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Attack_Whoosh_01")]
            CS_Broken_Beast_Golem_Attack_Whoosh_01,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Attack_Whoosh_02")]
            CS_Broken_Beast_Golem_Attack_Whoosh_02,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Attack_Whoosh_03")]
            CS_Broken_Beast_Golem_Attack_Whoosh_03,
            [EnumMember(Value = "CS_Broken_Beast_Golem_BodyFall_Impact_01")]
            CS_Broken_Beast_Golem_BodyFall_Impact_01,
            [EnumMember(Value = "CS_Broken_Beast_Golem_BodyFall_Impact_02")]
            CS_Broken_Beast_Golem_BodyFall_Impact_02,
            [EnumMember(Value = "CS_Broken_Beast_Golem_BodyFall_Impact_03")]
            CS_Broken_Beast_Golem_BodyFall_Impact_03,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Disfonction_01")]
            CS_Broken_Beast_Golem_Disfonction_01,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Disfonction_02")]
            CS_Broken_Beast_Golem_Disfonction_02,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Disfonction_03")]
            CS_Broken_Beast_Golem_Disfonction_03,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Double_Attack_Swing_01")]
            CS_Broken_Beast_Golem_Double_Attack_Swing_01,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Double_Attack_Swing_02")]
            CS_Broken_Beast_Golem_Double_Attack_Swing_02,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Double_Attack_Swing_03")]
            CS_Broken_Beast_Golem_Double_Attack_Swing_03,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Footsteps_01")]
            CS_Broken_Beast_Golem_Footsteps_01,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Footsteps_02")]
            CS_Broken_Beast_Golem_Footsteps_02,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Footsteps_03")]
            CS_Broken_Beast_Golem_Footsteps_03,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Footsteps_04")]
            CS_Broken_Beast_Golem_Footsteps_04,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Footsteps_05")]
            CS_Broken_Beast_Golem_Footsteps_05,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Footsteps_06")]
            CS_Broken_Beast_Golem_Footsteps_06,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Hurt_Grunt_01")]
            CS_Broken_Beast_Golem_Hurt_Grunt_01 = 51845,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Hurt_Grunt_03")]
            CS_Broken_Beast_Golem_Hurt_Grunt_03,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Hurt_Grunt_04")]
            CS_Broken_Beast_Golem_Hurt_Grunt_04,
            [EnumMember(Value = "CS_Broken_Beast_Golem_Hurt_Grunt_05")]
            CS_Broken_Beast_Golem_Hurt_Grunt_05,
            [EnumMember(Value = "CS_Giant_Golem_Attack_Grunt_01_01")]
            CS_Giant_Golem_Attack_Grunt_01_01 = 51850,
            [EnumMember(Value = "CS_Giant_Golem_Attack_Grunt_02_01")]
            CS_Giant_Golem_Attack_Grunt_02_01,
            [EnumMember(Value = "CS_Giant_Golem_Attack_Grunt_03_01")]
            CS_Giant_Golem_Attack_Grunt_03_01,
            [EnumMember(Value = "CS_Giant_Golem_Attack_Grunt_04_01")]
            CS_Giant_Golem_Attack_Grunt_04_01,
            [EnumMember(Value = "CS_Giant_Golem_Attack_Grunt_05_01")]
            CS_Giant_Golem_Attack_Grunt_05_01,
            [EnumMember(Value = "CS_Giant_Golem_AttackSwingWhoosh_01")]
            CS_Giant_Golem_AttackSwingWhoosh_01,
            [EnumMember(Value = "CS_Giant_Golem_AttackSwingWhoosh_02")]
            CS_Giant_Golem_AttackSwingWhoosh_02,
            [EnumMember(Value = "CS_Giant_Golem_AttackSwingWhoosh_03")]
            CS_Giant_Golem_AttackSwingWhoosh_03,
            [EnumMember(Value = "CS_Giant_Golem_AttackSwingWhoosh_04")]
            CS_Giant_Golem_AttackSwingWhoosh_04,
            [EnumMember(Value = "CS_Giant_Golem_AttackSwingWhoosh_05")]
            CS_Giant_Golem_AttackSwingWhoosh_05,
            [EnumMember(Value = "CS_Giant_Golem_AttackSwingWhoosh_06")]
            CS_Giant_Golem_AttackSwingWhoosh_06,
            [EnumMember(Value = "CS_Giant_Golem_BigSword_Unsheathe")]
            CS_Giant_Golem_BigSword_Unsheathe,
            [EnumMember(Value = "CS_Giant_Golem_Footstep_01")]
            CS_Giant_Golem_Footstep_01,
            [EnumMember(Value = "CS_Giant_Golem_Footstep_02")]
            CS_Giant_Golem_Footstep_02,
            [EnumMember(Value = "CS_Giant_Golem_Footstep_03")]
            CS_Giant_Golem_Footstep_03,
            [EnumMember(Value = "CS_Giant_Golem_Footstep_04")]
            CS_Giant_Golem_Footstep_04,
            [EnumMember(Value = "CS_Giant_Golem_Footstep_05")]
            CS_Giant_Golem_Footstep_05,
            [EnumMember(Value = "CS_Giant_Golem_Footstep_06")]
            CS_Giant_Golem_Footstep_06,
            [EnumMember(Value = "CS_Giant_Golem_HeavyImpact_01")]
            CS_Giant_Golem_HeavyImpact_01,
            [EnumMember(Value = "CS_Giant_Golem_HeavyImpact_02")]
            CS_Giant_Golem_HeavyImpact_02,
            [EnumMember(Value = "CS_Giant_Golem_HeavyImpact_03")]
            CS_Giant_Golem_HeavyImpact_03,
            [EnumMember(Value = "CS_Giant_Golem_HurtGrunt_01_01")]
            CS_Giant_Golem_HurtGrunt_01_01,
            [EnumMember(Value = "CS_Giant_Golem_HurtGrunt_02_01")]
            CS_Giant_Golem_HurtGrunt_02_01,
            [EnumMember(Value = "CS_Giant_Golem_HurtGrunt_03_01")]
            CS_Giant_Golem_HurtGrunt_03_01,
            [EnumMember(Value = "CS_Giant_Golem_HurtImpact_01")]
            CS_Giant_Golem_HurtImpact_01,
            [EnumMember(Value = "CS_Giant_Golem_HurtImpact_02")]
            CS_Giant_Golem_HurtImpact_02,
            [EnumMember(Value = "CS_Giant_Golem_HurtImpact_03")]
            CS_Giant_Golem_HurtImpact_03,
            [EnumMember(Value = "CS_Giant_Golem_HurtImpact_04")]
            CS_Giant_Golem_HurtImpact_04,
            [EnumMember(Value = "CS_Giant_Golem_HurtImpact_05")]
            CS_Giant_Golem_HurtImpact_05,
            [EnumMember(Value = "CS_Giant_Golem_AttackSpinWeapon_01")]
            CS_Giant_Golem_AttackSpinWeapon_01,
            [EnumMember(Value = "CS_Giant_Golem_AttackSpinWeapon_02")]
            CS_Giant_Golem_AttackSpinWeapon_02,
            [EnumMember(Value = "CS_AncientDweller_DeathVoices_01")]
            CS_AncientDweller_DeathVoices_01 = 51890,
            [EnumMember(Value = "CS_AncientDweller_DeathVoices_02")]
            CS_AncientDweller_DeathVoices_02,
            [EnumMember(Value = "CS_AncientDweller_Fight_Whoosh_01")]
            CS_AncientDweller_Fight_Whoosh_01,
            [EnumMember(Value = "CS_AncientDweller_Fight_Whoosh_02")]
            CS_AncientDweller_Fight_Whoosh_02,
            [EnumMember(Value = "CS_AncientDweller_Fight_Whoosh_03")]
            CS_AncientDweller_Fight_Whoosh_03,
            [EnumMember(Value = "CS_AncientDweller_HurtVoices_01")]
            CS_AncientDweller_HurtVoices_01,
            [EnumMember(Value = "CS_AncientDweller_HurtVoices_02")]
            CS_AncientDweller_HurtVoices_02,
            [EnumMember(Value = "CS_AncientDweller_HurtVoices_03")]
            CS_AncientDweller_HurtVoices_03,
            [EnumMember(Value = "CS_AncientDweller_Mermaid_Sing_01")]
            CS_AncientDweller_Mermaid_Sing_01,
            [EnumMember(Value = "CS_AncientDweller_Mermaid_Sing_02")]
            CS_AncientDweller_Mermaid_Sing_02,
            [EnumMember(Value = "CS_AncientDweller_Mermaid_Sing_03")]
            CS_AncientDweller_Mermaid_Sing_03,
            [EnumMember(Value = "CS_AncientDweller_Mermaid_Sing_04")]
            CS_AncientDweller_Mermaid_Sing_04,
            [EnumMember(Value = "CS_AncientDweller_MermaidSing_01")]
            CS_AncientDweller_MermaidSing_01,
            [EnumMember(Value = "CS_AncientDweller_MermaidSing_02")]
            CS_AncientDweller_MermaidSing_02,
            [EnumMember(Value = "CS_AncientDweller_MermaidSing_03")]
            CS_AncientDweller_MermaidSing_03,
            [EnumMember(Value = "CS_AncientDweller_MermaidSing_04")]
            CS_AncientDweller_MermaidSing_04,
            [EnumMember(Value = "CS_AncientDweller_Spell_Impact_01")]
            CS_AncientDweller_Spell_Impact_01,
            [EnumMember(Value = "CS_AncientDweller_Spell_Impact_02")]
            CS_AncientDweller_Spell_Impact_02,
            [EnumMember(Value = "CS_AncientDweller_Spell_Impact_03")]
            CS_AncientDweller_Spell_Impact_03,
            [EnumMember(Value = "CS_AncientDweller_Spell_IN")]
            CS_AncientDweller_Spell_IN,
            [EnumMember(Value = "CS_AncientDweller_Spell_Loop")]
            CS_AncientDweller_Spell_Loop = 51911,
            [EnumMember(Value = "CS_AncientDweller_Spell_OUT")]
            CS_AncientDweller_Spell_OUT,
            [EnumMember(Value = "CS_AncientDweller_Voices_01")]
            CS_AncientDweller_Voices_01,
            [EnumMember(Value = "CS_AncientDweller_Voices_02")]
            CS_AncientDweller_Voices_02,
            [EnumMember(Value = "CS_AncientDweller_Voices_03")]
            CS_AncientDweller_Voices_03,
            [EnumMember(Value = "CS_AncientDweller_Voices_04")]
            CS_AncientDweller_Voices_04,
            [EnumMember(Value = "CS_AncientDweller_Voices_05")]
            CS_AncientDweller_Voices_05,
            [EnumMember(Value = "CS_AncientDweller_Voices_06")]
            CS_AncientDweller_Voices_06,
            [EnumMember(Value = "CS_BeetleFROST_AttackDouble_Grunt_01")]
            CS_BeetleFROST_AttackDouble_Grunt_01 = 51920,
            [EnumMember(Value = "CS_BeetleFROST_AttackDouble_Grunt_02")]
            CS_BeetleFROST_AttackDouble_Grunt_02,
            [EnumMember(Value = "CS_BeetleFROST_AttackWide_Grunt_01")]
            CS_BeetleFROST_AttackWide_Grunt_01,
            [EnumMember(Value = "CS_BeetleFROST_AttackWide_Grunt_02")]
            CS_BeetleFROST_AttackWide_Grunt_02,
            [EnumMember(Value = "CS_BeetleFROST_AttackWide_Grunt_03")]
            CS_BeetleFROST_AttackWide_Grunt_03,
            [EnumMember(Value = "CS_BeetleFROST_HeavyHurt_Grunt_01")]
            CS_BeetleFROST_HeavyHurt_Grunt_01,
            [EnumMember(Value = "CS_BeetleFROST_HeavyHurt_Grunt_02")]
            CS_BeetleFROST_HeavyHurt_Grunt_02,
            [EnumMember(Value = "CS_BeetleFROST_HeavyHurt_Grunt_03")]
            CS_BeetleFROST_HeavyHurt_Grunt_03,
            [EnumMember(Value = "CS_BeetleFROST_Hurt_Grunt_01")]
            CS_BeetleFROST_Hurt_Grunt_01,
            [EnumMember(Value = "CS_BeetleFROST_Hurt_Grunt_02")]
            CS_BeetleFROST_Hurt_Grunt_02,
            [EnumMember(Value = "CS_BeetleFROST_Hurt_Grunt_03")]
            CS_BeetleFROST_Hurt_Grunt_03,
            [EnumMember(Value = "CS_BeetleFROST_Hurt_Grunt_04")]
            CS_BeetleFROST_Hurt_Grunt_04,
            [EnumMember(Value = "CS_BeetleFROST_RangeAttack_a_Grunt_01")]
            CS_BeetleFROST_RangeAttack_a_Grunt_01,
            [EnumMember(Value = "CS_BeetleFROST_RangeAttack_a_Grunt_02")]
            CS_BeetleFROST_RangeAttack_a_Grunt_02,
            [EnumMember(Value = "CS_BeetleFROST_RangeAttackHeavy_aGrowl_01")]
            CS_BeetleFROST_RangeAttackHeavy_aGrowl_01,
            [EnumMember(Value = "CS_BeetleFROST_RangeAttackHeavy_aGrowl_02")]
            CS_BeetleFROST_RangeAttackHeavy_aGrowl_02,
            [EnumMember(Value = "CS_BeetleFROST_RangeAttackHeavy_aGrowl_03")]
            CS_BeetleFROST_RangeAttackHeavy_aGrowl_03,
            [EnumMember(Value = "CS_BeetleFROST_Spell_Explosion_01")]
            CS_BeetleFROST_Spell_Explosion_01,
            [EnumMember(Value = "CS_BeetleFROST_Spell_Explosion_02")]
            CS_BeetleFROST_Spell_Explosion_02,
            [EnumMember(Value = "CS_BeetleFROST_Spell_Explosion_03")]
            CS_BeetleFROST_Spell_Explosion_03,
            [EnumMember(Value = "CS_BeetleFROST_Spell_IceSpit_01")]
            CS_BeetleFROST_Spell_IceSpit_01,
            [EnumMember(Value = "CS_BeetleFROST_Spell_IceSpit_02")]
            CS_BeetleFROST_Spell_IceSpit_02,
            [EnumMember(Value = "CS_BeetleFROST_Spell_IceSpit_03")]
            CS_BeetleFROST_Spell_IceSpit_03,
            [EnumMember(Value = "CS_BigDjinn_Incantation_01")]
            CS_BigDjinn_Incantation_01 = 51951,
            [EnumMember(Value = "CS_BigDjinn_Incantation_02")]
            CS_BigDjinn_Incantation_02,
            [EnumMember(Value = "CS_BigDjinn_Incantation_03")]
            CS_BigDjinn_Incantation_03,
            [EnumMember(Value = "CS_BigDjinn_Lantern_Electricity_Explode_01")]
            CS_BigDjinn_Lantern_Electricity_Explode_01,
            [EnumMember(Value = "CS_BigDjinn_Lantern_Electricity_Explode_02")]
            CS_BigDjinn_Lantern_Electricity_Explode_02,
            [EnumMember(Value = "CS_BigDjinn_Lantern_Electricity_Spawn_01")]
            CS_BigDjinn_Lantern_Electricity_Spawn_01,
            [EnumMember(Value = "CS_BigDjinn_Lantern_Electricity_Spawn_02")]
            CS_BigDjinn_Lantern_Electricity_Spawn_02,
            [EnumMember(Value = "CS_BigDjinn_Lantern_Shoot_01")]
            CS_BigDjinn_Lantern_Shoot_01,
            [EnumMember(Value = "CS_BigDjinn_Lantern_Shoot_02")]
            CS_BigDjinn_Lantern_Shoot_02,
            [EnumMember(Value = "CS_BigDjinn_Lantern_Shoot_03")]
            CS_BigDjinn_Lantern_Shoot_03,
            [EnumMember(Value = "CS_BigDjinn_Lantern_Shoot_04")]
            CS_BigDjinn_Lantern_Shoot_04,
            [EnumMember(Value = "CS_BigDjinn_Laugh_01")]
            CS_BigDjinn_Laugh_01,
            [EnumMember(Value = "CS_BigDjinn_Laugh_02")]
            CS_BigDjinn_Laugh_02,
            [EnumMember(Value = "CS_BigDjinn_Laugh_03")]
            CS_BigDjinn_Laugh_03,
            [EnumMember(Value = "CS_BigDjinn_Laugh_04")]
            CS_BigDjinn_Laugh_04,
            [EnumMember(Value = "CS_BigDjinn_Laugh_05")]
            CS_BigDjinn_Laugh_05,
            [EnumMember(Value = "CS_BigDjinn_Laugh_06")]
            CS_BigDjinn_Laugh_06,
            [EnumMember(Value = "CS_BigDjinn_Laugh_07")]
            CS_BigDjinn_Laugh_07,
            [EnumMember(Value = "CS_BigDjinn_Laugh_08")]
            CS_BigDjinn_Laugh_08,
            [EnumMember(Value = "CS_BigDjinn_Laugh_09")]
            CS_BigDjinn_Laugh_09,
            [EnumMember(Value = "CS_BigDjinn_Voice_Hurt_01")]
            CS_BigDjinn_Voice_Hurt_01,
            [EnumMember(Value = "CS_BigDjinn_Voice_Hurt_02")]
            CS_BigDjinn_Voice_Hurt_02,
            [EnumMember(Value = "CS_BigDjinn_Voice_01")]
            CS_BigDjinn_Voice_01,
            [EnumMember(Value = "CS_BigDjinn_Voice_02")]
            CS_BigDjinn_Voice_02,
            [EnumMember(Value = "CS_BigDjinn_Voice_03")]
            CS_BigDjinn_Voice_03,
            [EnumMember(Value = "CS_BigDjinn_Voice_04")]
            CS_BigDjinn_Voice_04,
            [EnumMember(Value = "CS_BigDjinn_Voice_05")]
            CS_BigDjinn_Voice_05,
            [EnumMember(Value = "CS_BigDjinn_Voice_06")]
            CS_BigDjinn_Voice_06,
            [EnumMember(Value = "CS_Crimson_DashJump_01")]
            CS_Crimson_DashJump_01 = 51980,
            [EnumMember(Value = "CS_Crimson_DashJump_02")]
            CS_Crimson_DashJump_02,
            [EnumMember(Value = "CS_Crimson_FireBurn_Loop")]
            CS_Crimson_FireBurn_Loop,
            [EnumMember(Value = "CS_Crimson_Footstep_01")]
            CS_Crimson_Footstep_01,
            [EnumMember(Value = "CS_Crimson_Footstep_02")]
            CS_Crimson_Footstep_02,
            [EnumMember(Value = "CS_Crimson_Footstep_03")]
            CS_Crimson_Footstep_03,
            [EnumMember(Value = "CS_Crimson_Footstep_04")]
            CS_Crimson_Footstep_04,
            [EnumMember(Value = "CS_Crimson_Footstep_05")]
            CS_Crimson_Footstep_05,
            [EnumMember(Value = "CS_Crimson_Footstep_06")]
            CS_Crimson_Footstep_06,
            [EnumMember(Value = "CS_Crimson_Lantern_Spell_01")]
            CS_Crimson_Lantern_Spell_01,
            [EnumMember(Value = "CS_Crimson_Lantern_Spell_02")]
            CS_Crimson_Lantern_Spell_02,
            [EnumMember(Value = "CS_Crimson_Spell_Explosion_Effect_01")]
            CS_Crimson_Spell_Explosion_Effect_01,
            [EnumMember(Value = "CS_Crimson_Spell_Explosion_Effect_02")]
            CS_Crimson_Spell_Explosion_Effect_02,
            [EnumMember(Value = "CS_Crimson_Spell_Explosion_Effect_03")]
            CS_Crimson_Spell_Explosion_Effect_03,
            [EnumMember(Value = "CS_Crimson_Spell_fireBall_BurnIdle_loop")]
            CS_Crimson_Spell_fireBall_BurnIdle_loop,
            [EnumMember(Value = "CS_Crimson_Spell_fireBall_Spawn_01")]
            CS_Crimson_Spell_fireBall_Spawn_01,
            [EnumMember(Value = "CS_Crimson_Spell_fireBall_Spawn_02")]
            CS_Crimson_Spell_fireBall_Spawn_02,
            [EnumMember(Value = "CS_Crimson_Spell_FireCircle_FireBurn_loop")]
            CS_Crimson_Spell_FireCircle_FireBurn_loop,
            [EnumMember(Value = "CS_Crimson_Spell_FireCircle_Spawn")]
            CS_Crimson_Spell_FireCircle_Spawn,
            [EnumMember(Value = "CS_Crimson_Staff_Impact_01")]
            CS_Crimson_Staff_Impact_01,
            [EnumMember(Value = "CS_Crimson_Staff_Impact_02")]
            CS_Crimson_Staff_Impact_02,
            [EnumMember(Value = "CS_Crimson_Staff_Impact_03")]
            CS_Crimson_Staff_Impact_03,
            [EnumMember(Value = "CS_Crimson_Staff_Impact_04")]
            CS_Crimson_Staff_Impact_04,
            [EnumMember(Value = "CS_Crimson_Voice_01")]
            CS_Crimson_Voice_01,
            [EnumMember(Value = "CS_Crimson_Voice_02")]
            CS_Crimson_Voice_02,
            [EnumMember(Value = "CS_Crimson_Voice_03")]
            CS_Crimson_Voice_03,
            [EnumMember(Value = "CS_Crimson_Voice_04")]
            CS_Crimson_Voice_04,
            [EnumMember(Value = "CS_Crimson_Voice_05")]
            CS_Crimson_Voice_05,
            [EnumMember(Value = "CS_Crimson_Voice_06")]
            CS_Crimson_Voice_06,
            [EnumMember(Value = "CS_Crimson_Voice_07")]
            CS_Crimson_Voice_07,
            [EnumMember(Value = "CS_Crimson_WhooshAttack_01")]
            CS_Crimson_WhooshAttack_01,
            [EnumMember(Value = "CS_Crimson_WhooshAttack_02")]
            CS_Crimson_WhooshAttack_02,
            [EnumMember(Value = "CS_Crimson_WhooshAttack_03")]
            CS_Crimson_WhooshAttack_03,
            [EnumMember(Value = "CS_Crimson_WhooshAttack_04")]
            CS_Crimson_WhooshAttack_04,
            [EnumMember(Value = "CS_Crimson_DeathVoice")]
            CS_Crimson_DeathVoice,
            [EnumMember(Value = "CS_Crimson_HurtVoice_01")]
            CS_Crimson_HurtVoice_01,
            [EnumMember(Value = "CS_Crimson_HurtVoice_02")]
            CS_Crimson_HurtVoice_02,
            [EnumMember(Value = "CS_Crimson_HurtVoice_03")]
            CS_Crimson_HurtVoice_03,
            [EnumMember(Value = "CS_Djinn_Attack_Whoosh_01")]
            CS_Djinn_Attack_Whoosh_01 = 52020,
            [EnumMember(Value = "CS_Djinn_Attack_Whoosh_02")]
            CS_Djinn_Attack_Whoosh_02,
            [EnumMember(Value = "CS_Djinn_Attack_Whoosh_03")]
            CS_Djinn_Attack_Whoosh_03,
            [EnumMember(Value = "CS_Djinn_Attack_Whoosh_04")]
            CS_Djinn_Attack_Whoosh_04,
            [EnumMember(Value = "CS_Djinn_BlowLantern_01")]
            CS_Djinn_BlowLantern_01,
            [EnumMember(Value = "CS_Djinn_BlowLantern_02")]
            CS_Djinn_BlowLantern_02,
            [EnumMember(Value = "CS_Djinn_Dash_Jump_01")]
            CS_Djinn_Dash_Jump_01,
            [EnumMember(Value = "CS_Djinn_Dash_Jump_02")]
            CS_Djinn_Dash_Jump_02,
            [EnumMember(Value = "CS_Djinn_Death")]
            CS_Djinn_Death,
            [EnumMember(Value = "CS_Djinn_FireBurn_Idle")]
            CS_Djinn_FireBurn_Idle,
            [EnumMember(Value = "CS_Djinn_Incantation_01")]
            CS_Djinn_Incantation_01,
            [EnumMember(Value = "CS_Djinn_Incantation_02")]
            CS_Djinn_Incantation_02,
            [EnumMember(Value = "CS_Djinn_Incantation_03")]
            CS_Djinn_Incantation_03,
            [EnumMember(Value = "CS_Djinn_Incantation_04")]
            CS_Djinn_Incantation_04,
            [EnumMember(Value = "CS_Djinn_Incantation_05")]
            CS_Djinn_Incantation_05,
            [EnumMember(Value = "CS_Djinn_Lantern_Impact_01")]
            CS_Djinn_Lantern_Impact_01,
            [EnumMember(Value = "CS_Djinn_Lantern_Impact_02")]
            CS_Djinn_Lantern_Impact_02,
            [EnumMember(Value = "CS_Djinn_Lantern_Impact_03")]
            CS_Djinn_Lantern_Impact_03,
            [EnumMember(Value = "CS_Djinn_Lantern_Impact_04")]
            CS_Djinn_Lantern_Impact_04,
            [EnumMember(Value = "CS_Djinn_Lantern_Spell_01")]
            CS_Djinn_Lantern_Spell_01,
            [EnumMember(Value = "CS_Djinn_Lantern_Spell_02")]
            CS_Djinn_Lantern_Spell_02,
            [EnumMember(Value = "CS_Djinn_Spell_DrumBlast")]
            CS_Djinn_Spell_DrumBlast,
            [EnumMember(Value = "CS_Djinn_Spell_FireBall_Appear")]
            CS_Djinn_Spell_FireBall_Appear,
            [EnumMember(Value = "CS_Djinn_Spell_FireBall_Explode_01")]
            CS_Djinn_Spell_FireBall_Explode_01,
            [EnumMember(Value = "CS_Djinn_Spell_FireBall_Explode_02")]
            CS_Djinn_Spell_FireBall_Explode_02,
            [EnumMember(Value = "CS_Djinn_Spell_FireBall_Explode_03")]
            CS_Djinn_Spell_FireBall_Explode_03,
            [EnumMember(Value = "CS_Djinn_Spell_FireBall_Fire_Loop")]
            CS_Djinn_Spell_FireBall_Fire_Loop,
            [EnumMember(Value = "CS_Djinn_Spell_Homing_Cloud_Loop")]
            CS_Djinn_Spell_Homing_Cloud_Loop,
            [EnumMember(Value = "CS_Djinn_Spell_Homing_Launch")]
            CS_Djinn_Spell_Homing_Launch,
            [EnumMember(Value = "CS_Djinn_Spell_Wave")]
            CS_Djinn_Spell_Wave,
            [EnumMember(Value = "CS_Djinn_Voice_Hurt_01")]
            CS_Djinn_Voice_Hurt_01,
            [EnumMember(Value = "CS_Djinn_Voice_Hurt_02")]
            CS_Djinn_Voice_Hurt_02,
            [EnumMember(Value = "CS_Djinn_Voice_Hurt_03")]
            CS_Djinn_Voice_Hurt_03,
            [EnumMember(Value = "CS_Djinn_Voice_01")]
            CS_Djinn_Voice_01,
            [EnumMember(Value = "CS_Djinn_Voice_02")]
            CS_Djinn_Voice_02,
            [EnumMember(Value = "CS_Djinn_Voice_03")]
            CS_Djinn_Voice_03,
            [EnumMember(Value = "CS_Djinn_Voice_04")]
            CS_Djinn_Voice_04,
            [EnumMember(Value = "CS_Djinn_Voices_01")]
            CS_Djinn_Voices_01,
            [EnumMember(Value = "CS_Djinn_Voices_02")]
            CS_Djinn_Voices_02,
            [EnumMember(Value = "CS_Djinn_Voices_03")]
            CS_Djinn_Voices_03,
            [EnumMember(Value = "CS_Djinn_Voices_04")]
            CS_Djinn_Voices_04,
            [EnumMember(Value = "CS_Djinn_Voices_05")]
            CS_Djinn_Voices_05,
            [EnumMember(Value = "CS_Djinn_Voices_06")]
            CS_Djinn_Voices_06,
            [EnumMember(Value = "CS_Djinn_Voices_07")]
            CS_Djinn_Voices_07,
            [EnumMember(Value = "CS_Gargoyle_Attack_Whoosh_01")]
            CS_Gargoyle_Attack_Whoosh_01,
            [EnumMember(Value = "CS_Gargoyle_Attack_Whoosh_02")]
            CS_Gargoyle_Attack_Whoosh_02,
            [EnumMember(Value = "CS_Gargoyle_Attack_Whoosh_03")]
            CS_Gargoyle_Attack_Whoosh_03,
            [EnumMember(Value = "CS_Gargoyle_Attack_Whoosh_04")]
            CS_Gargoyle_Attack_Whoosh_04,
            [EnumMember(Value = "CS_Gargoyle_Footstep_01")]
            CS_Gargoyle_Footstep_01,
            [EnumMember(Value = "CS_Gargoyle_Footstep_02")]
            CS_Gargoyle_Footstep_02,
            [EnumMember(Value = "CS_Gargoyle_Footstep_03")]
            CS_Gargoyle_Footstep_03,
            [EnumMember(Value = "CS_Gargoyle_Footstep_04")]
            CS_Gargoyle_Footstep_04,
            [EnumMember(Value = "CS_Gargoyle_Footstep_05")]
            CS_Gargoyle_Footstep_05,
            [EnumMember(Value = "CS_Gargoyle_Footstep_06")]
            CS_Gargoyle_Footstep_06,
            [EnumMember(Value = "CS_Gargoyle_Footstep_07")]
            CS_Gargoyle_Footstep_07,
            [EnumMember(Value = "CS_Gargoyle_HeavyImpact_Floor_01")]
            CS_Gargoyle_HeavyImpact_Floor_01,
            [EnumMember(Value = "CS_Gargoyle_HeavyImpact_Floor_02")]
            CS_Gargoyle_HeavyImpact_Floor_02,
            [EnumMember(Value = "CS_Gargoyle_Hurt_Impact_01")]
            CS_Gargoyle_Hurt_Impact_01,
            [EnumMember(Value = "CS_Gargoyle_Hurt_Impact_02")]
            CS_Gargoyle_Hurt_Impact_02,
            [EnumMember(Value = "CS_Gargoyle_Hurt_Impact_03")]
            CS_Gargoyle_Hurt_Impact_03,
            [EnumMember(Value = "CS_Gargoyle_Hurt_Impact_04")]
            CS_Gargoyle_Hurt_Impact_04,
            [EnumMember(Value = "CS_Gargoyle_Voices_01")]
            CS_Gargoyle_Voices_01,
            [EnumMember(Value = "CS_Gargoyle_Voices_02")]
            CS_Gargoyle_Voices_02,
            [EnumMember(Value = "CS_Gargoyle_Voices_03")]
            CS_Gargoyle_Voices_03,
            [EnumMember(Value = "CS_Gargoyle_Voices_04")]
            CS_Gargoyle_Voices_04,
            [EnumMember(Value = "CS_Gargoyle_Voices_05")]
            CS_Gargoyle_Voices_05,
            [EnumMember(Value = "CS_Gargoyle_Statue_01")]
            CS_Gargoyle_Statue_01,
            [EnumMember(Value = "CS_Gargoyle_Statue_02")]
            CS_Gargoyle_Statue_02,
            [EnumMember(Value = "CS_Gargoyle_Statue_wakeup_01")]
            CS_Gargoyle_Statue_wakeup_01,
            [EnumMember(Value = "CS_Gargoyle_Statue_wakeup_02")]
            CS_Gargoyle_Statue_wakeup_02,
            [EnumMember(Value = "CS_GiantHunter_LargeArrow_Shoot_01")]
            CS_GiantHunter_LargeArrow_Shoot_01,
            [EnumMember(Value = "CS_GiantHunter_LargeArrow_Shoot_02")]
            CS_GiantHunter_LargeArrow_Shoot_02,
            [EnumMember(Value = "CS_GiantHunter_LargeArrow_Shoot_03")]
            CS_GiantHunter_LargeArrow_Shoot_03,
            [EnumMember(Value = "CS_GiantHunter_LargeBow_Crack_01")]
            CS_GiantHunter_LargeBow_Crack_01,
            [EnumMember(Value = "CS_GiantHunter_LargeBow_Crack_02")]
            CS_GiantHunter_LargeBow_Crack_02,
            [EnumMember(Value = "CS_GiantHunter_Poison_Impact_01")]
            CS_GiantHunter_Poison_Impact_01,
            [EnumMember(Value = "CS_GiantHunter_Poison_Impact_02")]
            CS_GiantHunter_Poison_Impact_02,
            [EnumMember(Value = "CS_GiantHunter_Poison_Impact_03")]
            CS_GiantHunter_Poison_Impact_03,
            [EnumMember(Value = "CS_Hyppo_AttackWhoosh_01")]
            CS_Hyppo_AttackWhoosh_01 = 52100,
            [EnumMember(Value = "CS_Hyppo_AttackWhoosh_02")]
            CS_Hyppo_AttackWhoosh_02,
            [EnumMember(Value = "CS_Hyppo_FireSpit_01")]
            CS_Hyppo_FireSpit_01,
            [EnumMember(Value = "CS_Hyppo_FireSpit_02")]
            CS_Hyppo_FireSpit_02,
            [EnumMember(Value = "CS_Hyppo_Footstep_01")]
            CS_Hyppo_Footstep_01,
            [EnumMember(Value = "CS_Hyppo_Footstep_02")]
            CS_Hyppo_Footstep_02,
            [EnumMember(Value = "CS_Hyppo_Footstep_03")]
            CS_Hyppo_Footstep_03,
            [EnumMember(Value = "CS_Hyppo_Footstep_04")]
            CS_Hyppo_Footstep_04,
            [EnumMember(Value = "CS_Hyppo_Footstep_05")]
            CS_Hyppo_Footstep_05,
            [EnumMember(Value = "CS_Hyppo_Footstep_06")]
            CS_Hyppo_Footstep_06,
            [EnumMember(Value = "CS_Hyppo_HurtVoice_01")]
            CS_Hyppo_HurtVoice_01,
            [EnumMember(Value = "CS_Hyppo_HurtVoice_02")]
            CS_Hyppo_HurtVoice_02,
            [EnumMember(Value = "CS_Hyppo_HurtVoice_03")]
            CS_Hyppo_HurtVoice_03,
            [EnumMember(Value = "CS_Hyppo_Voice_01")]
            CS_Hyppo_Voice_01,
            [EnumMember(Value = "CS_Hyppo_Voice_02")]
            CS_Hyppo_Voice_02,
            [EnumMember(Value = "CS_Hyppo_Voice_03")]
            CS_Hyppo_Voice_03,
            [EnumMember(Value = "CS_Hyppo_Voice_04")]
            CS_Hyppo_Voice_04,
            [EnumMember(Value = "CS_JellyFish_ElectricCercle_01")]
            CS_JellyFish_ElectricCercle_01 = 52120,
            [EnumMember(Value = "CS_JellyFish_ElectricCercle_02")]
            CS_JellyFish_ElectricCercle_02,
            [EnumMember(Value = "CS_JellyFish_ElectricExplosion_01")]
            CS_JellyFish_ElectricExplosion_01,
            [EnumMember(Value = "CS_JellyFish_ElectricExplosion_02")]
            CS_JellyFish_ElectricExplosion_02,
            [EnumMember(Value = "CS_JellyFish_ElectricExplosion_03")]
            CS_JellyFish_ElectricExplosion_03,
            [EnumMember(Value = "CS_JellyFish_IdleMovement_LOOP")]
            CS_JellyFish_IdleMovement_LOOP,
            [EnumMember(Value = "CS_JellyFish_Attack_01")]
            CS_JellyFish_Attack_01,
            [EnumMember(Value = "CS_JellyFish_Attack_02")]
            CS_JellyFish_Attack_02,
            [EnumMember(Value = "CS_JellyFish_Death")]
            CS_JellyFish_Death,
            [EnumMember(Value = "CS_Lionman_Death_01")]
            CS_Lionman_Death_01 = 52130,
            [EnumMember(Value = "CS_Lionman_Death_02")]
            CS_Lionman_Death_02,
            [EnumMember(Value = "CS_Lionman_ElectricityExplosion_01")]
            CS_Lionman_ElectricityExplosion_01,
            [EnumMember(Value = "CS_Lionman_ElectricityExplosion_02")]
            CS_Lionman_ElectricityExplosion_02,
            [EnumMember(Value = "CS_Lionman_ElectricityShoot_01")]
            CS_Lionman_ElectricityShoot_01,
            [EnumMember(Value = "CS_Lionman_ElectricityShoot_02")]
            CS_Lionman_ElectricityShoot_02,
            [EnumMember(Value = "CS_Lionman_Growl_01")]
            CS_Lionman_Growl_01,
            [EnumMember(Value = "CS_Lionman_Growl_02")]
            CS_Lionman_Growl_02,
            [EnumMember(Value = "CS_Lionman_HammerHitGround_01")]
            CS_Lionman_HammerHitGround_01,
            [EnumMember(Value = "CS_Lionman_HammerHitGround_02")]
            CS_Lionman_HammerHitGround_02,
            [EnumMember(Value = "CS_Lionman_HammerHitGround_03")]
            CS_Lionman_HammerHitGround_03,
            [EnumMember(Value = "CS_Lionman_HurtVoice_01")]
            CS_Lionman_HurtVoice_01,
            [EnumMember(Value = "CS_Lionman_HurtVoice_02")]
            CS_Lionman_HurtVoice_02,
            [EnumMember(Value = "CS_Lionman_HurtVoice_03")]
            CS_Lionman_HurtVoice_03,
            [EnumMember(Value = "CS_Lionman_Voice_01")]
            CS_Lionman_Voice_01 = 52145,
            [EnumMember(Value = "CS_Lionman_Voice_02")]
            CS_Lionman_Voice_02,
            [EnumMember(Value = "CS_Lionman_Voice_03")]
            CS_Lionman_Voice_03,
            [EnumMember(Value = "CS_Lionman_Voice_04")]
            CS_Lionman_Voice_04,
            [EnumMember(Value = "CS_Lionman_Voice_05")]
            CS_Lionman_Voice_05,
            [EnumMember(Value = "CS_Lionman_Voice_06")]
            CS_Lionman_Voice_06,
            [EnumMember(Value = "CS_Lionman_Voice_07")]
            CS_Lionman_Voice_07,
            [EnumMember(Value = "CS_Lionman_Voice_08")]
            CS_Lionman_Voice_08,
            [EnumMember(Value = "CS_Lionman_Voice_09")]
            CS_Lionman_Voice_09,
            [EnumMember(Value = "CS_Myrm_Fire_Loop")]
            CS_Myrm_Fire_Loop = 52160,
            [EnumMember(Value = "CS_Myrm_HurtVoice_01")]
            CS_Myrm_HurtVoice_01,
            [EnumMember(Value = "CS_Myrm_HurtVoice_02")]
            CS_Myrm_HurtVoice_02,
            [EnumMember(Value = "CS_Myrm_HurtVoice_03")]
            CS_Myrm_HurtVoice_03,
            [EnumMember(Value = "CS_Myrm_ImpactFloor_01")]
            CS_Myrm_ImpactFloor_01,
            [EnumMember(Value = "CS_Myrm_ImpactFloor_02")]
            CS_Myrm_ImpactFloor_02,
            [EnumMember(Value = "CS_Myrm_ImpactFloor_03")]
            CS_Myrm_ImpactFloor_03,
            [EnumMember(Value = "CS_Myrm_VoiceAttack_01")]
            CS_Myrm_VoiceAttack_01,
            [EnumMember(Value = "CS_Myrm_VoiceAttack_02")]
            CS_Myrm_VoiceAttack_02,
            [EnumMember(Value = "CS_Myrm_VoiceAttack_03")]
            CS_Myrm_VoiceAttack_03,
            [EnumMember(Value = "CS_Myrm_VoiceAttack_04")]
            CS_Myrm_VoiceAttack_04,
            [EnumMember(Value = "CS_Myrm_VoiceAttack_05")]
            CS_Myrm_VoiceAttack_05,
            [EnumMember(Value = "CS_Myrm_VoiceAttack_06")]
            CS_Myrm_VoiceAttack_06,
            [EnumMember(Value = "CS_Myrm_VoiceRage_01")]
            CS_Myrm_VoiceRage_01,
            [EnumMember(Value = "CS_Myrm_VoiceRage_02")]
            CS_Myrm_VoiceRage_02,
            [EnumMember(Value = "CS_Myrm_WhooshAttack_01")]
            CS_Myrm_WhooshAttack_01,
            [EnumMember(Value = "CS_Myrm_WhooshAttack_02")]
            CS_Myrm_WhooshAttack_02,
            [EnumMember(Value = "CS_Myrm_WhooshAttack_03")]
            CS_Myrm_WhooshAttack_03,
            [EnumMember(Value = "CS_Myrm_WhooshNoseAttack_01")]
            CS_Myrm_WhooshNoseAttack_01,
            [EnumMember(Value = "CS_Myrm_DeathVoice")]
            CS_Myrm_DeathVoice,
            [EnumMember(Value = "CS_Myrm_Growl_01")]
            CS_Myrm_Growl_01,
            [EnumMember(Value = "CS_Myrm_Grunt_01")]
            CS_Myrm_Grunt_01,
            [EnumMember(Value = "CS_Myrm_Grunt_02")]
            CS_Myrm_Grunt_02,
            [EnumMember(Value = "CS_Myrm_Grunt_03")]
            CS_Myrm_Grunt_03,
            [EnumMember(Value = "CS_Myrm_VoiceShort_01")]
            CS_Myrm_VoiceShort_01,
            [EnumMember(Value = "CS_Myrm_VoiceShort_02")]
            CS_Myrm_VoiceShort_02,
            [EnumMember(Value = "CS_Myrm_VoiceShort_03")]
            CS_Myrm_VoiceShort_03,
            [EnumMember(Value = "CS_Myrm_VoiceShort_04")]
            CS_Myrm_VoiceShort_04,
            [EnumMember(Value = "CS_ScarletteEmissary_Death_01")]
            CS_ScarletteEmissary_Death_01 = 52190,
            [EnumMember(Value = "CS_ScarletteEmissary_Death_02")]
            CS_ScarletteEmissary_Death_02,
            [EnumMember(Value = "CS_ScarletteEmissary_Fire_Idle_loop")]
            CS_ScarletteEmissary_Fire_Idle_loop,
            [EnumMember(Value = "CS_ScarletteEmissary_FireExplosion_01")]
            CS_ScarletteEmissary_FireExplosion_01,
            [EnumMember(Value = "CS_ScarletteEmissary_HurtVoice_01")]
            CS_ScarletteEmissary_HurtVoice_01,
            [EnumMember(Value = "CS_ScarletteEmissary_HurtVoice_02")]
            CS_ScarletteEmissary_HurtVoice_02,
            [EnumMember(Value = "CS_ScarletteEmissary_HurtVoice_03")]
            CS_ScarletteEmissary_HurtVoice_03,
            [EnumMember(Value = "CS_ScarletteEmissary_Voice_01")]
            CS_ScarletteEmissary_Voice_01,
            [EnumMember(Value = "CS_ScarletteEmissary_Voice_02")]
            CS_ScarletteEmissary_Voice_02,
            [EnumMember(Value = "CS_ScarletteEmissary_Voice_03")]
            CS_ScarletteEmissary_Voice_03,
            [EnumMember(Value = "CS_ScarletteEmissary_Voice_04")]
            CS_ScarletteEmissary_Voice_04,
            [EnumMember(Value = "CS_ScarletteEmissary_Voice_05")]
            CS_ScarletteEmissary_Voice_05,
            [EnumMember(Value = "CS_ScarletteEmissary_Voice_06")]
            CS_ScarletteEmissary_Voice_06,
            [EnumMember(Value = "CS_ScarletteEmissary_Voice_07")]
            CS_ScarletteEmissary_Voice_07,
            [EnumMember(Value = "CS_ScarletteEmissary_Voice_08")]
            CS_ScarletteEmissary_Voice_08,
            [EnumMember(Value = "CS_ScarletteEmissary_WhooshAttack_01")]
            CS_ScarletteEmissary_WhooshAttack_01,
            [EnumMember(Value = "CS_ScarletteEmissary_WhooshAttack_02")]
            CS_ScarletteEmissary_WhooshAttack_02,
            [EnumMember(Value = "CS_ScarletteEmissary_WhooshAttack_03")]
            CS_ScarletteEmissary_WhooshAttack_03,
            [EnumMember(Value = "CS_ScarletteEmissary_Enrage")]
            CS_ScarletteEmissary_Enrage,
            [EnumMember(Value = "CS_ScarletteEmissary_Combust")]
            CS_ScarletteEmissary_Combust,
            [EnumMember(Value = "CS_SlugHell_BallExplosion_01")]
            CS_SlugHell_BallExplosion_01,
            [EnumMember(Value = "CS_SlugHell_BallExplosion_02")]
            CS_SlugHell_BallExplosion_02,
            [EnumMember(Value = "CS_SlugHell_BallExplosion_03")]
            CS_SlugHell_BallExplosion_03,
            [EnumMember(Value = "CS_SlugHell_Death_01")]
            CS_SlugHell_Death_01,
            [EnumMember(Value = "CS_SlugHell_Death_02")]
            CS_SlugHell_Death_02,
            [EnumMember(Value = "CS_SlugHell_HurtVoice_01")]
            CS_SlugHell_HurtVoice_01,
            [EnumMember(Value = "CS_SlugHell_HurtVoice_03")]
            CS_SlugHell_HurtVoice_03,
            [EnumMember(Value = "CS_SlugHell_HurtVoice_05")]
            CS_SlugHell_HurtVoice_05,
            [EnumMember(Value = "CS_SlugHell_Slide_Loop")]
            CS_SlugHell_Slide_Loop,
            [EnumMember(Value = "CS_SlugHell_SpellLaunch_01")]
            CS_SlugHell_SpellLaunch_01,
            [EnumMember(Value = "CS_SlugHell_SpellLaunch_02")]
            CS_SlugHell_SpellLaunch_02 = 52221,
            [EnumMember(Value = "CS_SlugHell_SpellLaunch_03")]
            CS_SlugHell_SpellLaunch_03,
            [EnumMember(Value = "CS_SlugHell_Voice_01")]
            CS_SlugHell_Voice_01,
            [EnumMember(Value = "CS_SlugHell_Voice_02")]
            CS_SlugHell_Voice_02,
            [EnumMember(Value = "CS_SlugHell_Voice_03")]
            CS_SlugHell_Voice_03,
            [EnumMember(Value = "CS_SlugHell_Voice_04")]
            CS_SlugHell_Voice_04,
            [EnumMember(Value = "CS_SlugHell_Voice_05")]
            CS_SlugHell_Voice_05,
            [EnumMember(Value = "CS_SlugHell_Voice_06")]
            CS_SlugHell_Voice_06,
            [EnumMember(Value = "CS_TorchCrabe_Death_01")]
            CS_TorchCrabe_Death_01 = 52230,
            [EnumMember(Value = "CS_TorchCrabe_Death_02")]
            CS_TorchCrabe_Death_02,
            [EnumMember(Value = "CS_TorchCrabe_FireExplosion_01")]
            CS_TorchCrabe_FireExplosion_01,
            [EnumMember(Value = "CS_TorchCrabe_FireExplosion_02")]
            CS_TorchCrabe_FireExplosion_02,
            [EnumMember(Value = "CS_TorchCrabe_FireExplosion_03")]
            CS_TorchCrabe_FireExplosion_03,
            [EnumMember(Value = "CS_TorchCrabe_FireLaunch_01")]
            CS_TorchCrabe_FireLaunch_01,
            [EnumMember(Value = "CS_TorchCrabe_FireLaunch_02")]
            CS_TorchCrabe_FireLaunch_02,
            [EnumMember(Value = "CS_TorchCrabe_HurtVoice_01")]
            CS_TorchCrabe_HurtVoice_01,
            [EnumMember(Value = "CS_TorchCrabe_HurtVoice_02")]
            CS_TorchCrabe_HurtVoice_02,
            [EnumMember(Value = "CS_TorchCrabe_HurtVoice_03")]
            CS_TorchCrabe_HurtVoice_03,
            [EnumMember(Value = "CS_TorchCrabe_Voice_01")]
            CS_TorchCrabe_Voice_01,
            [EnumMember(Value = "CS_TorchCrabe_Voice_02")]
            CS_TorchCrabe_Voice_02,
            [EnumMember(Value = "CS_TorchCrabe_Voice_03")]
            CS_TorchCrabe_Voice_03,
            [EnumMember(Value = "CS_TorchCrabe_Voice_04")]
            CS_TorchCrabe_Voice_04,
            [EnumMember(Value = "CS_TorchCrabe_Voice_05")]
            CS_TorchCrabe_Voice_05,
            [EnumMember(Value = "CS_TorchCrabe_Voice_06")]
            CS_TorchCrabe_Voice_06,
            [EnumMember(Value = "UI_GENERAL_Select")]
            UI_GENERAL_Select = 90000,
            [EnumMember(Value = "UI_GENERAL_Click")]
            UI_GENERAL_Click,
            [EnumMember(Value = "UI_GENERAL_SelectQuieter")]
            UI_GENERAL_SelectQuieter,
            [EnumMember(Value = "UI_GENERAL_OpenMenu")]
            UI_GENERAL_OpenMenu = 90010,
            [EnumMember(Value = "UI_GENERAL_CloseMenu")]
            UI_GENERAL_CloseMenu,
            [EnumMember(Value = "UI_NEWGAME_SelectSave")]
            UI_NEWGAME_SelectSave = 90100,
            [EnumMember(Value = "UI_CRAFTING_Survival")]
            UI_CRAFTING_Survival = 90200,
            [EnumMember(Value = "UI_CRAFTING_Campfire")]
            UI_CRAFTING_Campfire,
            [EnumMember(Value = "UI_CRAFTING_CookingPot")]
            UI_CRAFTING_CookingPot,
            [EnumMember(Value = "UI_CRAFTING_Alchemy")]
            UI_CRAFTING_Alchemy,
            [EnumMember(Value = "UI_REST_StartRest")]
            UI_REST_StartRest = 90300,
            [EnumMember(Value = "UI_MERCHANT_CompleteTransaction")]
            UI_MERCHANT_CompleteTransaction = 90400,
            [EnumMember(Value = "UI_INVENTORY_MoveItem")]
            UI_INVENTORY_MoveItem = 90500,
            [EnumMember(Value = "UI_TRAINER_UnlockSkill")]
            UI_TRAINER_UnlockSkill = 90600,
            [EnumMember(Value = "UI_TRAP_ArmPlateTrap")]
            UI_TRAP_ArmPlateTrap = 90700,
            [EnumMember(Value = "UI_TRAP_ArmWireTrap")]
            UI_TRAP_ArmWireTrap
        }
    }
}
