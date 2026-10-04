using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Conflux.NodeCanvas.Serialization
{
    internal class GraphTemplateSerializer
    {
        private static readonly JsonSerializerSettings SerializerSettings = new()
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.Auto,
            MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
            SerializationBinder = new NodeCanvasSerializationBinder(),
            DefaultValueHandling = DefaultValueHandling.Ignore,
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
            var serializer = JsonSerializer.Create(SerializerSettings);
            var rootToken = JToken.FromObject(template, serializer);

            var usedRefs = new HashSet<string>();
            CollectReferences(rootToken, usedRefs);
            PruneAndReorderMetadata(rootToken, usedRefs);

            return rootToken.ToString(SerializerSettings.Formatting);
        }

        private static void CollectReferences(JToken token, ISet<string> usedRefs)
        {
            if (token is JObject obj)
            {
                var refProp = obj.Property("$ref");
                if (refProp?.Value.Type == JTokenType.String)
                {
                    var id = refProp.Value.Value<string>();
                    if (id is not null)
                    {
                        usedRefs.Add(id);
                    }
                }

                foreach (var prop in obj.Properties())
                {
                    CollectReferences(prop.Value, usedRefs);
                }
            }
            else if (token is JArray arr)
            {
                foreach (var item in arr)
                {
                    CollectReferences(item, usedRefs);
                }
            }
        }

        private static void PruneAndReorderMetadata(JToken token, IReadOnlySet<string> usedRefs)
        {
            if (token is JObject obj)
            {
                var typeProp = obj.Property("$type");
                var idProp = obj.Property("$id");
                var refProp = obj.Property("$ref");

                typeProp?.Remove();
                idProp?.Remove();
                refProp?.Remove();

                foreach (var prop in obj.Properties())
                {
                    PruneAndReorderMetadata(prop.Value, usedRefs);
                }

                if (typeProp is not null)
                {
                    obj.Add(typeProp);
                }

                if (idProp is not null && idProp.Value.Type == JTokenType.String)
                {
                    var id = idProp.Value.Value<string>();
                    if (id is not null && usedRefs.Contains(id))
                    {
                        obj.Add(idProp);
                    }
                }

                if (refProp is not null)
                {
                    obj.Add(refProp);
                }
            }
            else if (token is JArray arr)
            {
                foreach (var item in arr)
                {
                    PruneAndReorderMetadata(item, usedRefs);
                }
            }
        }
    }
}
