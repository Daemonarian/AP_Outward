using Conflux.GraphViz;
using Conflux.Outward;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.References
{
    public class QuestReference : IGraphVizLabelable
    {
        public static QuestReference FromKey(string key) => new()
        {
            ItemID = Item.ByKey[key].ID,
        };

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