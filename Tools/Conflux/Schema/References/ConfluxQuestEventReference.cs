using Conflux.NodeCanvas.References;
using Conflux.Outward;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.Exceptions;
using YamlDotNet.Serialization;

namespace Conflux.Schema.References
{
    internal class ConfluxQuestEventReference : INodeCanvasObjectBuilder<QuestEventReference>
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "key")]
        public string? Key { get; set; } = null;

        public QuestEventReference BuildNodeCanvasObject(NodeCanvasGraphContext context)
        {
            if (string.IsNullOrWhiteSpace(Key))
            {
                throw new ConfluxValueException("The value questEvent.key must be specified.");
            }

            if (!QuestEvent.ByKey.TryGetValue(Key, out var questEvent))
            {
                throw new ConfluxValueException($"Unknown quest event: {Key}.");
            }

            return new()
            {
                EventUID = questEvent.UID,
            };
        }
    }
}
