using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.References;
using Conflux.Outward;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("isQuestComplete")]
    internal class IsQuestCompleteConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "quest")]
        public string Quest { get; set; } = string.Empty;

        public override ConditionTask BuildCondition(INodeCanvasGraphContext context)
        {
            return new Condition_IsQuestCompleted
            {
                QuestRef = new BBParameter<QuestReference>
                {
                    Value = new QuestReference
                    {
                        ItemID = Item.ByKey[Quest].ID,
                    },
                },
            };
        }
    }
}
