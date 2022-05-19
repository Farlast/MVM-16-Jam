using UnityEngine;

namespace Script.Player
{
    public class Jump : State
    {
        public Jump(PlayerBase ctx, StateFactory factory) : base(ctx, factory)
        {
        }
       
        int JumpCounter;
        public override void CheckSwitchState()
        {

        }

        public override void OnStateEnter()
        {
            JumpCounter += 1;
        }

        public override void OnStateExit()
        {
            JumpCounter = 0;
        }

        public override void OnStateRun()
        {

        }

        public override void InitializeSubState()
        {
           
        }
        private void DoJump()
        {
            if (IsCanDoubleJump())
            {
                Ctx.Animator.SetTrigger("Jump");
                JumpCounter += 1;
                Ctx.rigidBody2D.velocity = new Vector2(Ctx.rigidBody2D.velocity.x, Ctx.Status.JumpForce);
            }
        }
       
        private bool IsCanDoubleJump()
        {
            return Ctx.Status.HasDoubleJump && Ctx.InputMapPress.JumpBuffering && JumpCounter < 2;
        }
        public override void OnStateFixedUpdate()
        {
            DoJump();
            AddVelocity();
            UpdateMove();
        }
        void AddVelocity()
        {
            if (Ctx.rigidBody2D.velocity.y < 0)
            {
                Ctx.rigidBody2D.velocity += (Ctx.FallMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
            }
            else if (Ctx.rigidBody2D.velocity.y > 0 && !Ctx.InputMapPress.RawJumpInput)
            {
                Ctx.rigidBody2D.velocity += (Ctx.LowJumpMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
            }
        }
        private void UpdateMove()
        {
            Ctx.rigidBody2D.velocity = new Vector2(Ctx.InputMapPress.MoveInputVector.x * Ctx.Status.Speed, Ctx.rigidBody2D.velocity.y);
            Ctx.FlipSprite();
        }
    }
}