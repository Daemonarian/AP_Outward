using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;

namespace OutwardArchipelago.Dialogue.Builders.Actions
{
    internal class SendQuestEventActionBuilder : IActionBuilder
    {
        public string EventUID { get; set; }

        public int StackAmount { get; set; } = 1;

        public bool IgnoreNetworkSync { get; set; } = false;

        public ActionTask BuildAction(IDialoguePatchContext context)
        {
            return new SendQuestEvent
            {
                QuestEventRef = new QuestEventReference { m_eventUID = EventUID },
                StackAmount = StackAmount,
                IgnoreNetworkSync = IgnoreNetworkSync,
            };
        }
    }
}
