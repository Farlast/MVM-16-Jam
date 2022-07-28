using UnityEngine;

namespace Script.Enemy
{
    public class Door : MonoBehaviour
    {
        bool open;
        Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
            animator.Play("Idle");
        }
        public void OnEventTrigger()
        {
            if (open) return;

            animator.Play("Open");
            open = true;
        }
    }
}
