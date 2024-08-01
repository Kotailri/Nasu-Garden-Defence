using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStat_DamageModule : MonoBehaviour, IAttackModule
{
    public AttackModuleInfoContainer Process(AttackModuleInfoContainer info)
    {
        switch (info.AttackType)
        {
            case PlayerAttackType.Projectile:
                info.Damage = Mathf.RoundToInt(info.Damage * GlobalStats.GetStatValue(PlayerStatEnum.damage) * GlobalStats.GetStatValue(PlayerStatEnum.projectileDamage));
                break;

            case PlayerAttackType.Melee:
                info.Damage = Mathf.RoundToInt(info.Damage * GlobalStats.GetStatValue(PlayerStatEnum.damage) * GlobalStats.GetStatValue(PlayerStatEnum.meleeDamage));
                break;

            case PlayerAttackType.Explosion:
                info.Damage = Mathf.RoundToInt(info.Damage * GlobalStats.GetStatValue(PlayerStatEnum.damage) * GlobalStats.GetStatValue(PlayerStatEnum.explosionDamage));
                break;

            case PlayerAttackType.Neutral:
                info.Damage = Mathf.RoundToInt(info.Damage * GlobalStats.GetStatValue(PlayerStatEnum.damage));
                break;

        }
        return info;
    }
}
