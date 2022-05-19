using System.Collections;
using UnityEngine;

namespace Script.Player
{
    public class PlayerStatusManager : MonoBehaviour
    {
        [field:SerializeField] public PlayerStatus Status { get; set; }

        [SerializeField] Rigidbody2D rigidBody2D;
        HealthSystem healthSystem;
        
        private void Awake()
        {
            healthSystem = new HealthSystem(Status.MaxHealth);
        }
    }
}
