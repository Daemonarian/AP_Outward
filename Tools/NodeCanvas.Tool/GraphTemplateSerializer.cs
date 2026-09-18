using System.Text.Json;
using System.Text.Json.Serialization;
using NodeCanvas.Tool.Schema;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool
{
    internal static class GraphTemplateSerializer
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            AllowOutOfOrderMetadataProperties = true,
            WriteIndented = true,
        };

        public static GraphReplacementTemplate Deserialize(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<GraphReplacementTemplate>(json, SerializerOptions) ?? throw new Exception("Input did not contain a NodeCanvas graph replacement template.");
            }
            catch (Exception e)
            {
                try
                {
                    return new GraphReplacementTemplate
                    {
                        Graph = JsonSerializer.Deserialize<GraphSerializationData>(json, SerializerOptions) ?? throw new Exception("Input did not contain a NodeCanvas graph."),
                    };
                }
                catch (Exception)
                {
                    throw e;
                }
            }
        }

        public static string Serialize(GraphReplacementTemplate template)
        {
            return JsonSerializer.Serialize(template, SerializerOptions);
        }
    }
}
