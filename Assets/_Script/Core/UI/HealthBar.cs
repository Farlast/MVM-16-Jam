using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField] protected Image Fill;
    [SerializeField] protected Image subFill;
    [SerializeField] protected bool hideOnFull;
    //[SerializeField] protected HealthEventChannel _OnHpChange;
    [SerializeField] protected PlayerStatus Status;

    protected const float MAX_TIME_SHINK = 1f;
    protected IEnumerator Ishrink;
   
    private void Start()
    {
        SetUp();
    }

    public virtual void SetUp()
    {
        if (Status != null)
        {
            Status.Ondamage += HealthSystem_OnDamage;
            Status.OnHealed += HealthSystem_OnHeal;

            SetValue(Status.GetHealthNormalized());
            SetSubFillValue(Status.GetHealthNormalized());
        }
    }

    private void OnDestroy()
    {
        if(Status != null)
        {
            Status.Ondamage -= HealthSystem_OnDamage;
            Status.OnHealed -= HealthSystem_OnHeal;
        }
       
        if (Ishrink != null)
        {
            StopCoroutine(Ishrink);
        }
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
        SetValue(Status.GetHealthNormalized());

        if (Ishrink == null && !Hide())
        {
            Ishrink = IShrinkEffect();
            StartCoroutine(Ishrink);
        }
    }
    private void HealthSystem_OnHeal(object sender, System.EventArgs e)
    {
        SetValue(Status.GetHealthNormalized());
        SetSubFillValue(Status.GetHealthNormalized());
        Hide();
    }
    private bool Hide()
    {
        if (hideOnFull && Status.GetHealthNormalized() >= 1)
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
