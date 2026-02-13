namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal class MoneyGiver : BaseAPItemGiver
    {
        public int Amount { get; private set; }

        public MoneyGiver(int amount) => Amount = amount;

        public override int? OutwardItemID => Currency.SilverItemID;

        public override void GiveItemToCharacter(Character character) => character.Inventory.ReceiveMoneyReward(Amount);
    }
}
