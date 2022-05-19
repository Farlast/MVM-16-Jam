using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Core
{
    /// ------------------------
    ///   Game Settings
    /// ------------------------
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private GameObject settingPanel;
        [SerializeField] private GameObject settingPanelFirstSelected;
        [SerializeField] private GameObject oldSettingPanelFirstSelected;
        [SerializeField] private VoidEventChannel _ToggleOptionListener = default;

        public EventSystem EventSystem { get => eventSystem;private set => eventSystem = value; }

        private bool activeStatus;


        void Awake()
        {
            if (Instance == null) Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);     
        }

        private void Start()
        {
            //QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
            settingPanel.SetActive(false);
            
            activeStatus = false;
            if (_ToggleOptionListener != null) _ToggleOptionListener.onEventRaised += ToggleOptionMenu;
        }
        private void OnDestroy()
        {
            if (_ToggleOptionListener != null) _ToggleOptionListener.onEventRaised += ToggleOptionMenu;
        }
        #region Settings/option
        public void ToggleOptionMenu()
        {
            activeStatus = !activeStatus;
            if (activeStatus) 
                OpenGameSettings(); 
            else 
                CloseGameSettings();
        }
        public void OpenGameSettings()
        {
            settingPanel.SetActive(true);
            SetFirstSelected(settingPanelFirstSelected);
        }
        public void CloseGameSettings()
        {
            settingPanel.SetActive(false);
            if(oldSettingPanelFirstSelected!= null)
                SetFirstSelected(oldSettingPanelFirstSelected);
        }
        #endregion
        #region Effect
        public void InstantiateAndDestroy(GameObject effect, Transform target)
        {
            if (effect == null) return;

            GameObject childObject = Instantiate(effect);
            childObject.transform.parent = transform;
            childObject.transform.position = target.position;
            Destroy(childObject, 1f);
        }
       
        public void DesloveAndDestroy(GameObject effect, Transform target)
        {
            if (effect == null) return;

            GameObject childObject = Instantiate(effect);
            childObject.transform.parent = transform;
            childObject.transform.position = target.position;
            Material material = childObject.GetComponent<SpriteRenderer>().material;
            if (material == null){
                Destroy(childObject);
                return; 
            }
            StartCoroutine(IDesloveEffect(material, childObject));
        }
        IEnumerator IDesloveEffect(Material material,GameObject childObject)
        {
            float dissolveAmount = 1f;
            while (dissolveAmount > 0)
            {
                material.SetFloat("_FadeValue", dissolveAmount);
                dissolveAmount -= 0.01f;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
            Destroy(childObject);
        }
        #endregion

        #region Editor
        public void SetTragetFPS(int fps)
        {
            Application.targetFrameRate = fps;
        }
        public void UnlockTragetFPS()
        {
            Application.targetFrameRate = -1;
        }
        #endregion

        public void SetFirstSelected(GameObject gameObject)
        {
            oldSettingPanelFirstSelected = EventSystem.firstSelectedGameObject;
            EventSystem.firstSelectedGameObject = gameObject;
            EventSystem.SetSelectedGameObject(gameObject);
        }
        
    }
}
