using NodeCanvas.Framework;
using UnityEngine;

namespace OutwardArchipelago.Graphs.Conditions
{
    internal class Condition_KnowQuest : ConditionTask
    {
        [SerializeField]
        private readonly QuestReference _quest = new();

        public Quest Quest => _quest.RefQuest;

        public override string info => $"Knows quest: {Quest}";

        public override bool OnCheck()
        {
            if (Quest is null)
            {
                return false;
            }

            var worldHostCharacter = CharacterManager.Instance.GetWorldHostCharacter();
            if (worldHostCharacter is null)
            {
                return false;
            }

            return worldHostCharacter.Inventory.QuestKnowledge.IsItemLearned(Quest);
        }
    }
}
