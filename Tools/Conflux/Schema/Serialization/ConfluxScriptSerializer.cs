using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Conflux.Schema.Serialization
{
    internal static class ConfluxScriptSerializer
    {
        public static IDeserializer Deserializer => _deserializer.Value;

        public static ISerializer Serializer => _serializer.Value;

        private static readonly Lazy<IDeserializer> _deserializer = new(BuildDeserializer);

        private static readonly Lazy<ISerializer> _serializer = new(BuildSerializer);

        private static IDeserializer BuildDeserializer() => new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithTypeDiscriminatingNodeDeserializer(options =>
            {
                options.AddKeyValueTypeDiscriminator<ConfluxScript>("type", new Dictionary<string, Type>()
                {
                    { "behaviour", typeof(ConfluxBTScript) },
                    { "dialogue", typeof(ConfluxDTScript) },
                });
            })
            .WithNodeDeserializer(new ConfluxPolymorphicDeserializer(), s => s.OnTop())
            .WithNodeDeserializer(new ConfluxMainPropertyDeserializer(), s => s.OnTop())
            .Build();

        private static ISerializer BuildSerializer() => new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .WithTypeConverter(new ConfluxMainPropertyTypeConverter())
            .WithEventEmitter(next => new ConfluxPolymorphicEventEmitter(next))
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitEmptyCollections)
            .Build();

        public static ConfluxScript Deserialize(string yaml)
        {
            return Deserializer.Deserialize<ConfluxScript>(yaml);
        }

        public static string Serialize(ConfluxScript script)
        {
            return Serializer.Serialize(script);
        }
    }
}
