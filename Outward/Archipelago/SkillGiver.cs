namespace OutwardArchipelago.Archipelago
{
    internal class SkillGiver : IOutwardGiver
    {
        public int SkillID { get; private set; }

        public SkillGiver(int skillID) => SkillID = skillID;

        void IOutwardGiver.GiveToPlayer(Character character) => character.Inventory.ReceiveSkillReward(SkillID);
    }
}
