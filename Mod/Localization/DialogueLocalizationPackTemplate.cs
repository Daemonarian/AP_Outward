using System.Collections.Generic;
using Newtonsoft.Json;

namespace OutwardArchipelago.Localization
{
    internal class DialogueLocalizationPackTemplate
    {
        [JsonProperty("language", Required = Required.Always)]
        public string Language { get; private set; }

        [JsonProperty("entries", Required = Required.Always)]
        public List<DialogueLocalizationTemplate> Entries { get; private set; }
    }
}
