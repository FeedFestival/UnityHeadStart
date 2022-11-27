using GameScrypt.Channel.Inventory;
using GameScrypt.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace GameScrypt.Channel.InventoryChannel
{
    public class GSInventoryChannel : IInventoryChannel
    {
        public UnityAction<IItem, IUnit> OnAddItemToInventory { get; set; }

        public void Register(UnityAction<IItem, IUnit> onAddItemToInventory)
        {
            Debug.Log("register");
            OnAddItemToInventory += onAddItemToInventory;
        }

        public void AddItemToUnitInventory(IItem item, IUnit unit)
        {
            OnAddItemToInventory?.Invoke(item, unit);
        }
    }
}
