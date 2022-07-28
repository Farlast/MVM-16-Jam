using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Core
{
    public class ScenePartLoader : MonoBehaviour
    {
        [Header("SceneLoad Settings")]
        [SerializeField] private bool isLoaded;
        [SerializeField] private bool shouldLoad;

        private void Start()
        {
            if(SceneManager.sceneCount > 0)
            {
                for  (int i = 0; i < SceneManager.sceneCount; ++i)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if(scene.name == gameObject.name)
                    {
                        isLoaded = true;
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            LoadScene();
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            UnLoadScene();
        }
        private void LoadScene()
        {
            if (isLoaded) return;
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }
        private void UnLoadScene()
        {
            if (!isLoaded) return;
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }
}
