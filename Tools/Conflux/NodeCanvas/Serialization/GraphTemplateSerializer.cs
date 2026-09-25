using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Conflux.NodeCanvas.Serialization
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

        public static GraphTemplate Deserialize(string json)
        {
            var rootObject = JObject.Parse(json);
            var isTemplateLikely = rootObject.ContainsKey("graph");

            if (isTemplateLikely)
            {
                return JsonConvert.DeserializeObject<GraphTemplate>(json, SerializerSettings) ?? throw new Exception("Input did not contain a NodeCanvas graph replacement template.");
            }

            var graph = JsonConvert.DeserializeObject<Graph>(json, SerializerSettings) ?? throw new Exception("Input did not contain a NodeCanvas graph.");
            return new GraphTemplate
            {
                Graph = graph,
            };
        }

        public static string Serialize(GraphTemplate template)
        {
            return JsonConvert.SerializeObject(template, SerializerSettings);
        }
    }
}
