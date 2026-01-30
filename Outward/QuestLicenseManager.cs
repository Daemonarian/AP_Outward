using System.Collections.Generic;

namespace OutwardArchipelago
{
    public static class QuestLicenseManager
    {
        public static readonly IReadOnlyList<int> QuestLicenseSkillIDs = new List<int> { 8861501, 8861502, 8861503, 8861504, 8861505, 8861506, 8861507, 8861508, 8861509, 8861510 };

        public static int GetQuestLicenseLevel()
        {
            if (!PhotonNetwork.isMasterClient)
            {
                return 0;
            }

            var character = CharacterManager.Instance.GetFirstLocalCharacter();
            if (character == null)
            {
                return 0;
            }

            var level = 0;
            for (var i = 0; i < QuestLicenseSkillIDs.Count; i++)
            {
                if (character.Inventory.SkillKnowledge.IsItemLearned(QuestLicenseSkillIDs[i]))
                {
                    level = i + 1;
                }
            }

            return level;
        }

        public static void SetQuestLicenseLevel(int level)
        {
            OutwardArchipelagoMod.Log.LogInfo($"Setting Quest License level to {level}");

            if (!PhotonNetwork.isMasterClient)
            {
                return;
            }

            var character = CharacterManager.Instance.GetFirstLocalCharacter();
            if (character == null)
            {
                return;
            }

            for (var i = 0; i < QuestLicenseSkillIDs.Count; i++)
            {
                var skillId = QuestLicenseSkillIDs[i];
                var known = character.Inventory.SkillKnowledge.IsItemLearned(skillId);
                if (level == i + 1)
                {
                    if (!known)
                    {
                        OutwardArchipelagoMod.Log.LogDebug($"Giving Quest License {i + 1} skill: {skillId}");
                        character.Inventory.ReceiveSkillReward(QuestLicenseSkillIDs[i]);
                    }
                }
                else
                {
                    if (known)
                    {
                        OutwardArchipelagoMod.Log.LogDebug($"Removing Quest License {i + 1} skill: {skillId}");
                        character.Inventory.SkillKnowledge.RemoveItem(skillId);
                    }
                }
            }
        }
    }
}
