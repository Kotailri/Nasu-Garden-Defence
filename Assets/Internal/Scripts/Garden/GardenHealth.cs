using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenHealth : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;

    private void Start()
    {
        CurrentHP = MaxHP;
        Global.gardenHealthUI.UpdateUI(CurrentHP);
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

        Global.gardenHealthUI.UpdateUI(CurrentHP);
    }

    public void RemoveHealth(int _hp)
    {
        CurrentHP -= _hp;
        CheckDeath();

        Global.gardenHealthUI.UpdateUI(CurrentHP);
    }

    public void AddMaxHealth(int _hp)
    {
        CurrentHP += _hp;
        MaxHP = _hp;
        Global.gardenHealthUI.UpdateUI(CurrentHP);
    }

    public void RemoveMaxHealth(int _hp)
    {
        MaxHP -= _hp;
        if (MaxHP <= 0) { MaxHP = 1; }

        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        Global.gardenHealthUI.UpdateUI(CurrentHP);
    }

    private void CheckDeath()
    {
        if (CurrentHP <= 0)
        {
            Global.GameOver();
        }
    }
}
