using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    public class WaterBall : MonoBehaviour
    {
        [SerializeField] float decayTime;
        [SerializeField] float healAmount;
        IEnumerator decay;
        void Start()
        {
            decay = IDacay();
            StartCoroutine(decay);
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
            Destroy(gameObject);
        }
    }
}
