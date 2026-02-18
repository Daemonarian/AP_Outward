using System;
using OutwardArchipelago.Archipelago;
using UnityEngine;
using static OutwardArchipelago.Archipelago.APWorld;

namespace OutwardArchipelago.Scenes.Patches.GameObjects
{
    /// <summary>
    /// Builds AP Items to be placed in the world.
    /// </summary>
    internal class APItemGameObjectBuilder : IGameObjectBuilder
    {
        /// <summary>
        /// The location check associated with the newly built AP Item.
        /// </summary>
        public APWorld.Location Location { get; set; }

        public void Validate()
        {
            if (Location is null)
            {
                throw new ArgumentNullException(nameof(Location));
            }
        }

        public GameObject Build()
        {
            var item = ItemManager.Instance.GenerateItemNetwork(OutwardItem.APItem);
            item.SetSideData("AP_Location", Location.Id);

            return item.gameObject;
        }
    }
}
