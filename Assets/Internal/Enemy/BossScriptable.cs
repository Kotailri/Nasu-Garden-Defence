using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Create Boss")]
public class BossScriptable : ScriptableObject
{
    [Header("Info")]
    public string BossName;

    [Header("Health")]
    public int Health;
    public float Resistance;
    public float DodgeChance;
    public int HealthRegen;

    [Header("Movement")]
    public float MovementSpeed;

    [Header("Combat")]
    public int ContactDamage;
    public int GardenContactDamage;
}
