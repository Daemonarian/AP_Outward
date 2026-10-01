using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("knowQuest")]
    internal class KnowQuestConfluxCondition : ConfluxCondition
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "quest")]
        public ConfluxQuestReference? Quest { get; init; }

        [YamlMember(Alias = "questObject")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? QuestObject { get; init; }

        public override ConditionTask BuildCondition(NodeCanvasGraphContext context)
        {
            if (Quest is null)
            {
                if (QuestObject is null)
                {
                    throw new ConfluxValueException("One of knowQuest.quest or knowQuest.questObject must be specified.");
                }
                else
                {
                    return new Condition_KnowQuest
                    {
                        Quest = QuestObject.BuildBBParameter(context),
                    };
                }
            }
            else
            {
                if (QuestObject is null)
                {
                    return new Condition_CustomKnowQuest
                    {
                        Quest = Quest.BuildNodeCanvasObject(context),
                    };
                }
                else
                {
                    throw new ConfluxValueException("Both of knowQuest.quest and knowQuest.questObject must not be specified.");
                }
            }
        }
    }
}
