using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerStatInitializer : MonoBehaviour
{
    public List<PlayerStatScriptable> stats;

    [ContextMenu("Load Items From Folder")]
    public void LoadItems()
    {
        stats = LoadAllItems<PlayerStatScriptable>("Assets/Internal/ScriptableObjects/StatScriptables");
    }

    public List<T> LoadAllItems<T>(string path) where T : ScriptableObject
    {
        List<T> assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { path });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            // Check if the asset is in the specified directory and not in a subdirectory
            if (System.IO.Path.GetDirectoryName(assetPath).Replace('\\', '/').Equals(path.Replace('\\', '/')))
            {
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null)
                {
                    assets.Add(asset);
                }
            }
        }

        return assets;
    }

    private void Awake()
    {
        foreach (PlayerStatScriptable stat in stats)
        {
            if (GlobalPlayer.PlayerStatDict.ContainsKey(stat.StatEnum))
            {
                print("WARNING: " + stat.StatName + " stat is already used by " + GlobalPlayer.PlayerStatDict[stat.StatEnum].GetStatName());
                continue;
            }
            else
            {
                GlobalPlayer.PlayerStatDict.Add(stat.StatEnum, PlayerStatFactory.CreatePlayerStat(stat.StatName, stat.StatEnum, stat.StatDescription,
                                                                                              stat.StatBase, stat.StatGrowth,
                                                                                              stat.StatGrowthType));
            }
        }
    }
}
