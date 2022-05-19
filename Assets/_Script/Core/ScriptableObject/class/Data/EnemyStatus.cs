using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Status/EnemyStatus")]
public class EnemyStatus : ScriptableObject
{
    public string monsterName;
    [Header("[Status]")]
    public float maxHealth;
    public float maxPoised;
    
    [Header("[Movement]")]
    public float moveSpeed;
    public float jumpForce;
    
    [Header("[Attack]")]
    public float attackSpeed;
    public float attackDamage;
    public float knockBackValue;
    
    [Header("[Resistans]")]
    public float physicRes;
    public float fireRes;
    public float iceRes;
    public float poisonRes;

    public GameObject AttackedEffect;
    public GameObject Prefab;

    [SerializeField] private Sound hitedSound;
    public Sound GetHitedSounds() => hitedSound;
}
