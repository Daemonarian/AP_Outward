using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("warp")]
    internal class WarpConfluxAction : ConfluxAction
    {
        [YamlMember(Alias = "allPlayers")]
        public ConfluxBlackboardVariableReference<bool>? AllPlayers { get; set; }

        [YamlMember(Alias = "turnCamera")]
        public ConfluxBlackboardVariableReference<bool>? TurnCamera { get; set; }

        [YamlMember(Alias = "target")]
        public ConfluxBlackboardVariableReference<ConfluxUnityObjectReference>? Target { get; set; }

        [YamlMember(Alias = "targetTransform")]
        public ConfluxBlackboardVariableReference<ConfluxUnityObjectReference>? TargetTransform { get; set; }

        [YamlMember(Alias = "targetUID")]
        public string? TargetUID { get; set; }

        public override WarpCharacterNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            AllPlayers = AllPlayers?.BuildBBParameter(context),
            TurnCamera = TurnCamera?.BuildBBParameter(context),
            Target = Target?.BuildBBParameter<UnityObject>(context),
            TargetTransform = TargetTransform?.BuildBBParameter<UnityObject>(context),
            TargetUID = TargetUID,
        };
    }
}
