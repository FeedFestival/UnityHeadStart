using GameScrypt.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScrypt.InventorySystem
{
    public class GSWorldItem : MonoBehaviour, IItem
    {
        public virtual string GetName()
        {
            return "Forbidden Item";
        }

        protected IInteractable _interactable;

        private void Start()
        {
            _interactable = gameObject.GetComponent<IInteractable>();
            _interactable.InteractionCallback += onUnitPickedUpObject;
        }

        protected virtual void onUnitPickedUpObject(IUnit unit)
        {
            throw new InvalidOperationException("This class togheter with this method, should be extended and not used. See onUnitPickedUpObject() function for example");
            /*
            var inventory = unit.GetInventory();
            inventory.SetItem(null);
            Destroy(gameObject.GetComponent<GSWorldItem>());
            */
        }
    }
}
