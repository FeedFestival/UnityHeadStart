using GameScrypt.Interfaces;
using UnityEngine;

namespace GameScrypt.GSUnit
{
    public class GSUnit : MonoBehaviour, IUnit
    {
        public int Id;
        public GSSignalController SenseController;

        internal GSUnitInventory UnitInventory;

        void Start()
        {
            SenseController.Init(this);
        }

        public void Init()
        {
            Id = __.Core.GenerateId();

            UnitInventory = new GSUnitInventory();
        }

        public int GetUnitId()
        {
            return Id;
        }

        public IUnitInventory GetInventory()
        {
            return UnitInventory;
        }
    }
}
