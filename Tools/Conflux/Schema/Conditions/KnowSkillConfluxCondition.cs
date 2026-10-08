using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("knowSkill")]
    internal class KnowSkillConfluxCondition : ConfluxCondition
    {
        [YamlMember(Alias = "character")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? Character { get; set; }

        [ConfluxMainProperty]
        [YamlMember(Alias = "skill")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? Skill { get; set; }

        public override KnowSkillNodeCanvasCondition BuildCondition(NodeCanvasGraphContext context) => new()
        {
            Character = Character?.BuildBBParameter(context),
            Skill = Skill?.BuildBBParameter(context),
        };
    }
}
