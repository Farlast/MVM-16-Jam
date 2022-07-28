using UnityEngine;
using Cinemachine;

namespace Script.Core
{
    public class CameraConfinder : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private void Start()
        {
            virtualCamera.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                virtualCamera.enabled = true;
                
                virtualCamera.Follow = collision.gameObject.transform;
                CameraManager.Instance.SetNewCamera(virtualCamera, GetComponent<PolygonCollider2D>());
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            virtualCamera.Priority = 1;
        }
    }
}
