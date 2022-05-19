using UnityEngine;
using UnityEngine.SceneManagement;

/*Disables the cursor, freezes timeScale*/

namespace Script.Core
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] AudioClip pressSound;
        [SerializeField] AudioClip openSound;
        [SerializeField] GameObject pauseMenu;
        [SerializeField] GameObject firstSelected;

        [SerializeField] private VoidEventChannel _quitGameListener = default;
        [SerializeField] private VoidEventChannel _quitToMenuListener = default;
        [SerializeField] private VoidEventChannel _ToggleMenuListener = default;
        private bool _activeStatus = false;
        private void Start()
        {
            pauseMenu.SetActive(false);
            GameManager.Instance.SetFirstSelected(firstSelected);
            if (_quitGameListener != null) _quitGameListener.onEventRaised += Quit;
            if (_quitToMenuListener != null) _quitToMenuListener.onEventRaised += QuitToMenu;
            if (_ToggleMenuListener != null) _ToggleMenuListener.onEventRaised += ToggleMenu;
          
            GameStateManager.Instance.onGameStateChange += OnGameStateChange;
        }
        private void OnDestroy()
        {
            if (_quitGameListener != null) _quitGameListener.onEventRaised -= Quit;
            if (_quitToMenuListener != null) _quitToMenuListener.onEventRaised -= QuitToMenu;
            if (_ToggleMenuListener != null) _ToggleMenuListener.onEventRaised -= ToggleMenu;
            GameStateManager.Instance.onGameStateChange -= OnGameStateChange;
        }

        public void OnGameStateChange(GameStates newGameStates)
        {
            enabled = newGameStates == GameStates.GamePlay;
        }

        public void Quit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Time.timeScale = 1f;
            Application.Quit();
        }
        public void QuitToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
        public void TurnOffMenu()
        {
            _activeStatus = false;
            pauseMenu.SetActive(_activeStatus);
            GameStateManager.Instance.SetGameState(GameStates.GamePlay);
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        private void ToggleMenu()
        {
            _activeStatus = !_activeStatus;
            pauseMenu.SetActive(_activeStatus);
            if (_activeStatus)
            {
                GameStateManager.Instance.SetGameState(GameStates.Paused);
                GameManager.Instance.SetFirstSelected(firstSelected);
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
            else
            {
                GameStateManager.Instance.SetGameState(GameStates.GamePlay);
                Cursor.visible = false;
                Time.timeScale = 1f;
            }
        }
    }
}
