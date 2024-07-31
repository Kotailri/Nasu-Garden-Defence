using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApexStride_DamageModule : MonoBehaviour, IDamageModule
{
    public int ProcessDamage(int damage, PlayerAttackType attackType)
    {
        return Mathf.RoundToInt(damage * Global.keystoneItemManager.ApexStrideDamageMultiplier);
    }
}
