using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameObject : MonoBehaviour
{
    public CameraMoveToStart moveCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!Global.gameplayStarted)
            {
                moveCam.MoveToGameplay();
                Global.waveManager.StartGame();
                Global.gameplayStarted = true;
            }

            Destroy(gameObject);
        }
    }
}
