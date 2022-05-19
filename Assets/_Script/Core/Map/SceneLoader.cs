using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Core
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance;

        [SerializeField] private SceneEventChannel _loadLocation = default;
        [SerializeField] private Vector3EventChannel setPlayerPositionEventChannel = default;

        private SceneData sceneEntranceData;
        private Vector3 exitPosition;

        void Awake()
        {
            if (Instance == null) Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            _loadLocation.onEventRaised += LoadTo;         
        }
        private void OnDestroy()
        {
            _loadLocation.onEventRaised -= LoadTo;
        }
        // call when locationEntrances onStart
        public bool IsComefromThisLocation(ExitLocationIndex tag,Vector3 vector3)
        {
            if(sceneEntranceData != null && sceneEntranceData.MoveToEnteranceTag == tag)
            {
                exitPosition = vector3;
                return true;
            }
            return false;
        }
        public void LoadTo(SceneData sceneData)
        {
            print("get event :" + sceneData.MoveToEnteranceTag + " | At scene index:" + sceneData.SceneIndex);
            
            sceneEntranceData = sceneData;

            StartCoroutine(LoadSceneObject(sceneData.SceneIndex));
        }
        public IEnumerator LoadSceneObject(int SceneIndex)
        {
            GameStateManager.Instance.SetGameState(GameStates.Paused);

            AsyncOperation async = SceneManager.LoadSceneAsync(SceneIndex, LoadSceneMode.Single);
            async.allowSceneActivation = false;
            
            SceneFadeControl.Instance.FadeIn();
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
            setPlayerPositionEventChannel?.RiseEvent(exitPosition);
            GameStateManager.Instance.SetGameState(GameStates.GamePlay);
        }
       
    }
}
