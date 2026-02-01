using System.Collections.Generic;
using OutwardArchipelago.Archipelago.APItemGivers;

namespace OutwardArchipelago.Archipelago
{
    internal static partial class APWorld
    {
        public static readonly IReadOnlyDictionary<int, Location> ItemToLocation = new Dictionary<int, Location>
        {
            { OutwardItem.AnglerShield, Location.SpawnAnglerShield },
            { OutwardItem.AntiquePlateBoots, Location.CommissionAntiquePlateBoots },
            { OutwardItem.AntiquePlateGarb, Location.CommissionAntiquePlateGarb },
            { OutwardItem.AntiquePlateSallet, Location.CommissionAntiquePlateSallet },
            { OutwardItem.BlueSandArmor, Location.CommissionBlueSandArmor },
            { OutwardItem.BlueSandBoots, Location.CommissionBlueSandBoots },
            { OutwardItem.BlueSandHelm, Location.CommissionBlueSandHelm },
            { OutwardItem.Brand, Location.SpawnBrand },
            { OutwardItem.BrassWolfBackpack, Location.SpawnBrassWolfBackpack },
            { OutwardItem.CeremonialBow, Location.SpawnCeremonialBow },
            { OutwardItem.CompasswoodStaff, Location.SpawnCompasswoodStaff },
            { OutwardItem.CopalArmor, Location.CommissionCopalArmor },
            { OutwardItem.CopalBoots, Location.CommissionCopalBoots },
            { OutwardItem.CopalHelm, Location.CommissionCopalHelm },
            { OutwardItem.CrackedRedMoon, Location.SpawnCrackedRedMoon },
            { OutwardItem.DepoweredBludgeon, Location.SpawnDepoweredBludgeon },
            { OutwardItem.DistortedExperiment, Location.SpawnDistortedExperiment },
            { OutwardItem.DreamerHalberd, Location.SpawnDreamerHalberd },
            { OutwardItem.Duty, Location.SpawnDuty },
            { OutwardItem.ExperimentalChakram, Location.SpawnExperimentalChakram },
            { OutwardItem.FabulousPalladiumShield, Location.SpawnFabulousPalladiumShield },
            { OutwardItem.FossilizedGreataxe, Location.SpawnFossilizedGreataxe },
            { OutwardItem.GepsBlade, Location.SpawnGepsBlade },
            { OutwardItem.GepsLongblade, Location.SpawnGepsBlade },
            { OutwardItem.GhostParallel, Location.SpawnGhostParallel },
            { OutwardItem.GildedShiverOfTramontane, Location.SpawnGildedShiverOfTramontane },
            { OutwardItem.GoldLichArmor, Location.QuestMinorBewareTheGoldLich1 },
            { OutwardItem.GoldLichBoots, Location.QuestMinorBewareTheGoldLich2 },
            { OutwardItem.GoldLichClaymore, Location.QuestMinorBewareTheGoldLich3 },
            { OutwardItem.GoldLichSpear, Location.QuestMinorBewareTheGoldLich4 },
            { OutwardItem.Grind, Location.SpawnGrind },
            { OutwardItem.JadeLichBoots, Location.QuestMinorBewareTheJadeLich1 },
            { OutwardItem.JadeLichMask, Location.QuestMinorBewareTheJadeLich2 },
            { OutwardItem.JadeLichRobes, Location.QuestMinorBewareTheJadeLich3 },
            { OutwardItem.JadeLichStaff, Location.QuestMinorBewareTheJadeLich4 },
            { OutwardItem.KrypteiaTombKey, Location.SpawnKrypteiaTombKey },
            { OutwardItem.LightMendersBackpack, Location.QuestMinorStrangeApparitions },
            { OutwardItem.LightMendersLexicon, Location.SpawnLightMendersLexicon },
            { OutwardItem.MefinosTradeBackpack, Location.SpawnMefinosTradeBackpack },
            { OutwardItem.MertonsFirepoker, Location.SpawnMertonsFirepoker },
            { OutwardItem.MertonsRibcage, Location.SpawnMertonsRibcage },
            { OutwardItem.MertonsShinbones, Location.SpawnMertonsShinbones },
            { OutwardItem.MertonsSkull, Location.SpawnMertonsSkull },
            { OutwardItem.Murmure, Location.SpawnMurmure },
            { OutwardItem.MyrmitaurHavenGateKey, Location.SpawnMyrmitaurHavenGateKey },
            { OutwardItem.MysteriousBlade, Location.SpawnMysteriousLongBlade },
            { OutwardItem.MysteriousChakram, Location.QuestMinorBarrelMan },
            { OutwardItem.MysteriousLongblade, Location.SpawnMysteriousLongBlade },
            { OutwardItem.OrnateBoneShield, Location.QuestMinorSkullsForCremeuh },
            { OutwardItem.PalladiumArmor, Location.CommissionPalladiumArmor },
            { OutwardItem.PalladiumBoots, Location.CommissionPalladiumBoots },
            { OutwardItem.PalladiumHelm, Location.CommissionPalladiumHelm },
            { OutwardItem.PearlescentMail, Location.SpawnPearlescentMail },
            { OutwardItem.PetrifiedWoodArmor, Location.CommissionPetrifiedWoodArmor },
            { OutwardItem.PetrifiedWoodBoots, Location.CommissionPetrifiedWoodBoots },
            { OutwardItem.PetrifiedWoodHelm, Location.CommissionPetrifiedWoodHelm },
            { OutwardItem.PillarGreathammer, Location.SpawnPillarGreathammer },
            { OutwardItem.PorcelainFists, Location.SpawnPorcelainFists },
            { OutwardItem.RedLadysDagger, Location.SpawnRedLadysDagger },
            { OutwardItem.RevenantMoon, Location.SpawnRevenantMoon },
            { OutwardItem.RotwoodStaff, Location.SpawnRotwoodStaff },
            { OutwardItem.RuinedHalberd, Location.SpawnRuinedHalberd },
            { OutwardItem.RustedSpear, Location.SpawnRustedSpear },
            { OutwardItem.RustLichArmor, Location.QuestParallelRustAndVengeance1 },
            { OutwardItem.RustLichBoots, Location.QuestParallelRustAndVengeance2 },
            { OutwardItem.RustLichHelmet, Location.QuestParallelRustAndVengeance3 },
            { OutwardItem.Sandrose, Location.SpawnSandrose },
            { OutwardItem.ScarletBoots, Location.SpawnScarletBoots },
            { OutwardItem.ScarletGem, Location.SpawnScarletGem },
            { OutwardItem.ScarletLichsIdol, Location.SpawnScarletLichsIdol },
            { OutwardItem.ScarletMask, Location.SpawnScarletMask },
            { OutwardItem.ScarletRobes, Location.SpawnScarletRobes },
            { OutwardItem.ScarredDagger, Location.SpawnScarredDagger },
            { OutwardItem.ScepterOfTheCruelPriest, Location.SpawnScepterOfTheCruelPriest },
            { OutwardItem.SealedMace, Location.SpawnSealedMace },
            { OutwardItem.Shriek, Location.SpawnShriek },
            { OutwardItem.SkycrownMace, Location.SpawnSkycrownMace },
            { OutwardItem.SlumberingShield, Location.SpawnSlumberingShield },
            { OutwardItem.SmellySealedBox, Location.SpawnSmellySealedBox },
            { OutwardItem.StarchildClaymore, Location.SpawnStarchildClaymore },
            { OutwardItem.StrangeRustedSword, Location.SpawnStrangeRustedSword },
            { OutwardItem.SunfallAxe, Location.SpawnSunfallAxe },
            { OutwardItem.TenebrousArmor, Location.CommissionTenebrousArmor },
            { OutwardItem.TenebrousBoots, Location.CommissionTenebrousBoots },
            { OutwardItem.TenebrousHelm, Location.CommissionTenebrousHelm },
            { OutwardItem.ThriceWroughtHalberd, Location.SpawnThriceWroughtHalberd },
            { OutwardItem.Tokebakicit, Location.SpawnTokebakicit },
            { OutwardItem.TsarArmor, Location.CommissionTsarArmor },
            { OutwardItem.TsarBoots, Location.CommissionTsarBoots },
            { OutwardItem.TsarFists, Location.SpawnTsarFists },
            { OutwardItem.TsarHelm, Location.CommissionTsarHelm },
            { OutwardItem.UnusualKnuckles, Location.SpawnUnusualKnuckles },
            { OutwardItem.WarmAxe, Location.SpawnWarmAxe },
            { OutwardItem.WerligSpear, Location.SpawnWerligSpear },
            { OutwardItem.WillOWisp, Location.QuestMinorWilliamOfTheWisp },
            { OutwardItem.WorldedgeGreataxe, Location.SpawnWorldedgeGreataxe },
            { OutwardItem.ZhornsDemonShield, Location.SpawnZhornsDemonShield },
            { OutwardItem.ZhornsGlowstoneDagger, Location.SpawnZhornsGlowstoneDagger },
            { OutwardItem.ZhornsHuntingBackpack, Location.SpawnZhornsHuntingBackpack },
        };

        /// <summary>
        /// A mapping from Outward APWorld item IDs to APWorld item givers.
        /// </summary>
        public static readonly IReadOnlyDictionary<APWorld.Item, IAPItemGiver> ItemToGiver = new Dictionary<APWorld.Item, IAPItemGiver>
        {
            { Item.AnglerShield, new ItemGiver(OutwardItem.AnglerShield) },
            { Item.AntiquePlateBoots, new ItemGiver(OutwardItem.AntiquePlateBoots) },
            { Item.AntiquePlateGarb, new ItemGiver(OutwardItem.AntiquePlateGarb) },
            { Item.AntiquePlateSallet, new ItemGiver(OutwardItem.AntiquePlateSallet) },
            { Item.BlueSandArmor, new ItemGiver(OutwardItem.BlueSandArmor) },
            { Item.BlueSandBoots, new ItemGiver(OutwardItem.BlueSandBoots) },
            { Item.BlueSandHelm, new ItemGiver(OutwardItem.BlueSandHelm) },
            { Item.Brand, new ItemGiver(OutwardItem.Brand) },
            { Item.BrassWolfBackpack, new ItemGiver(OutwardItem.BrassWolfBackpack) },
            { Item.CeremonialBow, new ItemGiver(OutwardItem.CeremonialBow) },
            { Item.CompasswoodStaff, new ItemGiver(OutwardItem.CompasswoodStaff) },
            { Item.CopalArmor, new ItemGiver(OutwardItem.CopalArmor) },
            { Item.CopalBoots, new ItemGiver(OutwardItem.CopalBoots) },
            { Item.CopalHelm, new ItemGiver(OutwardItem.CopalHelm) },
            { Item.CrackedRedMoon, new ItemGiver(OutwardItem.CrackedRedMoon) },
            { Item.DepoweredBludgeon, new ItemGiver(OutwardItem.DepoweredBludgeon) },
            { Item.DistortedExperiment, new ItemGiver(OutwardItem.DistortedExperiment) },
            { Item.DreamerHalberd, new ItemGiver(OutwardItem.DreamerHalberd) },
            { Item.Duty, new ItemGiver(OutwardItem.Duty) },
            { Item.ExperimentalChakram, new ItemGiver(OutwardItem.ExperimentalChakram) },
            { Item.FabulousPalladiumShield, new ItemGiver(OutwardItem.FabulousPalladiumShield) },
            { Item.FossilizedGreataxe, new ItemGiver(OutwardItem.FossilizedGreataxe) },
            { Item.GepsLongblade, new ItemGiver(OutwardItem.GepsLongblade) },
            { Item.GhostParallel, new ItemGiver(OutwardItem.GhostParallel) },
            { Item.GildedShiverOfTramontane, new ItemGiver(OutwardItem.GildedShiverOfTramontane) },
            { Item.GoldLichArmor, new ItemGiver(OutwardItem.GoldLichArmor) },
            { Item.GoldLichBoots, new ItemGiver(OutwardItem.GoldLichBoots) },
            { Item.GoldLichMask, new ItemGiver(OutwardItem.GoldLichMask) },
            { Item.GoldLichSpear, new ItemGiver(OutwardItem.GoldLichSpear) },
            { Item.Grind, new ItemGiver(OutwardItem.Grind) },
            { Item.JadeLichBoots, new ItemGiver(OutwardItem.JadeLichBoots) },
            { Item.JadeLichMask, new ItemGiver(OutwardItem.JadeLichMask) },
            { Item.JadeLichRobes, new ItemGiver(OutwardItem.JadeLichRobes) },
            { Item.JadeLichStaff, new ItemGiver(OutwardItem.JadeLichStaff) },
            { Item.KrypteiaTombKey, new ItemGiver(OutwardItem.KrypteiaTombKey) },
            { Item.LightMendersBackpack, new ItemGiver(OutwardItem.LightMendersBackpack) },
            { Item.LightMendersLexicon, new ItemGiver(OutwardItem.LightMendersLexicon) },
            { Item.MefinosTradeBackpack, new ItemGiver(OutwardItem.MefinosTradeBackpack) },
            { Item.MertonsFirepoker, new ItemGiver(OutwardItem.MertonsFirepoker) },
            { Item.MertonsRibcage, new ItemGiver(OutwardItem.MertonsRibcage) },
            { Item.MertonsShinbones, new ItemGiver(OutwardItem.MertonsShinbones) },
            { Item.MertonsSkull, new ItemGiver(OutwardItem.MertonsSkull) },
            { Item.Murmure, new ItemGiver(OutwardItem.Murmure) },
            { Item.MyrmitaurHavenGateKey, new ItemGiver(OutwardItem.MyrmitaurHavenGateKey) },
            { Item.MysteriousChakram, new ItemGiver(OutwardItem.MysteriousChakram) },
            { Item.MysteriousLongBlade, new ItemGiver(OutwardItem.MysteriousLongblade) },
            { Item.OrnateBoneShield, new ItemGiver(OutwardItem.OrnateBoneShield) },
            { Item.PalladiumArmor, new ItemGiver(OutwardItem.PalladiumArmor) },
            { Item.PalladiumBoots, new ItemGiver(OutwardItem.PalladiumBoots) },
            { Item.PalladiumHelm, new ItemGiver(OutwardItem.PalladiumHelm) },
            { Item.PearlescentMail, new ItemGiver(OutwardItem.PearlescentMail) },
            { Item.PetrifiedWoodArmor, new ItemGiver(OutwardItem.PetrifiedWoodArmor) },
            { Item.PetrifiedWoodBoots, new ItemGiver(OutwardItem.PetrifiedWoodBoots) },
            { Item.PetrifiedWoodHelm, new ItemGiver(OutwardItem.PetrifiedWoodHelm) },
            { Item.PillarGreathammer, new ItemGiver(OutwardItem.PillarGreathammer) },
            { Item.PorcelainFists, new ItemGiver(OutwardItem.PorcelainFists) },
            { Item.RedLadysDagger, new ItemGiver(OutwardItem.RedLadysDagger) },
            { Item.RevenantMoon, new ItemGiver(OutwardItem.RevenantMoon) },
            { Item.RotwoodStaff, new ItemGiver(OutwardItem.RotwoodStaff) },
            { Item.RuinedHalberd, new ItemGiver(OutwardItem.RuinedHalberd) },
            { Item.RustedSpear, new ItemGiver(OutwardItem.RustedSpear) },
            { Item.RustLichArmor, new ItemGiver(OutwardItem.RustLichArmor) },
            { Item.RustLichBoots, new ItemGiver(OutwardItem.RustLichBoots) },
            { Item.RustLichHelmet, new ItemGiver(OutwardItem.RustLichHelmet) },
            { Item.Sandrose, new ItemGiver(OutwardItem.Sandrose) },
            { Item.ScarletBoots, new ItemGiver(OutwardItem.ScarletBoots) },
            { Item.ScarletLichsIdol, new ItemGiver(OutwardItem.ScarletLichsIdol) },
            { Item.ScarletMask, new ItemGiver(OutwardItem.ScarletMask) },
            { Item.ScarletRobes, new ItemGiver(OutwardItem.ScarletRobes) },
            { Item.ScarredDagger, new ItemGiver(OutwardItem.ScarredDagger) },
            { Item.ScepterOfTheCruelPriest, new ItemGiver(OutwardItem.ScepterOfTheCruelPriest) },
            { Item.SealedMace, new ItemGiver(OutwardItem.SealedMace) },
            { Item.Shriek, new ItemGiver(OutwardItem.Shriek) },
            { Item.SkycrownMace, new ItemGiver(OutwardItem.SkycrownMace) },
            { Item.SlumberingShield, new ItemGiver(OutwardItem.SlumberingShield) },
            { Item.SmellySealedBox, new ItemGiver(OutwardItem.SmellySealedBox) },
            { Item.StarchildClaymore, new ItemGiver(OutwardItem.StarchildClaymore) },
            { Item.StrangeRustedSword, new ItemGiver(OutwardItem.StrangeRustedSword) },
            { Item.SunfallAxe, new ItemGiver(OutwardItem.SunfallAxe) },
            { Item.TenebrousArmor, new ItemGiver(OutwardItem.TenebrousArmor) },
            { Item.TenebrousBoots, new ItemGiver(OutwardItem.TenebrousBoots) },
            { Item.TenebrousHelm, new ItemGiver(OutwardItem.TenebrousHelm) },
            { Item.ThriceWroughtHalberd, new ItemGiver(OutwardItem.ThriceWroughtHalberd) },
            { Item.Tokebakicit, new ItemGiver(OutwardItem.Tokebakicit) },
            { Item.TsarArmor, new ItemGiver(OutwardItem.TsarArmor) },
            { Item.TsarBoots, new ItemGiver(OutwardItem.TsarBoots) },
            { Item.TsarFists, new ItemGiver(OutwardItem.TsarFists) },
            { Item.TsarHelm, new ItemGiver(OutwardItem.TsarHelm) },
            { Item.UnusualKnuckles, new ItemGiver(OutwardItem.UnusualKnuckles) },
            { Item.WarmAxe, new ItemGiver(OutwardItem.WarmAxe) },
            { Item.WerligSpear, new ItemGiver(OutwardItem.WerligSpear) },
            { Item.WillOWisp, new ItemGiver(OutwardItem.WillOWisp) },
            { Item.WorldedgeGreataxe, new ItemGiver(OutwardItem.WorldedgeGreataxe) },
            { Item.ZhornsDemonShield, new ItemGiver(OutwardItem.ZhornsDemonShield) },
            { Item.ZhornsGlowstoneDagger, new ItemGiver(OutwardItem.ZhornsGlowstoneDagger) },
            { Item.ZhornsHuntingBackpack, new ItemGiver(OutwardItem.ZhornsHuntingBackpack) },

            { Item.CurseHex, new SkillGiver(OutwardSkill.CurseHex) },
            { Item.PossessedSkill, new SkillGiver(OutwardSkill.Possessed) },

            { Item.QuestLicense, new ProgressiveSkillGiver(OutwardSkill.QuestLicense) },

            { Item.SilverCurrency, new MoneyGiver(50) },
        };

        public sealed partial class Item
        {
            private readonly long _id;

            private readonly string _name;

            private Item(long id, string name)
            {
                _id = id;
                _name = name;
            }

            public long Id => _id;

            public string Name => _name;

            public override bool Equals(object obj) => obj is Item item && _id == item._id;
            public override int GetHashCode() => 1969571243 + _id.GetHashCode();
            public override string ToString() => $"APItem: '{_name}' ({_id})";

            public static bool operator ==(Item left, Item right) => EqualityComparer<Item>.Default.Equals(left, right);
            public static bool operator !=(Item left, Item right) => !(left == right);
        }

        public sealed partial class Location
        {
            private readonly long _id;

            private readonly string _name;

            private Location(long id, string name)
            {
                _id = id;
                _name = name;
            }

            public long Id => _id;

            public string Name => _name;

            public override bool Equals(object obj) => obj is Location location && _id == location._id;
            public override int GetHashCode() => 1969571243 + _id.GetHashCode();
            public override string ToString() => $"APLocation: '{_name}' ({_id})";

            public static bool operator ==(Location left, Location right) => EqualityComparer<Location>.Default.Equals(left, right);
            public static bool operator !=(Location left, Location right) => !(left == right);
        }
    }
}
