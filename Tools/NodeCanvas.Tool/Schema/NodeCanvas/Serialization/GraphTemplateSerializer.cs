using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Serialization
{
    internal class GraphTemplateSerializer
    {
        private static readonly JsonSerializerSettings SerializerSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.Auto,
            MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
            SerializationBinder = new NodeCanvasSerializationBinder(),
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
        };

        public static GraphReplacementTemplate Deserialize(string json)
        {
            var rootObject = JObject.Parse(json);
            var isTemplateLikely = rootObject.ContainsKey("graph");

            if (isTemplateLikely)
            {
                return JsonConvert.DeserializeObject<GraphReplacementTemplate>(json, SerializerSettings) ?? throw new Exception("Input did not contain a NodeCanvas graph replacement template.");
            }

            var graph = JsonConvert.DeserializeObject<GraphSerializationData>(json, SerializerSettings) ?? throw new Exception("Input did not contain a NodeCanvas graph.");
            return new GraphReplacementTemplate
            {
                Graph = graph,
            };
        }

        public static string Serialize(GraphReplacementTemplate template)
        {
            return JsonConvert.SerializeObject(template, SerializerSettings);
        }
    }
}
