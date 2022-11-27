using GameScrypt;
using GameScrypt.Channel.InventoryChannel;
using GameScrypt.Interfaces;
using UnityEngine;

namespace GameScrypt.InventorySystem
{
    public class GSInventorySystem : MonoBehaviour, IInventorySystem
    {
        public void Init()
        {
            __.SetInventoryChannel(new GSInventoryChannel());

            __.InventoryChannel?.Register(this.onAddItemToInventory);
        }

        private void onAddItemToInventory(IItem item, IUnit unit)
        {
            var inventory = unit.GetInventory();
            inventory.SetItem(item);
        }
    }
}