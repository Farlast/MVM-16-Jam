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
            KnockBackValue = 5;
        }
        
        public override void Attack(int currentCombo,float attackDirection)
        {
            switch (currentCombo)
            {
                case 1:
                    playerBase.Animator.Play("Attack1");
                    KnockBackValue = 5;
                    break;
                case 2:
                    playerBase.Animator.Play("Attack2");
                    KnockBackValue = 5;
                    break;
                case 3:
                    playerBase.Animator.Play("Attack3");
                    KnockBackValue = 10;
                    //playerBase.StartCoroutine(playerBase.IMove(10, 1, attackDirection));
                    break;
                default:
                    break;
            }
        }
    }
}
