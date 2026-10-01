using Conflux.GraphViz;
using Conflux.Outward;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.References
{
    internal class QuestEventReference : IGraphVizLabelable, IConfluxObjectBuilder<ConfluxQuestEventReference>
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

        public ConfluxQuestEventReference BuildConfluxObject(Graph graph)
        {
            if (EventUID is null)
            {
                throw new ConfluxValueException("QuestEventReference requires a valid EventUID.");
            }

            if (!QuestEvent.ByUID.TryGetValue(EventUID, out var questEvent))
            {
                throw new ConfluxValueException($"Unknown quest event: {EventUID}.");
            }

            return new()
            {
                Key = questEvent.Key,
            };
        }
    }
}