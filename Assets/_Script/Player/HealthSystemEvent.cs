using UnityEngine;
using System;
using UnityEngine.Events;

namespace Script.Player
{
    public class HealthSystemEvent : ScriptableObject
    {
        
        public event EventHandler Ondamage;
        public event EventHandler OnHealed;

        public event EventHandler OnManaUse;
        public event EventHandler OnManaHeal;

        private float Health;
        private float MaxHealth;

        private float Mana;
        private float MaxMana;


        public UnityAction<PlayerStatus> onEventRaised;
        public void RiseEvent(PlayerStatus healthSystem)
        {
            onEventRaised?.Invoke(healthSystem);
        }

        public float Damage(DamageInfo damage)
        {
            Health -= damage.Damage;
            if (Health < 0) Health = 0;
            Ondamage?.Invoke(this, EventArgs.Empty);
            return Health;
        }
        public float Heal(float amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
            OnHealed?.Invoke(this, EventArgs.Empty);
            return Health;
        }

        public float GetHealthNormalized()
        {   //   get % health
            return Health / MaxHealth;
        }

        public float ManaUse(float cost)
        {
            Mana -= cost;
            if (Mana < 0) Mana = 0;
            OnManaUse?.Invoke(this, EventArgs.Empty);
            return Mana;
        }
        public float ManaHeal(float amount)
        {
            Mana += amount;
            if (Mana > MaxMana) Mana = MaxMana;
            OnManaHeal?.Invoke(this, EventArgs.Empty);
            return Mana;
        }

        public float GetManaNormalized()
        {   //   get % mana
            return Mana / MaxMana;
        }
    }
}
