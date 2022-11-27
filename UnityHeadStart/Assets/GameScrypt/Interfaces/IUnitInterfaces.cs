using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScrypt.Interfaces
{
    public interface IUnit
    {
        public int GetUnitId();
        public IUnitInventory GetInventory();
    }

    public interface IUnitInventory
    {
        public void SetItem(IItem item);
    }
}