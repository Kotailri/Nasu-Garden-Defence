using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    private void Awake()
    {
        Global.statManager = this;
    }

    public void IncreaseStatLevel(PlayerStatEnum stat, int levelGained)
    {
        GlobalPlayer.GetStat(stat).SetLevel(levelGained, true);
        EventManager.TriggerEvent(EventStrings.STATS_UPDATED, null);
    }
}
