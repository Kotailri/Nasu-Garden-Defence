using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApexStride_DamageModule : MonoBehaviour, IAttackModule
{
    public int Process(int damage, PlayerAttackType attackType, GameObject obj)
    {
        return Mathf.RoundToInt(damage * Global.keystoneItemManager.ApexStrideDamageMultiplier);
    }
}
