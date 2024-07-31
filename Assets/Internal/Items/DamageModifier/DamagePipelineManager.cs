using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageModule
{
    public int ProcessDamage(int damage, PlayerAttackType attackType);
}

public interface IDamagePipelineMng : IManager
{
    public int GetProcessedDamage(int damage, PlayerAttackType attackType);
}

public class DamagePipelineManager : MonoBehaviour, IDamagePipelineMng
{
    public int GetProcessedDamage(int damage, PlayerAttackType attackType)
    {
        int _dmg = damage;
        foreach (IDamageModule module in GetComponents<IDamageModule>())
        {
            _dmg = module.ProcessDamage(_dmg, attackType);
        }
        return _dmg;
    }
}
