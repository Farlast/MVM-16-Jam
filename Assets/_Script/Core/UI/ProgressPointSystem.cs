using UnityEngine;
using System.Collections.Generic;

namespace Script.Core
{
    /*
     sudo

    setmax mana :
    gen queue of mana point

    on damage : set nufill n from queue

    on heal : set fill n from queue

     */
    public class ProgressPointSystem : MonoBehaviour
    {
        private HealthSystem healthSystem;
        
        [SerializeField] private bool hideOnFull;
        [SerializeField] private HealthEventChannel _OnChange;
        [SerializeField] private GameObject pointDisplay;
        private List<GameObject> pointDisplays = new List<GameObject>();

        private void OnEnable()
        {
            if (_OnChange != null)
                _OnChange.onEventRaised += SetUp;
        }

        private void OnDisable()
        {
            if (_OnChange != null)
                _OnChange.onEventRaised -= SetUp;
        }

        public void SetUp(HealthSystem healthSystem)
        {
            this.healthSystem = healthSystem;

            healthSystem.Ondamage += HealthSystem_OnDamage;
            healthSystem.OnHealed += HealthSystem_OnHeal;

            SetMaxHealth(healthSystem.GetManaRound());
        }
        public void SetMaxHealth(float maxValue)
        {
           
            pointDisplays.Clear();
            Helpers.DeleteChildren(transform);

            for (int i =0;i < maxValue ; i++)
            {
                var obj = Instantiate(pointDisplay);
                pointDisplays.Add(obj);
                obj.transform.SetParent(transform);
            }
        }
        private void SetValue(float value)
        {
            int i = 0;
            foreach (var item in pointDisplays)
            {
                if (i < value)
                    item.GetComponent<PointDisplay>().SetFill(true);
                else
                    item.GetComponent<PointDisplay>().SetFill(false);
                i++;
            }
        }
        private void HealthSystem_OnDamage(object sender, System.EventArgs e)
        {
            SetValue(healthSystem.GetManaRound());
        }
        private void HealthSystem_OnHeal(object sender, System.EventArgs e)
        {

        }
        private void Hide()
        {
            if (hideOnFull)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
