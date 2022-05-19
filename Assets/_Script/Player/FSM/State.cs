
namespace Script.Player
{
    public abstract class State
    {
        protected bool _isRootState = false;
        private PlayerBase _ctx;
        protected StateFactory _factory;
        protected State _currentSubstate;
        protected State _currentSuperstate;

        public string CurrentSuper => _currentSuperstate?.GetType().Name;
        public string CurrentSubName => _currentSubstate?.GetType().Name;
        public State CurrentSub => _currentSubstate;

        protected PlayerBase Ctx { get => _ctx; set => _ctx = value; }

        protected State(PlayerBase ctx, StateFactory factory)
        {
            _ctx = ctx;
            _factory = factory;
        }

        public abstract void OnStateEnter();
        public abstract void OnStateRun();
        public abstract void OnStateFixedUpdate();
        public abstract void OnStateExit();
        public abstract void CheckSwitchState();
        protected void SwitchState(State newState) {
           
            if (newState == Ctx.CurrentState) return;

            if (_isRootState)
            {
                //Exit current state and subStates
                ExitStates();
                //set to new state
                newState.OnStateEnter();
                Ctx.CurrentState = newState;
            }
            else if(_currentSuperstate != null)
            {
                //Exit current state
                ExitStates();
                //set to new state
                _currentSuperstate.SetSubState(newState);
            }
        }
        public abstract void InitializeSubState();
        public void UpdateStates() {
            OnStateRun();
            if(_currentSubstate != null)
            {
                _currentSubstate.UpdateStates();
            }
        }
        public void FixedUpdateStates()
        {
            OnStateFixedUpdate();
            if (_currentSubstate != null)
            {
                _currentSubstate.FixedUpdateStates();
            }
        }

        public void ExitStates()
        {
            OnStateExit();
            if (_currentSubstate != null)
            {
                _currentSubstate.ExitStates();
            }
        }
        public void EnterStates()
        {
            OnStateEnter();
            if (_currentSubstate != null)
            {
                _currentSubstate.EnterStates();
            }
        }
        protected void SetSuperState(State newSuperState) {
           _currentSuperstate = newSuperState;
        }
        protected void SetSubState(State newSubState) {
            _currentSubstate = newSubState;
            newSubState.SetSuperState(this);
            newSubState.OnStateEnter();
        }

    }
}
