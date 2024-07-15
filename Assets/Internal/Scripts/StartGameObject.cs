using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameObject : MonoBehaviour
{
    public CameraMoveToStart moveCam;

    private void Awake()
    {
        Global.gameplayStarted = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!Global.gameplayStarted)
            {
                EventManager.TriggerEvent(EventStrings.GAME_START, null);
                moveCam.MoveToGameplay();
                Global.waveManager.StartGame();
                Global.gameplayStarted = true;
            }

            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            Destroy(gameObject);
        }
    }
}
