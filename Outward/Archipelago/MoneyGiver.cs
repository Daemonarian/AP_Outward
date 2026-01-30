namespace OutwardArchipelago.Archipelago
{
    internal class MoneyGiver : IOutwardGiver
    {
        public int Amount { get; private set; }

        public MoneyGiver(int amount) => Amount = amount;

        void IOutwardGiver.GiveToPlayer(Character character) => character.Inventory.ReceiveMoneyReward(Amount);
    }
}
