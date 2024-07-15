using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that triggers collision events of other objects.
/// </summary>
public class CallsTriggerCollisions : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IHasTriggerEnter enter))
        {
            enter.OnTriggerEnterEvent(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IHasTriggerStay stay))
        {
            stay.OnTriggerStayEvent(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IHasTriggerExit exit))
        {
            exit.OnTriggerExitEvent(gameObject);
        }
    }
}
