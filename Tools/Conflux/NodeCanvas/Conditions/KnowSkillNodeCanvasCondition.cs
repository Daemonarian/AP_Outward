using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Conditions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_KnowSkill")]
    [GraphVizObject(Alias = "KnowSkill")]
    internal class KnowSkillNodeCanvasCondition : NodeCanvasCondition
    {
        [JsonProperty("character")]
        public BBParameter<UnityObject>? Character { get; set; }

        [JsonProperty("skill")]
        [GraphVizProperty(IsMainProperty = true)]
        public BBParameter<UnityObject>? Skill { get; set; }

        protected override KnowSkillConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            Character = Character?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            Skill = Skill?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
        };
    }
}
