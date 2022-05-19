using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Enemy
{
    public class Enemy : MonoBehaviour,IDamageable
    {
        [SerializeField] private float HP;
        [SerializeField] private float MaxHP;
        [SerializeField] private Transform fallCheck;
        [SerializeField] private Transform wallCheck;
        public LayerMask GroundLayer;
        private Rigidbody2D rb;
        //private bool facingRight;
        private bool IsAlive;

        public float speed = 5f;
        public bool isInvincible = false;
        //private bool isHitted = false;

        public void TakeDamage(DamageInfo damage)
        {
            if (isInvincible) return;
            HP -= damage.Damage;
            IsAlive = HP > 0;
            if (!IsAlive) Destroy(gameObject);
            else
            StartCoroutine(HitTime());
        }

        void Start()
        {
            HP = MaxHP;
        }

        void Update()
        {
        
        }
        IEnumerator HitTime()
        {
            //isHitted = true;
            isInvincible = true;
            yield return new WaitForSeconds(0.1f);
            //isHitted = false;
            isInvincible = false;
        }
    }
}
