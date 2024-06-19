using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatePlayerSettings")]
public class PlayerScriptable : ScriptableObject
{
    [Header("Moving")]
    public float PlayerMovespeed;

    [Header("Shooting")]
    public float ShootTimer;
    public int  ProjectileDamage;
    public float ProjectileSpeed;

    [Space(5f)]
    public GameObject ProjectilePrefab;

    [Header("Combat")]
    public float InvincibilityDuration;
    [Range(1f, 1000f)]
    public float PlayerDamageAmp;

    private void OnValidate()
    {
        if (PlayerDamageAmp <= 0)
            PlayerDamageAmp = 1;
    }
}
