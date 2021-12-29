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
            CoreSession._.SessionOpts.Points = Random.Range(500, 3000);
            TextRandomPoints.text = CoreSession._.SessionOpts.Points.ToString();
            Debug.Log("TextRandomPoints: " + TextRandomPoints);
            Main._.Game.GameOver();
        });
    }
}
