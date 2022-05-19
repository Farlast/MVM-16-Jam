using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Player
{
    public class WallSliding : State
    {
        public WallSliding(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
            _isRootState = true;
        }
        bool IsWallJumping;
        public override void CheckSwitchState()
        {
            if (IsWallJumping) return;

            if (!Ctx.IsWallAtFront && !Ctx.IsGrounded)
            {
                SwitchState(_factory.Airborn());
            }
            else if (!Ctx.IsWallAtFront && Ctx.IsGrounded)
            {
                SwitchState(_factory.Grounded());
            }
            else if (Ctx.InputMapPress.RawJumpInput)
            {
                Ctx.StartCoroutine(IWallJump());
            }
        }
        IEnumerator IDelay()
        {
            IsWallJumping = true;
            yield return Helpers.GetWait(0.1f);
            IsWallJumping = false;
        }
        IEnumerator IWallJump()
        {
            IsWallJumping = true;
            Ctx.Animator.SetTrigger("WallSliding");
            Ctx.Animator.SetBool("IsWall", false);
            var jumpDir = new Vector2((Ctx.InputMapPress.LatesDirection * -1) * 15 , 15);
            Ctx.rigidBody2D.AddForce(jumpDir, ForceMode2D.Impulse);

            Ctx.FlipSprite();
            yield return Helpers.GetWait(0.2f);
            IsWallJumping = false;
            SwitchState(_factory.Airborn());
        }
        public override void InitializeSubState()
        {

        }

        public override void OnStateEnter()
        {
            Ctx.Animator.SetBool("IsWall", true);
            Ctx.rigidBody2D.velocity = Vector2.zero;
            Ctx.rigidBody2D.gravityScale = 0;
            Ctx.StartCoroutine(IDelay());
        }

        public override void OnStateExit()
        {
            Ctx.Animator.SetBool("IsWall", false);
            IsWallJumping = false;
            Ctx.rigidBody2D.gravityScale = Ctx.SaveGravity;
        }

        public override void OnStateFixedUpdate()
        {
            if (IsWallJumping) return;
            Ctx.rigidBody2D.velocity = new Vector2(0, -2);
        }

        public override void OnStateRun()
        {
            CheckSwitchState();
            Ctx.FlipSprite();
        }
        
    }
}
