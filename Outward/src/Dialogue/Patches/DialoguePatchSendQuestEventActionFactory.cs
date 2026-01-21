using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatchSendQuestEventActionFactory : IDialoguePatchActionFactory
    {
        public string EventUID { get; private set; }

        public int StackAmount { get; private set; }

        public bool IgnoreNetworkSync { get; private set; }

        public DialoguePatchSendQuestEventActionFactory(string eventUID, int stackAmount = 1, bool ignoreNetworkSync = false)
        {
            EventUID = eventUID;
            StackAmount = stackAmount;
            IgnoreNetworkSync = ignoreNetworkSync;
        }

        public ActionTask CreateAction(IDialoguePatchContext context)
        {
            return new SendQuestEvent
            {
                QuestEventRef = new QuestEventReference
                {
                    EventUID = EventUID,
                },
                StackAmount = StackAmount,
                IgnoreNetworkSync = IgnoreNetworkSync,
            };
        }
    }
}
