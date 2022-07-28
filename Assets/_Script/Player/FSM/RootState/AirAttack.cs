using UnityEngine;

namespace Script.Player
{
    public class AirAttack : State
    {
        public AirAttack(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
            InitializeSubState();
        }

        private bool finishAttack;
        private float waitAttackTimeCounter;
        private float waitAttackAnimation;

        public override void CheckSwitchState()
        {
            if (!finishAttack) return;
            Ctx.rigidBody2D.gravityScale = Ctx.SaveGravity;
            
            if (waitAttackTimeCounter < 0.3) return;

            SwitchState(_factory.Airborn());
        }

        public override void OnStateEnter()
        {
            ResetPara();
            switch (Ctx.combatManager.CurrentAttackType)
            {
                case DamageInfo.AttackType.Sword:
                    Ctx.Animator.Play("Attack3");
                    break;
                case DamageInfo.AttackType.Lance:
                    Ctx.Animator.Play("LanceAttack");
                    break;
            }
            Ctx.Attacking = true;
            Ctx.Animator.SetBool("Attacking", true);
            Ctx.rigidBody2D.velocity = Vector2.zero;
            Ctx.rigidBody2D.gravityScale = 0;
            Ctx.PlayAttackSound();
        }

        public override void OnStateExit()
        {
            ResetPara();
            Ctx.rigidBody2D.gravityScale = Ctx.SaveGravity;
            Ctx.Attacking = false;
            Ctx.Animator.SetBool("Attacking", false);
        }

        public override void OnStateRun()
        {
            GroundCheck();
            DashCheck();
            Timer();
            CheckSwitchState();
        }

        public override void InitializeSubState()
        {
        }

        public override void OnStateFixedUpdate()
        {

        }
        private void DashCheck()
        {
            if (Ctx.InputMapPress.Dash && Ctx.DashStates == DashState.Ready)
            {
                SwitchState(_factory.Dash());
            }
        }

        private void GroundCheck()
        {
            if (Ctx.IsGrounded)
            {
                SwitchState(_factory.Grounded());
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
