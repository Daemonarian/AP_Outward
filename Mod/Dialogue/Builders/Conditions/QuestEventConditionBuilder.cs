using NodeCanvas.Framework;
using NodeCanvas.Tasks.Conditions;

namespace OutwardArchipelago.Dialogue.Builders.Conditions
{
    internal class QuestEventConditionBuilder : IConditionBuilder
    {
        public string EventUID { get; set; } = null;

        public int MinStack { get; set; } = 1;

        public bool IsInverted { get; set; } = false;

        public ConditionTask BuildCondition(IDialoguePatchContext context)
        {
            return new Condition_QuestEventOccured
            {
                QuestEventRef = new QuestEventReference
                {
                    m_eventUID = EventUID,
                },
                MinStack = MinStack,
                invert = IsInverted,
            };
        }
    }
}
