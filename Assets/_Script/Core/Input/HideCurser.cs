using UnityEngine;

namespace Script.Core
{
    public class HideCurser : MonoBehaviour
    {
        [SerializeField] VoidEventChannel pauseMenuevent;
        private bool togle = false;
        void Start()
        {
            Cursor.visible = false;
            pauseMenuevent.onEventRaised += OnPaseMenu;
        }
        private void OnDestroy()
        {
            pauseMenuevent.onEventRaised -= OnPaseMenu;
        }
        private void OnPaseMenu()
        {
            togle = !togle;
            Cursor.visible = togle;
        }
    }
}
