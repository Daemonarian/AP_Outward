using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using Conflux.GraphViz;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Serialization
{
    [JsonConverter(typeof(NodeCanvasListConverter))]
    internal class NodeCanvasList<T> : IConfluxObjectBuilder, IGraphVizLabelable
    {
        [GraphVizProperty(IsMainProperty = true)]
        public List<T>? Items { get; set; }

        public ConfluxList<T2> BuildConfluxList<T2>(Graph graph) => new()
        {
            Items = Items is null ? null : [.. Items.Select(item => ConfluxObjectBuilder.Build<T, T2>(graph, item)!)],
        };

        public bool TryBuildConfluxObject<T2>(Graph graph, [NotNullWhen(true)] out T2? target)
        {
            if (typeof(T2).IsGenericType && typeof(T2).GetGenericTypeDefinition() == typeof(ConfluxList<>))
            {
                var buildMethod = GetType()
                    .GetMethod(nameof(BuildConfluxList), BindingFlags.Instance)
                    ?.MakeGenericMethod(typeof(T2).GetGenericArguments());
                if (buildMethod is not null)
                {
                    target = (T2)buildMethod.Invoke(this, [graph])!;
                    return true;
                }
            }

            target = default;
            return false;
        }

        public string ToGraphVizLabel()
        {
            var content = new StringBuilder();
            content.AppendLine();

            if (Items is not null)
            {
                foreach (var item in Items)
                {
                    var itemLabel = GraphVizConverter.BuildGraphVizLabel(item);
                    content.AppendLine($"- {GraphVizConverter.IndentLines(itemLabel, indentFirstLine: false)}");
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
