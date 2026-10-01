using Conflux.NodeCanvas.References;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.Exceptions;
using YamlDotNet.Serialization;

namespace Conflux.Schema.References
{
    internal class ConfluxLocationReference : INodeCanvasObjectBuilder<APWorldLocationReference>
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "key")]
        public string? Key { get; set; } = null;

        public APWorldLocationReference BuildNodeCanvasObject(NodeCanvasGraphContext context)
        {
            if (string.IsNullOrWhiteSpace(Key))
            {
                throw new ConfluxValueException($"The value location.key must be specified.");
            }

            return new()
            {
                Key = Key,
            };
        }
    }
}
