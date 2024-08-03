using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthChangeDebugMenu : MonoBehaviour, IDebugMenu
{
    [Header("Player Health")]
    public Button setPlayerHealthButton;
    public TMP_InputField playerHealthField;
    public Slider playerHealthSlider;

    [Header("Garden Health")]
    public Button setGardenHealthButton;
    public TMP_InputField gardenHealthField;
    public Slider gardenHealthSlider;

    private PlayerHealth playerHealth;
    private GardenHealth gardenHealth;

    private void Start()
    {
        playerHealth = Global.playerTransform.gameObject.GetComponent<PlayerHealth>();
        gardenHealth = Global.gardenHealth;

        // Player 
        playerHealthSlider.onValueChanged.AddListener((float val) => 
        {
            int newHp = Mathf.FloorToInt((float)playerHealth.MaxHP * val);
            playerHealthField.text = newHp.ToString();

            if (newHp == 0)
            {
                setPlayerHealthButton.interactable = false;
            }
            else
            {
                setPlayerHealthButton.interactable = true;
            }
        });

        setPlayerHealthButton.onClick.AddListener(() =>
        {
            CleanInputs();
            playerHealth.SetHealth(int.Parse(playerHealthField.text), false);
        });

        // Garden
        gardenHealthSlider.onValueChanged.AddListener((float val) => 
        {
            int newHp = Mathf.FloorToInt((float)gardenHealth.MaxHP * val);
            gardenHealthField.text = newHp.ToString();

            if (newHp == 0)
            {
                setGardenHealthButton.interactable = false;
            }
            else
            {
                setGardenHealthButton.interactable = true;
            }
        });

        setGardenHealthButton.onClick.AddListener(() =>
        {
            CleanInputs();
            gardenHealth.SetHealth(int.Parse(gardenHealthField.text), false);
        });
    }

    private void CleanInputs()
    {
        playerHealthField.text = Regex.Replace(playerHealthField.text, @"\D", "");
        gardenHealthField.text = Regex.Replace(gardenHealthField.text, @"\D", "");
    }

    public void LoadMenu()
    {
        playerHealthField.text = playerHealth.CurrentHP.ToString();
        playerHealthSlider.value = playerHealth.GetHealthPercent();

        gardenHealthField.text = gardenHealth.CurrentHP.ToString();
        gardenHealthSlider.value = gardenHealth.GetHealthPercent();
    }
}
