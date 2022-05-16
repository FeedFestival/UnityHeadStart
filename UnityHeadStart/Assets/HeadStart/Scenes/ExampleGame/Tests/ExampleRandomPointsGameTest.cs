using Assets.GameTester;
using Assets.HeadStart.Core;
using UnityEngine;

public class ExampleRandomPointsGameTest : GameTestBase
{
    [Header("ExampleRandomPoints")]
    public float TimeBetweenActions = 0.5f;

    void Start()
    {
        Time.timeScale = TimeScale;
    }

    public override void Test()
    {
        __.Time.RxWait(() =>
        {
            InvokeButton("ButtonGenerateRandom");

            __.Time.RxWait(() =>
            {
                InvokeButton("ButtonContinue");
            }, TimeBetweenActions);
        }, TimeBetweenActions);
    }
}
