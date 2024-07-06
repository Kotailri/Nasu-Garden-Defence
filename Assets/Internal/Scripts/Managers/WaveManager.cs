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
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 1 && isWaveOngoing)
        {
            Destroy(currentWave);
            isWaveOngoing = false;

            /*if (CurrentWaveIndex == 2)
            {
                Global.itemSelectManager.CreateItems(ItemTier.Keystone);
                return;
            }*/

            if (CurrentWaveIndex == 1) { Global.EnemySpeedMultiplier = 0.75f; }
            if (CurrentWaveIndex == 2) { Global.EnemySpeedMultiplier = 1; }

            Global.itemSelectManager.CreateItems(ItemTier.Tier1);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //StartCoroutine(KillWave());
        }
    }

    private IEnumerator KillWave()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            yield return new WaitForSeconds(0.1f);
            EventManager.TriggerEvent(EventStrings.ENEMY_KILLED, null);
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

        currentWave = Instantiate(waves[CurrentWaveIndex], new Vector3(Global.MaxX, 0, transform.position.z), Quaternion.identity);
        CurrentWaveIndex++;

        if (CurrentWaveIndex >= waves.Count)
        {
            //CurrentWaveIndex = 0;
        }
    }
}
