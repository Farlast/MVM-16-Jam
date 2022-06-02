using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Player;

namespace Script.Player
{
    public class SwitchWeapon : State
    {
        public SwitchWeapon(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
        }

        private float timeCounter = 0.5f;
        private bool countFinish;

        public override void CheckSwitchState()
        {
            if (!countFinish) return;

            SwitchState(_factory.Grounded());
        }

        public override void InitializeSubState()
        {
        }

        public override void OnStateEnter()
        {
            Ctx.rigidBody2D.velocity = Vector2.zero;
            Ctx.combatManager.SwicthAttackType();
            Ctx.Animator.Play("BlendSword");
           
            Ctx.StartCoroutine(TimeCount());
        }

        public override void OnStateExit()
        {
        }

        public override void OnStateFixedUpdate()
        {
        }

        public override void OnStateRun()
        {
            CheckSwitchState();
        }
        IEnumerator TimeCount()
        {
            countFinish = false;
            yield return Helpers.GetWait(timeCounter);
            countFinish = true;
        }
    }
}
