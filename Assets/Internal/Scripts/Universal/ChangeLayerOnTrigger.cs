using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayerOnTrigger : MonoBehaviour
{
    public string BehindPlayerLayer;
    public string InFrontOfPlayerLayer;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sortingLayerName = BehindPlayerLayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sortingLayerName = InFrontOfPlayerLayer;
        }
    }
}
