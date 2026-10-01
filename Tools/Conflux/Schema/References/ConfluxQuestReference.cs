using Conflux.NodeCanvas.References;
using Conflux.Outward;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.References
{
    internal class ConfluxQuestReference : INodeCanvasObjectBuilder<QuestReference>
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "key")]
        public string? Key { get; set; }

        public QuestReference BuildNodeCanvasObject(NodeCanvasGraphContext context)
        {
            if (Key is null)
            {
                throw new ConfluxValueException("The value quest.key must be specified.");
            }

            if (!Item.ByKey.TryGetValue(Key, out var item))
            {
                throw new ConfluxValueException($"Unknown quest: {Key}.");
            }

            return new()
            {
                ItemID = item.ID,
            };
        }
    }
}
