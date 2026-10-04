using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.Outward;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.References
{
    internal class ItemReference : IGraphVizLabelable, IConfluxObjectBuilder<ConfluxItemReference>
    {
        [JsonProperty("m_itemID")]
        [DefaultValue(-1)]
        public int ItemID { get; set; } = -1;

        public string ToGraphVizLabel()
        {
            if (Item.ByID.TryGetValue(ItemID, out var item))
            {
                return item.Key;
            }

            return $"Unknown Item {ItemID}";
        }

        public ConfluxItemReference BuildConfluxObject(Graph graph) => new()
        {
            ID = ItemID,
        };
    }
}
