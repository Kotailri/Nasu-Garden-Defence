using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    public GameObject AttackPrefab;
    [Tooltip("Is the attack attached to the player")]
    public bool IsAttached = false;
    public Color AttackColor = Color.white;
    public int BaseDamage = 1;
    public float KnockbackAmount = 0f;
    public float KnockbackTime = 0f;
    public PlayerKnockbackType KnockbackType;

    [Tooltip("Damage Multiplier from INTERNAL Sources")]
    public float DamageMultiplier = 1f;

    [Space(10f)]
    public int AttackLevel = 1;

    [Space(5f)]
    [Header("Attack Timer")]
    public bool IsOnAttackTimer;
    [Range(1, 10)]
    [Tooltip("Attacks every X number of basic attacks")]
    public int  AttackCount;
    public bool OnOffset = false;

    [Space(10f)]
    public AudioEnum AttackSound = AudioEnum.None;

    private void OnValidate()
    {
        if (AttackCount <= 0)
            AttackCount = 1;
    }

    public void SetFromScriptable(PlayerAttackScriptable atk)
    {
        AttackPrefab = atk.AttackPrefab;
        AttackColor = atk.AttackColor;
        BaseDamage = atk.BaseDamage;
        DamageMultiplier = atk.DamageMultiplier;

        IsOnAttackTimer = atk.IsOnAttackTimer;
        AttackCount = atk.AttackCount;
    }

    public void SetAttackPrefab(GameObject prefab)
    {
        AttackPrefab = prefab;
    }

    public void SetAttackColour(Color col)
    {
        AttackColor = col;
    }

    public void SetBaseDamage(int damage, bool isRelative=false)
    {
        BaseDamage = damage;
    }

    public void SetDamageMultiplier(float damageMultiplier, bool isRelative=false)
    {
        if (isRelative)
        {
            DamageMultiplier += damageMultiplier;
        }
        else
        {
            DamageMultiplier = damageMultiplier;
        }
    }

    public void IncrementLevel(int level)
    {
        AttackLevel += level;
    }

    public void SetAttackTiming(bool IsOnTimer,  int AttackTiming)
    {
        IsOnAttackTimer = IsOnTimer;
        AttackCount = AttackTiming;
    }

    public int GetDamage()
    {
        return Mathf.RoundToInt(BaseDamage * DamageMultiplier * GlobalStats.CurrentPlayerDamageMultiplier);
    }

    public abstract void DoAttack(Vector2 attackPosition, Transform attachObject=null);
}

[CreateAssetMenu(fileName = "PlayerAttack")]
public class PlayerAttackScriptable : ScriptableObject
{
    public GameObject AttackPrefab;
    public bool IsPetFacingRequired = false;
    public Color AttackColor = Color.white;
    public int BaseDamage = 1;
    public float DamageMultiplier = 1f;

    [Header("On Attack Timer")]
    public bool IsOnAttackTimer;
    [Range(1, 10)]
    public int AttackCount;
}