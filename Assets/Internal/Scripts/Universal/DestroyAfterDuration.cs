using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDuration : MonoBehaviour
{
    public float Duration;

    private void Start()
    {
        Destroy(gameObject, Duration);
    }
}
