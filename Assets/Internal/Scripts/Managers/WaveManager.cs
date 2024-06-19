using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> waves = new();
    public int CurrentWaveIndex = 0;

    private GameObject currentWave;
    private bool waveEnded = false;

    private void Awake()
    {
        Global.waveManager = this;
    }

    private void Start()
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
        if (!waveEnded)
        {
            return;
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 1)
        {
            Destroy(currentWave);
            SpawnNextWave();
        }
    }

    private void SpawnNextWave()
    {
        currentWave = Instantiate(waves[CurrentWaveIndex], new Vector3(Global.MaxX, 0, transform.position.z), Quaternion.identity);
        CurrentWaveIndex++;

        if (CurrentWaveIndex >= waves.Count)
        {
            CurrentWaveIndex = 0;
        }

        waveEnded = false;
    }

    public void TriggerLastEnemySpawned()
    {
        waveEnded = true;
    }
}
