using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    public class Die : State
    {
        public Die(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {

        }

        public override void InitializeSubState()
        {

        }

        public override void OnStateEnter()
        {
            Ctx.rigidBody2D.velocity = Vector2.zero;
            Ctx.Animator.SetTrigger("Dead");
        }
        public override void OnStateRun()
        {
        }

        public override void OnStateExit()
        {
        }
       

        public override void OnStateFixedUpdate()
        {
        }
    }
}