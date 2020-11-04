using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Scripts.utils
{
    public static class utils
    {
        public static readonly string _version = "1.0.2";
        public static string ConvertNumberToK(int num)
        {
            if (num >= 1000)
                return string.Concat(num / 1000, "k");
            else
                return num.ToString();
        }

        public static int GetPennyToss()
        {
            var randomNumber = Random.Range(0, 100);
            return (randomNumber > 50) ? 1 : 0;
        }

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
                output = JsonHelper.ToJson<object>(obj);
            }
            else
            {
                output = JsonUtility.ToJson(obj, true);
            }
            Debug.Log(output);
        }

        public static void DumpToJsonConsole(IJsonConsole[] jsonConsoles)
        {
            foreach (var json in jsonConsoles)
            {
                Debug.Log(json.ToJsonString());
            }
        }

        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public static string DebugList(List<int> array, string name)
        {
            string debug = string.Empty;
            foreach (int bId in array)
            {
                debug += bId + ",";
            }
            return name + " (" + debug + ")[" + array.Count + "]";
        }

        public static string DebugQueue<T>(Queue<T> array, string name)
        {
            string debug = string.Empty;
            foreach (T t in array)
            {
                debug += JsonHelper.ToJson<T>(t);
            }
            return name + " (" + debug + ")[" + array.Count + "]";
        }

        public static void AddIfNone(int value, ref List<int> array, string debugAdd = null)
        {
            if (array.Contains(value))
            {
                return;
            }
            array.Add(value);
            if (string.IsNullOrEmpty(debugAdd))
            {
                Debug.Log(debugAdd);
            }
        }
    }

    public static class percent
    {
        public static float Find(float _percent, float _of)
        {
            return (_of / 100f) * _percent;
        }
        public static float What(float _is, float _of)
        {
            return (_is * 100f) / _of;
        }
    }
}
