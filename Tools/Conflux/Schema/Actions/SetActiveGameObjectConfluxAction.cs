using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("setObjectActive")]
    internal class SetActiveGameObjectConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "target")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? Target { get; set; }

        [YamlMember(Alias = "isActive")]
        public ConfluxBlackboardVariableReference<bool>? IsActive { get; set; }

        public override SetActiveGameObjectNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            Target = Target?.BuildBBParameter(context),
            IsActive = IsActive?.BuildBBParameter(context),
        };
    }
}
