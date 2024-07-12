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
    public ProgressBar waveProgressBar;

    private bool isWaveOngoing = false;

    private GameObject lastEnemy = null;
    private float lastEnemyStartingX = 0f;

    [Space(5f)]
    public GameObject DemoCompleteUI;

    private void Awake()
    {
        Global.waveManager = this;
        
    }

    private void Start()
    {
        waveProgressBar.UpdateValue(0);
        DemoCompleteUI.SetActive(false);
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
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && isWaveOngoing)
        {
            isWaveOngoing = false;
            Destroy(currentWave);

            if (CurrentWaveIndex == 3)
            {
                Global.itemSelectManager.CreateItems(ItemTier.Keystone);
            }

            else if (CurrentWaveIndex % 2 == 1) 
            {
                Global.itemSelectManager.CreateItems(ItemTier.Tier1);
            }
            else
            {
                Global.itemSelectManager.CreateItems(ItemTier.Tier2);
            }

            EventManager.TriggerEvent(EventStrings.WAVE_END, null);
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

        if (lastEnemy != null)
        {
            float totalDistance = lastEnemyStartingX - Global.MaxX;
            float distanceCovered = lastEnemyStartingX - lastEnemy.transform.position.x;

            waveProgressBar.UpdateValue(Mathf.Clamp(distanceCovered/totalDistance, 0f, 1f));
        }
    }

    private IEnumerator KillWave()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //yield return new WaitForSeconds(0.1f);
            Destroy(g);
        }
        yield return null;
    }

    public void SpawnNextWave()
    {
        if (CurrentWaveIndex == 0) { print("Wave 0 speed reduction");  Global.EnemySpeedMultiplier = 0.5f; }
        if (CurrentWaveIndex == 1) { print("Wave 1 speed reduction");  Global.EnemySpeedMultiplier = 0.75f; }
        if (CurrentWaveIndex == 2) { print("speed restored");  Global.EnemySpeedMultiplier = 1f; }

        isWaveOngoing = true;
        waveNameUI.text = "Wave " + (CurrentWaveIndex+1);

        if (CurrentWaveIndex >= waves.Count)
        {
            waveNameUI.text = "Thanks for Playing!";
            DemoCompleteUI.SetActive(true);
            return;
        }

        StartCoroutine(DelayNextWave(1f));
        waveProgressBar.UpdateValue(0);

        IEnumerator DelayNextWave(float delay)
        {
            yield return new WaitForSeconds(delay);
            currentWave = Instantiate(waves[CurrentWaveIndex], new Vector3(Global.MaxX, 0, transform.position.z), Quaternion.identity);
            CurrentWaveIndex++;

            if (CurrentWaveIndex >= waves.Count)
            {
                //CurrentWaveIndex = 0;
            }

            MarkLastEnemy();
        }
    }

    
    private void MarkLastEnemy()
    {
        lastEnemy = null;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (lastEnemy == null || enemy.transform.position.x > lastEnemy.transform.position.x)
            {
                lastEnemy = enemy;
            }
        }
        lastEnemyStartingX = lastEnemy.transform.position.x;
        
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
