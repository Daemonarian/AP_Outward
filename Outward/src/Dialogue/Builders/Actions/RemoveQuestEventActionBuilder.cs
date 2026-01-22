using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;

namespace OutwardArchipelago.Dialogue.Builders.Actions
{
    internal class RemoveQuestEventActionBuilder : IActionBuilder
    {
        public string EventUID { get; set; }

        public bool RemoveAllStacks { get; set; } = true;

        public int StackDecreaseAmount { get; set; } = 1;

        public ActionTask BuildAction(IDialoguePatchContext context)
        {
            return new RemoveQuestEvent
            {
                QuestEventRef = new QuestEventReference { m_eventUID = EventUID },
                RemoveAllStack = RemoveAllStacks,
                StackDecreaseAmount = StackDecreaseAmount,
            };
        }
    }
}
