using UnityEngine;

public interface IDamageable
{
    void TakeDamage(DamageInfo damage);
}
public class DamageInfo{
    public enum AttackType
    {
        None,
        Sword,
        Lance,
        Wire
    }
    public DamageInfo()
    {
    }

    public DamageInfo(float damage,float knockBack, Vector3 attackerPosition,AttackType type)
    {
        Damage = damage;
        KnockBack = knockBack;
        AttackerPosition = attackerPosition;
        Type = type;
    }

    public float Damage { get; set; }
    public float KnockBack { get; set; }
    public Vector3 AttackerPosition { get; set; }
    public AttackType Type { get; set; }
  
    public float GetDiraction(Vector3 currentPosition)
    {
        return (currentPosition - AttackerPosition).normalized.x;
    }
}
