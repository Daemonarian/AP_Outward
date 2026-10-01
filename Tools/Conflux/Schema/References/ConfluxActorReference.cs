using Conflux.Schema.Context;
using Conflux.Schema.DerivedDatas;
using Conflux.Schema.Deserializer;
using Conflux.Schema.Exceptions;
using YamlDotNet.Serialization;

namespace Conflux.Schema.References
{
    internal class ConfluxActorReference
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "key")]
        public string? Key { get; set; } = null;

        public ConfluxActor BuildActor(NodeCanvasGraphContext context)
        {
            if (string.IsNullOrWhiteSpace(Key))
            {
                throw new ConfluxValueException("The value actor.key must be specified.");
            }

            if (!((ConfluxDTScript)context.Script).DTDerivedData.Actors.TryGetValue(Key, out var actor))
            {
                throw new ConfluxValueException($"Unknown actor: {Key}.");
            }

            return actor;
        }
    }
}
