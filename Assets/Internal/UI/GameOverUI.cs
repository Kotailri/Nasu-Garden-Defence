using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI waveTextBox;

    public void InitializeGameOverUI()
    {
        waveTextBox.text = "Wave " + (Global.waveManager.CurrentWaveIndex).ToString();
    }
}
