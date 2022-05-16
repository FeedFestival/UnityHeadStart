using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.HeadStart.Core;
using Assets.HeadStart.Scenes.ExampleGame;

public class ExampleGameTests
{
    [Test]
    public void GenerateRandomPoints()
    {
        // ARRANGE
        SessionOpts sessionOpts = new SessionOpts();


        // ACT
        ExampleRandomPointsLogic.generateRandomPoints(ref sessionOpts);

 
        // ASSERT
        bool passedTest = (sessionOpts.Points >= 800 && sessionOpts.Points <= 1500);
        // Debug.Log("sessionOpts: " + sessionOpts.ToString());
        Assert.AreEqual(true, passedTest);
    }

    [Test]
    public void InitializeMonobehaviourTrick()
    {
        // ARRANGE
        IExampleRandomPoints exampleRandomPoints = Substitute.For<IExampleRandomPoints>();

        // ACT
        exampleRandomPoints.Init();
 
        // ASSERT
        Assert.AreEqual(true, true);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ExampleGameTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
