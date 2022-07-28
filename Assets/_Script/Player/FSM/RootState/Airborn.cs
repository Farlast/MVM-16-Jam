using UnityEngine;

namespace Script.Player
{
    public class Airborn : State
    {
        public Airborn(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
        }
        bool velocitydown;
        public override void CheckSwitchState()
        {
            if (Ctx.IsGrounded)
            {
                SwitchState(_factory.Grounded());
            }
            else if (Ctx.TakeHit)
            {
                SwitchState(_factory.KnockBack());
            }
            else if (Ctx.Status.HasDash && Ctx.InputMapPress.Dash && Ctx.DashStates == DashState.Ready)
            {
                SwitchState(_factory.Dash());
            }
            else if(Ctx.InputMapPress.RawAttackInput && !Ctx.Attacking)
            {
                SwitchState(_factory.AirAttack());
            }
            else if(Ctx.IsWallAtFront && Ctx.Status.HasWallJump)
            {
                SwitchState(_factory.WallSliding());
            }
            else if (Ctx.InputMapPress.SkillFirst && Ctx.combatManager.CanUseSkill())
            {
                SwitchState(_factory.CastSkill());
            }
        }

        public override void InitializeSubState()
        {
             SetSubState(_factory.Jump());
        }

        public override void OnStateEnter()
        {
            InitializeSubState();
        }

        public override void OnStateExit()
        {
            Ctx.Animator.SetBool("Fall", false);
        }

        public override void OnStateFixedUpdate()
        {
        }

        public override void OnStateRun()
        {
            velocitydown = Ctx.rigidBody2D.velocity.y < 0;
            Ctx.Animator.SetBool("Fall", velocitydown);
            CheckSwitchState();
        }
    }
}
