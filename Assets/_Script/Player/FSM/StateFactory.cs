using System.Collections.Generic;
namespace Script.Player
{
    public class StateFactory
    {
        PlayerBase _context;
        enum States
        {
            Idle,
            Move,
            Jump,
            Grounded,
            Dash,
            Die,
            KnockBack,
            Attack,
            AirAttack,
            HoldAttack,
            Airborn,
            CastSkill,
            WallSliding
        }

        public StateFactory(PlayerBase context)
        {
            _context = context;
            _stateDictionary.Clear();
            _stateDictionary.Add(States.Idle, new Idle(_context, this));
            _stateDictionary.Add(States.Move, new Move(_context, this));
            _stateDictionary.Add(States.Grounded, new Grounded(_context, this));
            _stateDictionary.Add(States.Dash, new Dash(_context, this));
            _stateDictionary.Add(States.Die, new Die(_context, this));
            _stateDictionary.Add(States.KnockBack, new KnockBack(_context, this));
            _stateDictionary.Add(States.Attack, new Attack(_context, this));
            _stateDictionary.Add(States.AirAttack, new AirAttack(_context, this));
            _stateDictionary.Add(States.HoldAttack, new HoldAttack(_context, this));
            _stateDictionary.Add(States.Jump, new Jump(_context, this));
            _stateDictionary.Add(States.Airborn, new Airborn(_context, this));
            _stateDictionary.Add(States.CastSkill, new CastSkill(_context, this));
            _stateDictionary.Add(States.WallSliding, new WallSliding(_context, this));
        }

        private static readonly Dictionary<States, State> _stateDictionary = new Dictionary<States, State>();
        
        public State Idle() => GetState(States.Idle);
        public State Move() => GetState(States.Move);
        public State Jump() => GetState(States.Jump);
        public State Grounded() => GetState(States.Grounded);
        public State Dash() => GetState(States.Dash);
        public State Die() => GetState(States.Die);
        public State KnockBack() => GetState(States.KnockBack);
        public State Attack() => GetState(States.Attack);
        public State AirAttack() => GetState(States.AirAttack);
        public State Airborn() => GetState(States.Airborn);
        public State HoldAttack() => GetState(States.HoldAttack);
        public State CastSkill() => GetState(States.CastSkill);
        public State WallSliding() => GetState(States.WallSliding);
        private State GetState(States stateName)
        {
            if (_stateDictionary.TryGetValue(stateName, out var state)) return state;
            
            return null;
        }
    }

}
