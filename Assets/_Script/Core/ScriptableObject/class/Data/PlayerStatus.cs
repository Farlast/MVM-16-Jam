using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Status/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    [Header("[Status]")]
    public float health;
    public float MaxHealth;
    public float MaxMana;
    public float mana;
    
    [Header("[Movement]")]
    public float Speed;
    public float JumpForce;
    public float MoveInput;
    
    [Header("[Abilitys]")]
    public bool HasDoubleJump;
    public bool HasDash;
    public bool HasWallJump;
    
    [Header("[Dash]")]
    public float DashSpeed;
    public float MaxDashTime;
    public float DashCooldown;

    public float Health { get => Mathf.Clamp(health,0,MaxHealth); set => health = value;}
    public float Mana { get => Mathf.Clamp(mana, 0, MaxMana); set => Mathf.Clamp(mana = value, 0, MaxMana); }

}
