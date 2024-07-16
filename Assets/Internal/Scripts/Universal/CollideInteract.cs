using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class CollideInteract : MonoBehaviour
{
    public UnityEvent InteractEvent;
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;
    public bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activated = true;
            OnPlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activated = false;
            OnPlayerExit?.Invoke();
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.INTERACT_PRESSED, TryInteract);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.INTERACT_PRESSED, TryInteract);
    }

    private void TryInteract(Dictionary<string, object>_)
    {
        if (activated)
        {
            InteractEvent?.Invoke();
        }
    }
}
