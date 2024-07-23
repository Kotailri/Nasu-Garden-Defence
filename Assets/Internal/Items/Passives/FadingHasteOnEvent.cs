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
        Global.statManager.AddFadingStat(gameObject, PlayerStatEnum.movespeed, fadingHasteAmount, fadingHasteTime);
    }
}
