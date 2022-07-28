using UnityEngine;

namespace Script.Core
{
    [CreateAssetMenu(menuName = "ScriptableObject/SaveData")]
    public class SaveData : ScriptableObject
    {
        public Vector3 position;
        
        [Header("[Status]")]
        public float health;
        public float MaxHealth;
        public float MaxMana;
        public float mana;

        [Header("[Abilitys]")]
        public bool HasDoubleJump;
        public bool HasDash;

        [Header("[Game state save]")]
        public bool CollectDoubleJump;
        public bool CollectDash;

        public void ResetGameState()
        {
            CollectDoubleJump = false;
            CollectDash = false;
        }
    }
}
