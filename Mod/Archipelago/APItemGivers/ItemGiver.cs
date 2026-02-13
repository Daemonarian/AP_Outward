namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal class ItemGiver : BaseAPItemGiver
    {
        public int ItemID { get; private set; }

        public ItemGiver(int itemID) => ItemID = itemID;

        public override int? OutwardItemID => ItemID;

        public override void GiveItemToCharacter(Character character) => character.Inventory.ReceiveItemReward(ItemID, 1, true);
    }
}
