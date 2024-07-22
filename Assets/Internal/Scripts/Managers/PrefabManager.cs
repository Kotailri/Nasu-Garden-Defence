using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PrefabEnum
{
    PurpleExplosion,
    NormalProjectile
}

[System.Serializable]
public class PrefabManagerClass
{
    public PrefabEnum prefabEnum;
    public GameObject prefab;
}

public class PrefabManager : MonoBehaviour
{
    public List<PrefabManagerClass> Prefabs = new();
    private Dictionary<PrefabEnum, GameObject> prefabList = new();

    private void Awake()
    {
        Global.prefabManager = this;

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