using Assets.HeadStart.Core;
using UnityEngine;

namespace Example1.ExampleGame
{
    public static class ExampleRandomPointsLogic
    {
        public static void generateRandomPoints(ref SessionOpts sessionOpts)
        {
            sessionOpts.Points = Random.Range(800, 1500);
            sessionOpts.ToiletPaper = Random.Range(0, 5);
        }
    }
}
