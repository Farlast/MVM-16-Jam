using UnityEngine;
using System.Collections;

namespace Script.Player
{
    public class Dash : State
    {
        [Header("[Dash]")]
        private float dashTimer;
        private float savedGravityScale;
        private float dashSpeed;
        private float dashDirection;
        IEnumerator updateDash;
        public Dash(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
        }

        public override void CheckSwitchState()
        {
            if (Ctx.DashStates == DashState.Cooldown && Ctx.IsGrounded)
            {
                SwitchState(_factory.Grounded());
            }
            else if (Ctx.DashStates == DashState.Cooldown &&  !Ctx.IsGrounded)
            {
                SwitchState(_factory.Airborn());
            }
            else if (Ctx.TakeHit)
            {
                if (updateDash != null) 
                { 
                    Ctx.StopCoroutine(updateDash);
                    Ctx.rigidBody2D.gravityScale = savedGravityScale;
                    Ctx.rigidBody2D.velocity = new Vector2(0, 0);
                    Ctx.DashStates = DashState.Ready;
                }
                SwitchState(_factory.KnockBack());
            }
        }

        public override void OnStateEnter()
        {
            //Ctx.SetIframe();
            Ctx.Animator.SetTrigger("Dashing");
            Ctx.Animator.SetBool("Dash",true);
            dashSpeed = Ctx.Status.DashSpeed;
            dashDirection = Ctx.InputMapPress.LatesDirection;
            savedGravityScale = Ctx.rigidBody2D.gravityScale;
            Ctx.rigidBody2D.gravityScale = 0;
            Ctx.DashStates = DashState.Dashing;
            Ctx.FlipSprite();
            updateDash = UpdateDash();
            Ctx.StartCoroutine(updateDash);
        }

        public override void OnStateExit()
        {
            //Ctx.ResetIframe();
            Ctx.FlipSprite();
            Ctx.Animator.SetBool("Dash", false);
        }

        public override void OnStateRun()
        {
            CheckSwitchState();          
        }

        public override void InitializeSubState()
        {
        }

        IEnumerator UpdateDash()
        {
            dashTimer = 0f;
            while (dashTimer <= Ctx.Status.MaxDashTime && !Ctx.IsWallAtFront)
            {
                dashSpeed -= dashSpeed * 5f * Time.fixedDeltaTime;
                Ctx.rigidBody2D.velocity = new Vector2(dashSpeed * dashDirection, 0);
                dashTimer += Time.deltaTime * 3;
                yield return new WaitForFixedUpdate();
            }
            Ctx.rigidBody2D.gravityScale = savedGravityScale;
            Ctx.rigidBody2D.velocity = new Vector2(0, 0);
            dashTimer = Ctx.Status.DashCooldown;
            Ctx.DashStates = DashState.Cooldown;
            yield return Helpers.GetWait(.5f);
            Ctx.DashStates = DashState.Ready;
        }

        public override void OnStateFixedUpdate()
        {
        }
    }
}