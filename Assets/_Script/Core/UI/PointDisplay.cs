using UnityEngine.UI;
using UnityEngine;

namespace Script.Core
{
    [RequireComponent(typeof(Image))]
    public class PointDisplay : MonoBehaviour
    {
        [SerializeField] private Sprite Fill;
        [SerializeField] private Sprite UnFill;
        [SerializeField] private Image current;

        void Start()
        {
            if (current == null)
                current = GetComponent<Image>();
        }
        public void SetFill(bool fill)
        {
            if (fill)
            {
                current.sprite = Fill;
            }
            else
            {
                current.sprite = UnFill;
            }
        }
        public void Animate()
        {

        }
    }
}
