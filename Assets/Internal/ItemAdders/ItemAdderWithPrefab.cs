using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdderWithPrefab : ItemAdder
{
    [Header("Prefab")]
    public GameObject prefab;
    public bool isAttachedToPet = true;

    public override bool IsExcemptFromPoolRemoval()
    {
        return false;
    }

    public void AttachToPet()
    {
        if (isAttachedToPet && GameObject.FindGameObjectWithTag("Bwo") != null)
        {
            GameObject gm = Instantiate(prefab, GameObject.FindGameObjectWithTag("Bwo").transform);
            gm.transform.localPosition = Vector3.zero;
        }
    }

    public override void OnItemGet()
    {
        GameObject gm = Instantiate(prefab, Global.playerTransform);
        gm.transform.localPosition = Vector3.zero;
        AttachToPet();
        AddItemToUI();
    }
}
