
using UnityEngine;

namespace Script.Enemy
{
    public class PatrolMove : State
    {
        public override void CheckSwicthState()
        {

        }

        public override void OnSeateExit()
        {

        }

        public override void OnstateEnter()
        {

        }

        public override void OnStateRun()
        {
            if (Enemy.TakeHit) {
                Enemy.Rb.velocity = Vector2.zero;
                return;
            }

            if (Enemy.IsWallAtFront() || Enemy.IsCliff())
            {
                Enemy.FaceDir *= -1;
                Enemy.FlipSprite(Enemy.FaceDir);
            }
            Enemy.Rb.velocity = new Vector2(Enemy.FaceDir * Enemy.MoveSpeed, Enemy.Rb.velocity.y);
        }
    }
}
