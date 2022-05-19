using UnityEngine;
namespace Script.Player
{
    public class Idle : State
    {
        
        public Idle(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
        }

        public override void CheckSwitchState()
        {
            if(Ctx.InputMapPress.MoveInputVector.x != 0)
            {
                SwitchState(_factory.Move());
            }
            else if (Ctx.TakeHit)
            {
                SwitchState(_factory.KnockBack());
            }
        }

        public override void OnStateEnter()
        {
            Ctx.SpeedAtTimeCurve = 0;
            Ctx.Currentspeed = Ctx.MoveVelocity.Evaluate(0);
            Ctx.rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public override void OnStateExit()
        {
            Ctx.rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public override void OnStateRun()
        {
            Ctx.FlipSprite();
            CheckSwitchState();
        }

        public override void InitializeSubState()
        {
        }

        public override void OnStateFixedUpdate()
        {

            if(!Ctx.IsGrounded && Ctx.InputMapPress.MoveInputVector.x == 0 && !Ctx.TakeHit)
            {
                Ctx.rigidBody2D.velocity = new Vector2(0f,Ctx.rigidBody2D.velocity.y);
            }
            else
            {
                Ctx.rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
       
    }
}