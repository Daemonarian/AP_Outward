using System;
using System.Collections.Generic;
using HarmonyLib;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.SkillTrainer
{
    internal static class SkillTrainerLocationCheck
    {
        public static readonly IReadOnlyDictionary<UID, APWorld.Location> SkillSchoolToLocation = new Dictionary<UID, APWorld.Location>
        {
            {  SkillSchoolUID.CabalHermit, APWorld.Location.SkillTrainerInteractAdalbert },
            {  SkillSchoolUID.Philosopher, APWorld.Location.SkillTrainerInteractAlemmon },
            {  SkillSchoolUID.HexMage, APWorld.Location.SkillTrainerInteractElla },
            {  SkillSchoolUID.KaziteSpellblade, APWorld.Location.SkillTrainerInteractEto },
            {  SkillSchoolUID.RuneSage, APWorld.Location.SkillTrainerInteractFlase },
            {  SkillSchoolUID.WarriorMonk, APWorld.Location.SkillTrainerInteractGalira },
            {  SkillSchoolUID.Mercenary, APWorld.Location.SkillTrainerInteractJaimon },
            {  SkillSchoolUID.Speedster, APWorld.Location.SkillTrainerInteractSerge },
            {  SkillSchoolUID.PrimalRitualist, APWorld.Location.SkillTrainerInteractSinai },
            {  SkillSchoolUID.RogueEngineer, APWorld.Location.SkillTrainerInteractStyx },
            {  SkillSchoolUID.WildHunter, APWorld.Location.SkillTrainerInteractTure },
        };

        [HarmonyPatch(typeof(CharacterUI), nameof(CharacterUI.ShowTrainerMenu), new Type[] { typeof(Trainer) })]
        private static class Patch_CharacterUI_ShowTrainerMenu
        {
            private static bool Prefix(CharacterUI __instance, Trainer _trainer)
            {
                if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled
                    && ArchipelagoConnector.Instance.SlotData.AreBreakthoughPointChecksEnabled
                    && SkillSchoolToLocation.TryGetValue(_trainer.m_skillTreeUID, out var location))
                {
                    ArchipelagoConnector.Instance.Locations.Complete(location);
                }

                return true;
            }
        }
    }
}
