using UnityEngine.UI;
using UnityEngine;
using System.Collections;


/// : Bar

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image Fill;
    [SerializeField] private Image subFill;
    [SerializeField] private bool hideOnFull;
    [SerializeField] private HealthEventChannel _OnHpChange;
    
    private const float MAX_TIME_SHINK = 1f;
    private HealthSystem healthSystem;
    IEnumerator Ishrink;
    
    private void OnEnable()
    {
        if (_OnHpChange != null)
            _OnHpChange.onEventRaised += SetUp;
    }

    private void OnDisable()
    {
        if (_OnHpChange != null)
            _OnHpChange.onEventRaised -= SetUp;
    }
    public void SetUp(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        SetValue(healthSystem.GetHealthNormalized());
        SetSubFillValue(healthSystem.GetHealthNormalized());
        healthSystem.Ondamage += HealthSystem_OnDamage;
        healthSystem.OnHealed += HealthSystem_OnHeal;
    }
    private void SetValue(float value)
    {
        Fill.fillAmount = value;
        Hide();
    }
    private void SetSubFillValue(float value)
    {
        subFill.fillAmount = value;
        Hide();
    }
    private void HealthSystem_OnDamage(object sender,System.EventArgs e)
    {
        SetValue(healthSystem.GetHealthNormalized());
       
        if (Ishrink == null && !Hide())
        {
            Ishrink = IShrinkEffect();
            StartCoroutine(Ishrink);
        }
    }
    private void HealthSystem_OnHeal(object sender, System.EventArgs e)
    {
        SetValue(healthSystem.GetHealthNormalized());
        SetSubFillValue(healthSystem.GetHealthNormalized());
        Hide();
    }
    private bool Hide()
    {
        if (hideOnFull && healthSystem.GetHealthNormalized() >= 1)
        {
            gameObject.SetActive(false);
            return true;
        }
        else
        {
            gameObject.SetActive(true);
            return false;
        }
    }
    private void OnDestroy()
    {
        if(healthSystem != null)
        {
            healthSystem.OnHealed -= HealthSystem_OnHeal;
            healthSystem.Ondamage -= HealthSystem_OnDamage;
        }
        if (Ishrink != null)
        {
            StopCoroutine(Ishrink);
        }
     }
    IEnumerator IShrinkEffect()
    {
        float shinkTime = MAX_TIME_SHINK;
        yield return Helpers.GetWait(0.05f);
        while (subFill.fillAmount > Fill.fillAmount)
        {
            shinkTime -= Time.deltaTime;
            subFill.fillAmount -= Mathf.Abs(shinkTime * Time.deltaTime);
            yield return Helpers.GetWait(0.01f);
        }
        Ishrink = null;
    }
}
