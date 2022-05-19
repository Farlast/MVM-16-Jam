using UnityEngine;
namespace Script.Player
{
    class Move : State
    {       
        public Move(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
        }

        public override void CheckSwitchState()
        {
            if(Ctx.InputMapPress.MoveInputVector.x == 0)
            {
                SwitchState(_factory.Idle());
            }
        }

        public override void OnStateEnter()
        {
            Ctx.Animator.SetBool("Run", true);
        }
        public override void OnStateRun()
        {
            Ctx.FlipSprite();
            CheckSwitchState();
        }

        public override void OnStateExit()
        {
            Ctx.Animator.SetBool("Run", false);
        }

        public override void InitializeSubState()
        {

        }
        private void UpdateMove()
        {

            Ctx.SpeedAtTimeCurve += Time.deltaTime;
            Ctx.Currentspeed = Ctx.MoveVelocity.Evaluate(Ctx.SpeedAtTimeCurve);
            Ctx.Currentspeed *= Ctx.Status.Speed;
            Ctx.rigidBody2D.velocity = new Vector2(Ctx.InputMapPress.MoveInputVector.x * (Ctx.Currentspeed), Ctx.rigidBody2D.velocity.y);

        }

        public override void OnStateFixedUpdate()
        {
            UpdateMove();
        }
    }
}
