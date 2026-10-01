using Conflux.NodeCanvas.References;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.References
{
    internal class ConfluxLocationReference : INodeCanvasObjectBuilder<APWorldLocationReference>
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "key")]
        public string? Key { get; set; }

        public APWorldLocationReference BuildNodeCanvasObject(NodeCanvasGraphContext context)
        {
            if (Key is null)
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
