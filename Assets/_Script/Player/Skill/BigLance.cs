using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    public class BigLance : MonoBehaviour
    {
        [field: SerializeField] public float Diraction { get; set; }
        [field: SerializeField] public float Damage { get; set; }
        [field: SerializeField] public float LifeTime { get; set; }

        private void Start()
        {
            FlipSprite();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) return;
            if (collision.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(new DamageInfo(
                Damage,
                10,
                transform.position,
                DamageInfo.AttackType.None));
            }
        }
        public void DestroyThis()
        {
            Destroy(gameObject);
        }
        public void FlipSprite()
        {
            transform.eulerAngles = Diraction > 0 ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
        }
    }
}
