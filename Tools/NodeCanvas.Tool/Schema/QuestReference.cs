using System.Text.Json.Serialization;
using NodeCanvasRenderer.GraphViz;
using NodeCanvasRenderer.Schema.Records;

namespace NodeCanvasRenderer.Schema
{
    public class QuestReference : IGraphVizLabelable
    {
        [JsonPropertyName("m_itemID")]
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