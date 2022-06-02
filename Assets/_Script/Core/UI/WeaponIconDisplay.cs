using UnityEngine.UI;
using UnityEngine;

namespace Script.Core
{
    public class WeaponIconDisplay : MonoBehaviour
    {
        [SerializeField] Sprite Sword;
        [SerializeField] Sprite Lance;
        [SerializeField] DamageInfoEvent infoEvent;

        private void Start()
        {
            infoEvent.onEventRaised += SetImageDisplay;
        }
        private void OnDestroy()
        {
            infoEvent.onEventRaised -= SetImageDisplay;
        }
        public void SetImageDisplay(DamageInfo.AttackType type)
        {
            switch (type)
            {
                case DamageInfo.AttackType.Sword:
                    GetComponent<Image>().sprite = Sword;
                    break;
                case DamageInfo.AttackType.Lance:
                    GetComponent<Image>().sprite = Lance;
                    break;
            }
        }
    }
}
