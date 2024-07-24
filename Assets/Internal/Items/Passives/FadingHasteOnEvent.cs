using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingHasteOnEvent : MonoBehaviour
{
    public string EventString;

    [Space(5f)]
    public float fadingHasteAmount;
    public float fadingHasteTime;

    private void OnEnable()
    {
        EventManager.StartListening(EventString, ApplyHaste);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventString, ApplyHaste);
    }

    private void ApplyHaste(Dictionary<string, object> _)
    {
        AddFadingHaste(fadingHasteAmount, fadingHasteTime);
    }

    private int FadingHasteID = -1;
    public void AddFadingHaste(float _boost, float _fadeTime)
    {
        PlayerStat stat = GlobalPlayer.GetStat(PlayerStatEnum.movespeed);

        if (LeanTween.isTweening(gameObject))
        {
            LeanTween.cancel(gameObject);
            stat.RemoveStatMultiplier(FadingHasteID);
        }
        
        FadingHasteID = stat.AddStatMultiplier(_boost);

        LeanTween.value(gameObject, _boost, 1, _fadeTime).setOnUpdate((float val) =>
        {
            stat.SetUniqueStatMultiplier(val, FadingHasteID);
        }).setOnComplete(() =>
        {
            stat.RemoveStatMultiplier(FadingHasteID);
        });
    }
}
