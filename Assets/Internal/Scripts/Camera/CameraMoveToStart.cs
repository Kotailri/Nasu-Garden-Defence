using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveToStart : MonoBehaviour
{
    public Camera cam;
    
    public Vector2 GameplayPosition;
    public Vector2 MenuPosition;

    [Space(10f)]
    public bool StartsAtMenu;

    private void Start()
    {
        if (!StartsAtMenu)
        {
            cam.gameObject.transform.position = new Vector3(GameplayPosition.x, GameplayPosition.y, transform.position.z);
            Global.playerTransform.position = Vector2.zero;
            Global.gameplayStarted = true;
            Global.waveManager.StartGame();
        }
        else
        {
            cam.gameObject.transform.position = new Vector3(MenuPosition.x, MenuPosition.y, transform.position.z);
            Global.playerTransform.position = new Vector3(-23, 0, 0);
        }
    }

    public void MoveToGameplay()
    {
        if (!StartsAtMenu)
            return;

        LeanTween.move(cam.gameObject, GameplayPosition, 1f).setEaseInOutSine();
    }
}
