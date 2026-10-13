using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.Outward
{
    internal class OutwardEquipmentSlot
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum EquipmentSlotIDs
        {
            [EnumMember(Value = "Helmet")]
            Helmet,
            [EnumMember(Value = "Chest")]
            Chest,
            [EnumMember(Value = "Legs")]
            Legs,
            [EnumMember(Value = "Foot")]
            Foot,
            [EnumMember(Value = "Hands")]
            Hands,
            [EnumMember(Value = "RightHand")]
            RightHand,
            [EnumMember(Value = "LeftHand")]
            LeftHand,
            [EnumMember(Value = "Back")]
            Back,
            [EnumMember(Value = "Quiver")]
            Quiver,
            [EnumMember(Value = "Count")]
            Count
        }
    }
}
