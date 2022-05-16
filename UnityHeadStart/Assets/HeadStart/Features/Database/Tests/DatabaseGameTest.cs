using System.Collections;
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

            _waitEnmtor = waitEnumerator();
            StartCoroutine(_waitEnmtor);
        }

        private IEnumerator waitEnumerator()
        {
            yield return new WaitForSeconds(1);

            StopCoroutine(_waitEnmtor);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
