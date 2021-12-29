using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.utils
{
    public static class __list
    {
        public static bool isNilOrEmpty<T>(List<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
