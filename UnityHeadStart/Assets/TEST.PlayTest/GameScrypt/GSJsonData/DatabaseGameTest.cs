using Assets.HeadStart.Features.Database;
using HeadStart;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabaseGameTest : MonoBehaviour
{
    private IEnumerator _waitEnmtor;

    void Start()
    {
        //var dataService = new DataService();
        //dataService.CleanDB();

        //Debug.Log("__json.Database.RecreateDatabase()");
        //var deviceJsonData = new DeviceJsonData("player.json");
        //deviceJsonData.Recreate();

        //_waitEnmtor = waitEnumerator();
        //StartCoroutine(_waitEnmtor);
    }

    private IEnumerator waitEnumerator()
    {
        yield return new WaitForSeconds(1);

        StopCoroutine(_waitEnmtor);
        SceneManager.LoadScene("MainMenu");
        //__.ClearSceneDependencies();
    }
}
