using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMovementType
{
    TargetGarden,
    TargetPlayer
}

[CreateAssetMenu(menuName = "Enemy/Create Enemy")]
public class EnemyScriptable : ScriptableObject
{
    [Header("Health")]
    public int Health;
    public float Resistance;
    public float DodgeChance;
    public int HealthRegenPerSecond;

    [Header("Movement")]
    public float MovementSpeed;
    [Range(0f, 100f)]
    public float MovementSpeedVariance;
    public EnemyMovementType MovementTargetType;
}
