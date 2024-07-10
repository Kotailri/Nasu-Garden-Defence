using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnWaveEnd : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.WAVE_END, DestroySelf);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.WAVE_END, DestroySelf);
    }

    protected virtual void DestroySelf(Dictionary<string, object> _)
    {
        Destroy(gameObject);
    }
}
