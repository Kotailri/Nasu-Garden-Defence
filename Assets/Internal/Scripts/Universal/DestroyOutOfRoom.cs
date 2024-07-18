using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfRoom : MonoBehaviour
{
    public bool Left;
    public bool Right;
    public bool Top;
    public bool Bottom;

    [Space(5f)]
    public float padding = 0f;

    [Space(5f)]
    public float Timer = 2.5f;
    private float CurrentTimer = 0f;

    private void CheckDestroyX()
    {
        if (Left && transform.position.x < Global.XRange.min - padding)
            Destroy(gameObject);
        if (Right && transform.position.x > Global.XRange.max + padding)
            Destroy(gameObject);
    }
    private void CheckDestroyY()
    {
        if (Bottom && transform.position.y < Global.YRange.min - padding)
            Destroy(gameObject);
        if (Top && transform.position.y > Global.YRange.max + padding)
            Destroy(gameObject);
    }

    private void Update()
    {
        CurrentTimer += Time.deltaTime;
        if (CurrentTimer >= Timer)
        {
            CheckDestroyX();
            CheckDestroyY();
            CurrentTimer = 0f;
        }
    }
}
