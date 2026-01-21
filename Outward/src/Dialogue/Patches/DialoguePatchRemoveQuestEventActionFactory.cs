using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatchRemoveQuestEventActionFactory : IDialoguePatchActionFactory
    {
        public string EventUID { get; private set; }

        public bool RemoveAllStacks { get; private set; }

        public int StackDecreaseAmount { get; private set; }

        public DialoguePatchRemoveQuestEventActionFactory(string eventUID, bool removeAllStacks = true, int stackDecreaseAmount = 1)
        {
            EventUID = eventUID;
            RemoveAllStacks = removeAllStacks;
            StackDecreaseAmount = stackDecreaseAmount;
        }

        public ActionTask CreateAction(IDialoguePatchContext context)
        {
            return new RemoveQuestEvent
            {
                QuestEventRef = new QuestEventReference
                {
                    EventUID = EventUID,
                },
                RemoveAllStack = RemoveAllStacks,
                StackDecreaseAmount = StackDecreaseAmount,
            };
        }
    }
}
