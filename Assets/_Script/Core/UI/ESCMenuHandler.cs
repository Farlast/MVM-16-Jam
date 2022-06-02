using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Script.Core
{
    public class ESCMenuHandler : MonoBehaviour
    {
        [Header("[Event]")]
        [SerializeField] VoidEventChannel pauseMenuEvent;
        [Space]
        [SerializeField] GameObject firstSelected;
        [SerializeField] GameObject canvas;
        [SerializeField] SceneFadeControl fadeCanvas;

        bool open;
        private void Start()
        {
            open = false;
            canvas?.SetActive(open);

            if(pauseMenuEvent!= null) pauseMenuEvent.onEventRaised += TolggleOpenCanvas;
        }
        private void OnDestroy()
        {
            if (pauseMenuEvent != null) pauseMenuEvent.onEventRaised -= TolggleOpenCanvas;
        }
        public void TolggleOpenCanvas()
        {
            open = !open;
            canvas?.SetActive(open);

            if(open)
                GameStateManager.Instance.SetGameState(GameStates.Paused);
            else
                GameStateManager.Instance.SetGameState(GameStates.GamePlay);
        }
        public void RestartLevel()
        {
            GameStateManager.Instance.SetGameState(GameStates.GamePlay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void Continue()
        {
            open = false;
            canvas?.SetActive(open);
            GameStateManager.Instance.SetGameState(GameStates.GamePlay);
        }
        public void ExitGame()
        {
            StartCoroutine(IExitGame());
        }

        public IEnumerator IExitGame()
        {
            TolggleOpenCanvas();
            fadeCanvas?.FadeIn();
            yield return Helpers.GetWait(1f);
            
            Application.Quit();

#if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            
        }
    }
}
