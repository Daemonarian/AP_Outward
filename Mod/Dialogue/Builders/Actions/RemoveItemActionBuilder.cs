using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using OutwardArchipelago.Dialogue.Builders.BBParameters;

namespace OutwardArchipelago.Dialogue.Builders.Actions
{
    internal class RemoveItemActionBuilder : IActionBuilder
    {
        public IBBParameterBuilder<Character> FromCharacter { get; set; }

        public IReadOnlyList<int> ItemIDs { get; set; }

        public int ItemID { set => ItemIDs = new int[] { value }; }

        public IReadOnlyList<int> Amounts { get; set; }

        public int Amount { set => Amounts = new int[] { value }; }

        public ActionTask BuildAction(IDialoguePatchContext context)
        {
            return new RemoveItem
            {
                fromCharacter = (FromCharacter ?? BBParameterBuilder.Instigator).BuildBBParameter(context),
                Items = ItemIDs.Select(id => new BBParameter<ItemReference> { _value = new ItemReference { m_itemID = id } }).ToList(),
                Amount = (Amounts ?? ItemIDs.Select(_ => 1)).Select(v => new BBParameter<int> { _value = v }).ToList(),
            };
        }
    }
}
