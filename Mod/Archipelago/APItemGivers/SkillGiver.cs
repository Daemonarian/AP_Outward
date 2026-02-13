namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal class SkillGiver : BaseAPItemGiver
    {
        public int SkillID { get; private set; }

        public SkillGiver(int skillID) => SkillID = skillID;

        public override int? OutwardItemID => SkillID;

        public override void GiveItemToCharacter(Character character) => character.Inventory.ReceiveSkillReward(SkillID);
    }
}
