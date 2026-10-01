using Conflux.NodeCanvas.References;
using Conflux.Outward;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.References
{
    internal class ConfluxItemReference : INodeCanvasObjectBuilder<ItemReference>
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "key")]
        public string? Key { get; set; }

        public ItemReference BuildNodeCanvasObject(NodeCanvasGraphContext context)
        {
            if (Key is null)
            {
                throw new ConfluxValueException("The value item.key must be specified.");
            }

            if (!Item.ByKey.TryGetValue(Key, out var item))
            {
                throw new ConfluxValueException($"Unknown item: {item}.");
            }

            return new()
            {
                ItemID = item.ID,
            };
        }
    }
}
