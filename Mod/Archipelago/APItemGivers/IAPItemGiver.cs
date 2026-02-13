namespace OutwardArchipelago.Archipelago.APItemGivers
{
    /// <summary>
    /// An interface for objects that know how to give Archipelago items to the player.
    /// </summary>
    internal interface IAPItemGiver
    {
        /// <summary>
        /// Attempt to get the Outward item prefab that corresponds to this AP Item, or `null` if it does not exist.
        /// </summary>
        /// <returns>An item prefab or null.</returns>
        public Item GetItemPrefab();

        /// <summary>
        /// Give the Archipelago item to the player. The implementation of this method can assume that
        /// it will only be called from the main thread when the player is ready to recieve items/skills.
        /// </summary>
        public void GiveItem(Character character = null);
    }
}
