using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.Outward
{
    internal class OutwardStore
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum DLCs
        {
            [EnumMember(Value = "None")]
            None,
            [EnumMember(Value = "Preorder")]
            Preorder,
            [EnumMember(Value = "Soroboreans")]
            Soroboreans,
            [EnumMember(Value = "DLC2")]
            DLC2,
            [EnumMember(Value = "Count")]
            Count
        }
    }
}
