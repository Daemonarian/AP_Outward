namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal abstract class BaseAPItemGiver : IAPItemGiver
    {
        public virtual int? OutwardItemID => null;

        public virtual Item GetItemPrefab()
        {
            var itemId = OutwardItemID;
            if (itemId.HasValue)
            {
                return ResourcesPrefabManager.Instance.GetItemPrefab(itemId.Value);
            }

            return null;
        }

        public abstract void GiveItemToCharacter(Character character);

        public void GiveItem(Character character = null)
        {
            if (!character)
            {
                character = CharacterManager.Instance.GetFirstLocalCharacter();
            }

            GiveItemToCharacter(character);
        }
    }
}
