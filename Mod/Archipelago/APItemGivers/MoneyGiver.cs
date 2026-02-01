namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal class MoneyGiver : IAPItemGiver
    {
        public int Amount { get; private set; }

        public MoneyGiver(int amount) => Amount = amount;

        void IAPItemGiver.GiveItem(Character character) => character.Inventory.ReceiveMoneyReward(Amount);
    }
}
