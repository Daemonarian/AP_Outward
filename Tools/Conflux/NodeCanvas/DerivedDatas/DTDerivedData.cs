using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.DerivedDatas;
using Conflux.Schema.Exceptions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.DerivedDatas
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.DialogueTree+DerivedSerializationData")]
    internal class DTDerivedData : DerivedData
    {
        [JsonProperty("actorParameters")]
        public List<ActorParameter> ActorParameters { get; set; } = [];

        public override ConfluxDTDerivedData BuildConfluxDerivedData(Graph graph) => new()
        {
            Actors = ActorParameters
                .Select(a => a.BuildConfluxObject(graph))
                .ToDictionary(a => a.Key ?? throw new ConfluxValueException("Actor Key must not be null")),
        };
    }
}
