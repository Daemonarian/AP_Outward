using System.Collections.Generic;

namespace OutwardArchipelago.Archipelago
{
    /// <summary>
    /// Represents additional configuration for this slot from the AP server.
    /// This includes additional options passed through from the YAML file.
    /// </summary>
    internal class APSlotData
    {
        private readonly GoalMode _goal = GoalMode.MainQuest7;
        private readonly bool _isDeathLinkEnabled = false;
        private readonly SkillSanityMode _skillSanity = SkillSanityMode.Vanilla;
        private readonly bool _areWindAltarChecksEnabled = true;
        private readonly bool _areBreakthoughPointChecksEnabled = true;

        /// <summary>
        /// Construct a slot data object with default values.
        /// </summary>
        public APSlotData() { }

        /// <summary>
        /// Construct a slot data object from the slot data provided by the AP server.
        /// </summary>
        /// <param name="slotData"></param>
        public APSlotData(Dictionary<string, object> slotData)
        {
            if (slotData.TryGetValue("goal", out var goal))
            {
                _goal = (GoalMode)(long)goal;
            }

            if (slotData.TryGetValue("slot_data", out var isDeathLinkEnabled))
            {
                _isDeathLinkEnabled = (bool)isDeathLinkEnabled;
            }

            if (slotData.TryGetValue("skillsanity", out var skillSanity))
            {
                _skillSanity = (SkillSanityMode)(long)skillSanity;
            }

            if (slotData.TryGetValue("wind_altar_checks", out var areWindAltarChecksEnabled))
            {
                _areWindAltarChecksEnabled = (bool)areWindAltarChecksEnabled;
            }

            if (slotData.TryGetValue("breakthrough_point_checks", out var areBreakthroughPointChecksEnabled))
            {
                _areBreakthoughPointChecksEnabled = (bool)areBreakthroughPointChecksEnabled;
            }
        }

        /// <summary>
        /// What counts as goaling or completing the game, aka when should we
        /// tell the AP server that the goal has been reached?
        /// </summary>
        public GoalMode Goal => _goal;

        /// <summary>
        /// Whether death-link should be enabled.
        /// </summary>
        public bool IsDeathLinkEnabled => _isDeathLinkEnabled;

        /// <summary>
        /// What skillsanity mode should be enabled.
        /// </summary>
        public SkillSanityMode SkillSanity => _skillSanity;

        /// <summary>
        /// Whether we should send replace the wind altars with location checks.
        /// </summary>
        public bool AreWindAltarChecksEnabled => _areWindAltarChecksEnabled;

        /// <summary>
        /// Whether we should add location checks for interacting with skill trainers.
        /// </summary>
        public bool AreBreakthoughPointChecksEnabled => _areBreakthoughPointChecksEnabled;

        /// <summary>
        /// The various different goals we can configure for the game.
        /// </summary>
        public enum GoalMode
        {
            MainQuest1 = 0,
            MainQuest2 = 1,
            MainQuest3 = 2,
            MainQuest4 = 3,
            MainQuest5 = 4,
            MainQuest6 = 5,
            MainQuest7 = 6,
            MainQuest8 = 7,
            MainQuest9 = 8,
            MainQuest10 = 9,
            MainQuest11 = 10,
            MainQuest12 = 11,

            ParallelQuestBloodUnderTheSun = 12,
            ParallelQuestPurifier = 13,
            ParallelQuestVendavelQuest = 14,
            ParallelQuestRustAndVengeance = 15,
        }

        /// <summary>
        /// The various skillsanity options.
        /// </summary>
        public enum SkillSanityMode
        {
            Vanilla = 0,
            TierOneOnly = 1,
            Full = 2,
        }
    }
}
