using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    public class Sword : Weapon
    {
        public Sword(float damage, PlayerBase playerBase) : base(damage, playerBase)
        {
            Maxcombo = 3;
            KnockBackValue = 0;
        }
        
        public override void Attack(int currentCombo,float attackDirection)
        {
            switch (currentCombo)
            {
                case 1:
                    playerBase.Animator.Play("Attack1");
                    break;
                case 2:
                    playerBase.Animator.Play("Attack2");
                    break;
                case 3:
                    playerBase.Animator.Play("Attack3");
                    playerBase.StartCoroutine(playerBase.IMove(10, 1, attackDirection));
                    break;
                default:
                    break;
            }
        }
    }
}
