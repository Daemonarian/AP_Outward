using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.Records;

namespace NodeCanvas.Tool.Schema
{
    public class QuestEventReference : IGraphVizLabelable
    {
        [JsonProperty("m_eventUID")]
        public string? EventUID { get; set; }

        public string ToGraphVizLabel()
        {
            if (EventUID is not null)
            {
                if (QuestEvent.ByUID.TryGetValue(EventUID, out var questEvent))
                {
                    return questEvent.Key;
                }

                return EventUID;
            }

            return string.Empty;
        }
    }
}