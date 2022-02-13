using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.utils
{
    public static class __list
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        public static bool isNilOrEmpty<T>(List<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
