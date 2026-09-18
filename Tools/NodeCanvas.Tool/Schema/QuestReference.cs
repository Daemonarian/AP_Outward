using System.Text.Json.Serialization;
using NodeCanvas.Tool;
using NodeCanvas.Tool.Schema.Records;

namespace NodeCanvas.Tool.Schema
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