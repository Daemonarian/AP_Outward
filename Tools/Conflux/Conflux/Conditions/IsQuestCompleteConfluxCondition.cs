using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework.Conditions;
using NodeCanvas.Tool.Schema.Records;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Conditions
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
