using GameScrypt.Interfaces;
using UnityEngine;

namespace GameScrypt.TriggerSystem
{
    public class GSTriggerSignal : MonoBehaviour, ITriggerSignal
    {
        private IUnit _unit;
        public void Init(IUnit unit)
        {
            _unit = unit;
        }

        public IUnit GetUnit()
        {
            return _unit;
        }
    }
}
