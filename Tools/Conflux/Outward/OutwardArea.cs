using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Conflux.Outward
{
    internal class OutwardArea
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum AreaEnum
        {
            [EnumMember(Value = "PT_Test")]
            PT_Test,
            [EnumMember(Value = "PL_Test")]
            PL_Test,
            [EnumMember(Value = "Nath_Test")]
            Nath_Test,
            [EnumMember(Value = "Enemies_Test")]
            Enemies_Test,
            [EnumMember(Value = "Aurelie_Test")]
            Aurelie_Test,
            [EnumMember(Value = "Aurelie_Test2")]
            Aurelie_Test2,
            [EnumMember(Value = "CierzoVillage")]
            CierzoVillage = 100,
            [EnumMember(Value = "CierzoOutside")]
            CierzoOutside,
            [EnumMember(Value = "CierzoDungeon")]
            CierzoDungeon,
            [EnumMember(Value = "ChersoDungeon1")]
            ChersoDungeon1,
            [EnumMember(Value = "ChersoDungeon2")]
            ChersoDungeon2,
            [EnumMember(Value = "ChersoDungeon3")]
            ChersoDungeon3,
            [EnumMember(Value = "ChersoDungeon4")]
            ChersoDungeon4,
            [EnumMember(Value = "ChersoDungeon5")]
            ChersoDungeon5,
            [EnumMember(Value = "ChersoDungeon6")]
            ChersoDungeon6,
            [EnumMember(Value = "ChersoDungeon7")]
            ChersoDungeon7,
            [EnumMember(Value = "ChersoDungeon8")]
            ChersoDungeon8,
            [EnumMember(Value = "ChersoDungeon9")]
            ChersoDungeon9,
            [EnumMember(Value = "ChersoDungeon4_BC")]
            ChersoDungeon4_BC,
            [EnumMember(Value = "ChersoDungeon4_HM")]
            ChersoDungeon4_HM,
            [EnumMember(Value = "ChersoDungeon4_LV")]
            ChersoDungeon4_LV,
            [EnumMember(Value = "CierzoDestroyed")]
            CierzoDestroyed,
            [EnumMember(Value = "ChersoDungeonsSmall")]
            ChersoDungeonsSmall = 150,
            [EnumMember(Value = "ChersoDungeonsBosses")]
            ChersoDungeonsBosses,
            [EnumMember(Value = "Monsoon")]
            Monsoon = 200,
            [EnumMember(Value = "HallowedMarsh")]
            HallowedMarsh,
            [EnumMember(Value = "HallowedDungeon1")]
            HallowedDungeon1,
            [EnumMember(Value = "HallowedDungeon2")]
            HallowedDungeon2,
            [EnumMember(Value = "HallowedDungeon3")]
            HallowedDungeon3,
            [EnumMember(Value = "HallowedDungeon4")]
            HallowedDungeon4,
            [EnumMember(Value = "HallowedDungeon4_Interior")]
            HallowedDungeon4_Interior,
            [EnumMember(Value = "HallowedDungeon5")]
            HallowedDungeon5,
            [EnumMember(Value = "HallowedDungeon6")]
            HallowedDungeon6,
            [EnumMember(Value = "HallowedDungeon7")]
            HallowedDungeon7,
            [EnumMember(Value = "HallowedDungeonsSmall")]
            HallowedDungeonsSmall = 250,
            [EnumMember(Value = "HallowedDungeonsBosses")]
            HallowedDungeonsBosses,
            [EnumMember(Value = "Levant")]
            Levant = 300,
            [EnumMember(Value = "Abrassar")]
            Abrassar,
            [EnumMember(Value = "AbrassarDungeon1")]
            AbrassarDungeon1,
            [EnumMember(Value = "AbrassarDungeon2")]
            AbrassarDungeon2,
            [EnumMember(Value = "AbrassarDungeon3")]
            AbrassarDungeon3,
            [EnumMember(Value = "AbrassarDungeon4")]
            AbrassarDungeon4,
            [EnumMember(Value = "AbrassarDungeon5")]
            AbrassarDungeon5,
            [EnumMember(Value = "AbrassarDungeon6")]
            AbrassarDungeon6,
            [EnumMember(Value = "AbrassarDungeon7")]
            AbrassarDungeon7,
            [EnumMember(Value = "AbrassarDungeon8")]
            AbrassarDungeon8,
            [EnumMember(Value = "AbrassarDungeonsSmall")]
            AbrassarDungeonsSmall = 350,
            [EnumMember(Value = "AbrassarDungeonsBosses")]
            AbrassarDungeonsBosses,
            [EnumMember(Value = "Harmattan")]
            Harmattan = 400,
            [EnumMember(Value = "AntiqueField")]
            AntiqueField,
            [EnumMember(Value = "AntiqueFieldDungeon1")]
            AntiqueFieldDungeon1,
            [EnumMember(Value = "AntiqueFieldDungeon2")]
            AntiqueFieldDungeon2,
            [EnumMember(Value = "AntiqueFieldDungeon3")]
            AntiqueFieldDungeon3,
            [EnumMember(Value = "AntiqueFieldDungeon4")]
            AntiqueFieldDungeon4,
            [EnumMember(Value = "AntiqueFieldDungeon5")]
            AntiqueFieldDungeon5,
            [EnumMember(Value = "AntiqueFieldDungeon6")]
            AntiqueFieldDungeon6,
            [EnumMember(Value = "AntiqueFieldDungeon7")]
            AntiqueFieldDungeon7,
            [EnumMember(Value = "AntiqueFieldDungeon8")]
            AntiqueFieldDungeon8,
            [EnumMember(Value = "AntiqueFieldDungeon9")]
            AntiqueFieldDungeon9,
            [EnumMember(Value = "AntiqueFieldDungeonsSmall")]
            AntiqueFieldDungeonsSmall = 450,
            [EnumMember(Value = "AntiqueFieldDungeonsBosses")]
            AntiqueFieldDungeonsBosses,
            [EnumMember(Value = "Berg")]
            Berg = 500,
            [EnumMember(Value = "Emercar")]
            Emercar,
            [EnumMember(Value = "EmercarDungeon1")]
            EmercarDungeon1,
            [EnumMember(Value = "EmercarDungeon2")]
            EmercarDungeon2,
            [EnumMember(Value = "EmercarDungeon3")]
            EmercarDungeon3,
            [EnumMember(Value = "EmercarDungeon4")]
            EmercarDungeon4,
            [EnumMember(Value = "EmercarDungeon5")]
            EmercarDungeon5,
            [EnumMember(Value = "EmercarDungeon6")]
            EmercarDungeon6,
            [EnumMember(Value = "EmercarDungeon7")]
            EmercarDungeon7,
            [EnumMember(Value = "EmercarDungeonsSmall")]
            EmercarDungeonsSmall = 550,
            [EnumMember(Value = "DreamWorld")]
            DreamWorld,
            [EnumMember(Value = "Tutorial")]
            Tutorial,
            [EnumMember(Value = "EmercarDungeonsBosses")]
            EmercarDungeonsBosses,
            [EnumMember(Value = "Sirocco")]
            Sirocco = 600,
            [EnumMember(Value = "NewSirocco")]
            NewSirocco,
            [EnumMember(Value = "Caldera")]
            Caldera,
            [EnumMember(Value = "CalderaDungeon1")]
            CalderaDungeon1,
            [EnumMember(Value = "CalderaDungeon2")]
            CalderaDungeon2,
            [EnumMember(Value = "CalderaDungeon3")]
            CalderaDungeon3,
            [EnumMember(Value = "CalderaDungeon4")]
            CalderaDungeon4,
            [EnumMember(Value = "CalderaDungeon5")]
            CalderaDungeon5,
            [EnumMember(Value = "CalderaDungeon6")]
            CalderaDungeon6,
            [EnumMember(Value = "CalderaDungeon7")]
            CalderaDungeon7,
            [EnumMember(Value = "CalderaDungeon8")]
            CalderaDungeon8,
            [EnumMember(Value = "CalderaDungeon9")]
            CalderaDungeon9,
            [EnumMember(Value = "CalderaDungeon10")]
            CalderaDungeon10,
            [EnumMember(Value = "CalderaDungeonsSmall")]
            CalderaDungeonsSmall = 650,
            [EnumMember(Value = "CalderaDungeonsBosses")]
            CalderaDungeonsBosses
        }
    }
}
