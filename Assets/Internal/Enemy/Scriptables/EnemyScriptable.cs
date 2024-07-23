using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Enemy/Create Enemy")]
public class EnemyScriptable : ScriptableObject
{
    [Header("Info")]
    public string EnemyName;

    [Header("Health")]
    public int Health;
    public float Resistance;
    public float DodgeChance;
    public float HealthRegen;

    [Header("Movement")]
    [Range(0f, 100f)]
    public float MovementSpeedMin;
    [Range(0f, 100f)]
    public float MovementSpeedMax;
    public float MovementSpeed;
    public BasicEnemyMovementType MovementTargetType;

    [Header("Combat")]
    public int ContactDamage;
    public int GardenContactDamage;
}
