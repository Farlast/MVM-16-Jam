using System.Collections;
using UnityEngine;

namespace Script.Player
{
    public class KnockBack : State
    {
        private bool finishKnockback;
        public KnockBack(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {
            if (!finishKnockback) return;
            
            if (Ctx.Status.Health <= 0)
            {
                SwitchState(_factory.Die());
            }
            else if (Ctx.IsGrounded)
            {
                SwitchState(_factory.Grounded());
            }else
            {
                SwitchState(_factory.Airborn());
            }
            
        }

        public override void InitializeSubState()
        {
           
        }

        public override void OnStateEnter()
        {
            finishKnockback = false;
            Ctx.Animator.Play("Knockback");
        }
        public override void OnStateRun()
        {
            CheckSwitchState();
        }

        public override void OnStateExit()
        {
            Ctx.Animator.SetBool("Attacking", false);
        }

        public override void OnStateFixedUpdate()
        {

        }/*
        IEnumerator IKnockBack()
        {
            yield return new WaitForFixedUpdate();
            Ctx.rigidBody2D.velocity = Vector2.zero;
            Ctx.rigidBody2D.AddForce(Ctx.AttackerDirection, ForceMode2D.Impulse);
            yield return Helpers.GetWait(0.5f);
            finishKnockback = true;
        }*/
    }
}