namespace OutwardArchipelago.Archipelago
{
    internal class ItemGiver : IOutwardGiver
    {
        public int ItemID { get; private set; }

        public ItemGiver(int itemID)
        {
            ItemID = itemID;
        }

        void IOutwardGiver.GiveToPlayer(Character character)
        {
            character.Inventory.ReceiveItemReward(ItemID, 1, true);
        }
    }
}
