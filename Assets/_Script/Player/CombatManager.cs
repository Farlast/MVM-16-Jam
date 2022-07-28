using Script.Core;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Script.Player
{
    public class CombatManager : MonoBehaviour,IDamageable
    {
        [SerializeField] private PlayerBase playerBase;
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

        [Header("[Weapons]")]
        [SerializeField] private float skillCost;
        [SerializeField] DamageInfoEvent infoEvent;
        [field: SerializeField] public Weapon CurrentWeapon { get; private set; }

        [field: SerializeField] public int CurrentCombo { get; private set; }
        [field: SerializeField] public bool Invincible { get; private set; }
        [field: SerializeField] public bool GetHit { get; private set; }
        [field: SerializeField] public PlayerStatus Status { get; set; }
        [field: SerializeField] public DamageInfo.AttackType CurrentAttackType { get; private set; }
        [field: SerializeField] public Vector2 KnockbackTaken { get; private set; }

        private Dictionary<DamageInfo.AttackType, Weapon> Weapons = new Dictionary<DamageInfo.AttackType, Weapon>(); 
        
        #region setup
        private void Awake()
        {
            GetHit = false;
        }
        private void Start()
        {
            Status.SetUp();

            infoEvent.RiseEvent(CurrentAttackType);
            healEffect?.SetActive(false);

            WeaponSetup();

            if (GameStateManager.Instance != null)
                GameStateManager.Instance.onGameStateChange += OnGameStateChange;
        }
        private void OnDestroy()
        {
            if (GameStateManager.Instance != null)
                GameStateManager.Instance.onGameStateChange -= OnGameStateChange;
        }
        public void OnGameStateChange(GameStates newGameStates)
        {
            enabled = newGameStates == GameStates.GamePlay;
        }
        private void WeaponSetup()
        {
            Weapons.Add(DamageInfo.AttackType.Sword, new Sword(3, playerBase));
            Weapons.Add(DamageInfo.AttackType.Lance, new Lance(2, playerBase));

            CurrentWeapon = Weapons[DamageInfo.AttackType.Sword];
            CurrentAttackType = DamageInfo.AttackType.Sword;
        }
        #endregion
        #region Skill
        public bool CanUseSkill()
        {
            return Status.mana - skillCost >= 0;
        }
        public void CastWaterball()
        {
            if (Status.mana - skillCost < 0) return;

            Status.mana = Status.ManaUse(skillCost);
            float Distance = 2f;
            float front = input.LatesDirection * Distance;
            Vector2 InputDir = new Vector2(transform.position.x + front, transform.position.y);

            Instantiate(waterballSkill, InputDir, Quaternion.identity);
        }
        public void Heal(float amount)
        {
            StartCoroutine(IHealEffect());
            Status.Health = Status.Heal(amount);
        }
        IEnumerator IHealEffect()
        {
            healEffect?.SetActive(true);
            yield return Helpers.GetWait(0.5f);
            healEffect?.SetActive(false);
        }

        #endregion
        #region Attack
        public DamageInfo.AttackType SwicthAttackType()
        {
            switch (CurrentAttackType)
            {
                case DamageInfo.AttackType.Sword:
                    CurrentAttackType = DamageInfo.AttackType.Lance;
                    CurrentWeapon = Weapons[CurrentAttackType];
                    break;

                case DamageInfo.AttackType.Lance:
                    CurrentAttackType = DamageInfo.AttackType.Sword;
                    CurrentWeapon = Weapons[CurrentAttackType];
                    break;

                default:
                    CurrentAttackType = DamageInfo.AttackType.Sword;
                    break;
            }
            infoEvent.RiseEvent(CurrentAttackType);
            return CurrentAttackType;
        }
        public bool CurrentMaxCombo()
        {
            return CurrentCombo > CurrentWeapon.Maxcombo;
        }
        public void AttackBycombo(float faceDirection)
        {
            CurrentWeapon.Attack(CurrentCombo, faceDirection);
        }
        public void ResetAttackCombo()
        {
            CurrentCombo = 1;
        }
        public void Attack()
        {
            CurrentCombo++;
            Collider2D[] TargetToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, attackLayer);
            
            if (TargetToDamage == null) return;

            bool hitEnemy = false;
            for (int i = 0; i < TargetToDamage.Length; i++)
            {
                IDamageable damageable = TargetToDamage[i].GetComponent<IDamageable>();

                hitEnemy = true;

                if (damageable == null) return;

                    damageable.TakeDamage(new DamageInfo(
                    CurrentWeapon.Damage,
                    CurrentWeapon.KnockBackValue,
                    transform.position,
                    CurrentAttackType));
            }
            if(hitEnemy) CameraManager.Instance.ScreenShake(1f,0.5f, 0.2f);
        }
        #endregion
        #region TakeDamage
        public void TakeDamage(DamageInfo damage)
        {
            if (Invincible) return;

            CameraManager.Instance.ScreenShake(1f, 0.3f, 0.2f);
            Status.Damage(damage);

            PrintDamage(damage);
            StartCoroutine(TakeHit(0.5f));
            StartCoroutine(MakeInvincible(1f));
            CalculateKnockbackTaken(damage);
            
        }
        void PrintDamage(DamageInfo damage)
        {
            print("<color=red> Takedamage = </color>" + damage.Damage
               + "\n <color=green> Health </color> = " + Status.Health
               + " <color=red> Health % </color> = " + (Status.GetHealthNormalized() * 100) + "%");
        }
        private void CalculateKnockbackTaken(DamageInfo damage)
        {
            if (damage.KnockBack > 0)
            {
                Vector2 direction = (transform.position - damage.AttackerPosition).normalized;
                
                KnockbackTaken = new Vector2(direction.x * damage.KnockBack, 1 * damage.KnockBack);
            }
        }
        #endregion
        
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
