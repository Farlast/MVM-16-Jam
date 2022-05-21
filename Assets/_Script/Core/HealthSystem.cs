using System;
public class HealthSystem
{
    public event EventHandler Ondamage;
    public event EventHandler OnHealed;

    private float Health;
    private float MaxHealth;

    private float Poised;
    private float MaxPoised;

    public HealthSystem(float maxHealth)
    {
        Health = maxHealth;
        MaxHealth = maxHealth;
    }

    public HealthSystem(float maxHealth, float maxPoised) : this(maxHealth)
    {
        Poised = maxPoised;
        MaxPoised = maxPoised;
    }

    public virtual float Damage(DamageInfo damage)
    {
        Health -= damage.Damage;
        Poised -= damage.Impact;
        if (Health < 0) Health = 0;
        if (Poised < 0) Poised = 0;
        Ondamage?.Invoke(this, EventArgs.Empty);
        return Health;
    }
    public virtual float Heal(float amount)
    {
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
        OnHealed?.Invoke(this, EventArgs.Empty);
        return Health;
    }
 
    public float GetHealthNormalized()
    {   //   get % health
        return Health/MaxHealth;
    }
    public int GetManaRound()
    {   //   get % health
        return (int)Math.Round(Health);
    }
    public float GetPoisedNormalized()
    {   //   get % poised
        return Poised / MaxPoised;
    }
    public float GetPoised()
    {   //   get poised
        return Poised;
    }
}
