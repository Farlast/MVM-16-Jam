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
        [SerializeField] private GameObject healEffect;
        [SerializeField] private GameObject waterballSkill;
        [SerializeField] private InputMapping input;

        [Header("[Attack & HitBox]")]
        [SerializeField] private LayerMask attackLayer;
        [SerializeField] private Transform attackPos;
        [SerializeField] private float attackRangeX;
        [SerializeField] private float attackRangeY;

        [SerializeField] private Color HitboxColor;
        Rigidbody2D rigidBody2D;

        [Header("[Attack]")]
        [SerializeField] private float Damage;
        [SerializeField] private float KnockBackValue;

        [SerializeField] private float skillCost;

        [field: SerializeField] public int CurrentCombo { get; private set; }
        [field: SerializeField] public bool Invincible { get; private set; }
        [field: SerializeField] public bool GetHit { get; private set; }
        [field: SerializeField] public PlayerStatus Status { get; set; }
        [field: SerializeField] public AttackType CurrentAttackType { get; private set; }
        [field: SerializeField] public Vector2 KnockbackTaken { get; private set; }

        private void Awake()
        {
            healthSystem = new HealthSystem(Status.MaxHealth,Status.MaxMana);
            rigidBody2D = GetComponent<Rigidbody2D>();
            GetHit = false;
        }
        private void Start()
        {
            Status.health = Status.MaxHealth;
            Status.mana = Status.MaxMana;

            healthEvent?.RiseEvent(healthSystem);
            healEffect?.SetActive(false);
        }
        public bool CanUseSkill()
        {
            return Status.mana - skillCost >= 0;
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
            bool hitEnemy = false;
            
            for (int i = 0; i < TargetToDamage.Length; i++)
            {
                IDamageable damageable = TargetToDamage[i].GetComponent<IDamageable>();
                hitEnemy = true;
                if (damageable == null) return;

                    damageable.TakeDamage(new DamageInfo(
                    Damage,
                    KnockBackValue,
                    transform.position));              
            }
            if(hitEnemy) CameraManager.Instance.ScreenShake(1f,0.5f, 0.2f);
        }
        public void CastWaterball()
        {
            if (Status.mana - skillCost < 0) return;

            Status.mana = healthSystem.ManaUse(skillCost);
            float Distance = 2f;
            float front = input.LatesDirection * Distance;
            Vector2 InputDir = new Vector2(transform.position.x + front, transform.position.y);

            Instantiate(waterballSkill, InputDir, Quaternion.identity);
        }
        public void Heal(float amount)
        {
            StartCoroutine(IHealEffect());
            Status.Health = healthSystem.Heal(amount);
        }
        IEnumerator IHealEffect()
        {
            healEffect?.SetActive(true);
            yield return Helpers.GetWait(0.5f);
            healEffect?.SetActive(false);
        }
        public void TakeDamage(DamageInfo damage)
        {
            if (Invincible) return;

            CameraManager.Instance.ScreenShake(1f, 0.3f, 0.2f);
            Status.Health = healthSystem.Damage(damage);

            PrintDamage(damage);
            StartCoroutine(TakeHit(0.5f));
            StartCoroutine(MakeInvincible(1f));
            CalculateKnockbackTaken(damage);
            
        }
        void PrintDamage(DamageInfo damage)
        {
            print("<color=red> Takedamage = </color>" + damage.Damage
               + "\n <color=green> Health </color> = " + Status.Health
               + " <color=red> Health % </color> = " + (healthSystem.GetHealthNormalized() * 100) + "%");
        }
        private void CalculateKnockbackTaken(DamageInfo damage)
        {
            if (damage.KnockBack > 0)
            {
                Vector2 direction = (transform.position - damage.AttackerPosition).normalized;
                
                KnockbackTaken = new Vector2(direction.x * damage.KnockBack, 1 * damage.KnockBack);
            }
        }
        IEnumerator MakeInvincible(float time)
        {
            Invincible = true;
            yield return Helpers.GetWait(time);
            Invincible = false;
            GetHit = false;
        }
        IEnumerator TakeHit(float time)
        {
            GetHit = true;
            yield return Helpers.GetWait(time);
            GetHit = false;
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
