using Script.Core;
using UnityEngine;

namespace Script.Player
{
    public enum DashState { Ready, Dashing, Cooldown }
    
    public class PlayerBase : MonoBehaviour
    {
        [Space]
        [Header("[Dependency]")]
        [SerializeField] private InputMapping inputMapping;
        [SerializeField] public CombatManager combatManager;

        internal State CurrentState;
        private StateFactory factory;

        internal Rigidbody2D rigidBody2D;
        internal Animator Animator;
        internal bool Attacking;
        internal float Currentspeed;
        internal float SpeedAtTimeCurve;

        [Space]
        [Header("[Information]")]
        public string stateDisplay;
        public string subStateDisplay;
        public Vector2 Velocity;

        [SerializeField] internal DashState DashStates;
        [SerializeField] internal AnimationCurve MoveVelocity;
        
        [field: SerializeField] internal PlayerStatus Status { get; set; }
        [field: SerializeField] internal bool IsGrounded { get; private set; }
        [field: SerializeField] internal bool IsWallAtFront { get; private set; }
        [field: SerializeField] internal bool CanAttack { get; set; }
        [field: SerializeField] internal bool IsWallSliding { get; set; }

        [Space]
        [Header("[Jump]")]
        private float coyoteTimer = 0;
        internal float SaveGravity { get; private set; }
        [field: SerializeField] internal float FallMultiplier { get; private set; }
        [field: SerializeField] internal float LowJumpMultiplier { get; private set; }
        [field: SerializeField] internal float CoyoteThreshold { get; private set; }
        [field: SerializeField] internal float MaxFallSpeed { get; private set; }

        [Space]
        [Header("[Collision]")]
        [SerializeField] private Transform feetPos;
        [SerializeField] private float checkRadiusFeetX;
        [SerializeField] private float checkRadiusFeetY;
        [SerializeField] private float eyeRadiusCheck;
        [SerializeField] private LayerMask groundLayer;
        
        [SerializeField] public bool TakeHit  => combatManager.GetHit;

        private void Awake()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            inputMapping = GetComponent<InputMapping>();
            
            factory = new StateFactory(this);
            CurrentState = factory.Grounded();
            CurrentState.OnStateEnter();

            SaveGravity = rigidBody2D.gravityScale;
            CanAttack = true;
        }
        private void Start()
        {
            //if(GameStateManager.Instance != null)
            //GameStateManager.Instance.onGameStateChange += OnGameStateChange;
        }
        private void OnDestroy()
        {
            //if(GameStateManager.Instance != null)
            //GameStateManager.Instance.onGameStateChange -= OnGameStateChange;
        }
        public InputMapping InputMapPress => inputMapping;
        private void StateDisplay() => stateDisplay = CurrentState.GetType().Name;
        private void SubStateDisplay() => subStateDisplay = CurrentState.CurrentSubName;
        private bool GroundCheck() => IsGrounded = Physics2D.OverlapBox(feetPos.position,new Vector2(checkRadiusFeetX,checkRadiusFeetY),0f, groundLayer);    
        private bool WallCheck() => IsWallAtFront = Physics2D.Raycast(transform.position, new Vector2(inputMapping.LatesDirection, 0), eyeRadiusCheck, groundLayer);
       
        private void Update()
        {
            CurrentState.UpdateStates();

            GroundCheck();
            CoyoteyCheck();
            WallCheck();
            StateDisplay();
            SubStateDisplay();

            Animator.SetBool("IsGround",IsGrounded);
            Velocity = rigidBody2D.velocity;
            rigidBody2D.velocity = new Vector2(Velocity.x,Mathf.Clamp(Velocity.y, MaxFallSpeed, 20)); 
        }
       
        private void FixedUpdate()
        {
            CurrentState.FixedUpdateStates();
        }
       
        #region other
        public bool CoyoteyCheck()
        {
            if (!IsGrounded)
            {
                coyoteTimer += 1 * Time.deltaTime;
            }
            else
            {
                coyoteTimer = 0;
            }
            return coyoteTimer < CoyoteThreshold;
        }
        public void FlipSprite()
        {
            transform.eulerAngles = inputMapping.LatesDirection > 0 ? new Vector3(0, 0, 0): new Vector3(0, 180, 0);
        }
        
        public void OnGameStateChange(GameStates newGameStates)
        {
            enabled = newGameStates == GameStates.GamePlay;
        }

        #endregion
        #region Gizmos
        private void OnDrawGizmos()
        {
            float _latesDirection = inputMapping ? inputMapping.LatesDirection : 1;
            if (Status != null)
            {
                /// eye
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, transform.position + new Vector3(_latesDirection * eyeRadiusCheck, 0, 0));
            }

            if (feetPos != null)
            {
                // feet
                Gizmos.color = Color.cyan;
                Vector3 radius = new Vector3(checkRadiusFeetX,checkRadiusFeetY,0);
                Gizmos.DrawWireCube(feetPos.position, radius);
            }
        }
        #endregion
    }
}
