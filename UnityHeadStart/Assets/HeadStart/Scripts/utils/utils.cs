using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Scripts.utils
{
    public static class utils
    {
        public static string _version = "1.0.0";
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
    }

    public static class percent
    {
        public static string _version = "1.0.0";
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
