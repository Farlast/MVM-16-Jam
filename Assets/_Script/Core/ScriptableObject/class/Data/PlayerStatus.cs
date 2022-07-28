using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObject/Status/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    public event EventHandler Ondamage;
    public event EventHandler OnHealed;

    public event EventHandler OnManaUse;
    public event EventHandler OnManaHeal;

    [Header("[Status]")]
    public float health;
    public float MaxHealth;
    public float MaxMana;
    public float mana;
    
    [Header("[Movement]")]
    public float Speed;
    public float JumpForce;
    public float MoveInput;
    
    [Header("[Abilitys]")]
    public bool HasDoubleJump;
    public bool HasDash;
    public bool HasWallJump;
    
    [Header("[Dash]")]
    public float DashSpeed;
    public float MaxDashTime;
    public float DashCooldown;

    public float Health { get => Mathf.Clamp(health,0,MaxHealth); set => health = value;}
    public float Mana { get => Mathf.Clamp(mana, 0, MaxMana); set => Mathf.Clamp(mana = value, 0, MaxMana); }

    public Vector3 SavePosition;
    public Vector3 GetLastSavePosition()
    {
        if(SavePosition != null)
            return SavePosition;

        return Vector3.zero;
    }
    public void SetUp()
    {
        health = MaxHealth;
        mana = MaxMana;
        OnHealed?.Invoke(this, EventArgs.Empty);
        OnManaHeal?.Invoke(this, EventArgs.Empty);
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
    // ----- mana ------------
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
