using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApexStride_DamageModule : MonoBehaviour, IAttackModule
{
    public AttackModuleInfoContainer Process(AttackModuleInfoContainer info)
    {
        info.Damage = Mathf.RoundToInt(info.Damage * Global.keystoneItemManager.ApexStrideDamageMultiplier);
        return info;
    }
}
