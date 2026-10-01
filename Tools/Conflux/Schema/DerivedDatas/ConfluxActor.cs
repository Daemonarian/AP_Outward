using Conflux.NodeCanvas.DerivedDatas;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.DerivedDatas
{
    internal class ConfluxActor
    {
        [YamlIgnore]
        public string? Key { get; set; } = null;

        [ConfluxMainProperty]
        [YamlMember(Alias = "id")]
        public Guid? ID { get; set; } = null;

        [YamlMember(Alias = "object")]
        public ConfluxUnityObjectReference? Object { get; set; } = null;

        public ActorParameter BuildActorParameter(NodeCanvasGraphContext context) => new()
        {
            Key = Key ?? throw new ConfluxValueException("The value actor.key must be specified."),
            ID = ID?.ToString(),
            Object = Object?.BuildNodeCanvasObject(context),
        };
    }
}
