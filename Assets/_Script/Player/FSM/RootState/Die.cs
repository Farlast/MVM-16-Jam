using Script.Core;
using UnityEngine;
using System.Collections;

namespace Script.Player
{
    public class Die : State
    {
        private bool finishRepond;
        public Die(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {
            if(finishRepond)
                SwitchState(_factory.Grounded());
        }

        public override void InitializeSubState()
        {

        }

        public override void OnStateEnter()
        {
            Ctx.rigidBody2D.velocity = Vector2.zero;
            Ctx.Animator.SetTrigger("Dead");
            
            Ctx.StartCoroutine(Ondead());
            
        }
        public override void OnStateRun()
        {
            CheckSwitchState();
        }

        public override void OnStateExit()
        {
        }
       

        public override void OnStateFixedUpdate()
        {
        }
        IEnumerator Ondead()
        {
            finishRepond = false;
            Ctx.rigidBody2D.velocity = Vector2.zero;
            // fade set hp mp position
            SceneFadeControl.Instance.FadeIn();
            yield return Helpers.GetWait(1f);

            Ctx.transform.position = Ctx.Status.GetLastSavePosition();
            Ctx.Status.SetUp();
            
            SceneFadeControl.Instance.FadeOut();
            yield return Helpers.GetWait(1f);

            finishRepond = true;

        }
    }
}