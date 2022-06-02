using UnityEngine;

namespace Script.Player
{
    public class SlashWave : MonoBehaviour
    {
        [field: SerializeField] public float MoveSpeed { get; set; }
        [field: SerializeField] public float Diraction { get; set; }
        [field: SerializeField] public float Damage { get; set; }
        [field: SerializeField] public float LifeTime { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player")) return;

            if(collision.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(new DamageInfo(
                Damage,
                0,
                transform.position,
                DamageInfo.AttackType.None));
                Destroy(gameObject);
            }
        }
        private void Update()
        {
            transform.position += new Vector3(Diraction * MoveSpeed, 0);
            FlipSprite();
            if (LifeTime <= 0) Destroy(gameObject);
            else LifeTime -= 1 * Time.deltaTime;
        }
        public void FlipSprite()
        {
            transform.eulerAngles = Diraction > 0 ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
        }
    }
}
