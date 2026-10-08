using Conflux.NodeCanvas.Actions;
using Conflux.Outward;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("music")]
    internal class MusicConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "music")]
        public ConfluxBlackboardVariableReference<OutwardAudio.Sounds>? Music { get; set; }

        [YamlMember(Alias = "stop")]
        public ConfluxBlackboardVariableReference<bool>? Stop { get; set; }

        public override MusicNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            Music = Music?.BuildBBParameter(context),
            Stop = Stop?.BuildBBParameter(context),
        };
    }
}
