using System.Collections;
using GameScrypt.Example;
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
        var deviceJsonData = new DeviceJsonData("player.json");
        deviceJsonData.Recreate();
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
        var deviceJsonData = new DeviceJsonData("player.json");
        PlayerSettings playerSettings = deviceJsonData.GetPlayer();

        yield return null;
        Debug.Log("playerExtension: " + playerSettings);

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
