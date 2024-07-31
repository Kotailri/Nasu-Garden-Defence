using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PrefabEnum
{
    PurpleExplosion,
    NormalProjectile,
    ExecuteEffect,
    DinkEffect
}

[System.Serializable]
public class PrefabManagerClass
{
    public PrefabEnum prefabEnum;
    public GameObject prefab;
}

public interface IPrefabMng : IManager
{
    public GameObject InstantiatePrefab(PrefabEnum prefab, Vector2 position, Quaternion rotation);
}


public class PrefabManager : MonoBehaviour, IPrefabMng
{
    public List<PrefabManagerClass> Prefabs = new();
    private Dictionary<PrefabEnum, GameObject> prefabList = new();

    private void Awake()
    {
        foreach (PrefabManagerClass prefab in Prefabs)
        {
            prefabList.Add(prefab.prefabEnum, prefab.prefab);
        }
    }

    public GameObject InstantiatePrefab(PrefabEnum prefab, Vector2 position, Quaternion rotation)
    {
        if (prefabList.ContainsKey(prefab))
        {
            return Instantiate(prefabList[prefab], position, rotation);
        }
        else
        {
            print("Prefab for " +  prefab.ToString() + " does not exist!");
            return null;
        }
        
    }
}
