using UnityEngine;
using UnityEngine.SceneManagement;
using Script.Core;
using System.Collections;

public class MenuHandler : MonoBehaviour {

	[SerializeField] private int SceneIndex;
    [SerializeField] GameObject firstSelected;
    [SerializeField] private SceneFadeControl fadeCanvas;

    private void OnEnable()
    {
        GameManager.Instance.SetFirstSelected(firstSelected);
    }
    private void Start()
    {
        GameManager.Instance.SetFirstSelected(firstSelected);
    }
    public void QuitGame()
    {
        StartCoroutine(IExitGame());
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneObject(SceneIndex));
    }
    public void Setting()
    {
        GameManager.Instance.OpenGameSettings();
    }
    public IEnumerator LoadSceneObject(int SceneIndex)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneIndex, LoadSceneMode.Single);
        async.allowSceneActivation = false;

        fadeCanvas?.FadeIn();
        yield return Helpers.GetWait(1f);

        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100).ToString("n0") + "%");

            // Loading completed
            if (progress == 1f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
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
