using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    public GameObject damageTextObject;

    private void Awake()
    {
        Global.damageTextSpawner = this;
    }

    public void SpawnText(Vector2 position, string damageNumber, DamageTextType col, float variance = 0f)
    {
        Vector2 spawnPos = position;
        if (variance > 0f)
        {
            spawnPos += new Vector2(Random.Range(-variance, variance), Random.Range(-variance, variance));
        }

        GameObject gm = Instantiate(damageTextObject, spawnPos, Quaternion.identity);
        gm.GetComponent<DamageText>().CreateText(damageNumber, col);
    }
}
