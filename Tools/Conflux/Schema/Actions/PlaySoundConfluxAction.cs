using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.References;
using Conflux.Outward;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("playSound")]
    internal class PlaySoundConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "sound")]
        public OutwardAudio.Sounds Sound { get; set; }

        [YamlMember(Alias = "transform")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? Transform { get; set; }

        [YamlMember(Alias = "position")]
        public ConfluxVector3? Position { get; set; }

        public override PlaySoundNodeCanvasAction BuildAction(NodeCanvasGraphContext context)
        {
            if (Transform is not null && Position is not null)
            {
                throw new ConfluxValueException($"At most one of transform and position may be specified.");
            }

            return new()
            {
                Sound = Sound,
                TransPos = Transform?.BuildBBParameter(context),
                Pos = Position?.BuildNodeCanvasObject(context),
                PlayAt = Transform is not null ? PlaySoundNodeCanvasAction.PositionType.Transform : (Position is not null ? PlaySoundNodeCanvasAction.PositionType.Pos : PlaySoundNodeCanvasAction.PositionType.Camera),
            };
        }
    }
}
