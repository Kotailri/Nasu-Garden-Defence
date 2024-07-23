using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunAdder : ItemAdderNormal
{
    public float WaveIncreasePercent;
    public override void OnItemGet()
    {
        Global.WaveSpeed *= WaveIncreasePercent;
        base.OnItemGet();
    }
}
