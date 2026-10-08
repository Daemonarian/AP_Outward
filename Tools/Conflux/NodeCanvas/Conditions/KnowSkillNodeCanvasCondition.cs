using System.Text;
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
    internal class KnowSkillNodeCanvasCondition : ConditionTask
    {
        [JsonProperty("character")]
        public BBParameter<UnityObject>? Character { get; set; }

        [JsonProperty("skill")]
        public BBParameter<UnityObject>? Skill { get; set; }

        public override string GetGraphVizShortName() => "KnowSkill";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();
            content.AppendLine();

            if (Skill is not null)
            {
                content.AppendLine($"Skill: {GraphVizConverter.IndentLines(Skill.ToGraphVizLabel())}");
            }

            if (Character is not null)
            {
                content.AppendLine($"Character: {GraphVizConverter.IndentLines(Character.ToGraphVizLabel())}");
            }

            return content.ToString().TrimEnd();
        }

        protected override KnowSkillConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            Character = Character?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            Skill = Skill?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
        };
    }
}
