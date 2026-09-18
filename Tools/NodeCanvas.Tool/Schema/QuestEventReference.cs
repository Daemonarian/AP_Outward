using System.Text.Json.Serialization;
using NodeCanvasRenderer.GraphViz;
using NodeCanvasRenderer.Schema.Records;

namespace NodeCanvasRenderer.Schema
{
    public class QuestEventReference : IGraphVizLabelable
    {
        [JsonPropertyName("m_eventUID")]
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