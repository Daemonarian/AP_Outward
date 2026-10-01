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
        [ConfluxMainProperty]
        [YamlMember(Alias = "key")]
        public string? Key { get; set; }

        public QuestEventReference BuildNodeCanvasObject(NodeCanvasGraphContext context)
        {
            if (Key is null)
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
