using UnityEngine;

public interface IDamageable
{
    void TakeDamage(DamageInfo damage);
}
public class DamageInfo{
    public DamageInfo(float damage,float knockBack, Vector3 attackerPosition)
    {
        Damage = damage;
        KnockBack = knockBack;
        AttackerPosition = attackerPosition;
    }

    public float Damage { get; set; }
    public float Impact { get; set; }
    public float Thrust { get; set; }
    public float Magic { get; set; }
    public float KnockBack { get; set; }
    public Vector3 AttackerPosition { get; set; }
  
}
