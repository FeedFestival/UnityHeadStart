using GameScrypt.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameScrypt.GSUnit
{
    public class GSUnitInventory : IUnitInventory
    {
        public List<IItem> Items;
        public UnityAction<IItem> OnSetItem;

        public GSUnitInventory()
        {
            this.init();
        }

        // because this is a must known public news - that this user has a new item in inventory
        // (especially player) we need to set it through an UnityAction
        public void SetItem(IItem item)
        {
            Debug.Log($" item: { item.GetName() }");
            OnSetItem?.Invoke(item);
        }

        private void setItem(IItem item)
        {
            Items.Add(item);
        }

        private void init()
        {
            Items = new List<IItem>();

            // maybe get this unit's items already equiped

            OnSetItem += this.setItem;
        }
    }
}