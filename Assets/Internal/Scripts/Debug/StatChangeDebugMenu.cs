using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatChangeDebugMenu : MonoBehaviour, IDebugMenu
{
    public PlayerStatEnum selectedStat = PlayerStatEnum.attackSize;

    private TMP_Dropdown dropdown;
    private TMP_InputField setStatField;

    private DebugMenuController debugMenuController;

    private void Awake()
    {
        foreach (Transform t in transform)
        {
            if (t.gameObject.TryGetComponent(out TMP_InputField inputField))
            {
                setStatField = inputField;
                setStatField.text = "";
            }

            if (t.gameObject.TryGetComponent(out TMP_Dropdown _dropdown))
            {
                dropdown = _dropdown;
            }
        }
        
    }

    public void LoadMenu()
    {
        UpdateStatDisplay();
    }

    public void IncrementStat(bool isIncrement)
    {
        if (isIncrement)
        {
            GlobalStats.GetStat(selectedStat).SetLevel(1, true);
        }
        else
        {
            GlobalStats.GetStat(selectedStat).SetLevel(-1, true);
        }

        UpdateStatDisplay();
    }

    public void SetStat()
    {
        int num;
        bool success = int.TryParse(setStatField.text, out num);
        if (success)
        {
            GlobalStats.GetStat(selectedStat).SetLevel(num, false);
        }

        setStatField.text = "";
        UpdateStatDisplay();
    }

    private void UpdateStatDisplay()
    {
        int index = 0;

        string original = dropdown.captionText.text;
        int number = GlobalStats.GetStat(selectedStat).GetLevel();
        dropdown.captionText.text = original[..^4] + " " + number.ToString().PadLeft(3);

        foreach (TMP_Dropdown.OptionData text in dropdown.options)
        {
            string original2 = text.text;
            int number2 = GlobalStats.GetStat(IntToStatEnum(index)).GetLevel();
            text.text = original2[..^4] + " " + number2.ToString().PadLeft(3);

            index++;
        }

    }

    private PlayerStatEnum IntToStatEnum(int num)
    {
        switch (num)
        {
            case 0:
                return PlayerStatEnum.attackSize;

            case 1:
                return PlayerStatEnum.attackspeed;
                
            case 2:
                return PlayerStatEnum.critchance;
                
            case 3:
                return PlayerStatEnum.explosionDamage;
                
            case 4:
                return PlayerStatEnum.explosionRadius;
                
            case 5:
                return PlayerStatEnum.gardenHealth;
                
            case 6:
                return PlayerStatEnum.invincDuration;
                
            case 7:
                return PlayerStatEnum.meleeDamage;
                
            case 8:
                return PlayerStatEnum.movespeed;
                
            case 9:
                return PlayerStatEnum.damage;
                
            case 10:
                return PlayerStatEnum.playerHealth;
               
            case 11:
                return PlayerStatEnum.projectileDamage;
               
            case 12:
                return PlayerStatEnum.projectileSpeed;
               
            case 13:
                return PlayerStatEnum.slowReduction;
               
        }

        return PlayerStatEnum.attackSize;
    }


    public void SetSelectedStat(int selection)
    {
        selectedStat = IntToStatEnum(selection);
    }

    public void SetDebugMenuController(DebugMenuController controller)
    {
        debugMenuController = controller;
    }
}
