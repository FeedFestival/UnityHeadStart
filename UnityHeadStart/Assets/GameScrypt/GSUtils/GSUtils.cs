using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GameScrypt.GSUtils
{
    public static class GSUtils
    {
#pragma warning disable 0414
        public static readonly string _version = "3.0.0";
#pragma warning restore 0414

        public static bool isNilOrEmpty<T>(List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static string ConvertNumberToK(int num)
        {
            if (num >= 1000)
                return string.Concat(num / 1000, "k");
            else
                return num.ToString();
        }

        public static Color SetColorAlpha(Color color, int value)
        {
            Color tempColor = color;
            tempColor.a = GetAlphaValue(value);
            return tempColor;
        }

        public static float GetAlphaValue(int value)
        {
            var perc = GSPercent.What(value, 255);
            return perc * 0.01f;
        }

        public static int GetRGBAAlphaValue(float value)
        {
            float perc = value * 100;
            return (int)GSPercent.Find(perc, 255);
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

        public static int CreateLayerMask(bool aExclude, params int[] aLayers)
        {
            int v = 0;
            foreach (var L in aLayers)
                v |= 1 << L;
            if (aExclude)
                v = ~v;
            return v;
        }

        public static string GetUniqueIDS()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString();
        }
    }
}