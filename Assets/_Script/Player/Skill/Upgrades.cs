using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public class Upgrades : MonoBehaviour
    {
        [SerializeField] PlayerStatus status;
        [SerializeField] bool HasDoubleJump;
        [SerializeField] bool dash;
        [SerializeField] PopupEvent popup;
        [SerializeField] SaveData saveData;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            AddPower();
            Destroy(gameObject);
        }
        private void Start()
        {
            if(saveData.CollectDoubleJump && HasDoubleJump)
            {
                Destroy(gameObject);
            }
            if (saveData.CollectDash && dash)
            {
                Destroy(gameObject);
            }
        }
        private void AddPower()
        {
            if (HasDoubleJump)
            {
                status.HasDoubleJump = true;
                saveData.CollectDoubleJump = true;
                popup.SetContent("DoubleJump", "Now you can DoubleJump");
                popup?.RiseEvent();
            }
            if (dash)
            {
                status.HasDash = true;
                saveData.CollectDash = true;
                popup.SetContent("Air Dash", "Press SHIFT to dash in air.");
                popup?.RiseEvent();
            }
        }
    }
}
