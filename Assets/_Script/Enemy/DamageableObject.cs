using UnityEngine;

namespace Script.Enemy
{
    public class DamageableObject : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float knockback;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                var damageInfo = new DamageInfo(damage, knockback, transform.position);

                damageable.TakeDamage(damageInfo);
            }
        }
    }
}
