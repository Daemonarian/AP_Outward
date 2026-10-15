using System.ComponentModel;
using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("heal")]
    internal class HealCharacterConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "target")]
        public ConfluxBlackboardVariableReference<ConfluxUnityObjectReference>? Target { get; set; }

        [YamlMember(Alias = "targetUID")]
        public string? TargetUID { get; set; }

        [YamlMember(Alias = "fullHealth")]
        [DefaultValue(false)]
        public ConfluxBlackboardVariableReference<bool> FullHealth { get; set; } = false;

        [YamlMember(Alias = "amount")]
        [DefaultValue(0f)]
        public ConfluxBlackboardVariableReference<float> HealAmount { get; set; } = 0f;

        public override HealCharacterNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            FullHealth = FullHealth.BuildBBParameter(context),
            HealAmount = HealAmount.BuildBBParameter(context),
            Target = Target?.BuildBBParameter<UnityObject>(context),
            TargetUID = TargetUID,
        };
    }
}
