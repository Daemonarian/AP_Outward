using System.Collections.Generic;

namespace OutwardArchipelago.Archipelago
{
    internal class APSlotData
    {
        private readonly bool _isDeathLinkEnabled = false;
        public bool IsDeathLinkEnabled => _isDeathLinkEnabled;

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
            _isDeathLinkEnabled = slotData.TryGetValue("slot-data", out var isDeathLinkEnabled) ? (bool)isDeathLinkEnabled : false;
        }
    }
}
