using Cinemachine;
using UnityEngine;
namespace Script.Core
{
    public class CameraManager : MonoBehaviour
    {
        #region singleton
        public static CameraManager Instance;
        #endregion

        [SerializeField] private CinemachineVirtualCamera activeVirtualCamera;
        public CinemachineVirtualCamera ActiveVirtualCamera { get => activeVirtualCamera; set => activeVirtualCamera = value; }
        private CinemachineConfiner confiner;
        private CameraEffects cameraEffects;

        public delegate void GameStateChangeHandler(float shakeAmplitude, float frequency, float length);
        public event GameStateChangeHandler onCameraEffect;

        void Awake()
        {
            Instance = this;
            cameraEffects = GetComponent<CameraEffects>();
        }
        private void Start()
        {
            cameraEffects.Onshake += ScreenShake;
            cameraEffects.SetCamera(activeVirtualCamera);
        }
        public void SetConfinder(PolygonCollider2D collider2D)
        {
            confiner.m_BoundingShape2D = collider2D;
        }
        public void SetNewCamera(CinemachineVirtualCamera virtualCamera, PolygonCollider2D collider2D) {
            if(activeVirtualCamera != null) activeVirtualCamera.Priority = 1;
            virtualCamera.Priority = 10;
            activeVirtualCamera = virtualCamera;
            virtualCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = collider2D;
            cameraEffects.SetCamera(virtualCamera);
        }
        public void ScreenShake(float shakeAmplitude, float frequency, float length)
        {
            onCameraEffect.Invoke(shakeAmplitude, frequency, length);
        }
    }
}
