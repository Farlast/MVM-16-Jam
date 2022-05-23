using UnityEngine;
using Script.Core;
using System.Collections;

public class ESCMenuHandler : MonoBehaviour {

    [SerializeField] GameObject firstSelected;
    [SerializeField] GameObject Canvas;
    [SerializeField] private SceneFadeControl fadeCanvas;

    private void Start()
    {
        Canvas?.SetActive(false);
    }
    public void Continue()
    {
        Canvas.SetActive(false);
    }
    public void ExitGame()
    {
        StartCoroutine(IExitGame());
    }

    public IEnumerator IExitGame()
    {
        fadeCanvas?.FadeIn();
        yield return Helpers.GetWait(1f);

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}
