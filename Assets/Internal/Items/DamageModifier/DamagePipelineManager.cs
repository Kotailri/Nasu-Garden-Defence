using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackModule
{
    public int Process(int damage, PlayerAttackType attackType, GameObject obj);
}

public interface IAttackPipelineMng : IManager
{
    public int ProcessAttackMods(int damage, PlayerAttackType attackType, GameObject obj);
}

public class DamagePipelineManager : MonoBehaviour, IAttackPipelineMng
{
    public int ProcessAttackMods(int damage, PlayerAttackType attackType, GameObject obj)
    {
        int _dmg = damage;
        foreach (IAttackModule module in GetComponents<IAttackModule>())
        {
            _dmg = module.Process(_dmg, attackType, obj);
        }
        return _dmg;
    }
}
