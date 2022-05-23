using UnityEngine;

namespace Script.Player
{
    public class HoldAttack : State
    {
        public HoldAttack(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
        }

        private bool finishAttack;
        private float waitAttackTimeCounter;
        private float waitAttackAnimation;

        public override void CheckSwitchState()
        {
            if (!finishAttack) return;

            //Ctx.Animator.Play("ReleastAttack");

            if (waitAttackTimeCounter > 0.3)
            {
                SwitchState(_factory.Grounded());
            }
        }

        public override void InitializeSubState()
        {
        }

        public override void OnStateEnter()
        {
            ResetPara();
            Ctx.rigidBody2D.velocity = Vector2.zero;
            //Ctx.Animator.Play("HoldAttack");
            Ctx.Attacking = true;
            Ctx.Animator.SetBool("Attacking", true);
        }

        public override void OnStateExit()
        {
            ResetPara();
            Ctx.Attacking = false;
            Ctx.rigidBody2D.velocity = Vector2.zero;
            Ctx.rigidBody2D.gravityScale = Ctx.SaveGravity;
            Ctx.Animator.SetBool("Attacking", false);
        }

        public override void OnStateFixedUpdate()
        {
        }

        public override void OnStateRun()
        {
            GroundCheck();
            DashCheck();
            Timer();
            CheckSwitchState();
        }
        private void GroundCheck()
        {
            if (!Ctx.IsGrounded)
            {
                SwitchState(_factory.Airborn());
            }
        }
        private void DashCheck()
        {
            if (Ctx.InputMapPress.Dash && Ctx.DashStates == DashState.Ready)
            {
                SwitchState(_factory.Dash());
            }
        }
        private void Timer()
        {
            if (waitAttackAnimation > 0.3)
            {
                finishAttack = true;
            }
            else
            {
                waitAttackAnimation += Time.deltaTime;
            }
            if (!finishAttack) return;
            waitAttackTimeCounter += Time.deltaTime;
        }
        private void ResetPara()
        {
            finishAttack = false;
            waitAttackTimeCounter = 0;
            waitAttackAnimation = 0;
        }
    }
}
