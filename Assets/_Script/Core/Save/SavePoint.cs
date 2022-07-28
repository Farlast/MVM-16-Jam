using UnityEngine;

namespace Script.Core
{
    public class SavePoint : MonoBehaviour
    {
        [SerializeField] SaveData save;
        [SerializeField] PlayerStatus playerStatus;
        [field:SerializeField] public Transform standPoint;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            save.position = collision.gameObject.transform.position;
           
            playerStatus.Heal(playerStatus.MaxHealth);
            playerStatus.ManaHeal(playerStatus.MaxMana);
            playerStatus.SavePosition = standPoint.position;
        }
    }
}
