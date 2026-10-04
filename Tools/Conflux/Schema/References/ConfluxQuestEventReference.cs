using Conflux.NodeCanvas.References;
using Conflux.Outward;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.References
{
    internal class ConfluxQuestEventReference : INodeCanvasObjectBuilder<QuestEventReference>
    {
        [YamlIgnore]
        private QuestEvent? _questEvent = null;

        [YamlIgnore]
        public string? UID
        {
            get => _questEvent?.UID;

            set
            {
                if (value is null)
                {
                    _questEvent = null;
                    return;
                }

                if (!QuestEvent.ByUID.TryGetValue(value, out var questEvent))
                {
                    throw new ConfluxException($"Unknown quest event UID: {value}.");
                }

                _questEvent = questEvent;
            }
        }

        [ConfluxMainProperty]
        [YamlMember(Alias = "key")]
        public string? Key
        {
            get => _questEvent?.Key;

            set
            {
                if (value is null)
                {
                    _questEvent = null;
                    return;
                }

                if (!QuestEvent.ByKey.TryGetValue(value, out var questEvent))
                {
                    throw new ConfluxException($"Unknown quest event key: {value}.");
                }

                _questEvent = questEvent;
            }
        }

        public QuestEventReference BuildNodeCanvasObject(NodeCanvasGraphContext context)
        {
            if (UID is null)
            {
                throw new ConfluxValueException("The quest event must be specified.");
            }

            return new()
            {
                EventUID = UID,
            };
        }
    }
}
