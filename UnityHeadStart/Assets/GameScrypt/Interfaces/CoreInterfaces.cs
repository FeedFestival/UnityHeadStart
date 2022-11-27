using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScrypt.Interfaces
{
    public interface ICore
    {
        public int GenerateId();
        public void SetPlayer(IUnit unit);

        public void InjectSystem(string system);
    }
}