using System.Collections.Generic;
using UnityEngine;

namespace GameScrypt.GSUtils
{
    public static class GSPercent
    {
#pragma warning disable 0414
        public static readonly string _version = "3.0.0";
#pragma warning restore 0414

        public static float Find(float _percent, float _of)
        {
            return (_of / 100f) * _percent;
        }
        public static float What(float _is, float _of)
        {
            return (_is * 100f) / _of;
        }

        public static int PennyToss(int _from = 0, int _to = 100)
        {
            var randomNumber = Random.Range(_from, _to);
            return (randomNumber > 50) ? 1 : 0;
        }

        public static T GetRandomFromArray<T>(T[] list)
        {
            List<int> percentages = new List<int>();
            int splitPercentages = Mathf.FloorToInt(100 / list.Length);
            int remainder = 100 - (splitPercentages * list.Length);
            for (int i = 0; i < list.Length; i++)
            {
                int percent = i == (list.Length - 1) ? splitPercentages + remainder : splitPercentages;
                percentages.Add(percent);
            }
            for (int i = 1; i < percentages.Count; i++)
            {
                percentages[i] = percentages[i - 1] + percentages[i];
            }
            int randomNumber = UnityEngine.Random.Range(0, 100);
            int index = percentages.FindIndex(p => randomNumber < p);
            percentages = null;
            return list[index];
        }

        public static T GetRandomFromList<T>(List<T> list)
        {
            List<int> percentages = new List<int>();
            int splitPercentages = Mathf.FloorToInt(100 / list.Count);
            int remainder = 100 - (splitPercentages * list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                int percent = i == (list.Count - 1) ? splitPercentages + remainder : splitPercentages;
                percentages.Add(percent);
            }
            for (int i = 1; i < percentages.Count; i++)
            {
                percentages[i] = percentages[i - 1] + percentages[i];
            }
            int randomNumber = UnityEngine.Random.Range(0, 100);
            int index = percentages.FindIndex(p => randomNumber < p);
            percentages = null;
            return list[index];
        }
    }
}
