using UnityEngine;

namespace Script.Core
{
    public class ManaBar : HealthBar
    {
        public override void SetUp()
        {
            Status.OnManaHeal += Mana_OnHeal;
            Status.OnManaUse += Mana_OnUse;

            SetValue(Status.GetManaNormalized());
            SetSubFillValue(Status.GetManaNormalized());
        }
      
        private void Mana_OnUse(object sender, System.EventArgs e)
        {
            SetValue(Status.GetManaNormalized());

            if (Ishrink == null)
            {
                Ishrink = IShrinkEffect();
                StartCoroutine(Ishrink);
            }
        }
        private void Mana_OnHeal(object sender, System.EventArgs e)
        {
            SetValue(Status.GetManaNormalized());
            SetSubFillValue(Status.GetManaNormalized());
        }
        private void OnDestroy()
        {
            if (Status != null)
            {
                Status.OnManaHeal -= Mana_OnHeal;
                Status.OnManaUse -= Mana_OnUse;
            }
            if (Ishrink != null)
            {
                StopCoroutine(Ishrink);
            }
        }
    }
}
