using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Core
{
    public class MainMenu : MonoBehaviour
    {
        
        void Start()
        {
        
        }

        public void EnterGame()
        {
            SceneManager.LoadScene(1);
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
