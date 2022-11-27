using System.Collections.Generic;
using UnityEngine;

namespace GameScrypt.GSUnit
{
    public class GSUnitsController : MonoBehaviour
    {
        public GSUnit Player;
        public List<GSUnit> Units;

        public void Init()
        {
            Player.Init();
            __.Core.SetPlayer(Player);

            Units.ForEach(u => u.Init());
        }
    }
}