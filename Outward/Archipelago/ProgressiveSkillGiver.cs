using System.Collections.Generic;

namespace OutwardArchipelago.Archipelago
{
    internal class ProgressiveSkillGiver : IOutwardGiver
    {
        public IReadOnlyList<int> SkillIDs { get; private set; }

        public ProgressiveSkillGiver(IReadOnlyList<int> skillIDs) => SkillIDs = skillIDs;

        void IOutwardGiver.GiveToPlayer(Character character)
        {
            var level = 0;
            for (var i = 0; i < SkillIDs.Count; i++)
            {
                if (character.Inventory.SkillKnowledge.IsItemLearned(SkillIDs[i]))
                {
                    level = i;
                }
            }

            level += 1;
            if (level >= SkillIDs.Count)
            {
                OutwardArchipelagoMod.Log.LogError($"received another instance of a progressive skill from Archipelago, but character is already at the max level: {SkillIDs[SkillIDs.Count - 1]}");
                return;
            }

            for (var i = 0; i < SkillIDs.Count; i++)
            {
                if (i != level && character.Inventory.SkillKnowledge.IsItemLearned(SkillIDs[i]))
                {
                    character.Inventory.SkillKnowledge.RemoveItem(SkillIDs[i]);
                }
            }

            if (!character.Inventory.SkillKnowledge.IsItemLearned(SkillIDs[level]))
            {
                character.Inventory.ReceiveSkillReward(SkillIDs[level]);
            }
        }
    }
}
