using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdderNormal : ItemAdder
{
    public bool isExcamptFromPoolRemoval;
    public override bool IsExcemptFromPoolRemoval()
    {
        return isExcamptFromPoolRemoval;
    }

    public override void OnItemGet()
    {
        AddItemToUI();
    }
}
