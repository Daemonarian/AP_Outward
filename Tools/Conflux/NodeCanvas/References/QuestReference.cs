using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.Outward;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.References
{
    internal class QuestReference : IGraphVizLabelable, IConfluxObjectBuilder<ConfluxQuestReference>
    {
        public static QuestReference FromKey(string key) => new()
        {
            ItemID = Item.ByKey[key].ID,
        };

        [JsonProperty("m_itemID")]
        [DefaultValue(-1)]
        public int ItemID { get; set; } = -1;

        public string ToGraphVizLabel()
        {
            if (Item.ByID.TryGetValue(ItemID, out var item))
            {
                return item.Key;
            }

            return $"{ItemID}";
        }

        public ConfluxQuestReference BuildConfluxObject(Graph graph)
        {
            if (!Item.ByID.TryGetValue(ItemID, out var item))
            {
                throw new ConfluxValueException($"Unknown item ID {ItemID} for QuestReference.");
            }

            return new()
            {
                Key = item.Key,
            };
        }
    }
}