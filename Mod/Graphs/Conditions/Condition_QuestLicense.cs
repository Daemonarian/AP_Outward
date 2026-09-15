using NodeCanvas.Framework;
using UnityEngine;

namespace OutwardArchipelago.Graphs.Conditions
{
    public class Condition_QuestLicense : ConditionTask
    {
        [SerializeField]
        private readonly int _minimumQuestLevel = 0;

        public int MinimumQuestLevel => _minimumQuestLevel;

        public override string info
        {
            get
            {
                var taskInfo = invert ? $"Requires Quest License < {MinimumQuestLevel}" : $"Requires Quest License >= {MinimumQuestLevel}";
                return $"{base.info}\n{taskInfo}";
            }
        }

        public override bool OnCheck()
        {
            var currentQuestLicenseLevel = QuestLicenseManager.GetQuestLicenseLevel();
            var check = currentQuestLicenseLevel >= MinimumQuestLevel;

            OutwardArchipelagoMod.Log.LogDebug($"Condition_CheckLicense::OnCheck MinimumQuestLevel={MinimumQuestLevel} currentQuestLicenseLevel={currentQuestLicenseLevel} return {check}");

            return check;
        }
    }
}
