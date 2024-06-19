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

    public void SpawnText(Vector2 position, string damageNumber, DamageTextType col)
    {
        GameObject gm = Instantiate(damageTextObject, position, Quaternion.identity);
        gm.GetComponent<DamageText>().CreateText(damageNumber, col);
    }
}
