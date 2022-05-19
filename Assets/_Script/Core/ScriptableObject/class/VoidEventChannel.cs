using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/Event/void event channel")]
public class VoidEventChannel : ScriptableObject
{
    public UnityAction onEventRaised;

    public void RiseEvent()
    {
        onEventRaised?.Invoke();
    }
}
