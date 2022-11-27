using UnityEngine;

namespace GameScrypt.GSMain
{
    public class GSMain : MonoBehaviour
    {
        public GSGameScene GameScene;

        private void Start()
        {
            this.preStartInit();
            __.Timeout.Milliseconds(() =>
            {
                this.startGameScene();
            }, 128);
        }

        private void preStartInit()
        {
            this.assertDominant();
            GameScene.Init();
        }

        private void startGameScene()
        {
            GameScene.StartGameScene();
        }

        private void assertDominant()
        {
            gameObject.name = "____________main____________";
        }
    }
}