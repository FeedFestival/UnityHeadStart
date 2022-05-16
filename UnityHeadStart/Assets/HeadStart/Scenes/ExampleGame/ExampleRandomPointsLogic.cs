using Assets.HeadStart.Core;
using UnityEngine;

namespace Assets.HeadStart.Scenes.ExampleGame
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
