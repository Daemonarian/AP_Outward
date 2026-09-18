using System.Text.Json;
using System.Text.Json.Serialization;
using NodeCanvasRenderer.GraphViz;

namespace NodeCanvasRenderer.Schema
{
    [JsonConverter(typeof(UnityObjectConverter))]
    internal class UnityObject : IGraphVizLabelable
    {
        public int SideCarIndex { get; set; }

        public string ToGraphVizLabel() => $"UnityObject[{SideCarIndex}]";
    }

    internal class UnityObjectConverter : JsonConverter<UnityObject>
    {
        public override UnityObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return new UnityObject
                {
                    SideCarIndex = reader.GetInt32(),
                };
            }

            throw new JsonException($"Expected a number but got {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, UnityObject value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.SideCarIndex);
        }
    }
}
