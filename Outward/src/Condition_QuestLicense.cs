using NodeCanvas.Framework;

namespace OutwardArchipelago
{
    public class Condition_QuestLicense : ConditionTask
    {
        public int MinimumQuestLevel { get; private set; }
        public bool IsInverted { get; private set; }

        public Condition_QuestLicense(int minimumQuestLevel, bool isInverted = false) : base()
        {
            MinimumQuestLevel = minimumQuestLevel;
            IsInverted = isInverted;
        }

        public override string info
        {
            get
            {
                string taskInfo = IsInverted ? $"Requires Quest License < {MinimumQuestLevel}" : $"Requires Quest License >= {MinimumQuestLevel}";
                return $"{base.info}\n{taskInfo}";
            }
        }

        public override bool OnCheck()
        {
            var currentQuestLicenseLevel = QuestLicenseManager.GetQuestLicenseLevel();
            bool check = currentQuestLicenseLevel >= MinimumQuestLevel;
            if (IsInverted)
            {
                check = !check;
            }

            Plugin.Log.LogDebug($"Condition_CheckLicense::OnCheck MinimumQuestLevel={MinimumQuestLevel} IsInverted={IsInverted} currentQuestLicenseLevel={currentQuestLicenseLevel} return {check}");
            return check;
        }
    }
}
