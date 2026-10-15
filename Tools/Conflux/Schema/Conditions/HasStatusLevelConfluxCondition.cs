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
    [ConfluxDerived("hasStatus")]
    internal class HasStatusLevelConfluxCondition : ConfluxCondition
    {
        [YamlMember(Alias = "statuses")]
        public ConfluxBlackboardVariableReference<ConfluxList<ConfluxUnityObjectReference>>? Statuses { get; set; }

        [YamlMember(Alias = "minLevel")]
        public ConfluxBlackboardVariableReference<int>? MinLevel { get; set; }

        public override HasStatusLevelNodeCanvasCondition BuildCondition(NodeCanvasGraphContext context) => new()
        {
            Statuses = Statuses?.BuildBBParameter<NodeCanvasList<UnityObject>>(context),
            MinLevel = MinLevel?.BuildBBParameter(context),
        };
    }
}
