using Conflux.NodeCanvas.DerivedDatas;
using Conflux.Schema.Context;
using YamlDotNet.Serialization;

namespace Conflux.Schema.DerivedDatas
{
    internal class ConfluxDTDerivedData : ConfluxDerivedData
    {
        [YamlIgnore]
        public Dictionary<string, ConfluxActor> Actors { get; set; } = [];

        [YamlMember(Alias = "actors")]
        public Dictionary<string, ConfluxActor> ActorProxy
        {
            get => Actors;
            set
            {
                foreach (var (key, actor) in value)
                {
                    actor.Key = key;
                }

                Actors = value;
            }
        }

        public override DTDerivedData BuildDerivedData(NodeCanvasGraphContext context) => new()
        {
            ActorParameters = [.. Actors.Values.Select(a => a.BuildActorParameter(context))],
        };

        public override bool IsEmpty() => Actors.Count == 0;
    }
}
