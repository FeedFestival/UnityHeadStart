using System.Collections;
using System.Collections.Generic;
using Assets.HeadStart.Features.Database.JSON;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ExampleGamePlayTest
{
    private string _scene = "JSONDatabaseTest";
    [UnityTest]
    public IEnumerator ReinitializePlayerJson()
    {
        // ARRANGE
        LoadTheScene();
        yield return null;

        // ACT
        __json.Database.RecreateDatabase();
        yield return null;

        // ASSERT
        Assert.AreEqual(true, true);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestGetPlayer()
    {
        // ARRANGE
        LoadTheScene();
        yield return null;


        // ACT
        DevicePlayer playerExtension = __json.Database.GetPlayer();

        yield return null;
        Debug.Log("playerExtension: " + playerExtension);

        // ASSERT
        Assert.AreEqual(true, true);
    }

    private void LoadTheScene()
    {
        var scene = SceneManager.GetActiveScene().name;
        if (scene != _scene)
            SceneManager.LoadScene(_scene);
    }
}
