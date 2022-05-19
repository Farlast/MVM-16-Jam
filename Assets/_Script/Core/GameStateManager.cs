using UnityEngine;

namespace Script.Core
{
    public enum GameStates
    {
        GamePlay,
        Paused
    }
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance;
        public GameStates CurrentGameState { get; private set; }
        public delegate void GameStateChangeHandler(GameStates newGameStates);
        public event GameStateChangeHandler onGameStateChange;

        private void Awake()
        {
            Instance = this;
        }
        public void SetGameState(GameStates newGameStates)
        {
            if (newGameStates == CurrentGameState) return;
            CurrentGameState = newGameStates;
            onGameStateChange?.Invoke(newGameStates);
        }
    }
}
