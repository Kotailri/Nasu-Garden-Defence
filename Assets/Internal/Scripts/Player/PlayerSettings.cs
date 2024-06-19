using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public PlayerScriptable settings;

    void Start()
    {
        AdjustableStats.PlayerMovespeed = settings.PlayerMovespeed;
        AdjustableStats.BonusProjectileSpeed = settings.BonusProjectileSpeed;
        AdjustableStats.InvincibilityDuration = settings.InvincibilityDuration;
    }
}

