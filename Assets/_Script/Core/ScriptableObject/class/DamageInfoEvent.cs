using UnityEngine;
using UnityEngine.Events;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/Event/WeaponType event channel")]
    public class DamageInfoEvent : ScriptableObject
    {
        public UnityAction<DamageInfo.AttackType> onEventRaised;

        public void RiseEvent(DamageInfo.AttackType info)
        {
            onEventRaised?.Invoke(info);
        }
    }
}
