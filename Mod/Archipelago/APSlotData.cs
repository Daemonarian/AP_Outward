using System.Collections.Generic;

namespace OutwardArchipelago.Archipelago
{
    internal class APSlotData
    {
        private readonly bool _isDeathLinkEnabled = false;
        public bool IsDeathLinkEnabled => _isDeathLinkEnabled;

        private readonly SkillSanityMode _skillSanity = SkillSanityMode.Vanilla;
        public SkillSanityMode SkillSanity => _skillSanity;

        private readonly bool _areWindAltarChecksEnabled = true;
        public bool AreWindAltarChecksEnabled => _areWindAltarChecksEnabled;

        private readonly bool _areBreakthoughPointChecksEnabled = true;
        public bool AreBreakthoughPointChecksEnabled => _areBreakthoughPointChecksEnabled;

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
            _isDeathLinkEnabled = slotData.TryGetValue("slot_data", out var isDeathLinkEnabled) ? (bool)isDeathLinkEnabled : false;
            _skillSanity = slotData.TryGetValue("skillsanity", out var skillSanity) ? (SkillSanityMode)(long)skillSanity : SkillSanityMode.Vanilla;
            _areWindAltarChecksEnabled = slotData.TryGetValue("wind_altar_checks", out var areWindAltarChecksEnabled) ? (bool)areWindAltarChecksEnabled : true;
            _areBreakthoughPointChecksEnabled = slotData.TryGetValue("breakthrough_point_checks", out var areBreakthroughPointChecksEnabled) ? (bool)areBreakthroughPointChecksEnabled : true;
        }

        public enum SkillSanityMode
        {
            Vanilla = 0,
            TierOneOnly = 1,
            Full = 2,
        }
    }
}
