using UnityEngine;

namespace OutwardArchipelago.Archipelago.APItemGivers
{
    internal class NotificationItemGiver : BaseAPItemGiver
    {
        private readonly APWorld.Item _item;
        public APWorld.Item Item => _item;

        private readonly Sprite _icon;
        public Sprite Icon => _icon;

        public NotificationItemGiver(APWorld.Item item, Sprite icon = null)
        {
            _item = item;
            _icon = icon;
        }

        public override void GiveItemToCharacter(Character character)
        {
            var fmt = OutwardArchipelagoMod.Instance.GetLocalizedModString("notification.item_recieved");
            var msg = string.Format(fmt, Item.Name);
            character.CharacterUI.ShowInfoNotification(msg, Icon);
        }
    }
}
