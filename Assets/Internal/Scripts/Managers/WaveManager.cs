using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> waves = new();
    public int CurrentWaveIndex = 0;

    private GameObject currentWave;
    public TextMeshProUGUI waveNameUI;

    private bool isWaveOngoing = false;

    private void Awake()
    {
        Global.waveManager = this;
    }

    private void Start()
    {
        
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
        EventManager.StartListening(EventStrings.ENEMY_KILLED, CheckWaveEnd);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.ENEMY_KILLED, CheckWaveEnd);
    }

    private void CheckWaveEnd(Dictionary<string, object> message)
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 1)
        {
            Destroy(currentWave);
            isWaveOngoing = false;
            Global.itemSelectManager.CreateItems();
        }
    }

    public void SpawnNextWave()
    {
        isWaveOngoing = true;
        waveNameUI.text = "Wave " + (CurrentWaveIndex+1);
        currentWave = Instantiate(waves[CurrentWaveIndex], new Vector3(Global.MaxX, 0, transform.position.z), Quaternion.identity);
        CurrentWaveIndex++;

        if (CurrentWaveIndex >= waves.Count)
        {
            CurrentWaveIndex = 0;
        }
    }
}
