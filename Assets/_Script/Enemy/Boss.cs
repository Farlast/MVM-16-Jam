using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Enemy
{
    public class Boss : Enemy
    {
        [SerializeField] VoidEventChannel endGameEvent;
        public override void OnDead()
        {
            endGameEvent?.RiseEvent();
            base.OnDead();
        }
    }
}
