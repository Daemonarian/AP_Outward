using System.Collections.Generic;
using System.Linq;

namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal class QuestEventGiver : BaseAPItemGiver
    {
        private readonly IReadOnlyDictionary<string, int> _eventToStackCount;
        public IReadOnlyDictionary<string, int> EventToStackCount => _eventToStackCount;

        public QuestEventGiver(IReadOnlyDictionary<string, int> eventToStackCount) => _eventToStackCount = eventToStackCount;

        public QuestEventGiver(params string[] outwardQuestEvents) : this(outwardQuestEvents.ToDictionary(uid => uid, uid => 1)) { }

        public override void GiveItemToCharacter(Character character)
        {
            foreach (var pair in EventToStackCount)
            {
                QuestEventManager.Instance.AddEvent(pair.Key, pair.Value);
            }
        }
    }
}
