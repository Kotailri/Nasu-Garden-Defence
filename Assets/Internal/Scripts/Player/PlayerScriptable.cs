using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatePlayerSettings")]
public class PlayerScriptable : ScriptableObject
{
    public float PlayerMovespeed;
    public float BonusProjectileSpeed;
    public float InvincibilityDuration;
}
