using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEnemyWave : MonoBehaviour
{
    private bool flag = false;
    // Update is called once per frame
    void Update()
    {
        if (!flag && transform.position.x < Global.MaxX)
        {
            Global.waveManager.TriggerLastEnemySpawned();
            flag = true;
        }
    }
}
