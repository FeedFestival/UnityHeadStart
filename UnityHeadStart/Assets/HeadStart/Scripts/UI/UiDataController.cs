using UnityEngine;
using UnityEngine.UI;

public class UiDataController : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.0.7";
#pragma warning restore 0414 //
    public Text FpsText;
    public int avgFrameRate;

    public void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        FpsText.text = avgFrameRate.ToString();
    }
}
