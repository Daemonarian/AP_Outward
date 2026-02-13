namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal class QuestEventGiver : BaseAPItemGiver
    {
        private readonly string _outwardQuestEvent;
        public string OutwardQuestEvent => _outwardQuestEvent;

        public QuestEventGiver(string outwardQuestEvent)
        {
            _outwardQuestEvent = outwardQuestEvent;
        }

        public override void GiveItemToCharacter(Character character) => QuestEventManager.Instance.AddEvent(OutwardQuestEvent);
    }
}
