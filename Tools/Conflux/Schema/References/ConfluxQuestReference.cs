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
        [YamlIgnore]
        private Item? _item = null;

        [YamlIgnore]
        public int? ID
        {
            get => _item?.ID;

            set
            {
                if (!value.HasValue)
                {
                    _item = null;
                    return;
                }

                if (!Item.ByID.TryGetValue(value.Value, out var item))
                {
                    throw new ConfluxException($"Unknown quest ID: {value}.");
                }

                _item = item;
            }
        }

        [ConfluxMainProperty]
        [YamlMember(Alias = "key")]
        public string? Key
        {
            get => _item?.Key;

            set
            {
                if (value is null)
                {
                    _item = null;
                    return;
                }

                if (!Item.ByKey.TryGetValue(value, out var item))
                {
                    throw new ConfluxException($"Unknown quest key: {value}.");
                }

                _item = item;
            }
        }

        public QuestReference BuildNodeCanvasObject(NodeCanvasGraphContext context)
        {
            if (!ID.HasValue)
            {
                throw new ConfluxValueException("The quest must be specified.");
            }

            return new()
            {
                ItemID = ID.Value,
            };
        }
    }
}
