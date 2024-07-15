using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealTarget
{
    Player,
    Garden
}

public class ItemAdderWithHeal : ItemAdder
{
    public HealTarget target;

    public override void OnItemGet()
    {
        if (target == HealTarget.Player)
        {
            Global.playerTransform.gameObject.GetComponent<PlayerHealth>().FullHeal();
        }

        if (target == HealTarget.Garden)
        {
            Global.gardenHealth.FullHeal();
        }
    }
}
