using UnityEngine;
using TMPro;

namespace Script.Core
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] GameObject PopupPanel;
        [SerializeField] TextMeshProUGUI Name;
        [SerializeField] TextMeshProUGUI Content;
        [SerializeField] PopupEvent OpenEvent;
        private void Start()
        {
            PopupPanel?.SetActive(false);
            OpenEvent.onEventRaised += OpenPopup;
        }
        private void OnDestroy()
        {
            OpenEvent.onEventRaised -= OpenPopup;
        }
        public void OpenPopup()
        {
            Cursor.visible = true;
            GameStateManager.Instance.SetGameState(GameStates.Paused);
            Name.text = OpenEvent.TitleName;
            Content.text = OpenEvent.Content;
            PopupPanel?.SetActive(true);
        }
        public void ClosePopup()
        {
            Cursor.visible = false;
            GameStateManager.Instance.SetGameState(GameStates.GamePlay);
            PopupPanel?.SetActive(false);
        }
    }
    
}
