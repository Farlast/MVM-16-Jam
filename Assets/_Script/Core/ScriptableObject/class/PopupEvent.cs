using UnityEngine;
using UnityEngine.Events;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/Event/upgrades event channel")]
    public class PopupEvent : ScriptableObject
    {
        public UnityAction onEventRaised;

        public void RiseEvent()
        {
            onEventRaised?.Invoke();
        }

        [field:SerializeField] public string TitleName { get; set; }
        [field: SerializeField] public string Content { get; set; }

        public void SetContent(string Name ,string Content)
        {
            this.TitleName = Name;
            this.Content = Content;
        }
    }
}
