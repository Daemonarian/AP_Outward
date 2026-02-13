using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.SkillTrainer
{
    internal static class SkillTrainerSanity
    {
        public static readonly IReadOnlyList<SkillTrainerInfo> AllSkillTrainerInfos = new SkillTrainerInfo[]
        {
            new(OutwardSkill.CallToElements, APWorld.Location.SkillTrainerAdalbertCallToElements, 1),
            new(OutwardSkill.ManaPush, APWorld.Location.SkillTrainerAdalbertManaPush, 1),
            new(OutwardSkill.RevealSoul, APWorld.Location.SkillTrainerAdalbertRevealSoul, 1),
            new(OutwardSkill.WeatherTolerance, APWorld.Location.SkillTrainerAdalbertWeatherTolerance, 1),
            new(OutwardSkill.ShamanicResonance, APWorld.Location.SkillTrainerAdalbertShamanicResonance, 2),
            new(OutwardSkill.SigilOfWind, APWorld.Location.SkillTrainerAdalbertSigilOfWind, 3),
            new(OutwardSkill.InfuseWind, APWorld.Location.SkillTrainerAdalbertInfuseWind, 3),
            new(OutwardSkill.Conjure, APWorld.Location.SkillTrainerAdalbertConjure, 3),

            new(OutwardSkill.ChakramArc, APWorld.Location.SkillTrainerAlemmonChakramArc, 1),
            new(OutwardSkill.ChakramPierce, APWorld.Location.SkillTrainerAlemmonChakramPierce, 1),
            new(OutwardSkill.ManaWard, APWorld.Location.SkillTrainerAlemmonManaWard, 1),
            new(OutwardSkill.SigilOfFire, APWorld.Location.SkillTrainerAlemmonSigilOfFire, 1),
            new(OutwardSkill.LeylineConnection, APWorld.Location.SkillTrainerAlemmonLeylineConnection, 2),
            new(OutwardSkill.ChakramDance, APWorld.Location.SkillTrainerAlemmonChakramDance, 3),
            new(OutwardSkill.SigilOfIce, APWorld.Location.SkillTrainerAlemmonSigilOfIce, 3),
            new(OutwardSkill.FireAffinity, APWorld.Location.SkillTrainerAlemmonFireAffinity, 3),

            new(OutwardSkill.WarriorsVein, APWorld.Location.SkillTrainerBeaWarriorsVein, 1),
            new(OutwardSkill.Dispersion, APWorld.Location.SkillTrainerBeaDispersion, 1),
            new(OutwardSkill.MomentOfTruth, APWorld.Location.SkillTrainerBeaMomentOfTruth, 1),
            new(OutwardSkill.Technique, APWorld.Location.SkillTrainerBeaTechnique, 1),
            new(OutwardSkill.ScalpCollector, APWorld.Location.SkillTrainerBeaScalpCollector, 1),
            new(OutwardSkill.Crescendo, APWorld.Location.SkillTrainerBeaCrescendo, 1),
            new(OutwardSkill.ViciousCycle, APWorld.Location.SkillTrainerBeaViciousCycle, 1),
            new(OutwardSkill.Splitter, APWorld.Location.SkillTrainerBeaSplitter, 1),
            new(OutwardSkill.VitalCrash, APWorld.Location.SkillTrainerBeaVitalCrash, 1),
            new(OutwardSkill.StrafingRun, APWorld.Location.SkillTrainerBeaStrafingRun, 1),

            new(OutwardSkill.Jinx, APWorld.Location.SkillTrainerEllaJinx, 1),
            new(OutwardSkill.Nightmares, APWorld.Location.SkillTrainerEllaNightmares, 1),
            new(OutwardSkill.Torment, APWorld.Location.SkillTrainerEllaTorment, 1),
            new(OutwardSkill.Bloodlust, APWorld.Location.SkillTrainerEllaBloodlust, 2),
            new(OutwardSkill.BloodSigil, APWorld.Location.SkillTrainerEllaBloodSigil, 3),
            new(OutwardSkill.Rupture, APWorld.Location.SkillTrainerEllaRupture, 3),
            new(OutwardSkill.Cleanse, APWorld.Location.SkillTrainerEllaCleanse, 3),
            new(OutwardSkill.LockwellsRevelation, APWorld.Location.SkillTrainerEllaLockwellsRevelation, 3),

            new(OutwardSkill.Fitness, APWorld.Location.SkillTrainerEtoFitness, 1),
            new(OutwardSkill.ShieldCharge, APWorld.Location.SkillTrainerEtoShieldCharge, 1),
            new(OutwardSkill.SteadyArm, APWorld.Location.SkillTrainerEtoSteadyArm, 1),
            new(OutwardSkill.SpellbladesAwakening, APWorld.Location.SkillTrainerEtoSpellbladesAwakening, 2),
            new(OutwardSkill.InfuseFire, APWorld.Location.SkillTrainerEtoInfuseFire, 3),
            new(OutwardSkill.InfuseFrost, APWorld.Location.SkillTrainerEtoInfuseFrost, 3),
            new(OutwardSkill.GongStrike, APWorld.Location.SkillTrainerEtoGongStrike, 3),
            new(OutwardSkill.ElementalDischarge, APWorld.Location.SkillTrainerEtoElementalDischarge, 3),

            new(OutwardSkill.RuneDez, APWorld.Location.SkillTrainerFlaseRuneDez, 1),
            new(OutwardSkill.RuneEgoth, APWorld.Location.SkillTrainerFlaseRuneEgoth, 1),
            new(OutwardSkill.RuneFal, APWorld.Location.SkillTrainerFlaseRuneFal, 1),
            new(OutwardSkill.RuneShim, APWorld.Location.SkillTrainerFlaseRuneShim, 1),
            new(OutwardSkill.WellOfMana, APWorld.Location.SkillTrainerFlaseWellOfMana, 2),
            new(OutwardSkill.ArcaneSyntax, APWorld.Location.SkillTrainerFlaseArcaneSyntax, 3),
            new(OutwardSkill.InternalizedLexicon, APWorld.Location.SkillTrainerFlaseInternalizedLexicon, 3),
            new(OutwardSkill.RunicPrefix, APWorld.Location.SkillTrainerFlaseRunicPrefix, 3),

            new(OutwardSkill.Brace, APWorld.Location.SkillTrainerGaliraBrace, 1),
            new(OutwardSkill.Focus, APWorld.Location.SkillTrainerGaliraFocus, 1),
            new(OutwardSkill.SlowMetabolism, APWorld.Location.SkillTrainerGaliraSlowMetabolism, 1),
            new(OutwardSkill.SteadfastAscetic, APWorld.Location.SkillTrainerGaliraSteadfastAscetic, 2),
            new(OutwardSkill.PerfectStrike, APWorld.Location.SkillTrainerGaliraPerfectStrike, 3),
            new(OutwardSkill.MasterOfMotion, APWorld.Location.SkillTrainerGaliraMasterOfMotion, 3),
            new(OutwardSkill.FlashOnslaught, APWorld.Location.SkillTrainerGaliraFlashOnslaught, 3),
            new(OutwardSkill.Counterstrike, APWorld.Location.SkillTrainerGaliraCounterstrike, 3),

            new(OutwardSkill.ArmorTraining, APWorld.Location.SkillTrainerJaimonArmorTraining, 1),
            new(OutwardSkill.FastMaintenance, APWorld.Location.SkillTrainerJaimonFastMaintenance, 1),
            new(OutwardSkill.FrostBullet, APWorld.Location.SkillTrainerJaimonFrostBullet, 1),
            new(OutwardSkill.ShatterBullet, APWorld.Location.SkillTrainerJaimonShatterBullet, 1),
            new(OutwardSkill.SwiftFoot, APWorld.Location.SkillTrainerJaimonSwiftFoot, 2),
            new(OutwardSkill.Marathoner, APWorld.Location.SkillTrainerJaimonMarathoner, 3),
            new(OutwardSkill.ShieldInfusion, APWorld.Location.SkillTrainerJaimonShieldInfusion, 3),
            new(OutwardSkill.BloodBullet, APWorld.Location.SkillTrainerJaimonBloodBullet, 3),

            new(OutwardSkill.Acrobatics, APWorld.Location.SkillTrainerJustinAcrobatics, 1),
            new(OutwardSkill.Brains, APWorld.Location.SkillTrainerJustinBrains, 1),
            new(OutwardSkill.Brawns, APWorld.Location.SkillTrainerJustinBrawns, 1),
            new(OutwardSkill.Cruelty, APWorld.Location.SkillTrainerJustinCruelty, 1),
            new(OutwardSkill.Patience, APWorld.Location.SkillTrainerJustinPatience, 1),
            new(OutwardSkill.Unsealed, APWorld.Location.SkillTrainerJustinUnsealed, 1),

            new(OutwardSkill.Efficiency, APWorld.Location.SkillTrainerSergeEfficiency, 1),
            new(OutwardSkill.MetabolicPurge, APWorld.Location.SkillTrainerSergeMetabolicPurge, 1),
            new(OutwardSkill.Probe, APWorld.Location.SkillTrainerSergeProbe, 1),
            new(OutwardSkill.Daredevil, APWorld.Location.SkillTrainerSergeDaredevil, 2),
            new(OutwardSkill.Prime, APWorld.Location.SkillTrainerSergePrime, 3),
            new(OutwardSkill.UnerringRead, APWorld.Location.SkillTrainerSergeUnerringRead, 3),
            new(OutwardSkill.Blitz, APWorld.Location.SkillTrainerSergeBlitz, 3),
            new(OutwardSkill.Anticipation, APWorld.Location.SkillTrainerSergeAnticipation, 3),

            new(OutwardSkill.HauntingBeat, APWorld.Location.SkillTrainerSinaiHauntingBeat, 1),
            new(OutwardSkill.MiasmicTolerance, APWorld.Location.SkillTrainerSinaiMiasmicTolerance, 1),
            new(OutwardSkill.WelkinRing, APWorld.Location.SkillTrainerSinaiWelkinRing, 1),
            new(OutwardSkill.SacredFumes, APWorld.Location.SkillTrainerSinaiSacredFumes, 2),
            new(OutwardSkill.NurturingEcho, APWorld.Location.SkillTrainerSinaiNurturingEcho, 3),
            new(OutwardSkill.Reverberation, APWorld.Location.SkillTrainerSinaiReverberation, 3),
            new(OutwardSkill.HarmonyAndMelody, APWorld.Location.SkillTrainerSinaiHarmonyAndMelody, 3),
            new(OutwardSkill.BattleRhythm, APWorld.Location.SkillTrainerSinaiBattleRythym, 3),

            new(OutwardSkill.Backstab, APWorld.Location.SkillTrainerStyxBackstab, 1),
            new(OutwardSkill.OpportunistStab, APWorld.Location.SkillTrainerStyxOpportunistStab, 1),
            new(OutwardSkill.PressurePlateTraining, APWorld.Location.SkillTrainerStyxPressurePlateTraining, 1),
            new(OutwardSkill.SweepKick, APWorld.Location.SkillTrainerStyxSweepKick, 1),
            new(OutwardSkill.FeatherDodge, APWorld.Location.SkillTrainerStyxFeatherDodge, 2),
            new(OutwardSkill.SerpentsParry, APWorld.Location.SkillTrainerStyxSerpentsParry, 3),
            new(OutwardSkill.StealthTraining, APWorld.Location.SkillTrainerStyxStealthTraining, 3),
            new(OutwardSkill.PressurePlateExpertise, APWorld.Location.SkillTrainerStyxPressurePlateExpertise, 3),

            new(OutwardSkill.Enrage, APWorld.Location.SkillTrainerTureEnrage, 1),
            new(OutwardSkill.EvasionShot, APWorld.Location.SkillTrainerTureEvasionShot, 1),
            new(OutwardSkill.HuntersEye, APWorld.Location.SkillTrainerTureHuntersEye, 1),
            new(OutwardSkill.SniperShot, APWorld.Location.SkillTrainerTureSniperShot, 1),
            new(OutwardSkill.SurvivorsResilience, APWorld.Location.SkillTrainerTureSurvivorsResilience, 2),
            new(OutwardSkill.PredatorLeap, APWorld.Location.SkillTrainerTurePredatorLeap, 3),
            new(OutwardSkill.PiercingShot, APWorld.Location.SkillTrainerTurePiercingShot, 3),
            new(OutwardSkill.FeralStrikes, APWorld.Location.SkillTrainerTureFeralStrikes, 3),
        };

        public static readonly IReadOnlyDictionary<int, SkillTrainerInfo> SkillToInfo = AllSkillTrainerInfos.ToDictionary(info => info.ItemId);

        public static bool TryGetLocationBySkill(int itemId, out APWorld.Location location)
        {
            if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled && SkillToInfo.TryGetValue(itemId, out var info))
            {
                var maxTierToReplace = ArchipelagoConnector.Instance.SlotData.SkillSanity switch
                {
                    APSlotData.SkillSanityMode.Vanilla => 0,
                    APSlotData.SkillSanityMode.TierOneOnly => 1,
                    APSlotData.SkillSanityMode.Full => 3,
                    _ => 0,
                };

                if (info.Tier <= maxTierToReplace)
                {
                    location = info.Location;
                    return true;
                }
            }

            location = default;
            return false;
        }

        public static bool IsEnabled => OutwardArchipelagoMod.Instance.IsArchipelagoEnabled;

        public sealed class SkillTrainerInfo
        {
            private readonly int _itemId;
            private readonly APWorld.Location _location;
            private readonly int _tier;

            public SkillTrainerInfo(int itemId, APWorld.Location location, int tier)
            {
                _itemId = itemId;
                _location = location;
                _tier = tier;
            }

            public int ItemId => _itemId;
            public APWorld.Location Location => _location;
            public int Tier => _tier;
        }

        [HarmonyPatch(typeof(SkillTreeHolder), nameof(SkillTreeHolder.GetSkillTreeFromUID), new Type[] { typeof(UID) })]
        private static class Patch_SkillTreeHolder_GetSkillTreeFromUID
        {
            private static void Postfix(ref SkillSchool __result)
            {
                if (__result && IsEnabled)
                {
                    foreach (var skillSlot in __result.GetComponentsInChildren<SkillSlot>())
                    {
                        if (skillSlot.m_skill && skillSlot.m_skill is not APSkill && TryGetLocationBySkill(skillSlot.m_skill.ItemID, out var location))
                        {
                            skillSlot.m_skill = APSkill.GetPrefab(location);
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(SkillSlot), nameof(SkillSlot.UnlockSkill), new Type[] { typeof(Character) })]
        private static class Patch_SkillSlot_UnlockSkill
        {
            private static bool Prefix(SkillSlot __instance, Character _character)
            {
                if (__instance.m_skill && __instance.m_skill is APSkill skill)
                {
                    if (_character.IsLocalPlayer)
                    {
                        SoundOnEventTrigger.TryPlaySound(_character.OwnerPlayerSys.PlayerID, GlobalAudioManager.Sounds.UI_TRAINER_UnlockSkill, false);
                    }

                    ArchipelagoConnector.Instance.Locations.Complete(skill.Location);

                    if (__instance.IsBreakthrough && _character.IsLocalPlayer)
                    {
                        _character.PlayerStats.UseBreakthrough();
                    }

                    return false;
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(SkillSlot), nameof(SkillSlot.HasSkill), new Type[] { typeof(Character) })]
        private static class Patch_SkillSlot_HasSkill
        {
            private static bool Prefix(ref bool __result, SkillSlot __instance, Character _character)
            {
                if (__instance.m_skill && __instance.m_skill is APSkill skill)
                {
                    __result = ArchipelagoConnector.Instance.Locations.IsComplete(skill.Location);
                    return false;
                }

                __result = default;
                return true;
            }
        }
    }
}
