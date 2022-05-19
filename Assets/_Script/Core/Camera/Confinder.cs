using UnityEngine;
using Cinemachine;
namespace Script.Core
{
    public class Confinder : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                CameraManager.Instance.SetNewCamera(virtualCamera, GetComponent<PolygonCollider2D>());
            }
        }
    }
}
