using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> waves = new();

    public string WavesLocation;
    public int CurrentWaveIndex = 0;

    private GameObject currentWave;
    public TextMeshProUGUI waveNameUI;

    private bool isWaveOngoing = false;

    private void Awake()
    {
        Global.waveManager = this;
    }

    public bool IsWaveOngoing()
    {
        return isWaveOngoing;
    }


    public void StartGame()
    {
        SpawnNextWave();
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.ENEMY_DELETED, CheckWaveEnd);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.ENEMY_DELETED, CheckWaveEnd);
    }

    private void CheckWaveEnd(Dictionary<string, object> message)
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 1 && isWaveOngoing)
        {
            Destroy(currentWave);
            isWaveOngoing = false;

            /*if (CurrentWaveIndex == 2)
            {
                Global.itemSelectManager.CreateItems(ItemTier.Keystone);
                return;
            }*/

            if (CurrentWaveIndex == 1) { Global.EnemySpeedMultiplier = 0.6f; }
            if (CurrentWaveIndex == 2) { Global.EnemySpeedMultiplier = 0.75f; }
            if (CurrentWaveIndex == 3) { Global.EnemySpeedMultiplier = 0.9f; }
            if (CurrentWaveIndex == 4) { Global.EnemySpeedMultiplier = 1f; }

            if (CurrentWaveIndex % 2 == 0) 
            {
                Global.itemSelectManager.CreateItems(ItemTier.Tier1);
            }
            else
            {
                SpawnNextWave();
            }
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(KillWave());
        }
#endif
    }

    private IEnumerator KillWave()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            yield return new WaitForSeconds(0.1f);
            EventManager.TriggerEvent(EventStrings.ENEMY_DELETED, null);
            Destroy(g);
        }
    }

    public void SpawnNextWave()
    {
        isWaveOngoing = true;
        waveNameUI.text = "Wave " + (CurrentWaveIndex+1);

        if (CurrentWaveIndex >= waves.Count)
        {
            waveNameUI.text = "End";
            return;
        }

        StartCoroutine(DelayNextWave(1f));

        IEnumerator DelayNextWave(float delay)
        {
            yield return new WaitForSeconds(delay);
            currentWave = Instantiate(waves[CurrentWaveIndex], new Vector3(Global.MaxX, 0, transform.position.z), Quaternion.identity);
            CurrentWaveIndex++;

            if (CurrentWaveIndex >= waves.Count)
            {
                //CurrentWaveIndex = 0;
            }
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Waves")]
    public void RefreshWaves()
    {
        waves.Clear();
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { WavesLocation });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            // Check if the asset is in the specified directory and not in a subdirectory
            if (System.IO.Path.GetDirectoryName(assetPath).Replace('\\', '/').Equals(WavesLocation.Replace('\\', '/')))
            {
                GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if (asset != null)
                {
                    waves.Add(asset);
                }
            }
        }
    }
#endif
}
