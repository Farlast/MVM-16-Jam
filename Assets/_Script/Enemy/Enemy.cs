using System.Collections;
using UnityEngine;
using Script.Core;

namespace Script.Enemy
{
    public class Enemy : MonoBehaviour,IDamageable
    {
        [SerializeField] private float HP;
        [SerializeField] private float MaxHP;
        [SerializeField] private AnimationCurve knockbackCurve;
        [SerializeField] private float maxKnockbackTime;
        [SerializeField] private GameObject hitEffect;
        [Space]
        [Header("[Collision]")]
        [SerializeField] private FieldOfView fieldOfView;
        [SerializeField] private float eyeRadiusCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform feetPos;
        
        public Rigidbody2D Rb { get; set; }
        public bool isInvincible = false;

        [field: SerializeField] public float MoveSpeed { get; set; }
        [field: SerializeField] public float FaceDir { get; set; }
        [field:SerializeField]public State CurrentState { get; set; }
        [field: SerializeField] public bool IsAlive { get;private set; }
        [field: SerializeField] public bool TakeHit { get; private set; }
        [field: SerializeField] public bool isCliff { get; private set; }

        internal bool IsWallAtFront() => Physics2D.Raycast(transform.position, new Vector2(FaceDir, 0), eyeRadiusCheck, groundLayer);
        internal bool IsCliff() => isCliff = !Physics2D.Raycast(feetPos.position, Vector2.down, eyeRadiusCheck, groundLayer);
        Animator animator;
        public Animator GetAnimator => animator;
        public void TakeDamage(DamageInfo damage)
        {
            if (isInvincible) return;
            HP -= damage.Damage;
            IsAlive = HP > 0;
            StartCoroutine(HitTime());
            StartCoroutine(IKnockback(damage));
            if (!IsAlive) OnDead();
        }
        public virtual void OnDead()
        {
            Destroy(gameObject);
        }
        void Start()
        {
            HP = MaxHP;
            hitEffect.SetActive(false);
            Rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            FaceDir = -1;
            CurrentState.OnstateEnter();
        }

        void Update()
        {
            CurrentState.OnStateRun();
        }

        IEnumerator HitTime()
        {
            isInvincible = true;
            hitEffect.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            hitEffect.SetActive(false);
            isInvincible = false;
        }
        IEnumerator IKnockback(DamageInfo info)
        {
            TakeHit = true;
            var direction = info.GetDiraction(transform.position);
            Rb.AddForce(new Vector2((info.KnockBack * direction) * 20, 0));
            yield return Helpers.GetWait(1f);
            TakeHit = false;
        }
        public void FlipSprite(float latesDirection)
        {
            transform.eulerAngles = latesDirection < 0 ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
        }

        #region Gizmos
        private void OnDrawGizmos()
        {
           
            /// eye
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(FaceDir * eyeRadiusCheck, 0, 0));
           

            if (feetPos != null)
            {
                // feet
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(feetPos.position, feetPos.position + Vector3.down);
            }
        }
        #endregion
    }
}
