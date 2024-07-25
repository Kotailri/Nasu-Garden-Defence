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
    public float MovementSpeed;
    public BasicEnemyMovementType MovementTargetType;

    [Header("Combat")]
    public int ContactDamage;
    public int GardenContactDamage;
}
