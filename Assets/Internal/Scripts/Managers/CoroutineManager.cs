using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Coroutine StartCoroutine(Coroutine coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public new void StopCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }

    public void StopAllManagerCoroutines()
    {
        StopAllCoroutines();
    }
}
