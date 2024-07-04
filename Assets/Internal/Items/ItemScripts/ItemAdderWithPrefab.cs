using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdderWithPrefab : ItemAdder
{
    public GameObject prefab;
    public override bool IsExcemptFromPoolRemoval()
    {
        return false;
    }

    public override void OnItemGet()
    {
        GameObject gm = Instantiate(prefab, Global.playerTransform);
        gm.transform.localPosition = Vector3.zero;
        AddItemToUI();
    }
}
