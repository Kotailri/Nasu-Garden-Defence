using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AttackPrefab
{
    public AttackPrefabNameEnum name;
    public GameObject prefab;
}

public enum AttackPrefabNameEnum
{
    BasicShoot
}

public class PlayerAttackPrefabHolder : MonoBehaviour
{
    public List<AttackPrefab> attackPrefabs = new();
    private Dictionary<AttackPrefabNameEnum, GameObject> attackPrefabDictionary = new();

    private void Awake()
    {
        Global.attackPrefabHolder = this;

        foreach (AttackPrefab attackPrefab in attackPrefabs)
        {
            attackPrefabDictionary.Add(attackPrefab.name, attackPrefab.prefab);
        }
    }

    public GameObject GetPrefab(AttackPrefabNameEnum name)
    {
        return attackPrefabDictionary[name];
    }
}
