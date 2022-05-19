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
            Ctx.Animator.SetTrigger("Dead");
            Ctx.gameObject.layer = LayerMask.NameToLayer("PlayerGhost");
        }
        public override void OnStateRun()
        {
        }

        public override void OnStateExit()
        {
            Ctx.gameObject.layer = LayerMask.NameToLayer("Player");
        }
       

        public override void OnStateFixedUpdate()
        {
        }
    }
}