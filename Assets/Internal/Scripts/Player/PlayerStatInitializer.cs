using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatInitializer : MonoBehaviour
{
    public PlayerStatScriptable stats;

    private void Awake()
    {
        GlobalPlayer.MovespeedStat = PlayerStatFactory.CreatePlayerStat(stats.MovespeedStatName, stats.MovespeedStatBase, stats.MovespeedStatGrowth, stats.MovespeedGrowthType);
        GlobalPlayer.DodgeStat = PlayerStatFactory.CreatePlayerStat(stats.DodgeStatName, stats.DodgeStatBase, stats.DodgeStatGrowth, stats.DodgeGrowthType);
        GlobalPlayer.AttackspeedStat = PlayerStatFactory.CreatePlayerStat(stats.AttackspeedStatName, stats.AttackspeedStatBase, stats.AttackspeedStatGrowth, stats.AttackspeedGrowthType);
        GlobalPlayer.DamageStat = PlayerStatFactory.CreatePlayerStat(stats.DamageStatName, stats.DamageStatBase, stats.DamageStatGrowth, stats.DamageGrowthType);
        GlobalPlayer.ProjectileDamageStat = PlayerStatFactory.CreatePlayerStat(stats.ProjectileDamageStatName, stats.ProjectileDamageStatBase, stats.ProjectileDamageStatGrowth, stats.ProjectileDamageGrowthType);
        GlobalPlayer.MeleeDamageStat = PlayerStatFactory.CreatePlayerStat(stats.MeleeDamageStatName, stats.MeleeDamageStatBase, stats.MeleeDamageStatGrowth, stats.MeleeDamageGrowthType);
        GlobalPlayer.ProjectileSpeedStat = PlayerStatFactory.CreatePlayerStat(stats.ProjectileSpeedStatName, stats.ProjectileSpeedStatBase, stats.ProjectileSpeedStatGrowth, stats.ProjectileSpeedGrowthType);
        
    }
}
