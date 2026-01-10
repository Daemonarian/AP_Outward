using NodeCanvas.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutwardArchipelago
{
    public class QuestLicenseConditionTask : ConditionTask
    {
        public int MinimumQuestLevel { get; private set; }
        public bool IsInverted { get; private set; }

        public QuestLicenseConditionTask(int minimumQuestLevel, bool isInverted = false) : base()
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
