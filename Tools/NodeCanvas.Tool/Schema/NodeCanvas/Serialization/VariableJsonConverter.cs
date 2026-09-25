using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Serialization
{
    internal class VariableJsonConverter : JsonConverter<Variable>
    {
        private class BaseBehaviorResolver : CamelCasePropertyNamesContractResolver
        {
            protected override JsonContract CreateContract(Type objectType)
            {
                var contract = base.CreateContract(objectType);
                if (objectType == typeof(Variable))
                {
                    contract.Converter = null;
                }

                return contract;
            }
        }

        private static readonly JsonSerializer BaseSerializer = new()
        {
            ContractResolver = new BaseBehaviorResolver(),
            NullValueHandling = NullValueHandling.Ignore,
        };

        public override Variable? ReadJson(JsonReader reader, Type objectType, Variable? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var obj = JObject.Load(reader);
            var target = new Variable();
            using (var subReader = obj.CreateReader())
            {
                BaseSerializer.Populate(subReader, target);
            }

            return target;
        }

        public override void WriteJson(JsonWriter writer, Variable? value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            var obj = JObject.FromObject(value, BaseSerializer);
            obj.WriteTo(writer);
        }
    }
}
