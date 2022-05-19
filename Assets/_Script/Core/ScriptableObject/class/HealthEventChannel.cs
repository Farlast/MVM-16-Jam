using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/Event/health event channel")]
public class HealthEventChannel : ScriptableObject
{
    public UnityAction<HealthSystem> onEventRaised;

    public void RiseEvent(HealthSystem healthSystem)
    {
        onEventRaised?.Invoke(healthSystem);
    }
}
