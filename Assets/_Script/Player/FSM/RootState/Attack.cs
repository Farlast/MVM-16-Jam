using UnityEngine;
using System.Collections;

namespace Script.Player
{
    public class Attack : State
    {
        public Attack(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
            InitializeSubState();
        }

        private bool finishAttack;
        private float waitAttackTimeCounter;
        private float waitAttackAnimation;
        
        float FaceDirection;
        public override void CheckSwitchState()
        {
            
            if (!finishAttack) return;
            
            if (Ctx.InputMapPress.AttackBuffering && waitAttackTimeCounter < 0.3)
            {
                NextAttack();
            }
            else if (waitAttackTimeCounter > 0.3 || Ctx.combatManager.CurrentMaxCombo())
            {
                Ctx.StartCoroutine(ILastComboCoolDown(0.3f));
                SwitchState(_factory.Grounded());
            }
           
        }
        private void NextAttack()
        {
            ResetTimer();
            Ctx.combatManager.AttackBycombo(FaceDirection);
        }

        public override void OnStateEnter()
        {
            Ctx.rigidBody2D.velocity = Vector2.zero;
            Ctx.Animator.SetBool("Attacking", true);
            Ctx.Attacking = true;
            FaceDirection = Ctx.InputMapPress.LatesDirection;
            Ctx.combatManager.ResetAttackCombo();
            NextAttack();
        }

       
        public override void OnStateExit()
        {
            ResetTimer();
            Ctx.combatManager.ResetAttackCombo();
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
            if(waitAttackAnimation > 0.2)
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
        private void ResetTimer()
        {
            finishAttack = false;
            waitAttackTimeCounter = 0;
            waitAttackAnimation = 0;
        }
    
        IEnumerator ILastComboCoolDown(float time)
        {
            Ctx.CanAttack = false;
            yield return new WaitForSeconds(time);
            Ctx.CanAttack = true;
        }
    }
}