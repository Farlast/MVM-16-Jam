using System.Collections;
using UnityEngine;

namespace Script.Player
{
    public class WaterBall : MonoBehaviour,IDamageable
    {
        [SerializeField] float decayTime;
        [SerializeField] float healAmount;
        [SerializeField] GameObject decayEffect;
        [SerializeField] bool takeHit;
        IEnumerator decay;
        [SerializeField] GameObject slashWave;
        [SerializeField] GameObject bigLance;
        void Start()
        {
            takeHit = false;
            decay = IDacay();
            StartCoroutine(decay);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (takeHit) return;

            if(collision.gameObject.TryGetComponent(out CombatManager player))
            {
                StopCoroutine(decay);
                player.Heal(healAmount);
                Destroy(gameObject);
            }
        }

        IEnumerator IDacay()
        {
            yield return Helpers.GetWait(decayTime);
            if(decayEffect != null) Instantiate(decayEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }

        public void TakeDamage(DamageInfo damage)
        {
            takeHit = true;
            ShapeBlend(damage);
        }
        public void ShapeBlend(DamageInfo damage)
        {
            StopCoroutine(decay);
            if (damage.Type == DamageInfo.AttackType.Sword)
            {
                print("attack by sword");
                takeHit = true;
                //Instantiate slash and destroy this
                var sWave = slashWave.GetComponent<SlashWave>();
                sWave.Diraction = (transform.position - damage.AttackerPosition).normalized.x;

                Instantiate(slashWave, transform.position, Quaternion.identity);
                Destroy(gameObject);
            } else if (damage.Type == DamageInfo.AttackType.Lance)
            {
                print("attack by Lance");
                var lance = bigLance.GetComponent<BigLance>();
                lance.Diraction = (transform.position - damage.AttackerPosition).normalized.x;
                
                Instantiate(bigLance, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }
    }
}
