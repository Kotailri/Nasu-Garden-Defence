using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    public GameObject DebugMenuObject;
    private bool isDebugActive = false;

    private void Start()
    {
        DebugMenuObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            isDebugActive = !isDebugActive;
            DebugMenuObject.SetActive(isDebugActive);

            Time.timeScale = isDebugActive ? 0.0f : 1.0f;
        }
    }
}
