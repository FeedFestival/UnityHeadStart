using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FpsDisplay : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "1.0.3";
#pragma warning restore 0414 //
    public Text Text;
    float deltaTime = 0.0f;
    public bool TemperTime;
    public float TimeScale;

    void Start()
    {
        if (Text != null)
            StartCoroutine(ShowFps());
    }

    void Update()
    {
        if (TemperTime)
            Time.timeScale = TimeScale;
    }

    IEnumerator ShowFps()
    {
        yield return new WaitForSeconds(1f);

        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        Text.text = text;

        StartCoroutine(ShowFps());
    }
}
