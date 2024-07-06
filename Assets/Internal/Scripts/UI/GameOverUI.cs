using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI waveTextBox;
    private bool gameOverInitialized = false;

    public void InitializeGameOverUI()
    {
        gameOverInitialized = true;
        waveTextBox.text = "Wave " + (Global.waveManager.CurrentWaveIndex + 1).ToString();
    }

    public void Restart()
    {
        if (gameOverInitialized)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnRestartButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Restart();
        }
        
    }
}
