using System;
using System.Collections.Generic;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Archipelago.Data;

namespace OutwardArchipelago.src.Archipelago
{
    internal class ArchipelagoItemManager
    {
        public static ArchipelagoItemManager Instance { get; private set; } = new();

        private Dictionary<long, IOutwardGiver> _givers = new();

        private ArchipelagoItemManager()
        {
            RegisterAllGivers();
        }

        public void RegisterGiver(long archipelagoItemID, IOutwardGiver giver)
        {
            if (_givers.ContainsKey(archipelagoItemID))
            {
                throw new InvalidOperationException($"attempted to register a second item giver for the same archipelago item ID: {archipelagoItemID}");
            }

            _givers[archipelagoItemID] = giver;
        }

        public void GiveItemToPlayer(long archipelagoItemID, Character character)
        {
            _givers[archipelagoItemID].GiveToPlayer(character);
        }

        private void RegisterAllGivers()
        {
            RegisterGiver(APWorldItem.AntiquePlateBoots, new ItemGiver(3100252));
            RegisterGiver(APWorldItem.AntiquePlateGarb, new ItemGiver(3100250));
            RegisterGiver(APWorldItem.AntiquePlateSallet, new ItemGiver(3100251));
            RegisterGiver(APWorldItem.BlueSandArmor, new ItemGiver(3100080));
            RegisterGiver(APWorldItem.BlueSandBoots, new ItemGiver(3100082));
            RegisterGiver(APWorldItem.BlueSandHelm, new ItemGiver(3100081));
            RegisterGiver(APWorldItem.CeremonialBow, new ItemGiver(2200190));
            RegisterGiver(APWorldItem.CopalArmor, new ItemGiver(3000160));
            RegisterGiver(APWorldItem.CopalBoots, new ItemGiver(3000162));
            RegisterGiver(APWorldItem.CopalHelm, new ItemGiver(3000161));
            RegisterGiver(APWorldItem.CurseHex, new SkillGiver(8201023));
            RegisterGiver(APWorldItem.DepoweredBludgeon, new ItemGiver(2120270));
            RegisterGiver(APWorldItem.DreamerHalberd, new ItemGiver(2140120));
            RegisterGiver(APWorldItem.FossilizedGreataxe, new ItemGiver(2110260));
            RegisterGiver(APWorldItem.GoldLichArmor, new ItemGiver(3000210));
            RegisterGiver(APWorldItem.GoldLichBoots, new ItemGiver(3000212));
            RegisterGiver(APWorldItem.GoldLichMask, new ItemGiver(3000211));
            RegisterGiver(APWorldItem.GoldLichSpear, new ItemGiver(2130060));
            RegisterGiver(APWorldItem.JadeLichBoots, new ItemGiver(3000043));
            RegisterGiver(APWorldItem.JadeLichMask, new ItemGiver(3000041));
            RegisterGiver(APWorldItem.JadeLichRobes, new ItemGiver(3000040));
            RegisterGiver(APWorldItem.JadeLichStaff, new ItemGiver(2150010));
            RegisterGiver(APWorldItem.LightMendersBackpack, new ItemGiver(5300170));
            RegisterGiver(APWorldItem.MertonsFirepoker, new ItemGiver(2020070));
            RegisterGiver(APWorldItem.MysteriousChakram, new ItemGiver(5110070));
            RegisterGiver(APWorldItem.MysteriousLongBlade, new ItemGiver(2100300));
            RegisterGiver(APWorldItem.OrnateBoneShield, new ItemGiver(2300180));
            RegisterGiver(APWorldItem.PalladiumArmor, new ItemGiver(3100060));
            RegisterGiver(APWorldItem.PalladiumBoots, new ItemGiver(3100062));
            RegisterGiver(APWorldItem.PalladiumHelm, new ItemGiver(3100061));
            RegisterGiver(APWorldItem.PetrifiedWoodArmor, new ItemGiver(3100020));
            RegisterGiver(APWorldItem.PetrifiedWoodBoots, new ItemGiver(3100022));
            RegisterGiver(APWorldItem.PetrifiedWoodHelm, new ItemGiver(3100021));
            RegisterGiver(APWorldItem.PillarGreathammer, new ItemGiver(2120070));
            RegisterGiver(APWorldItem.PorcelainFists, new ItemGiver(2160090));
            RegisterGiver(APWorldItem.PossessedSkill, new ItemGiver(8200190));
            RegisterGiver(APWorldItem.QuestLicense, new ProgressiveSkillGiver(new int[] { 8861501, 8861502, 8861503, 8861504, 8861505, 8861506, 8861507, 8861508, 8861509, 8861510 }));
            RegisterGiver(APWorldItem.RuinedHalberd, new ItemGiver(2150170));
            RegisterGiver(APWorldItem.RustLichArmor, new ItemGiver(3000360));
            RegisterGiver(APWorldItem.RustLichBoots, new ItemGiver(3000362));
            RegisterGiver(APWorldItem.RustLichHelmet, new ItemGiver(3000361));
            RegisterGiver(APWorldItem.RustedSpear, new ItemGiver(2130310));
            RegisterGiver(APWorldItem.SealedMace, new ItemGiver(2020330));
            RegisterGiver(APWorldItem.SilverCurrency, new MoneyGiver(50));
            RegisterGiver(APWorldItem.StrangeRustedSword, new ItemGiver(2000151));
            RegisterGiver(APWorldItem.SunfallAxe, new ItemGiver(2010070));
            RegisterGiver(APWorldItem.TenebrousArmor, new ItemGiver(3000140));
            RegisterGiver(APWorldItem.TenebrousBoots, new ItemGiver(3000142));
            RegisterGiver(APWorldItem.TenebrousHelm, new ItemGiver(3000141));
            RegisterGiver(APWorldItem.ThriceWroughtHalberd, new ItemGiver(2140060));
            RegisterGiver(APWorldItem.TsarArmor, new ItemGiver(3100140));
            RegisterGiver(APWorldItem.TsarBoots, new ItemGiver(3100142));
            RegisterGiver(APWorldItem.TsarFists, new ItemGiver(2160100));
            RegisterGiver(APWorldItem.TsarHelm, new ItemGiver(3100141));
            RegisterGiver(APWorldItem.UnusualKnuckles, new ItemGiver(2160230));
            RegisterGiver(APWorldItem.WarmAxe, new ItemGiver(2010280));
            RegisterGiver(APWorldItem.WillOWisp, new ItemGiver(2300600));
        }
    }
}
