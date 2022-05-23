using UnityEngine.UI;
using UnityEngine;
using System.Collections;


/// : Bar

public class HealthBar : MonoBehaviour
{
    [SerializeField] protected Image Fill;
    [SerializeField] protected Image subFill;
    [SerializeField] protected bool hideOnFull;
    [SerializeField] protected HealthEventChannel _OnHpChange;

    protected const float MAX_TIME_SHINK = 1f;
    protected HealthSystem healthSystem;
    protected IEnumerator Ishrink;
    
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
    public virtual void SetUp(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        SetValue(healthSystem.GetHealthNormalized());
        SetSubFillValue(healthSystem.GetHealthNormalized());
        SetHookEvent();
    }
    public virtual void SetHookEvent()
    {
        healthSystem.Ondamage += HealthSystem_OnDamage;
        healthSystem.OnHealed += HealthSystem_OnHeal;
    }
    protected void SetValue(float value)
    {
        Fill.fillAmount = value;
        Hide();
    }
    protected void SetSubFillValue(float value)
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
    protected IEnumerator IShrinkEffect()
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
