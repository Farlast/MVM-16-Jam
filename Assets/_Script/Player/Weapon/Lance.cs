using UnityEngine;

namespace Script.Player
{
    public class Lance : Weapon
    {
        public Lance(float damage, PlayerBase playerBase) : base(damage, playerBase)
        {
            Maxcombo = 3;
            KnockBackValue = 10;
        }

        public override void Attack(int currentCombo, float attackDirection)
        {
            switch (currentCombo)
            {
                case 1:
                    playerBase.Animator.Play("LanceAttack");
                    break;
                case 2:
                    playerBase.Animator.Play("LanceAttack");
                    break;
                case 3:
                    playerBase.Animator.Play("LanceAttack");
                    break;
                default:
                    break;
            }
        }
    }
}
