using UnityEngine;
using Cinemachine;
using System;
namespace Script.Core
{
    public class CameraEffects : MonoBehaviour
    {
        [SerializeField] private CinemachineFramingTransposer _cinemachineFramingTransposer;
        [SerializeField] private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;
        [SerializeField] private float _shakeLength = 0;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        public event Action<float, float,float> Onshake = delegate { };

        void Start()
        {
            CameraManager.Instance.onCameraEffect += Shake;
        }
        private void OnDestroy()
        {
            CameraManager.Instance.onCameraEffect -= Shake;
        }
        void Update()
        {
            if (_multiChannelPerlin == null) return;
            if (_shakeLength > 0)
            {
                _shakeLength -= Time.deltaTime;
                if(_shakeLength <= 0f)
                {
                    _multiChannelPerlin.m_AmplitudeGain = 0f;
                }
            }
        }
        public void SetCamera(CinemachineVirtualCamera cinemachine)
        {
            if (cinemachine == null) return;
            _virtualCamera = cinemachine;
            _cinemachineFramingTransposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            _multiChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        public void Shake(float shakeAmplitude, float frequency, float length)
        {
            _shakeLength = length;
            _multiChannelPerlin.m_FrequencyGain = frequency;
            _multiChannelPerlin.m_AmplitudeGain = shakeAmplitude;
        }
    }
}
