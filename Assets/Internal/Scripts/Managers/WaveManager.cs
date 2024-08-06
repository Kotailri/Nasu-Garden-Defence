using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IWaveMng : IManager
{
    public void StartGame();
    public void SpawnNextWave();
    public int GetCurrentWaveIndex();
    public void KillWave();
    public bool IsWaveOngoing();
    public void StartWave(int waveIndex);
    public WaveWithReward GetCurrentWave();
    public List<WaveWithReward> GetWaves();
}


[System.Serializable]
public class WaveWithReward
{
    public GameObject Wave;
    public ItemTypeEnum ItemType;

    public WaveWithReward(GameObject _wave, ItemTypeEnum _itemType)
    {
        Wave = _wave;
        ItemType = _itemType;
    }
}

public class WaveManager : MonoBehaviour, IWaveMng
{
    public List<WaveWithReward> waves = new();
    public int DebugStartingIndex;
    
    public bool GivesItems = true;

    private GameObject currentWave;

    [Space(10f)]
    public TextMeshProUGUI waveNameUI;
    public ProgressBar waveProgressBar;

    private bool isWaveOngoing = false;

    private GameObject lastEnemy = null;
    private float lastEnemyStartingX = 0f;

    public StopwatchTimer timer;

    [Space(5f)]
    public GameObject DemoCompleteUI;

    [Space(15f)]
    public bool EnableEverything = true;

    [Header("Readonly")]
    public int CurrentWaveIndex = 0;
    public int StartingIndex = 0;

    public int GetCurrentWaveIndex()
    {
        return CurrentWaveIndex;
    }

    public void KillWave()
    {
        StartCoroutine(KillWaveCoroutine());
    }

    public List<WaveWithReward> GetWaves()
    {
        return waves;
    }

    public WaveWithReward GetCurrentWave()
    {
        return waves[CurrentWaveIndex];
    }

    private void Start()
    {
        waveProgressBar.UpdateValue(0);
        DemoCompleteUI.SetActive(false);
        if (CurrentWaveIndex < 0)
        {
            CurrentWaveIndex = 0;
        }
    }

    public bool IsWaveOngoing()
    {
        return isWaveOngoing;
    }

    public void StartGame()
    {
        Global.playerTransform.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

        if (!EnableEverything) { 
            isWaveOngoing=true;
            return; 
        }

        StartingIndex += GlobalGarden.LevelsToSkip;
        if (GivesItems) 
        {
            for (int i = 0; i < StartingIndex; i++)
            {
                Managers.Instance.Resolve<IItemInventoryMng>().AddRandomToInventory(waves[i].ItemType, i);
            }

        }
        
        if (DebugStartingIndex > 0)
        {
            StartingIndex = DebugStartingIndex;
        }

        CurrentWaveIndex = StartingIndex;

        timer.ResetTimer();
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

    public void StartWave(int waveIndex)
    {
        isWaveOngoing = false;

        if (currentWave)
            Destroy(currentWave);

        CurrentWaveIndex = waveIndex;
        SpawnNextWave();
    }

    private void CheckWaveEnd(Dictionary<string, object> message)
    {
        if (!EnableEverything)
            return;

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && isWaveOngoing)
        {
            if (currentWave)
                Destroy(currentWave);

            isWaveOngoing = false;

            EventManager.TriggerEvent(EventStrings.WAVE_END, null);
            
            timer.PauseTimer();

            if (CurrentWaveIndex >= waves.Count && !Global.isGameOver)
            {
                waveNameUI.text = "End!";
                DemoCompleteUI.SetActive(true);
                return;
            }

            Managers.Instance.Resolve<IItemSelectMng>().CreateItems(waves[CurrentWaveIndex].ItemType);
            Global.gardenHealth.SetHealth(GlobalGarden.GardenHealAfterWave, true);
            Global.playerTransform.gameObject.GetComponent<PlayerHealth>().HealPercent(GlobalGarden.PlayerPercentHealAfterWave);

            CurrentWaveIndex++;
            
        }
    }

    private void Update()
    {

        if (lastEnemy != null)
        {
            float totalDistance = lastEnemyStartingX - Global.MaxX;
            float distanceCovered = lastEnemyStartingX - lastEnemy.transform.position.x;

            waveProgressBar.UpdateValue(Mathf.Clamp(distanceCovered/totalDistance, 0f, 1f));
        }
    }

    private IEnumerator KillWaveCoroutine()
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
        if (CurrentWaveIndex == 0) { 
            //print("Wave 0 speed reduction");  
            Global.EnemySpeedMultiplier = 0.6f; 
        }
        if (CurrentWaveIndex == 1) { 
            //print("Wave 1 speed reduction");  
            Global.EnemySpeedMultiplier = 0.8f; 
        }
        if (CurrentWaveIndex >= 2) { 
            //print("speed restored");  
            Global.EnemySpeedMultiplier = 1f; 
        }

        timer.StartTimer();

        
        waveNameUI.text = "Wave " + CurrentWaveIndex.ToString();

        StartCoroutine(DelayNextWave(1f));
        waveProgressBar.UpdateValue(0);

        IEnumerator DelayNextWave(float delay)
        {
            yield return new WaitForSeconds(delay);
            isWaveOngoing = true;
            currentWave = Instantiate(waves[CurrentWaveIndex].Wave, new Vector3(Global.MaxX + 2f, -0.5f, transform.position.z), Quaternion.identity);
            
            if (Random.Range(0,2) == 0)
            {
                foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    e.transform.localPosition = new Vector3(e.transform.localPosition.x, -e.transform.localPosition.y, e.transform.localPosition.z);
                    //e.transform.localPosition += new Vector3(0, 0.57f, 0);
                }
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


}
