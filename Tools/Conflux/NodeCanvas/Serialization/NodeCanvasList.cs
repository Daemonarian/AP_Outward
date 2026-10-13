using System.Text;
using Conflux.GraphViz;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Serialization
{
    [JsonConverter(typeof(NodeCanvasListConverter))]
    internal class NodeCanvasList<T> : IConfluxObjectBuilder<ConfluxList<T>>, IGraphVizLabelable where T : IGraphVizLabelable
    {
        [GraphVizProperty(IsMainProperty = true)]
        public List<T>? Items { get; set; }

        public ConfluxList<T> BuildConfluxObject(Graph graph) => new()
        {
            Items = Items is null ? null : [.. Items],
        };

        public string ToGraphVizLabel()
        {
            var content = new StringBuilder();
            content.AppendLine();

            if (Items is not null)
            {
                foreach (var item in Items)
                {
                    content.AppendLine($"- {GraphVizConverter.IndentLines(item.ToGraphVizLabel()).Trim()}");
                }
            }

            return content.ToString().TrimEnd();
        }
    }

    [JsonConverter(typeof(NodeCanvasListConverter))]
    internal class NodeCanvasList<TNodeCanvas, TConflux> : IConfluxObjectBuilder<ConfluxList<TNodeCanvas, TConflux>>, IGraphVizLabelable where TNodeCanvas : IConfluxObjectBuilder<TConflux>, IGraphVizLabelable where TConflux : INodeCanvasObjectBuilder<TNodeCanvas>
    {
        public List<TNodeCanvas>? Items { get; set; }

        public ConfluxList<TNodeCanvas, TConflux> BuildConfluxObject(Graph graph) => new()
        {
            Items = Items is null ? null : [.. Items.Select(i => i.BuildConfluxObject(graph))],
        };

        public string ToGraphVizLabel()
        {
            var content = new StringBuilder();
            content.AppendLine();

            if (Items is not null)
            {
                foreach (var item in Items)
                {
                    content.AppendLine($"- {GraphVizConverter.IndentLines(item.ToGraphVizLabel()).Trim()}");
                }
            }

            return content.ToString().TrimEnd();
        }
    }

    internal class NodeCanvasListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(NodeCanvasList<>);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            var itemsProp = value.GetType().GetProperty(nameof(NodeCanvasList<>.Items)) ?? throw new ConfluxException($"Could not find property {nameof(NodeCanvasList<>.Items)} of {value.GetType()}.");
            var items = itemsProp.GetValue(value);
            if (items is null)
            {
                writer.WriteNull();
                return;
            }

            serializer.Serialize(writer, items);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var itemsProp = objectType.GetProperty(nameof(NodeCanvasList<>.Items)) ?? throw new ConfluxException($"Could not find property {nameof(NodeCanvasList<>.Items)} of {objectType}.");
            var items = serializer.Deserialize(reader, itemsProp.PropertyType);

            var list = Activator.CreateInstance(objectType);
            itemsProp.SetValue(list, items);

            return list;
        }
    }
}
