using System.Collections;
using Assets.HeadStart.Features.Database.JSON;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.HeadStart.Features.Database
{
    public class DatabaseGameTest : MonoBehaviour
    {
        private IEnumerator _waitEnmtor;

        void Start()
        {
            var dataService = new DataService();
            dataService.CleanDB();

            Debug.Log("__json.Database.RecreateDatabase()");
            __json.Database.RecreateDatabase();

            _waitEnmtor = waitEnumerator();
            StartCoroutine(_waitEnmtor);
        }

        private IEnumerator waitEnumerator()
        {
            yield return new WaitForSeconds(1);

            StopCoroutine(_waitEnmtor);
            SceneManager.LoadScene("MainMenu");
            //__.ClearSceneDependencies();
        }
    }
}
