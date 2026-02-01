namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal class ItemGiver : IAPItemGiver
    {
        public int ItemID { get; private set; }

        public ItemGiver(int itemID) => ItemID = itemID;

        void IAPItemGiver.GiveItem(Character character) => character.Inventory.ReceiveItemReward(ItemID, 1, true);
    }
}
