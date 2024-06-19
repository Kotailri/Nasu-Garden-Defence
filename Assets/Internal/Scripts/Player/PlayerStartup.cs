using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartup : MonoBehaviour
{
    private void Awake()
    {
        Global.playerTransform = transform;
        Player.playerGameObject = gameObject;
    }
}
