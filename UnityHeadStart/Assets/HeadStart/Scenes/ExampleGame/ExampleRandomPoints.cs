using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HeadStart.Scenes.ExampleGame
{
    public interface IExampleRandomPoints
    {
        void Init();
    }

    public class ExampleRandomPoints : MonoBehaviour, IExampleRandomPoints
    {
        public Text TextRandomPoints;
        public Button ButtonGenerateRandom;

        public void Init()
        {
            TextRandomPoints.text = string.Empty;

            ButtonGenerateRandom.onClick.AddListener(onGenerateRandom);
        }

        private void onGenerateRandom()
        {
            ExampleRandomPointsLogic.generateRandomPoints(ref CoreSession._.SessionOpts);
            TextRandomPoints.text = CoreSession._.SessionOpts.Points.ToString();
            Main._.Game.GameOver();
        }
    }
}
