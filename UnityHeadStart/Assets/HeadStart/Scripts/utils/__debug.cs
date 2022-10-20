
using System.Collections.Generic;
// using Assets.HeadStart.Features.Database.JSON;
using UnityEngine;

namespace Assets.Scripts.utils
{
    public static class __debug
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
        
        public static string ToJsonString(object obj)
        {
            string output = JsonUtility.ToJson(obj, true);
            return output;
        }
        
        public delegate string DebugFunc<T>(T obj);
        public delegate string DebugFunc<TSource, TProperty>(TSource key, TProperty prop);

        public static void DebugList<T>(List<T> array, string name)
        {
            var debugString = GetDebugList(array, name);
            Debug.Log(debugString);
        }

        public static void DebugList<T>(List<T> array, string name = null, DebugFunc<T> debugFunc = null)
        {
            var debugString = GetDebugList(array, name, debugFunc);
            Debug.Log(debugString);
        }

        public static string GetDebugList<T>(List<T> array, string name)
        {
            string debug = string.Empty;
            foreach (T bId in array)
            {
                debug += bId + ",";
            }
            return name + " [" + debug + "](" + array.Count + ")";
        }

        public static string GetDebugList<T>(List<T> array, string name = null, DebugFunc<T> debugFunc = null)
        {
            string debug = string.Empty;
            foreach (T bId in array)
            {
                if (debugFunc == null)
                {
                    debug += bId.ToString() + " ; ";
                }
                else
                {
                    debug += debugFunc(bId);
                }
            }
            return string.IsNullOrEmpty(name) ? debug
                : name + " [" + debug + "](" + array.Count + ")";
        }

        public static string DebugDict<TSource, TProperty>(Dictionary<TSource, TProperty> array, string name = null)
        {
            string debug = string.Empty;
            foreach (KeyValuePair<TSource, TProperty> kvp in array)
            {
                debug += "[" + kvp.Key.ToString() + "] " + kvp.Value.ToString();
            }
            return string.IsNullOrEmpty(name) ? debug
                : name + " [" + debug + "](" + array.Count + ")";
        }

        public static string DebugDict<TSource, TProperty>(Dictionary<TSource, TProperty> array, string name = null, DebugFunc<TSource, TProperty> debugFunc = null)
        {
            string debug = string.Empty;
            foreach (KeyValuePair<TSource, TProperty> kvp in array)
            {
                if (debugFunc == null)
                {
                    debug += "[" + kvp.Key.ToString() + "] " + kvp.Value.ToString();
                }
                else
                {
                    debug += debugFunc(kvp.Key, kvp.Value);
                }
            }
            return string.IsNullOrEmpty(name) ? debug
                : name + " [" + debug + "](" + array.Count + ")";
        }

        public static void AddIfNone(int value, ref List<int> array, string debugAdd = null)
        {
            if (array.Contains(value))
            {
                return;
            }
            array.Add(value);
            if (string.IsNullOrEmpty(debugAdd) == false)
            {
                Debug.Log(debugAdd);
            }
        }
    }
}
