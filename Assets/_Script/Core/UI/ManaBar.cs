
namespace Script.Core
{
    public class ManaBar : HealthBar
    {
        public override void SetUp(HealthSystem healthSystem)
        {
            this.healthSystem = healthSystem;
            SetValue(healthSystem.GetManaNormalized());
            SetSubFillValue(healthSystem.GetManaNormalized());
            SetHookEvent();
        }
        public override void SetHookEvent()
        {
            healthSystem.OnManaHeal += Mana_OnHeal;
            healthSystem.OnManaUse += Mana_OnUse;
        }

        private void Mana_OnUse(object sender, System.EventArgs e)
        {
            SetValue(healthSystem.GetManaNormalized());

            if (Ishrink == null)
            {
                Ishrink = IShrinkEffect();
                StartCoroutine(Ishrink);
            }
        }
        private void Mana_OnHeal(object sender, System.EventArgs e)
        {
            SetValue(healthSystem.GetManaNormalized());
            SetSubFillValue(healthSystem.GetManaNormalized());
        }
        private void OnDestroy()
        {
            if (healthSystem != null)
            {
                healthSystem.OnManaHeal -= Mana_OnHeal;
                healthSystem.OnManaUse -= Mana_OnUse;
            }
            if (Ishrink != null)
            {
                StopCoroutine(Ishrink);
            }
        }
    }
}
