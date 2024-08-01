using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritStat_DamageModule : MonoBehaviour, IAttackModule
{
    public float CritDamageMultiplier;
    private float CritPityMultiplier = 20;
    private int HitsWithoutCrit = 0;

    public AttackModuleInfoContainer Process(AttackModuleInfoContainer info)
    {

        if (MathUtil.RollChance(GlobalStats.GetStatValue(PlayerStatEnum.critchance)) || 
            (HitsWithoutCrit >= Mathf.FloorToInt((1 - GlobalStats.GetStatValue(PlayerStatEnum.critchance)) * CritPityMultiplier)
            && GlobalStats.GetStatValue(PlayerStatEnum.critchance) > 0))
        {
            info.Damage = Mathf.RoundToInt((float)info.Damage * CritDamageMultiplier);
            info.IsCrit = true;
            HitsWithoutCrit = 0;
        }
        else
        {
            if (GlobalStats.GetStatValue(PlayerStatEnum.critchance) > 0)
                HitsWithoutCrit++;
        }

        return info;
    }
}
