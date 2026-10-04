using Conflux.GraphViz;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.References
{
    [JsonConverter(typeof(UnityObjectConverter))]
    internal class UnityObject : IGraphVizLabelable, IConfluxObjectBuilder<ConfluxUnityObjectReference>
    {
        public int SideCarIndex { get; set; }

        public string ToGraphVizLabel() => $"UnityObject[{SideCarIndex}]";

        public ConfluxUnityObjectReference BuildConfluxObject(Graph graph) => new()
        {
            SideCarIndex = SideCarIndex,
        };
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
