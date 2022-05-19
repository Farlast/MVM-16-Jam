using UnityEngine;
using TMPro;

public class FPScounter : MonoBehaviour
{
    public TextMeshProUGUI fpsDisplay;
    void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fpsDisplay.text = "FPS: " + Mathf.Round(fps).ToString();
    }
}
