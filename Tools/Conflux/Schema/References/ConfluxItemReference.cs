using Conflux.NodeCanvas.References;
using Conflux.Outward;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.Exceptions;
using YamlDotNet.Serialization;

namespace Conflux.Schema.References
{
    internal class ConfluxItemReference : INodeCanvasObjectBuilder<ItemReference>
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "key")]
        public string Key { get; set; } = string.Empty;

        public ItemReference BuildNodeCanvasObject(NodeCanvasGraphContext context)
        {
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
