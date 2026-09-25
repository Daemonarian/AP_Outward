
using Newtonsoft.Json;

namespace NodeCanvas.Tool.Schema
{
    [JsonConverter(typeof(UnityObjectConverter))]
    internal class UnityObject : IGraphVizLabelable
    {
        public int SideCarIndex { get; set; }

        public string ToGraphVizLabel() => $"UnityObject[{SideCarIndex}]";
    }

    internal class UnityObjectConverter : JsonConverter<UnityObject>
    {
        public override UnityObject? ReadJson(JsonReader reader, Type objectType, UnityObject? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                var index = Convert.ToInt32(reader.Value);
                return new UnityObject
                {
                    SideCarIndex = index,
                };
            }

            throw new JsonSerializationException($"Expected a number for UnityObject, but got {reader.TokenType}.");
        }

        public override void WriteJson(JsonWriter writer, UnityObject? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(value.SideCarIndex);
        }
    }
}
