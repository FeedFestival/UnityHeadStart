using GameScrypt.Interfaces;
using UnityEngine.Events;

namespace GameScrypt.Channel.Inventory
{
    public interface IInventoryChannel
    {
        public void Register(UnityAction<IItem, IUnit> onAddItemToInventory);
        public UnityAction<IItem, IUnit> OnAddItemToInventory { get; set; }

        public void AddItemToUnitInventory(IItem item, IUnit unit);
    }
}