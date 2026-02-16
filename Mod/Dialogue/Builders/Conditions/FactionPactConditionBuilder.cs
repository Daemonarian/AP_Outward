using System.Linq;
using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Dialogue.Builders.Conditions
{
    /// <summary>
    /// Builds a condition that checks if the player is allowed to join one of the specified factions.
    /// </summary>
    internal class FactionPactConditionBuilder : IConditionBuilder
    {
        public APWorld.Faction Faction { get; set; } = APWorld.Faction.None;

        public ConditionTask BuildCondition(IDialoguePatchContext context)
        {
            return new ConditionListBuilder
            {
                CheckMode = ConditionList.ConditionsCheckMode.AnyTrueSuffice,
                Conditions = (from skillId in APWorld.GetOutwardFactionPactsForFaction(Faction)
                              where OutwardArchipelagoMod.Instance.IsArchipelagoEnabled && ArchipelagoConnector.Instance.SlotData.IsFactionPactEnabled
                              select new KnowSkillConditionBuilder { SkillID = skillId }).ToList(),
            }.BuildCondition(context);
        }
    }
}
