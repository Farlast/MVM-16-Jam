using Script.Core;
using UnityEngine;
using System.Collections;

namespace Script.Player
{
    public enum AttackType
    {
        Sword,
        Lance,
        Wire
    } 
    public class CombatManager : MonoBehaviour,IDamageable
    {
        private HealthSystem healthSystem;
        [SerializeField] private HealthEventChannel healthEvent;

        [Header("[Attack & HitBox]")]
        [SerializeField] private LayerMask attackLayer;
        [SerializeField] private Transform attackPos;
        [SerializeField] private float attackRangeX;
        [SerializeField] private float attackRangeY;

        [SerializeField] private Color HitboxColor;
        Rigidbody2D rigidBody2D;

        [Header("[Attack]")]
        [SerializeField] float Damage;
        [SerializeField] float KnockBackValue;

        [field: SerializeField] public int CurrentCombo { get; private set; }
        [field: SerializeField] public bool Invincible { get; private set; }
        [field: SerializeField] public bool GetHit { get; private set; }
        [field: SerializeField] public PlayerStatus Status { get; set; }
        [field: SerializeField] public AttackType CurrentAttackType { get; private set; }

        private void Awake()
        {
            healthSystem = new HealthSystem(Status.MaxHealth);
            rigidBody2D = GetComponent<Rigidbody2D>();
            GetHit = false;
        }
        private void Start()
        {
            Status.health = Status.MaxHealth;
            healthEvent?.RiseEvent(healthSystem);
        }
        public AttackType SwicthAttackType()
        {
            switch (CurrentAttackType)
            {
                case AttackType.Sword:
                    CurrentAttackType = AttackType.Lance;
                    break;

                case AttackType.Lance:
                    CurrentAttackType = AttackType.Wire;
                    break;

                case AttackType.Wire:
                    CurrentAttackType = AttackType.Sword;
                    break;

                default:
                    CurrentAttackType = AttackType.Sword;
                    break;
            }
            return CurrentAttackType;
        }

        public void Attack()
        {
            Collider2D[] TargetToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, attackLayer);
            
            if (TargetToDamage == null) return;
            //bool hitEnemy = false;
            
            for (int i = 0; i < TargetToDamage.Length; i++)
            {
                IDamageable damageable = TargetToDamage[i].GetComponent<IDamageable>();
                //hitEnemy = true;
                if (damageable == null) return;

                    damageable.TakeDamage(new DamageInfo(
                    Damage,
                    KnockBackValue,
                    transform.position));              
            }
            //if(hitEnemy) CameraManager.Instance.ScreenShake(1f,0.5f, 0.2f);
        }
        public void TakeDamage(DamageInfo damage)
        {
            if (Invincible) return;

            //CameraManager.Instance.ScreenShake(1f, 0.3f, 0.2f);
            Status.Health = healthSystem.Damage(damage);

            GetHit = true;
            PrintDamage(damage);
            StartCoroutine(MakeInvincible(1f));
            HandleTakeKnockback(damage);
            GetHit = false;
        }
        public void Heal(float amount)
        {
            Status.Health = healthSystem.Heal(amount);
        }
        void PrintDamage(DamageInfo damage)
        {
            print("<color=red> Takedamage = </color>" + damage.Damage
               + "\n <color=green> Health </color> = " + Status.Health
               + " <color=red> Health % </color> = " + (healthSystem.GetHealthNormalized() * 100) + "%");
        }
        private void HandleTakeKnockback(DamageInfo damage)
        {
            if (damage.KnockBack > 0)
            {
                Vector2 direction = (transform.position - damage.AttackerPosition).normalized;
                rigidBody2D.velocity = Vector2.zero;
                rigidBody2D.inertia = 0;
                Vector2 knockBack = new Vector2(direction.x * damage.KnockBack, 1 * damage.KnockBack);
                rigidBody2D.AddForce(knockBack, ForceMode2D.Impulse);
            }
        }
        IEnumerator MakeInvincible(float time)
        {
            Invincible = true;
            yield return new WaitForSeconds(time);
            Invincible = false;
        }
        void OnDrawGizmos()
        {
            if(attackPos != null)
            {
                Gizmos.color = HitboxColor;
                Gizmos.DrawCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
            }
        }
    }
}
