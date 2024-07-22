using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

public class WaveManager : MonoBehaviour
{
    public List<WaveWithReward> waves = new();

    public int CurrentWaveIndex = 0;
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

    private void Awake()
    {
        Global.waveManager = this;
        
    }

    private void Start()
    {
        waveProgressBar.UpdateValue(0);
        DemoCompleteUI.SetActive(false);

        CurrentWaveIndex--;

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

        CurrentWaveIndex += GlobalGarden.LevelsToSkip;
        if (GivesItems) 
        {
            for (int i = 0; i < CurrentWaveIndex; i++)
            {
                Global.itemInventoryManager.AddRandomToInventory(waves[i].ItemType, i);
            }

        }
        

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

    private void CheckWaveEnd(Dictionary<string, object> message)
    {
        if (!EnableEverything)
            return;

            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && isWaveOngoing)
        {
            isWaveOngoing = false;
            Destroy(currentWave);

            timer.PauseTimer();

            if (CurrentWaveIndex >= waves.Count && !Global.isGameOver)
            {
                waveNameUI.text = "End!";
                DemoCompleteUI.SetActive(true);
                return;
            }

            Global.itemSelectManager.CreateItems(waves[CurrentWaveIndex-1].ItemType);
            Global.gardenHealth.SetHealth(GlobalGarden.GardenHealAfterWave, true);
            Global.playerTransform.gameObject.GetComponent<PlayerHealth>().HealPercent(GlobalGarden.PlayerPercentHealAfterWave);

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

        isWaveOngoing = true;
        waveNameUI.text = "Wave " + (CurrentWaveIndex+1);

        StartCoroutine(DelayNextWave(1f));
        waveProgressBar.UpdateValue(0);

        IEnumerator DelayNextWave(float delay)
        {
            yield return new WaitForSeconds(delay);
            currentWave = Instantiate(waves[CurrentWaveIndex].Wave, new Vector3(Global.MaxX, -0.5f, transform.position.z), Quaternion.identity);
            
            if (Random.Range(0,2) == 0)
            {
                foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    e.transform.localPosition = new Vector3(e.transform.localPosition.x, -e.transform.localPosition.y, e.transform.localPosition.z);
                    //e.transform.localPosition += new Vector3(0, 0.57f, 0);
                }
            }
            

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


}
