using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.UI;
using Example1.ExampleGame;

namespace Example1.ExampleGame
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
            ExampleRandomPointsLogic.generateRandomPoints(ref CoreSession.S.SessionOpts);
            TextRandomPoints.text = CoreSession.S.SessionOpts.Points.ToString();
            // Main.S.Game.GameOver();
        }
    }
}
