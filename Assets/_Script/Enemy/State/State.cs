using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Enemy
{
    public abstract class State : MonoBehaviour
    {
        protected Enemy Enemy;
        protected State CurrentState;

        public abstract void OnstateEnter();
        public abstract void OnStateRun();
        public abstract void OnSeateExit();
        public abstract void CheckSwicthState();

        public void SwicthState(State state)
        {
            if (Enemy.CurrentState == state) return;
        }

        private void Start()
        {
            if(TryGetComponent(out Enemy enemy))
            {
                Enemy = enemy;
            }
        }
    }
}
