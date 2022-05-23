using UnityEngine;

public interface IDamageable
{
    void TakeDamage(DamageInfo damage);
}
public class DamageInfo{

    public DamageInfo()
    {
    }

    public DamageInfo(float damage,float knockBack, Vector3 attackerPosition)
    {
        Damage = damage;
        KnockBack = knockBack;
        AttackerPosition = attackerPosition;
    }

    public float Damage { get; set; }
    public float KnockBack { get; set; }
    public Vector3 AttackerPosition { get; set; }
    public int AttackType { get; set; }
  
}
