using UnityEngine;

[CreateAssetMenu()]
public class PlayerStatScriptable : ScriptableObject
{
    [Header("Player Movement Speed")]
    public float MovespeedStatBase;
    public float MovespeedStatGrowth;
    public PlayerStatGrowthType MovespeedGrowthType;
    public string MovespeedStatName = "Movespeed";

    [Header("Dodge Chance")]
    public float DodgeStatBase;
    public float DodgeStatGrowth;
    public PlayerStatGrowthType DodgeGrowthType;
    public string DodgeStatName = "Dodge";

    [Header("Attack Speed")]
    public float AttackspeedStatBase;
    public float AttackspeedStatGrowth;
    public PlayerStatGrowthType AttackspeedGrowthType;
    public string AttackspeedStatName = "Attack Speed";

    [Header("Overall Damage")]
    public float DamageStatBase;
    public float DamageStatGrowth;
    public PlayerStatGrowthType DamageGrowthType;
    public string DamageStatName = "Overall Damage";

    [Header("Projectile Damage")]
    public float ProjectileDamageStatBase;
    public float ProjectileDamageStatGrowth;
    public PlayerStatGrowthType ProjectileDamageGrowthType;
    public string ProjectileDamageStatName = "Projectile Damage";

    [Header("Melee Damage")]
    public float MeleeDamageStatBase;
    public float MeleeDamageStatGrowth;
    public PlayerStatGrowthType MeleeDamageGrowthType;
    public string MeleeDamageStatName = "Melee Damage";

    [Header("Projectile Speed")]
    public float ProjectileSpeedStatBase;
    public float ProjectileSpeedStatGrowth;
    public PlayerStatGrowthType ProjectileSpeedGrowthType;
    public string ProjectileSpeedStatName = "Projectile Speed";
}