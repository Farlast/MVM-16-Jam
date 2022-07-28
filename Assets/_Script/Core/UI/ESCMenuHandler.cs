using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Script.Core
{
    public class ESCMenuHandler : MonoBehaviour
    {
        [Header("[Event]")]
        [SerializeField] VoidEventChannel pauseMenuEvent;
        [SerializeField] VoidEventChannel endgameEvent;
        [Space]
        [SerializeField] GameObject firstSelected;
        [SerializeField] GameObject canvas;
        [SerializeField] SceneFadeControl fadeCanvas;
        [SerializeField] GameObject winCanvas;

        bool open;
        private void Start()
        {
            open = false;
            canvas?.SetActive(open);
            winCanvas?.SetActive(false);
            if (pauseMenuEvent != null) pauseMenuEvent.onEventRaised += TolggleOpenCanvas;
            if (endgameEvent != null) endgameEvent.onEventRaised += TolggleOpenWinCanvas;
        }
        private void OnDestroy()
        {
            if (pauseMenuEvent != null) pauseMenuEvent.onEventRaised -= TolggleOpenCanvas;
            if (endgameEvent != null) endgameEvent.onEventRaised -= TolggleOpenWinCanvas;
        }
        public void TolggleOpenWinCanvas()
        {
            Cursor.visible = true;
            winCanvas?.SetActive(true);
            GameStateManager.Instance.SetGameState(GameStates.Paused);
        }
        public void TolggleOpenCanvas()
        {
            open = !open;
            canvas?.SetActive(open);

            if (open)
                GameStateManager.Instance.SetGameState(GameStates.Paused);
            else
                GameStateManager.Instance.SetGameState(GameStates.GamePlay);
        }
        public void RestartLevel()
        {
            GameStateManager.Instance.SetGameState(GameStates.GamePlay);
            StartCoroutine(IFade());
        }
        public void Continue()
        {
            open = false;
            canvas?.SetActive(open);
            GameStateManager.Instance.SetGameState(GameStates.GamePlay);
        }
        public IEnumerator IFade()
        {
            fadeCanvas?.FadeIn();
            yield return Helpers.GetWait(1f);
            SceneManager.LoadScene(0);
        }
        public void ExitGame()
        {
            GameStateManager.Instance.SetGameState(GameStates.GamePlay);
            StartCoroutine(IExitGame());
        }

        public IEnumerator IExitGame()
        {
            canvas?.SetActive(false);
            winCanvas?.SetActive(false);

            fadeCanvas?.FadeIn();
            yield return Helpers.GetWait(1f);
            
            Application.Quit();

#if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            
        }
    }
}
