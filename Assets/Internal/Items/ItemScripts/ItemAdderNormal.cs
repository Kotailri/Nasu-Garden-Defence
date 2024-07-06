using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdderNormal : ItemAdder
{
    public override void OnItemGet()
    {
        AddItemToUI();
    }
}
