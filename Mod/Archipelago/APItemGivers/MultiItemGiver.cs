namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal class MultiItemGiver : BaseAPItemGiver
    {
        public int[] ItemIDs { get; private set; }

        public MultiItemGiver(int[] itemIDs) => ItemIDs = itemIDs;

        public override int? OutwardItemID => null;

        public override void GiveItemToCharacter(Character character)
        {
            foreach (var itemID in ItemIDs)
            {
                character.Inventory.ReceiveItemReward(itemID, 1, true);
            }
        }
    }
}
