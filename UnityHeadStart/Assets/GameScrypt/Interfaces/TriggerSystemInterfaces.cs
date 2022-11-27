using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScrypt.Interfaces
{
    public interface ITriggerSignal
    {
        public void Init(IUnit unit);
        public IUnit GetUnit();
    }
}