using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework.Conditions;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Conditions
{
    [ConfluxDerived("knowQuest")]
    internal class KnowQuestConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "quest")]
        public string QuestKey { get; init; } = string.Empty;

        public override ConditionTask BuildCondition(INodeCanvasGraphContext context)
        {
            return new Condition_CustomKnowQuest
            {
                Quest = QuestReference.FromKey(QuestKey),
            };
        }
    }
}
