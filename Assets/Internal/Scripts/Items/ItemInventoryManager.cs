using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ItemTier
{
    Tier1,
    Tier2,
    Keystone
}

public class ItemInventoryManager : MonoBehaviour
{
    public List<ItemAdder> ItemPool_T1 = new();
    public List<ItemAdder> ItemPool_T2 = new();
    public List<ItemAdder> ItemPool_Keystone = new();

    [Space(10f)]
    public List<ItemAdder> ItemInventory = new();

    [Space(5f)]
    [Header("Item Paths")]
    public string Tier1ItemPath;
    public string Tier2ItemPath;
    public string KeystoneItemPath;

    private void Awake()
    {
        Global.itemInventoryManager = this;
    }

    public List<ItemAdder> GetRandomFromPool(int num, ItemTier tier)
    {
        switch(tier)
        {
            case ItemTier.Tier1:
                return Global.GetRandomElements(ItemPool_T1, num);
            case ItemTier.Tier2:
                return Global.GetRandomElements(ItemPool_T2, num);
            case ItemTier.Keystone:
                return Global.GetRandomElements(ItemPool_Keystone, num);
        }
        return new List<ItemAdder>(); 
    } 
    
    public List<ItemAdder> GetRandomFromInventory(int num)
    {
        return Global.GetRandomElements(ItemInventory, num);
    }

    public void PoolToInventory(ItemAdder item)
    {
        ItemInventory.Add(item);
        if (item.IsExcemptFromPoolRemoval() == false)
        {
            if (ItemPool_T1.Contains(item))
            {
                ItemPool_T1.Remove(item);
            }
            else
            if (ItemPool_T2.Contains(item))
            {
                ItemPool_T2.Remove(item);
            }

            else
            if (ItemPool_Keystone.Contains(item))
            {
                ItemPool_Keystone.Remove(item);
            }
        }
        
    }

    [ContextMenu("Load Tier 1s")]
    public void LoadTier1()
    {
        ItemPool_T1.Clear();
        LoadItemPool(Tier1ItemPath, ItemTier.Tier1);
    }

    [ContextMenu("Load Tier 2s")]
    public void LoadTier2()
    {
        ItemPool_T2.Clear();
        LoadItemPool(Tier2ItemPath, ItemTier.Tier2);
    }

    [ContextMenu("Load Keystones")]
    public void LoadKeystone()
    {
        ItemPool_Keystone.Clear();
        LoadItemPool(KeystoneItemPath, ItemTier.Keystone);
    }

    [ContextMenu("Load All")]
    public void LoadAll()
    {
        LoadTier1();
        LoadTier2();
        LoadKeystone();
    }

    
    public void LoadItemPool(string path, ItemTier tier)
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
                        case ItemTier.Tier1:
                            ItemPool_T1.Add(asset.GetComponent<ItemAdder>());
                            break;

                        case ItemTier.Tier2:
                            ItemPool_T2.Add(asset.GetComponent<ItemAdder>());
                            break;

                        case ItemTier.Keystone:
                            ItemPool_Keystone.Add(asset.GetComponent<ItemAdder>());
                            break;
                    }
                }
            }
        }
    }
}
