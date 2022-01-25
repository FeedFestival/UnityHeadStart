using UnityEngine;
using UnityEngine.UI;

public class UiDataController : MonoBehaviour
{
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
