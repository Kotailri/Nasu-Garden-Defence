using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackModule
{
    public AttackModuleInfoContainer Process(AttackModuleInfoContainer info);
}

public class AttackModuleInfoContainer
{
    public int Damage;
    public PlayerAttackType AttackType;

    public Vector2 HitPosition = Vector2.zero;
    public GameObject AttackObject = null;
    public bool IsObjectDestroyedByDeflect = false;
    public bool IsCrit = false;

    public AttackModuleInfoContainer(int _damage, PlayerAttackType _type) 
    {
        Damage = _damage;
        AttackType = _type;
    }

    public AttackModuleInfoContainer(int _damage, PlayerAttackType _type, GameObject _attackObj, Vector2 _hitPosition)
    {
        Damage = _damage;
        AttackType = _type;

        AttackObject = _attackObj;
        HitPosition = _hitPosition;
    }
}

public interface IAttackPipelineMng : IManager
{
    public AttackModuleInfoContainer ProcessAttackMods(AttackModuleInfoContainer info);
}

public class DamagePipelineManager : MonoBehaviour, IAttackPipelineMng
{
    public AttackModuleInfoContainer ProcessAttackMods(AttackModuleInfoContainer info)
    {
        foreach (IAttackModule module in GetComponents<IAttackModule>())
        {
            info = module.Process(info);
        }
        return info;
    }
}
