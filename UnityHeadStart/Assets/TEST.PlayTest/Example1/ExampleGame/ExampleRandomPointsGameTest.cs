using Assets.GameTester;
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
        __.Timeout.RxSeconds(() =>
        {
            InvokeButton("ButtonGenerateRandom");

            __.Timeout.RxSeconds(() =>
            {
                InvokeButton("ButtonContinue");
            }, TimeBetweenActions);
        }, TimeBetweenActions);
    }
}
