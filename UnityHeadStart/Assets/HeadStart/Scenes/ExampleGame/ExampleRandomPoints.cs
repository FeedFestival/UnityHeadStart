using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.UI;

public class ExampleRandomPoints : MonoBehaviour
{
    public Text TextRandomPoints;
    public Button ButtonGenerateRandom;

    public void Init()
    {
        TextRandomPoints.text = string.Empty;

        ButtonGenerateRandom.onClick.AddListener(() =>
        {
            CoreSession._.SessionOpts.Points = Random.Range(800, 1500);
            CoreSession._.SessionOpts.ToiletPaper = Random.Range(0, 5);
            TextRandomPoints.text = CoreSession._.SessionOpts.Points.ToString();
            Main._.Game.GameOver();
        });
    }
}
