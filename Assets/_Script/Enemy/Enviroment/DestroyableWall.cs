using UnityEngine;

namespace Script.Enemy
{
    public class DestroyableWall : MonoBehaviour, IDamageable
    {
        [SerializeField] private bool destroyFromLeft;
        [SerializeField] private GameObject attackedEffect;
        [SerializeField] private GameObject destroyedEffect;
        [SerializeField] private int maxHP;
        private int HP;

        private void Start()
        {
            HP = maxHP;
        }
        public void TakeDamage(DamageInfo damage)
        {
            var normalizedDir = (damage.AttackerPosition - transform.position).normalized;

            if (destroyFromLeft && normalizedDir.x < 0)
            {
                Damage();
            }
            else if (!destroyFromLeft && normalizedDir.x > 0)
            {
                Damage();
            }

            if (HP <= 0)
            {
                Destroy(gameObject);
            }
        }
        void Damage()
        {
            HP -= 1;
        }
    }
}
