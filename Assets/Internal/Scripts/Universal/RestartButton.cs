using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void Restart()
    {
        Global.ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnRestartButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Restart();
        }

    }
}
