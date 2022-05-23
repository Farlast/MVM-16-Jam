using System.Collections;
using UnityEngine;

namespace Script.Player
{
    public class WaterBall : MonoBehaviour
    {
        [SerializeField] float decayTime;
        [SerializeField] float healAmount;
        [SerializeField] GameObject decayEffect;
        IEnumerator decay;
       
        void Start()
        {
            decay = IDacay();
            StartCoroutine(decay);
        }

        public void ShapeBlend(AttackType type)
        {
            if(type == AttackType.Sword)
            {
                //Instantiate slash and destroy this
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
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
       
    }
}
