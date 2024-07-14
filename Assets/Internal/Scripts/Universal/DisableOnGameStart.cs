using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnGameStart : MonoBehaviour
{
    public float DestroyDelay;

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.GAME_START, RemoveObject);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.GAME_START, RemoveObject);
    }

    private void RemoveObject(Dictionary<string, object> _)
    {
        Destroy(gameObject, DestroyDelay);
    }
}
