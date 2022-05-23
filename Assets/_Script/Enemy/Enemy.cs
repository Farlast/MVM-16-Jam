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
        [SerializeField] private GameObject hitEffect;
        
        public void TakeDamage(DamageInfo damage)
        {
            if (isInvincible) return;
            HP -= damage.Damage;
            IsAlive = HP > 0;
            StartCoroutine(HitTime());
            
            if (!IsAlive) Destroy(gameObject);
        }

        void Start()
        {
            HP = MaxHP;
            hitEffect.SetActive(false);
        }

        IEnumerator HitTime()
        {
            isInvincible = true;
            hitEffect.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            hitEffect.SetActive(false);
            isInvincible = false;
        }
    }
}
