using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Conflux.Schema
{
    internal static class ConfluxScriptSerializer
    {
        public static IDeserializer Deserializer => _deserializer.Value;

        private static readonly Lazy<IDeserializer> _deserializer = new(BuildDeserializer);

        private static IDeserializer BuildDeserializer()
        {
            return new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .WithNodeDeserializer(new ConfluxPolymorphicDeserializer(), s => s.OnTop())
                .WithNodeDeserializer(new ConfluxMainPropertyDeserializer(), s => s.OnTop())
                .Build();
        }

        public static ConfluxScript Deserialize(string yaml)
        {
            return Deserializer.Deserialize<ConfluxScript>(yaml);
        }
    }
}
