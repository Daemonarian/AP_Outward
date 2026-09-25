using Conflux.GraphViz;
using Conflux.Outward;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.References
{
    internal class ItemReference : IGraphVizLabelable
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
    }
}
