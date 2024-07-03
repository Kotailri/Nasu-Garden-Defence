using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressBarWithText : ProgressBar
{
    public TextMeshProUGUI hpText;

    public void UpdateText(int currentHealth)
    {
        hpText.text = currentHealth.ToString();
    }
}
