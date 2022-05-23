using UnityEngine;

namespace Script.Core
{
    public class SceneFadeControl : MonoBehaviour
    {
        [SerializeField] private GameObject fadeCanvas;
        Animator animator;
        public static SceneFadeControl Instance;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            Instance = this;
        }
        private void Start()
        {
            if (fadeCanvas == null) return;
            fadeCanvas.SetActive(true);
        }

        public void DisableFadeUI()
        {
            if (fadeCanvas == null) return;
            fadeCanvas.SetActive(false);
        }

        public void FadeIn()
        {
            fadeCanvas.SetActive(true);
            animator.Play("FadeIn");
        }
    }
}
