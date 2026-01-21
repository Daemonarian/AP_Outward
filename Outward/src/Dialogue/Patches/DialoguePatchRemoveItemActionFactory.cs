using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using System.Collections.Generic;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatchRemoveItemActionFactory : IDialoguePatchActionFactory
    {
        public int ItemID { get; private set; }

        public int Amount { get; private set; }

        public DialoguePatchRemoveItemActionFactory(int itemID, int amount = 1)
        {
            ItemID = itemID;
            Amount = amount;
        }

        public ActionTask CreateAction(IDialoguePatchContext context)
        {
            var character = CharacterManager.Instance.GetFirstLocalCharacter();

            return new RemoveItem
            {
                fromCharacter = new(character),
                Items = new List<BBParameter<ItemReference>> { new(new ItemReference { ItemID = ItemID }) },
                Amount = new List<BBParameter<int>> { new(Amount) },
            };
        }
    }
}
