using UnityEngine;
using UnityEngine.InputSystem;
using Script.Core;
using System.Collections;
namespace Script.Player
{
    public class InputMapping : MonoBehaviour
    {
        [SerializeField] PlayerStatus playerStatus;
        [Header("Input value")]
        [SerializeField] private Vector2 moveInputVector;
        [SerializeField] private bool attackInput;
        [SerializeField] private bool attack;
        [SerializeField] private bool holdAttack;
        [SerializeField] private bool jumpInput;
        [SerializeField] private bool jump;
        [SerializeField] private bool holdJump;
        [SerializeField] private float minHoldJumpTime;
        [SerializeField] private bool dash;
        [SerializeField] private bool jumpBuffering;
        [SerializeField] private float minHoldAttackTime;
        [SerializeField] private float jumpBufferingTime = 0.4f;
        [SerializeField] private bool skill1;
        [SerializeField] private bool skill2;
        [SerializeField] private bool swicthWeapon;
        [field:SerializeField] public int LatesDirection { get; private set; }

        private bool startJob;
        private bool startAttackCount;
        private float counter = 0;
        private float counter2 = 0;
        private bool gamePlayState;

        public Vector2 MoveInputVector { get => moveInputVector; private set => moveInputVector = value; }
        public bool Attack { get => attack; set => attack = value; }
        public bool Jump { get => jump; }
        public bool Dash { get => dash; }
        public bool HoldJump { get => holdJump; }
        public bool RawJumpInput { get => jumpInput; }
        public bool JumpBuffering { get => jumpBuffering; }
        public float JumpBufferingTimeCounter { get; set; } = 0f;
        public bool HoldAttack { get => holdAttack; private set => holdAttack = value; }
        public bool RawAttackInput { get => attackInput; private set => attackInput = value; }
        public bool SkillFirst { get => skill1; private set => skill1 = value; }
        public bool SkillSecond { get => skill2; private set => skill2 = value; }
        public bool SwicthWeapon { get => swicthWeapon; private set => swicthWeapon = value; }

        #region OnInput
        public void OnMove(InputValue value)
        {
            if (!gamePlayState) return;
            moveInputVector = value.Get<Vector2>();
            moveInputVector.x = Mathf.Round(moveInputVector.x);
            moveInputVector.y = Mathf.Round(moveInputVector.y);
            playerStatus.MoveInput = moveInputVector.x;
            LatesDirection = moveInputVector.x != 0 ? (int)moveInputVector.x : LatesDirection;
        }
        public void WallJumpDirFlip()
        {
            LatesDirection *= -1;
        }
        public void OnJump(InputValue value)
        {
            if (!gamePlayState) return;
            float _buttonJumpValue = value.Get<float>();
            JumpInput(_buttonJumpValue);
        }

        public void OnDash(InputValue value)
        {
            if (!gamePlayState) return;
            float btnAttack = value.Get<float>();
            DashInput(btnAttack);
        }

        public void OnAttack(InputValue value)
        {
            if (!gamePlayState) return;
            float btnAttack = value.Get<float>();
            AttackInput(btnAttack);
        }
        public void OnSkill1(InputValue value)
        {
            if (!gamePlayState) return;
            float btnValue = value.Get<float>();
            Skill1Input(btnValue);
        }
        public void OnSkill2(InputValue value)
        {
            if (!gamePlayState) return;
            float btnValue = value.Get<float>();
            Skill2Input(btnValue);
        }
        public void OnSwitchWeapon(InputValue value)
        {
            if (!gamePlayState) return;
            float btnValue = value.Get<float>();
            SwitchWeaponInput(btnValue);
        }
        #endregion

        #region Setting bool Input
        void AttackInput(float pressValue)
        {
            // 1 = press ,0 = releass
            attackInput = pressValue == 1 ? true : false;
            startAttackCount = true;

            if (attackInput) return;

            if (counter2 <= minHoldAttackTime)
            {
                attack = true;
            }
            HoldAttack = false;
            counter2 = 0;
            startAttackCount = false;

            StartCoroutine(IDelay(0.2f));
        }
        void Skill1Input(float pressValue)
        {
            // 1 = press ,0 = releass
            SkillFirst = pressValue == 1 ? true : false;
        }
        void Skill2Input(float pressValue)
        {
            // 1 = press ,0 = releass
            SkillSecond = pressValue == 1 ? true : false;
        }
        void SwitchWeaponInput(float pressValue)
        {
            // 1 = press ,0 = releass
            swicthWeapon = pressValue == 1 ? true : false;
        }
        void JumpInput(float pressValue)
        {
            // 1 = press ,0 = releass
            jumpInput = pressValue == 1 ? true : false;
            jump = jumpInput;
            startJob = true;

            if (!jumpInput)
            {
                holdJump = false;
                counter = 0;
                startJob = false;
            }
            else
            {
                JumpBufferingTimeCounter = jumpBufferingTime;
            }
        }
        void DashInput(float pressValue)
        {
            dash = pressValue == 1 ? true : false;
        }
        #endregion

        void JumpTimer()
        {
            if (!startJob) return;

            if (jumpInput && counter < minHoldJumpTime)
            {
                counter += 1f * Time.deltaTime;
            }
            if (counter >= minHoldJumpTime)
            {
                jump = false;
                holdJump = true;
            }
        }
        void JumpBuffer()
        {
            JumpBufferingTimeCounter -= Time.deltaTime;

            if (JumpBufferingTimeCounter > 0f)
            {
                jumpBuffering = true;
            }
            else
            {
                jumpBuffering = false;
            }
        }
        void AttackTimer()
        {
            if (!startAttackCount) return;

            if (attackInput && counter2 < minHoldAttackTime)
            {
                counter2 += 1f * Time.deltaTime;
            }
            if (counter2 >= minHoldAttackTime)
            {
                attack = false;
                HoldAttack = true;
            }
        }
        IEnumerator IDelay(float DelaySec)
        {
            yield return Helpers.GetWait(DelaySec);
            attack = false;
        }
        private void Start()
        {
            startAttackCount = startJob = false;
            gamePlayState = true;
            //GameStateManager.Instance.onGameStateChange += OnGameStateChange;
        }
        private void Update()
        {
            JumpTimer();
            JumpBuffer();
            AttackTimer();
        }
        private void OnDestroy()
        {
            //GameStateManager.Instance.onGameStateChange -= OnGameStateChange;
        }
        public void OnGameStateChange(GameStates newGameStates)
        {
            gamePlayState = enabled = newGameStates == GameStates.GamePlay;
        }
        public PlayerDirection GetPlayerDirectionName()
        {
            return LatesDirection switch
            {
                -1 => PlayerDirection.Left,
                1 => PlayerDirection.Rigth,
                _ => PlayerDirection.Idel,
            };
        }
        public enum PlayerDirection
        {
            Idel = 0,
            Left = -1,
            Rigth = 1
        }
    }
}
