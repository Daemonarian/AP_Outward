using System.Collections.Generic;

namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal class ProgressiveSkillGiver : BaseAPItemGiver
    {
        public IReadOnlyList<int> SkillIDs { get; private set; }

        public ProgressiveSkillGiver(IReadOnlyList<int> skillIDs) => SkillIDs = skillIDs;

        public override int? OutwardItemID
        {
            get
            {
                if (SkillIDs.Count == 0)
                {
                    OutwardArchipelagoMod.Log.LogError("attempted to get the OutwardItemID for a ProgressiveSkillGiver with no skill IDs");
                    return null;
                }

                var index = CurrentLevel();
                if (index < 0)
                {
                    index = 0;
                }
                else if (index >= SkillIDs.Count)
                {
                    index = SkillIDs.Count - 1;
                }

                return SkillIDs[index];
            }
        }

        public override void GiveItemToCharacter(Character character)
        {
            var index = CurrentLevel();
            if (index < 0)
            {
                OutwardArchipelagoMod.Log.LogError($"cannot give progressive skill because current level is less than zero: {index}");
                return;
            }
            else if (index >= SkillIDs.Count)
            {
                OutwardArchipelagoMod.Log.LogError($"cannot give progressive skill because current level is already at the maximum: {index}");
                return;
            }

            var newSkillId = SkillIDs[index];

            foreach (var skillId in SkillIDs)
            {
                if (skillId != newSkillId && character.Inventory.SkillKnowledge.IsItemLearned(skillId))
                {
                    character.Inventory.SkillKnowledge.RemoveItem(skillId);
                }
            }

            if (!character.Inventory.SkillKnowledge.IsItemLearned(newSkillId))
            {
                character.Inventory.ReceiveSkillReward(newSkillId);
            }
        }

        public int CurrentLevel(Character character = null)
        {
            if (!character)
            {
                character = CharacterManager.Instance.GetFirstLocalCharacter();
            }

            var level = 0;
            for (var i = 0; i < SkillIDs.Count; i++)
            {
                if (character.Inventory.SkillKnowledge.IsItemLearned(SkillIDs[i]))
                {
                    var newLevel = i + 1;
                    if (level < newLevel)
                    {
                        level = newLevel;
                    }
                }
            }

            return level;
        }
    }
}
