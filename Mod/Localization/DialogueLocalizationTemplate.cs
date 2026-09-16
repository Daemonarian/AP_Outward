using System.Collections.Generic;
using Newtonsoft.Json;

namespace OutwardArchipelago.Localization
{
    internal class DialogueLocalizationTemplate
    {
        [JsonProperty("key", Required = Required.Always)]
        public string Key { get; private set; }

        [JsonProperty("general", Required = Required.Always)]
        public string General { get; private set; }

        [JsonProperty("female")]
        public string Female { get; private set; } = null;

        [JsonProperty("uniqueAudioName")]
        public string UniqueAudioName { get; private set; } = null;

        [JsonProperty("emoteTags")]
        public List<string> EmoteTags { get; private set; } = new();

        [JsonProperty("animTags")]
        public List<string> AnimTags { get; private set; } = new();

        [JsonProperty("dlc")]
        public OTWStoreAPI.DLCs DLC { get; private set; } = OTWStoreAPI.DLCs.None;
    }
}
