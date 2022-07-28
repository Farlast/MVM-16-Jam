using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Core
{
    public class MainMenu : MonoBehaviour
    {
        public void EnterGame()
        {
            AsyncOperation systemScene = SceneManager.LoadSceneAsync(1);

            systemScene.completed += OnloadComplete; 
        }
        private void OnloadComplete(AsyncOperation asyncOperation)
        {
            AsyncOperation StartScene = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        }
        public void ExitGame()
        {
            Application.Quit();

        #if UNITY_EDITOR 
                    UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }
    }
}
