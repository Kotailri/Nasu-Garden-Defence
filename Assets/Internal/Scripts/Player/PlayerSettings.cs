using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public PlayerScriptable settings;

    private void Awake()
    {
        PlayerScriptableSettings.PlayerMovespeed = settings.PlayerMovespeed;
        
        PlayerScriptableSettings.ShootTimer = settings.ShootTimer;
        PlayerScriptableSettings.ProjectileDamage = settings.ProjectileDamage;
        PlayerScriptableSettings.ProjectileSpeed = settings.ProjectileSpeed;

        PlayerScriptableSettings.ProjectilePrefab = settings.ProjectilePrefab;

      
        PlayerScriptableSettings.InvincibilityDuration = settings.InvincibilityDuration;
        PlayerScriptableSettings.PlayerDamageAmp = settings.PlayerDamageAmp;
}
}

