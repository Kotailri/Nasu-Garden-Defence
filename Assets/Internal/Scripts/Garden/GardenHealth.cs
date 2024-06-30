using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenHealth : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;

    public ProgressBarWithText gardenHealthUI;

    private void Start()
    {
        CurrentHP = MaxHP;
        UpdateUI(CurrentHP);
    }

    public int GetHealth()
    {
        return CurrentHP;
    }

    public int GetMaxHealth()
    {
        return MaxHP;
    }

    public void AddHealth(int _hp)
    {
        CurrentHP += _hp;
        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        UpdateUI(CurrentHP);
    }

    public void RemoveHealth(int _hp)
    {
        CurrentHP -= _hp;
        CheckDeath();

        UpdateUI(CurrentHP);
    }

    public void AddMaxHealth(int _hp)
    {
        CurrentHP += _hp;
        MaxHP = _hp;

        UpdateUI(CurrentHP);
    }

    public void RemoveMaxHealth(int _hp)
    {
        MaxHP -= _hp;
        if (MaxHP <= 0) { MaxHP = 1; }

        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        UpdateUI(CurrentHP);
    }

    private void UpdateUI(int CurrentHP)
    {
        gardenHealthUI.UpdateValue((float)CurrentHP / (float)MaxHP);
        gardenHealthUI.UpdateText(CurrentHP);
    }

    private void CheckDeath()
    {
        if (CurrentHP <= 0)
        {
            Global.GameOver();
        }
    }
}
