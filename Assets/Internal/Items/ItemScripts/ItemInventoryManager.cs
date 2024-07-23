using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public enum ItemTypeEnum
{
    Weapon,
    StatUp,
    Passive,
    Keystone
}

public class ItemInventoryManager : MonoBehaviour
{
    public List<ItemAdder> debugInventory = new();

    [Space(10f)]
    public List<ItemAdder> ItemPool_Weapon = new();
    public List<ItemAdder> ItemPool_StatUp = new();
    public List<ItemAdder> ItemPool_Passive = new();
    public List<ItemAdder> ItemPool_Keystone = new();

    [Space(10f)]
    public List<ItemAdder> ItemInventory = new();
    public List<ItemAdder> StartingItems = new();

    [Space(5f)]
    [Header("Item Paths")]
    public string WeaponPoolPath;
    public string StatUpPoolPath;
    public string PassivePoolPath;
    public string KeystoneItemPath;

    private void Awake()
    {
        Global.itemInventoryManager = this;
    }

    private void Start()
    {
        foreach (ItemAdder item in debugInventory)
        {
            item.OnItemGet();
            PoolToInventory(item);
        }

        foreach (ItemAdder item in StartingItems)
        {
            item.OnItemGet();
            PoolToInventory(item);
        }

        // debug check item info
        foreach (ItemAdder item in ItemPool_Weapon)
        {
            item.GetInfo();
        }
        foreach (ItemAdder item in ItemPool_StatUp)
        {
            item.GetInfo();
        }
        foreach (ItemAdder item in ItemPool_Passive)
        {
            item.GetInfo();
        }
        foreach (ItemAdder item in ItemPool_Keystone)
        {
            item.GetInfo();
        }
    }

    public ItemAdder GetItemFromInventory(ItemAdder _item)
    {
        foreach (ItemAdder item in ItemInventory)
        {
            if (item == _item)
            {
                return item;
            }
        }
        return null;
    }

    public List<ItemAdder> GetFullPool(ItemTypeEnum tier, int waveRequirement = 0)
    {
        List<ItemAdder> selectedPool = new();

        switch (tier)
        {
            case ItemTypeEnum.Weapon:
                selectedPool = new List<ItemAdder>(ItemPool_Weapon);
                break;
            case ItemTypeEnum.StatUp:
                selectedPool = new List<ItemAdder>(ItemPool_StatUp);
                break;
            case ItemTypeEnum.Passive:
                selectedPool = new List<ItemAdder>(ItemPool_Passive);
                break;
            case ItemTypeEnum.Keystone:
                selectedPool = new List<ItemAdder>(ItemPool_Keystone);
                break;
        }

        selectedPool.RemoveAll(adder => adder.MinWaveIndex > waveRequirement);
        return selectedPool;
    }

    public List<ItemAdder> GetRandomFromPool(int num, ItemTypeEnum tier, int waveRequirement=0)
    {
        List<ItemAdder> selectedPool = new();

        switch(tier)
        {
            case ItemTypeEnum.Weapon:
                selectedPool = new List<ItemAdder>(ItemPool_Weapon);
                break;
            case ItemTypeEnum.StatUp:
                selectedPool = new List<ItemAdder>(ItemPool_StatUp);
                break;
            case ItemTypeEnum.Passive:
                selectedPool = new List<ItemAdder>(ItemPool_Passive);
                break;
            case ItemTypeEnum.Keystone:
                selectedPool = new List<ItemAdder>(ItemPool_Keystone);
                break;
        }

        selectedPool.RemoveAll(adder => adder.MinWaveIndex > waveRequirement);

        return Global.GetRandomElements(selectedPool, num);
    }

    public List<ItemAdder> GetRandomFromInventory(int num)
    {
        return Global.GetRandomElements(ItemInventory, num);
    }

    public void AddRandomToInventory(ItemTypeEnum itemType, int waveIndex=int.MaxValue)
    {
        List<ItemAdder> randomItems = Global.itemInventoryManager.GetRandomFromPool(3, itemType, waveIndex);
        if (randomItems.Count == 0)
        {
            print("No Items to add to inventory");
        }
        else
        {
            ItemAdder item = randomItems[UnityEngine.Random.Range(0, randomItems.Count)];
            item.OnItemGet();
            PoolToInventory(item);
        }
        
    }

    public void RemoveItemFromInventory(ItemAdder item)
    {
        ItemInventory.Remove(item);
    }

    public void PoolToInventory(ItemAdder item)
    {
        ItemInventory.Add(item);
        if (item.IsExcemptFromPoolRemoval() == false)
        {
            if (ItemPool_Weapon.Contains(item))
            {
                ItemPool_Weapon.Remove(item);
            }

            else
            if (ItemPool_StatUp.Contains(item))
            {
                ItemPool_StatUp.Remove(item);
            }

            else
            if (ItemPool_Passive.Contains(item))
            {
                ItemPool_Passive.Remove(item);
            }

            else
            if (ItemPool_Keystone.Contains(item))
            {
                ItemPool_Keystone.Remove(item);
            }
        }
        
    }

#if UNITY_EDITOR
    [ContextMenu("Load Weapon Pool")]
    public void LoadWeaponPool()
    {
        ItemPool_Weapon.Clear();
        LoadItemPool(WeaponPoolPath, ItemTypeEnum.Weapon);
    }

    [ContextMenu("Load StatUp Pool")]
    public void LoadStatUpPool()
    {
        ItemPool_StatUp.Clear();
        LoadItemPool(StatUpPoolPath, ItemTypeEnum.StatUp);
    }

    [ContextMenu("Load Passive Pool")]
    public void LoadPassivePool()
    {
        ItemPool_Passive.Clear();
        LoadItemPool(PassivePoolPath, ItemTypeEnum.Passive);
    }

    [ContextMenu("Load Keystone Pool")]
    public void LoadKeystone()
    {
        ItemPool_Keystone.Clear();
        LoadItemPool(KeystoneItemPath, ItemTypeEnum.Keystone);
    }

    [ContextMenu("Load All")]
    public void LoadAll()
    {
        LoadWeaponPool();
        LoadStatUpPool();
        LoadPassivePool();
        LoadKeystone();
    }

    
    public void LoadItemPool(string path, ItemTypeEnum tier)
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { path });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            // Check if the asset is in the specified directory and not in a subdirectory
            if (System.IO.Path.GetDirectoryName(assetPath).Replace('\\', '/').Equals(path.Replace('\\', '/')))
            {
                GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if (asset != null && asset.TryGetComponent(out ItemAdder _))
                {
                    switch(tier)
                    { 
                        case ItemTypeEnum.Weapon:
                            ItemPool_Weapon.Add(asset.GetComponent<ItemAdder>());
                            break;

                        case ItemTypeEnum.StatUp:
                            ItemPool_StatUp.Add(asset.GetComponent<ItemAdder>());
                            break;

                        case ItemTypeEnum.Passive:
                            ItemPool_Passive.Add(asset.GetComponent<ItemAdder>());
                            break;

                        case ItemTypeEnum.Keystone:
                            ItemPool_Keystone.Add(asset.GetComponent<ItemAdder>());
                            break;
                    }
                }
            }
        }
    }
#endif
}
