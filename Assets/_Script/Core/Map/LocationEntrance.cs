using UnityEngine;

namespace Script.Core
{
    public class LocationEntrance : MonoBehaviour
    {
        [Header("SceneLoad Settings")]
        [SerializeField] bool showLoadingScreen;
        [SerializeField] private SceneData thisSceneEntrance;
        [SerializeField] private SceneData SceneEntranceTo;
        [SerializeField] private Vector3EventChannel setPlayerPositionEventChannel;
        [SerializeField] private GameObject exitPosition;
        [Header("Event Channel")]
        [SerializeField] private SceneEventChannel eventChannel;


        private void Start()
        {
            if (exitPosition == null || thisSceneEntrance ==  null) return;
            SceneLoader.Instance.IsComefromThisLocation(thisSceneEntrance.MoveToEnteranceTag, exitPosition.transform.position);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;

            eventChannel.RiseEvent(SceneEntranceTo);
           
        }
    }
}
