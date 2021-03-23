
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts.utils;
using UnityEngine;

namespace Assets.Scripts.utils
{
    public static class __debug
    {
        public static void VarDump<T>(T obj)
        {
            foreach (var propertyInfo in obj.GetType()
                                .GetProperties(
                                        BindingFlags.Public
                                        | BindingFlags.Instance))
            {
                var x = propertyInfo;
                Debug.Log(propertyInfo);
            }
        }

        public static void DumpToConsole(object obj, bool isArray = false)
        {
            string output = string.Empty;
            if (isArray)
            {
                output = __json.ToJson<object>(obj);
            }
            else
            {
                output = JsonUtility.ToJson(obj, true);
            }
            Debug.Log(output);
        }

        public static string DebugList(List<int> array, string name)
        {
            string debug = string.Empty;
            foreach (int bId in array)
            {
                debug += bId + ",";
            }
            return name + " [" + debug + "](" + array.Count + ")";
        }

        public delegate string DebugFunc<T>(T obj);

        public static string DebugList<T>(List<T> array, string name = null, DebugFunc<T> debugFunc = null)
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

        public static string DebugQueue<T>(Queue<T> array, string name)
        {
            string debug = string.Empty;
            foreach (T t in array)
            {
                debug += __json.ToJson<T>(t);
            }
            return name + " (" + debug + ")[" + array.Count + "]";
        }

        public static string DebugDict<T>(Dictionary<T, T> array, string name = null)
        {
            string debug = string.Empty;
            foreach (var bId in array)
            {
                debug += "[" + bId.Key.ToString() + "] " + bId.Value.ToString();
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
