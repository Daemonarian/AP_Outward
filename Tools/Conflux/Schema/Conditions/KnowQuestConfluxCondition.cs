using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
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
