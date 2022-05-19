using UnityEngine;

namespace Script.Player
{
    public class Grounded : State
    {
        public Grounded(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
            InitializeSubState();
        }
        
        public override void CheckSwitchState()
        {
            if (!Ctx.IsGrounded)
            {
                SwitchState(_factory.Airborn());
            }
            else if (Ctx.InputMapPress.JumpBuffering)
            {
                Ctx.Animator.SetTrigger("Jump");
                Ctx.rigidBody2D.velocity = new Vector2(Ctx.rigidBody2D.velocity.x, Ctx.Status.JumpForce);
                Ctx.InputMapPress.JumpBufferingTimeCounter = 0f;
                SwitchState(_factory.Airborn());
            }
            else if (Ctx.TakeHit)
            {
                SwitchState(_factory.KnockBack());
            }
            else if (Ctx.InputMapPress.Dash && Ctx.DashStates == DashState.Ready)
            {
                SwitchState(_factory.Dash());
            }
            else if (Ctx.CanAttack && Ctx.InputMapPress.Attack && !Ctx.InputMapPress.HoldAttack)
            {
                SwitchState(_factory.Attack());
            }
            else if (Ctx.InputMapPress.HoldAttack)
            {
                SwitchState(_factory.HoldAttack());
            }
            else if (Ctx.InputMapPress.SkillFirst || Ctx.InputMapPress.SkillSecond)
            {
                SwitchState(_factory.CastSkill());
            }
            if(Ctx.Status.Health <= 0)
            {
                SwitchState(_factory.Die());
            }
        }

        public override void OnStateEnter()
        {
            InitializeSubState();
        }

        public override void OnStateExit()
        {
        }

        public override void OnStateRun()
        {
            CheckSwitchState();
        }

        public override void InitializeSubState()
        {
            if(Ctx.InputMapPress.MoveInputVector.x == 0)
            {
                SetSubState(_factory.Idle());
            }
            else
            {
                SetSubState(_factory.Move());
            }
        }

        public override void OnStateFixedUpdate()
        {
        }
        
    }
}