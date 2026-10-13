using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("HasStatus")]
    internal class HasStatusConfluxCondition : ConfluxCondition
    {
        [YamlMember(Alias = "character")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? Character { get; set; }

        [YamlMember(Alias = "statuses")]
        public ConfluxBlackboardVariableReference<NodeCanvasList<UnityObject, ConfluxUnityObjectReference>, ConfluxList<UnityObject, ConfluxUnityObjectReference>>? Statuses { get; set; }

        public override HasStatusNodeCanvasCondition BuildCondition(NodeCanvasGraphContext context) => new()
        {
            Character = Character?.BuildBBParameter(context),
            Statuses = Statuses?.BuildBBParameter(context),
        };
    }
}
