using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GameTester
{
    public class StartGameTester : MonoBehaviour
    {
        public string StartScene;

        void Start()
        {
            SceneManager.LoadScene(StartScene);
        }
    }
}
