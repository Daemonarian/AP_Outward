namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal class SkillGiver : IAPItemGiver
    {
        public int SkillID { get; private set; }

        public SkillGiver(int skillID) => SkillID = skillID;

        void IAPItemGiver.GiveItem(Character character) => character.Inventory.ReceiveSkillReward(SkillID);
    }
}
