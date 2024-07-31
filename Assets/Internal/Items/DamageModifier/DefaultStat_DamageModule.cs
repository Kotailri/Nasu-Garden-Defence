using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStat_DamageModule : MonoBehaviour, IDamageModule
{
    public int ProcessDamage(int damage, PlayerAttackType attackType)
    {
        switch (attackType)
        {
            case PlayerAttackType.Projectile:
                return Mathf.RoundToInt(damage * GlobalStats.GetStatValue(PlayerStatEnum.damage) * GlobalStats.GetStatValue(PlayerStatEnum.projectileDamage));

            case PlayerAttackType.Melee:
                return Mathf.RoundToInt(damage * GlobalStats.GetStatValue(PlayerStatEnum.damage) * GlobalStats.GetStatValue(PlayerStatEnum.meleeDamage));

            case PlayerAttackType.Explosion:
                return Mathf.RoundToInt(damage * GlobalStats.GetStatValue(PlayerStatEnum.damage) * GlobalStats.GetStatValue(PlayerStatEnum.explosionDamage));

            case PlayerAttackType.Neutral:
                return Mathf.RoundToInt(damage * GlobalStats.GetStatValue(PlayerStatEnum.damage));

        }
        return damage;
    }
}
