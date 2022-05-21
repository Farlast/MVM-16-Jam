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
        int comboCount;
        int maxCombo = 3;
        public override void CheckSwitchState()
        {
            
            if (!finishAttack) return;
            
            if (Ctx.InputMapPress.Attack && waitAttackTimeCounter < 0.3)
            {
                comboCount++;
                NextAttack();
                   
            }
            else if (waitAttackTimeCounter > 0.3 || comboCount > maxCombo)
            {
                SwitchState(_factory.Grounded());
            }
           
        }
        private void NextAttack()
        {
            ResetTimer();
            switch (comboCount)
            {
                case 1:
                    Ctx.Animator.Play("Attack1");
                    break;
                case 2:
                    Ctx.Animator.Play("Attack2");
                    break;
                case 3:
                    Ctx.Animator.Play("Attack3");
                    Ctx.StartCoroutine(ILastComBoCoolDown(0.5f));
                    Ctx.StartCoroutine(IMove(20f, 0.4f));
                    
                    break;
            }
        }
        public override void OnStateEnter()
        {
            comboCount++;

            Ctx.rigidBody2D.velocity = Vector2.zero;
            Ctx.Animator.SetBool("Attacking", true);
            Ctx.Attacking = true;

            NextAttack();
        }

       
        public override void OnStateExit()
        {
            ResetTimer();
            ResetAttackCombo();

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
        private void ResetAttackCombo()
        {
            comboCount = 0;
        }
        IEnumerator ILastComBoCoolDown(float time)
        {
            Ctx.CanAttack = false;
            yield return new WaitForSeconds(time);
            Ctx.CanAttack = true;
        }
        IEnumerator IMove(float dashSpeed,float time)
        {
            Debug.Log("move");
           
            var dashTimer = 0f;
            while (dashTimer <= time && !Ctx.combatManager.GetHit)
            {
                Debug.Log(dashTimer <= time);
                Ctx.rigidBody2D.velocity = new Vector2(dashSpeed * Ctx.InputMapPress.LatesDirection, 0);
                dashTimer += Time.deltaTime * 3;
                yield return new WaitForFixedUpdate();
            }
            
            yield return new WaitForFixedUpdate();
            Ctx.rigidBody2D.velocity = new Vector2(0, 0);
        }
    }
}