using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    private void Awake()
    {
        Global.statManager = this;
    }

    private int FadingHasteID = -1;
    public void AddFadingStat(GameObject _currentObject, PlayerStatEnum _stat, float _boost, float _fadeTime)
    {
        PlayerStat stat = GlobalPlayer.GetStat(_stat);
        FadingHasteID = stat.AddStatMultiplier(_boost);
        stat.RecalculateStatMultiplier();

        LeanTween.value(_currentObject, _boost, 1, _fadeTime).setOnUpdate((float val) =>
        {
            stat.statMultipliers[FadingHasteID] = val;
            stat.RecalculateStatMultiplier();
        }).setOnComplete(() =>
        {
            // Remove the boost after fading
            stat.RemoveStatMultiplier(FadingHasteID); // This should be 0 since it has faded out completely
        });
    }
}
