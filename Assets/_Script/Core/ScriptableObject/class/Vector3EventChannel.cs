using UnityEngine;
using UnityEngine.Events;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/Event/vector3 event channel")]
    public class Vector3EventChannel : ScriptableObject
    {
        public UnityAction<Vector3> onEventRaised;

        public void RiseEvent(Vector3 vector)
        {
            onEventRaised?.Invoke(vector);
        }
    }
}
