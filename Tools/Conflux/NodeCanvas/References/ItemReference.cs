using Conflux.GraphViz;
using Conflux.Outward;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.References
{
    internal class ItemReference : IGraphVizLabelable, IConfluxObjectBuilder<ConfluxItemReference>
    {
        [JsonProperty("m_itemID")]
        public int ItemID { get; set; } = -1;

        public string ToGraphVizLabel()
        {
            if (Item.ByID.TryGetValue(ItemID, out var item))
            {
                return item.Key;
            }

            return $"{ItemID}";
        }

        public ConfluxItemReference BuildConfluxObject(Graph graph) => new()
        {
            Key = Item.ByID.TryGetValue(ItemID, out var item) ? item.Key : throw new ConfluxValueException($"Unknown item ID: {ItemID}"),
        };
    }
}
