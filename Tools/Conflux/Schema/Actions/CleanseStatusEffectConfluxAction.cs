using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("cleanse")]
    internal class CleanseStatusEffectConfluxAction : ConfluxAction
    {
        [YamlMember(Alias = "target")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? Target { get; set; }

        [YamlMember(Alias = "statusEffect")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? StatusEffect { get; set; }

        public override CleanseStatusEffectNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            Target = Target?.BuildBBParameter(context),
            StatusEffect = StatusEffect?.BuildBBParameter(context),
        };
    }
}
